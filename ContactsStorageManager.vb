Imports KekeDataStore.Binary
Imports VB_BinaryDataStore_App.Models

Namespace MyDataStorage
    Public Class ContactsStorageManager
        Private _dataStore As New BinaryDataStore(Of Contact)

        Public Sub CreateContact(ByVal name As String, ByVal phonenumber As String)
            Dim contact As New Contact() With
            {
                .Id = Guid.NewGuid(),
                .Name = name,
                .PhoneNumber = phonenumber
            }

            _dataStore.Create(contact)
        End Sub

        Public Sub TruncateContacts()
            _dataStore.Truncate()
        End Sub

        Public Function GetContacts() As IEnumerable(Of Contact)
            Return _dataStore.GetAll()
        End Function

        Public Sub EditContact(ByVal id As String, ByVal obj As Contact)
            _dataStore.Update(id, obj)
        End Sub

        Public Sub DeleteContact(ByVal id As String)
            _dataStore.Delete(id)
        End Sub

        Public Sub Save()
            _dataStore.SaveChanges()
        End Sub


        Protected Overrides Sub Finalize()
            _dataStore.Dispose()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
