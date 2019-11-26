Imports Symmex.Snowflake.Common

Public Interface ISelfTrackingObject

    Property IsNew As Boolean
    Property IsDeleted As Boolean
    Property IsTrackingChanges As Boolean
    Property IsChanged As Boolean
    ReadOnly Property IsSaveRequired As Boolean
    ReadOnly Property SaveAction As SaveAction
    Sub ResetState()

End Interface