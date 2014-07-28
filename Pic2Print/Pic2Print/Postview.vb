Imports System
Imports System.IO
Imports System.Threading
'
'================================================================================================
'
'  Pic2Print - Photobooth Image Stream Manager.
'
'    Copyright (c) 2014. Bay Area Event Photography. All rights Reserved.
'
'
Public Class Postview

    Private Sub PostView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' if the user sent in a reset, then reposition on the desktop
        If Globals.cmdLineReset Then

            ' move the form.
            SetDesktopLocation(20, 20)

        End If

        Call FormLayout()

        'Call SwapButtons(Globals.fForm3.EmailCloudEnabled.Checked)

        PostView1PB.Image = My.Resources.nobk
        PostView2PB.Image = My.Resources.nobk
        PostView3PB.Image = My.Resources.nobk
        PostView4PB.Image = My.Resources.nobk

        Globals.fPostViewHasLoaded = True

    End Sub

    Public Sub FormLayout()
        Dim p As Point

        SwapButtons(Globals.fForm3.EmailCloudEnabled.Checked)

        If Globals.fForm3.MultipleBackgrounds.Checked Then
            p.X = 310
            p.Y = 206
            PostView1PB.Size = p
            PostView2PB.Visible = True
            PostView3PB.Visible = True
            PostView4PB.Visible = True

        Else
            p.X = 626
            p.Y = 418
            PostView1PB.Size = p
            PostView2PB.Visible = False
            PostView3PB.Visible = False
            PostView4PB.Visible = False
        End If

    End Sub

    Private Sub PreviewHideButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostCloseButton1.Click
        Call PostCloseClick()
    End Sub
    Private Sub PostCloseButton_Click(sender As System.Object, e As System.EventArgs) Handles PostCloseButton2.Click
        Call PostCloseClick()
    End Sub

    Private Sub PostCloseClick()

        Me.Hide()

        If Globals.PostViewsLoaded And 1 Then
            PostView1PB.Image.Dispose()
            PostView1PB.Image = My.Resources.nobk
        End If

        If Globals.PostViewsLoaded And 2 Then
            PostView2PB.Image.Dispose()
            PostView2PB.Image = My.Resources.nobk
        End If

        If Globals.PostViewsLoaded And 4 Then
            PostView3PB.Image.Dispose()
            PostView3PB.Image = My.Resources.nobk
        End If

        If Globals.PostViewsLoaded And 8 Then
            PostView4PB.Image.Dispose()
            PostView4PB.Image = My.Resources.nobk
        End If

        Globals.PostViewsLoaded = 0
    End Sub


    Private Sub btnPostSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostSend.Click
        ' just save the text for later..
        Globals.FileNameEmails(Globals.ScreenBase + Globals.PictureBoxSelected) = usrEmail2.Text
        Globals.fPic2Print.SaveFileNameData(Globals.ScreenBase + Globals.PictureBoxSelected)

    End Sub

    Public Sub SetLoadPostViews(ByRef pb As PictureBox, ByRef fnam As String, ByRef mask As Int16)

        ' If idx = 1 Then
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If pb.InvokeRequired Then

            Dim d As New Globals.SetPostViewCallback(AddressOf SetLoadPostViews)
            Me.Invoke(d, New Object() {pb, fnam, mask})

        Else

            ' safeguard - don't process if the file isn't found.
            If File.Exists(Globals.tmpPrint1_Folder & fnam) = False Then
                fnam = ""
            End If

            ' a null string means, dispose of the held image
            If fnam = "" Then
                pb.Image.Dispose()
                pb.Image = My.Resources.blank
                Globals.PostViewsLoaded = Globals.PostViewsLoaded And Not mask
            Else

                ' DSC!!! free this up or we have a memory leak!
                ' if there is some image, free it up
                'If pb.Image <> My.Resources.nobk Then
                'pb.Image.Dispose()
                ' End If

                ' now load the preview from the file
                pb.Image = Image.FromFile(Globals.tmpPrint1_Folder & fnam)
                Globals.PostViewsLoaded = Globals.PostViewsLoaded Or mask
            End If

        End If

    End Sub

    Public Sub SwapButtons(TurnOn As Boolean)

        If TurnOn = True Then
            PostGroup.Visible = True
            PostCloseButton2.Visible = False
        Else
            PostGroup.Visible = False
            PostCloseButton2.Visible = True
        End If
    End Sub

End Class