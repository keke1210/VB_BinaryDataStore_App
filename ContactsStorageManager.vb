Imports KekeDataStore.Binary
Imports VB_BinaryDataStore_App.Models

Namespace MyDataStorage
    Public Class ContactsStorageManager
        Private _dataStore As New BinaryDataStore(Of Contact)

        Public Sub CreateContact(ByVal name As String, ByVal phonenumber As String)
            Dim contact As New Contact() With
            {
                .Name = name,
                .PhoneNumber = phonenumber
            }

            _dataStore.Create(contact)
            _dataStore.SaveChanges()
        End Sub

        Public Sub TruncateContacts()
            _dataStore.Truncate()
            _dataStore.SaveChanges()
        End Sub

        Public Function GetContacts() As IEnumerable(Of Contact)
            Return _dataStore.GetAll()
        End Function

        Public Sub EditContact(ByVal id As String, ByVal obj As Contact)
            _dataStore.Update(id, obj)
            _dataStore.SaveChanges()
        End Sub

        Public Sub DeleteContact(ByVal id As String)
            _dataStore.Delete(id)
            _dataStore.SaveChanges()
        End Sub


        Protected Overrides Sub Finalize()
            _dataStore.Dispose()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
