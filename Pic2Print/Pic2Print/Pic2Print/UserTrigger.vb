Public Class UserTrigger

    Private Sub UserTrigger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.FileExists("C:\onsite\cameras\dcc\cameracontrolcmd.exe") Then
            CameraFound = True
        Else
            CameraFound = False
            trgUpdateUserMessage(-2)
        End If

        Call _UserTrigger_Resized()

    End Sub

    Private Sub UserTrigger_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Call _UserTrigger_Resized()

    End Sub

    Private Sub TriggerBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TriggerBtn.Click
        ' If no camera found we have to bail

        If CameraFound = False Then
            MsgBox("Cannot start until DigiCamControl Camera software is installed")
            Return
        End If
        ' first input, trigger the camera trigger timer

        If Globals.TriggerProcessRun < 2 Then
            Globals.TriggerProcessRun = 2
        End If

    End Sub

    Private Sub _UserTrigger_Resized()
        Dim tl As Point ' trigger button location
        Dim rl As Point ' reprint button location
        Dim ll As Point ' label location
        'Dim l As Point ' location
        Dim x As Integer
        Dim y As Integer
        Dim lblY = lblPicsToGoMsg.Location.Y + lblPicsToGoMsg.Size.Height + 4

        ' reposition the button box. Calculate the top left starting location point

        x = (Me.Size.Width / 2) - (480 / 2)
        y = (Me.Size.Height / 2) - (320 / 2)
        If x < 0 Then x = 0
        If y < lblY Then y = lblY

        ' float the button in the center
        tl.Y = y
        tl.X = x
        TriggerBtn.Location = tl

        ' now move the reprint button horizontally
        rl = UserReprint.Location
        rl.X = (Me.Size.Width / 2) - (UserReprint.Width / 2)
        rl.Y = tl.Y + TriggerBtn.Height + 34
        UserReprint.Location = rl

        ' keep the vertical, just change the horizontal
        ll.Y = tl.Y / 2 - lblPicsToGoMsg.Height / 2
        If ll.Y < 0 Then ll.Y = 0
        ll.X = tl.X
        lblPicsToGoMsg.Location = ll

        ' reposition the hide button, hiding it mostly off screen so the users dont find it

        x = Me.Size.Width - HideButton.Width - 4
        y = Me.Size.Height - HideButton.Height * 2
        If x < 0 Then x = 0
        If y < 0 Then y = 0
        tl.Y = y
        tl.X = x
        HideButton.Location = tl

    End Sub

    Public Delegate Sub trgPrintCallback(ByVal str As String)

    Public Sub trgSetButtonText(ByVal str As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If TriggerBtn.InvokeRequired Then
            Dim d As New trgPrintCallback(AddressOf trgSetButtonText)
            Me.Invoke(d, New Object() {str})
        Else
            TriggerBtn.Text = str
        End If
    End Sub

    Public Delegate Sub trgSetFontSize(ByVal siz As Integer)

    Public Sub trgSetFont(ByVal siz As Integer)
        Dim fnt As Font

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If TriggerBtn.InvokeRequired Then
            Dim d As New trgSetFontSize(AddressOf trgSetFont)
            Me.Invoke(d, New Object() {siz})
        Else
            fnt = TriggerBtn.Font
            TriggerBtn.Font = New Font(fnt.Name, siz, FontStyle.Regular)
            'TriggerBtn.Font.Height = siz
        End If
    End Sub

    Public Delegate Sub trgSetFrameCountdel(ByVal cnt As Integer)
    Public CameraFound As Boolean = True

    Private frmnum As Integer
    Public Sub trgUpdateUserMessage(ByVal cnt As Integer)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If lblPicsToGoMsg.InvokeRequired Then
            Dim d As New trgSetFrameCountdel(AddressOf trgUpdateUserMessage)
            Me.Invoke(d, New Object() {cnt})
        Else

            ' -2 No camera found!
            ' -1 clear out the message
            '  0 Pictures Done
            '  1 Picture to go
            '  2+ Pictures to go

            If cnt = -2 Then
                lblPicsToGoMsg.Text = "No Camera Found!"
                CameraFound = False
            End If

            If CameraFound = False Then Return

            If cnt = -1 Then
                frmnum = 0
                lblPicsToGoMsg.Text = " "
            End If

            If cnt = 0 Then
                lblPicsToGoMsg.Text = "Pictures Done"
            End If

            If cnt = 1 Then
                If frmnum = 0 Then
                    lblPicsToGoMsg.Text = "Get Ready - Look Up!"
                    frmnum += 1
                Else
                    frmnum += 1
                    lblPicsToGoMsg.Text = "Final Picture - Look Up!"
                End If
            End If

            If cnt > 1 Then
                If frmnum = 0 Then
                    lblPicsToGoMsg.Text = "First Picture - Look Up!"
                    frmnum += 1
                Else
                    lblPicsToGoMsg.Text = "Next Picture - Look Up!"
                    frmnum += 1
                End If
            End If

        End If

    End Sub

    'Public Delegate Sub trgSetFocusCallback()

    'Public Sub trgSetFocus()
    ' InvokeRequired required compares the thread ID of the
    ' calling thread to the thread ID of the creating thread.
    ' If these threads are different, it returns true.
    'If TriggerBtn.InvokeRequired Then
    'Dim d As New trgSetFocusCallback(AddressOf trgSetFocus)
    'Me.Invoke(d, New Object() {})
    'Else
    'TriggerBtn.Focus()
    'End If
    'End Sub

    Private Sub HideButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideButton.Click
        Me.Hide()
    End Sub

    Private Sub lblPicsToGoMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPicsToGoMsg.Click

    End Sub

    Private Sub UserReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserReprint.Click
        Globals.fPostView.postReprintLast()
    End Sub
End Class