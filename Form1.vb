Imports VB_BinaryDataStore_App.MyDataStorage
Imports VB_BinaryDataStore_App.Utils

Public Class ContactsForm

#Region "Constructor"
    Public Sub Form1()
        InitializeComponent()
    End Sub
#End Region

#Region "Events"
    Private Sub ContactsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BindGridView()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dataStore As New ContactsStorageManager()
            If (txtName.Text.IsNotEmpty() And txtPhoneNumber.Text.IsNotEmpty()) Then
                dataStore.CreateContact(txtName.Text, txtPhoneNumber.Text)
                BindGridView()
                ClearContactsData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim dataStore As New ContactsStorageManager()

        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)

            Dim idToBeDeleted = row.Cells(0).Value
            dataStore.DeleteContact(idToBeDeleted)
            ClearContactsData()
        Next
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick



    End Sub
#End Region

#Region "HelperMethods"
    Private Sub BindGridView()
        Dim dataStore As New ContactsStorageManager()
        Dim contacts = dataStore.GetContacts()

        DataGridView1.Rows.Clear()
        For Each item In contacts
            DataGridView1.Rows.Add(New String() {item.Id.ToString(), item.Name, item.PhoneNumber})
        Next
    End Sub

    Private Sub ClearContactsData()
        txtName.Text = ""
        txtPhoneNumber.Text = ""
    End Sub

#End Region

End Class
