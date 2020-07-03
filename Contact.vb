Imports KekeDataStore.Binary

Namespace Models
    <Serializable()>
    Public Class Contact : Implements IBaseEntity
        Private _Id As Guid
        Public Sub New()
            _Id = Guid.NewGuid()
        End Sub

        Public Property Id As Guid Implements IBaseEntity.Id
            Get
                Return _Id
            End Get
            Set(value As Guid)
                _Id = value
            End Set
        End Property

        Public Property Name As String
        Public Property PhoneNumber As String
    End Class
End Namespace

