'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'

Public Class Form4

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' this should be done in a handler when the form is shown, not here..

        txtPassword.PasswordChar = "*"
        ckShowPassword.Checked = False

    End Sub

    Private Sub lblServerPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Form4_Visible(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        ' problem here - we dont know if its going visible or invisible..
        If Globals.EmailProcessRun = 2 Then
            Globals.EmailProcessRun = 1 ' pause the print processor
        End If
    End Sub

    Private Sub SyncFolderPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncFolderPath.TextChanged

    End Sub

    Private Sub SyncFolder_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpCloudConfig.Enter

    End Sub

    Private Sub btnOKay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKay.Click
        Call Form4OKValidation()
    End Sub

    Public Sub Form4OKValidation()

        ' if sync folder has data, validate its path

        If Globals.fForm3.EmailCloudEnabled.Checked = True Then

            If SyncFolderPath.Text <> "" Then

                If Globals.fPic2Print.ValidatePaths(8, True) = False Then
                    'MessageBox.Show("Sync Path NOT VALID" & vbCrLf & "Try Again!")
                    Return
                Else

                    ' the sync folder can't equal the print folders
                    If (SyncFolderPath.Text = Globals.fForm3.Print_Folder_1.Text) Or (SyncFolderPath.Text = Globals.fForm3.Print_Folder_2.Text) Then
                        MessageBox.Show("Warning! Sync Folder and Print Folders" & vbCrLf & "cannot be the same or infinite" & vbCrLf & "looping file operations occur.")
                        Return
                    End If

                    'MessageBox.Show("All paths are good")

                End If

                If Globals.fPic2Print.ValidatePaths(16, True) = False Then
                    'MessageBox.Show("Postview Sync Path NOT VALID" & vbCrLf & "Try Again!")
                    Return
                Else

                    ' the sync folder can't equal the print folders
                    If (syncPostPath.Text = Globals.fForm3.Print_Folder_1.Text) Or (syncPostPath.Text = Globals.fForm3.Print_Folder_2.Text) Then
                        MessageBox.Show("Warning! PostView Sync Folder and Print Folders" & vbCrLf & "cannot be the same or infinite" & vbCrLf & "looping file operations occur.")
                        Return
                    End If

                    'MessageBox.Show("All paths are good")

                End If

            End If

        End If

        ' hide the form
        Me.Hide()

        ' Sync folder is null disabling sync copies, but we dont know about email till it runs
        ' update the globals to runstate
        form4MoveDataToGlobals()

    End Sub

    Private Sub Form4_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        Call form4MoveDataToGlobals()
    End Sub

    Private Sub form4MoveDataToGlobals()

        ' FORM4 hide/close - move control data to globals for threads
        Globals.tmpSubject = txtSubject.Text
        Globals.tmpEmailRecipient = txtEmailRecipient.Text

        Globals.tmpServerURL = txtServerURL.Text
        Globals.tmpServerPort = txtServerPort.Text
        Globals.tmpAcctName = txtAcctName.Text
        Globals.tmpPassword = txtPassword.Text
        Globals.tmpAcctEmailAddr = txtAcctEmailAddr.Text

    End Sub

    Private Sub ckShowPassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckShowPassword.CheckedChanged

        If ckShowPassword.Checked Then

            txtPassword.PasswordChar = ""

        Else

            txtPassword.PasswordChar = "*"

        End If

    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lblSyncLa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSyncLa.Click

    End Sub

    Private Sub btnEmailFolderDialog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailFolderDialog1.Click
        Dim str As String
        FolderBrowserDialog1.SelectedPath = SyncFolderPath.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            SyncFolderPath.Text = str
        End If
    End Sub

    Private Sub btnPostViewFinder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostViewFinder.Click
        Dim str As String
        FolderBrowserDialog1.SelectedPath = syncPostPath.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            syncPostPath.Text = str
        End If
    End Sub

    Private Sub syncPostPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles syncPostPath.TextChanged
        ' problem here - we dont know if its going visible or invisible..
        If Globals.EmailProcessRun = 2 Then
            Globals.EmailProcessRun = 1 ' pause the print processor
        End If
    End Sub

    Private Sub syncPostLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles syncPostLabel.Click

    End Sub
End Class