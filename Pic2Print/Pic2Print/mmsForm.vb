'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'

Public Class mmsForm
    Private Sub mmsSend_Click(sender As System.Object, e As System.EventArgs) Handles mmsSend.Click

        ' use this email as a default for the next time 
        lblLastEmail.Text = usrEmail1.Text

        ' save the text for later..
        Globals.ImageCache.emailAddr(Globals.ScreenBase + Globals.PictureBoxSelected) = usrEmail1.Text
        'Globals.fPic2Print.SaveFileNameData(Globals.ScreenBase + Globals.PictureBoxSelected)

        ' save the phone #, carrier info..
        Globals.ImageCache.phoneNumber(Globals.ScreenBase + Globals.PictureBoxSelected) = txtPhoneNum.Text
        Globals.ImageCache.carrierSelector(Globals.FileIndexSelected) = CarrierCB.SelectedIndex
        'Globals.FileNameMessage(Globals.FileIndexSelected) = "Test Message" ' DSC save the actual text
        Globals.ImageCache.message(Globals.ScreenBase + Globals.PictureBoxSelected) = Globals.fmmsForm.txtMessage.Text
        Globals.fPic2Print.SaveFileNameData(Globals.ImageCache, Globals.ScreenBase + Globals.PictureBoxSelected)

        Me.Hide()

    End Sub

    Private Sub mmsCancel_Click(sender As System.Object, e As System.EventArgs) Handles mmsCancel.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles useButton.Click
        usrEmail1.Text = lblLastEmail.Text
    End Sub

    'Dim lastUserEmail As String

End Class