Imports System.IO
Imports System.Reflection
Imports System.ComponentModel
Imports System.Resources
Imports System.Globalization
Imports Symmex.Snowflake.Common

Public Class ResourceManager
    Inherits ObservableObject

    Private _CurrentCultureChangedEventHandler As EventHandler(Of EventArgs)
    Private ReadOnly Property CurrentCultureChangedEventHandler As EventHandler(Of EventArgs)
        Get
            If _CurrentCultureChangedEventHandler Is Nothing Then
                _CurrentCultureChangedEventHandler = New EventHandler(Of EventArgs)(AddressOf Me.OnCurrentCultureChanged)
            End If

            Return _CurrentCultureChangedEventHandler
        End Get
    End Property

    Private _Resources As Dictionary(Of CultureInfo, Dictionary(Of String, Object))
    Private ReadOnly Property Resources As Dictionary(Of CultureInfo, Dictionary(Of String, Object))
        Get
            If _Resources Is Nothing Then
                _Resources = New Dictionary(Of CultureInfo, Dictionary(Of String, Object))()
            End If

            Return _Resources
        End Get
    End Property

    Private _Converters As ResourceConverterCollection
    Public ReadOnly Property Converters As ResourceConverterCollection
        Get
            If _Converters Is Nothing Then
                _Converters = New ResourceConverterCollection()
                Me.LoadConverters()
            End If

            Return _Converters
        End Get
    End Property

    Default Public ReadOnly Property Item(name As String) As Object
        Get
            Return Me.GetResource(name)
        End Get
    End Property

    Public Sub New()
        CultureManager.CurrentCultureChangedEvent.AddHandler(Me.CurrentCultureChangedEventHandler)
    End Sub

    Private Sub LoadConverters()
        Me.Converters.Add(New TextResourceConverter())
    End Sub

    Public Sub LoadResources(fromAssembly As Assembly)
        Dim rootName = fromAssembly.GetName().Name + ".g.resources"
        Dim rootStream = fromAssembly.GetManifestResourceStream(rootName)
        Dim rootReader As New ResourceReader(rootStream)
        Dim allEntries = rootReader.Cast(Of DictionaryEntry)()
        Dim cultureGroups = (From entry In allEntries
                                  Let fullName = DirectCast(entry.Key, String).ToLower()
                                  Where fullName.StartsWith("resources")
                                  Let culture = Me.GetResourceCulture(fullName)
                                  Let name = Path.GetFileName(fullName)
                                  Let value = Me.GetResourceValue(name, DirectCast(entry.Value, Stream))
                                  Group New With {.Name = name, .Value = value} By culture Into Group
                                  Select New With {.Culture = culture,
                                                   .Resources = Group})

        For Each cultureGroup In cultureGroups
            Dim rd As Dictionary(Of String, Object) = Nothing
            If Not Me.Resources.TryGetValue(cultureGroup.Culture, rd) Then
                rd = New Dictionary(Of String, Object)()
                Me.Resources.Add(cultureGroup.Culture, rd)
            End If

            For Each resource In cultureGroup.Resources
                rd(resource.Name) = resource.Value
            Next
        Next
    End Sub

    Private Function GetResourceValue(name As String, s As Stream) As Object
        Dim ext = Path.GetExtension(name)

        If Me.Converters.Contains(ext) Then
            Return Me.Converters(ext).Convert(s)
        Else
            Return s
        End If
    End Function

    Private Function GetResourceCulture(fullName As String) As CultureInfo
        Dim parts = fullName.Split(New Char() {"/"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim cultureName = parts(1)
        Return CultureInfo.GetCultureInfo(cultureName)
    End Function

    Public Function GetResource(Of T)(name As String) As T
        Return DirectCast(Me.GetResource(name), T)
    End Function

    Public Function GetResource(name As String) As Object
        name = name.ToLower()

        Dim dict As Dictionary(Of String, Object) = Nothing
        Dim value As Object = Nothing
        If CultureManager.CurrentCulture Is Nothing OrElse Not Me.Resources.TryGetValue(CultureManager.CurrentCulture, dict) OrElse Not dict.TryGetValue(name, value) Then
            If Me.Resources.TryGetValue(CultureManager.DefaultCulture, dict) Then
                dict.TryGetValue(name, value)
            Else
                Debug.Write(String.Format("{0} does not contain a resource with the name {1}.", Me.GetType().Name, name))
            End If
        End If

        Return value
    End Function

    Private Sub OnCurrentCultureChanged(sender As Object, e As EventArgs)
        Me.OnPropertyChanged(String.Empty)
    End Sub

End Class