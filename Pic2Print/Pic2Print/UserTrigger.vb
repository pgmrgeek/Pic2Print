Public Class UserTrigger

    Private Sub UserTrigger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.FileExists("C:\onsite\cameras\dcc\cameracontrolcmd.exe") Then
            CameraFound = True
        Else
            CameraFound = False
            trgSetFrameCount(-2)
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
        Dim s As Point ' size
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
        s.Y = y
        s.X = x
        TriggerBtn.Location = s

        ' keep the vertical, just change the horizontal
        s.Y = lblPicsToGoMsg.Location.Y
        lblPicsToGoMsg.Location = s

        ' reposition the hide button, hiding it mostly off screen so the users dont find it

        x = Me.Size.Width - 28
        y = Me.Size.Height - 34
        If x < 0 Then x = 0
        If y < 0 Then y = 0

        s.Y = y
        s.X = x
        HideButton.Location = s

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

    Public Sub trgSetFrameCount(ByVal cnt As Integer)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If lblPicsToGoMsg.InvokeRequired Then
            Dim d As New trgSetFrameCountdel(AddressOf trgSetFrameCount)
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
                lblPicsToGoMsg.Text = " "
            End If

            If cnt = 0 Then
                lblPicsToGoMsg.Text = "Pictures Done"
            End If

            If cnt = 1 Then
                lblPicsToGoMsg.Text = "1 More Picture"
            End If

            If cnt > 1 Then
                lblPicsToGoMsg.Text = cnt & " More Pictures"
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
End Class