'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class SendEmails

    Private Sub CancelSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelSend.Click
        Globals.SendEmailsAgain = 0
    End Sub

    Private Sub SendEmails_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class