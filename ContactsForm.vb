﻿Imports VB_BinaryDataStore_App.Models
Imports VB_BinaryDataStore_App.MyDataStorage
Imports VB_BinaryDataStore_App.Utils

Public Class ContactsForm

#Region "Members"

    Dim _dataStore As New ContactsStorageManager()
    Dim _table As New DataTable("table")
    Dim _index As Integer

#End Region

#Region "Constructor"

    Public Sub ContactsForm()
        InitializeComponent()
    End Sub

#End Region

#Region "Events"

    Private Sub ContactsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTable()
        BindGridView()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If (txtName.Text.IsNotEmpty() And txtPhoneNumber.Text.IsNotEmpty()) Then
                _dataStore.CreateContact(txtName.Text, txtPhoneNumber.Text)
                _dataStore.Save()
                BindGridView()
                ClearContactsData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        ' Iterate through all the selected rows
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            If (row.Cells(0).Value IsNot Nothing) Then
                Dim idToBeDeleted = row.Cells(0).Value
                DataGridView1.Rows.Remove(row)
                _dataStore.DeleteContact(idToBeDeleted)
                ClearContactsData()
            End If
        Next

        _dataStore.Save()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim selectedRow As DataGridViewRow

        _index = e.RowIndex

        If (_index < 0) Then
            Return
        End If

        selectedRow = DataGridView1.Rows(_index)

        txtName.Text = selectedRow.Cells(1).Value.ToString()
        txtPhoneNumber.Text = selectedRow.Cells(2).Value.ToString()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim newData As DataGridViewRow

        Try
            newData = DataGridView1.Rows(_index)

            Dim checkForFailure = newData Is Nothing Or
                                  newData.Cells Is Nothing Or
                                  newData.Cells(0).Value Is Nothing Or
                                  newData.Cells(0).Value.ToString().IsNotEmpty() <> True
        Catch ex As Exception
            MessageBox.Show("Row cannot be edited", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If (txtName.Text.IsNotEmpty And txtPhoneNumber.Text.IsNotEmpty) Then
            Dim contactModel As New Contact() With
            {
                .Name = txtName.Text,
                .PhoneNumber = txtPhoneNumber.Text
            }

            newData.Cells(1).Value = contactModel.Name
            newData.Cells(2).Value = contactModel.PhoneNumber

            Dim contactId = newData.Cells(0).Value.ToString()

            _dataStore.EditContact(contactId, contactModel)
            _dataStore.Save()

            ClearContactsData()
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearContactsData()
    End Sub

#End Region

#Region "HelperMethods"

    Private Sub BindGridView()
        Dim contacts = _dataStore.GetContacts()

        _table.Rows.Clear()
        For Each item In contacts
            _table.Rows.Add(item.Id.ToString(), item.Name, item.PhoneNumber)
        Next

        DataGridView1.DataSource = _table
    End Sub

    Private Sub InitializeTable()
        _table.Columns.Add("ID", Type.GetType("System.String"))
        _table.Columns.Add("Name", Type.GetType("System.String"))
        _table.Columns.Add("Phone Number", Type.GetType("System.String"))
        DataGridView1.ReadOnly = True
        DataGridView1.DataSource = _table
    End Sub

    Private Sub ClearContactsData()
        txtName.Text = ""
        txtPhoneNumber.Text = ""
    End Sub

#End Region

End Class
