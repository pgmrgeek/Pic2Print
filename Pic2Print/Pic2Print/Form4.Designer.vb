<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grpEmailConfig = New System.Windows.Forms.GroupBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.chkMakeEmailLog = New System.Windows.Forms.CheckBox()
        Me.txtEmailRecipient = New System.Windows.Forms.TextBox()
        Me.lblRecipientEmail = New System.Windows.Forms.Label()
        Me.txtAcctEmailAddr = New System.Windows.Forms.TextBox()
        Me.lblAcctEmailAddr = New System.Windows.Forms.Label()
        Me.ckShowPassword = New System.Windows.Forms.CheckBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtAcctName = New System.Windows.Forms.TextBox()
        Me.lblAcctName = New System.Windows.Forms.Label()
        Me.txtServerPort = New System.Windows.Forms.TextBox()
        Me.lblServerPort = New System.Windows.Forms.Label()
        Me.txtServerURL = New System.Windows.Forms.TextBox()
        Me.lblServerURL = New System.Windows.Forms.Label()
        Me.grpCloudConfig = New System.Windows.Forms.GroupBox()
        Me.SyncFolderPath2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.syncPostLabel = New System.Windows.Forms.Label()
        Me.btnPostViewFinder = New System.Windows.Forms.Button()
        Me.syncPostPath = New System.Windows.Forms.TextBox()
        Me.btnEmailFolderDialog1 = New System.Windows.Forms.Button()
        Me.lblSyncLa = New System.Windows.Forms.Label()
        Me.SyncFolderPath1 = New System.Windows.Forms.TextBox()
        Me.btnOKay = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnEmailFolderDialog2 = New System.Windows.Forms.Button()
        Me.grpEmailConfig.SuspendLayout()
        Me.grpCloudConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpEmailConfig
        '
        Me.grpEmailConfig.Controls.Add(Me.txtSubject)
        Me.grpEmailConfig.Controls.Add(Me.lblSubject)
        Me.grpEmailConfig.Controls.Add(Me.chkMakeEmailLog)
        Me.grpEmailConfig.Controls.Add(Me.txtEmailRecipient)
        Me.grpEmailConfig.Controls.Add(Me.lblRecipientEmail)
        Me.grpEmailConfig.Controls.Add(Me.txtAcctEmailAddr)
        Me.grpEmailConfig.Controls.Add(Me.lblAcctEmailAddr)
        Me.grpEmailConfig.Controls.Add(Me.ckShowPassword)
        Me.grpEmailConfig.Controls.Add(Me.txtPassword)
        Me.grpEmailConfig.Controls.Add(Me.lblPassword)
        Me.grpEmailConfig.Controls.Add(Me.txtAcctName)
        Me.grpEmailConfig.Controls.Add(Me.lblAcctName)
        Me.grpEmailConfig.Controls.Add(Me.txtServerPort)
        Me.grpEmailConfig.Controls.Add(Me.lblServerPort)
        Me.grpEmailConfig.Controls.Add(Me.txtServerURL)
        Me.grpEmailConfig.Controls.Add(Me.lblServerURL)
        Me.grpEmailConfig.Location = New System.Drawing.Point(12, 9)
        Me.grpEmailConfig.Name = "grpEmailConfig"
        Me.grpEmailConfig.Size = New System.Drawing.Size(288, 370)
        Me.grpEmailConfig.TabIndex = 13
        Me.grpEmailConfig.TabStop = False
        Me.grpEmailConfig.Text = "Email Config"
        '
        'txtSubject
        '
        Me.txtSubject.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtSubjectMsg", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtSubject.Location = New System.Drawing.Point(27, 313)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(248, 20)
        Me.txtSubject.TabIndex = 26
        Me.txtSubject.Text = Global.WindowsApplication1.My.MySettings.Default.txtSubjectMsg
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(9, 295)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(176, 13)
        Me.lblSubject.TabIndex = 25
        Me.lblSubject.Text = "Subject Line (caption for Facebook)"
        '
        'chkMakeEmailLog
        '
        Me.chkMakeEmailLog.AutoSize = True
        Me.chkMakeEmailLog.Checked = Global.WindowsApplication1.My.MySettings.Default.chkEmailLog
        Me.chkMakeEmailLog.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.WindowsApplication1.My.MySettings.Default, "chkEmailLog", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkMakeEmailLog.Location = New System.Drawing.Point(9, 339)
        Me.chkMakeEmailLog.Name = "chkMakeEmailLog"
        Me.chkMakeEmailLog.Size = New System.Drawing.Size(198, 17)
        Me.chkMakeEmailLog.TabIndex = 8
        Me.chkMakeEmailLog.Text = "Create connection log for debugging"
        Me.chkMakeEmailLog.UseVisualStyleBackColor = True
        '
        'txtEmailRecipient
        '
        Me.txtEmailRecipient.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtEmailRcvAddr", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtEmailRecipient.Location = New System.Drawing.Point(27, 269)
        Me.txtEmailRecipient.Name = "txtEmailRecipient"
        Me.txtEmailRecipient.Size = New System.Drawing.Size(248, 20)
        Me.txtEmailRecipient.TabIndex = 7
        Me.txtEmailRecipient.Text = Global.WindowsApplication1.My.MySettings.Default.txtEmailRcvAddr
        '
        'lblRecipientEmail
        '
        Me.lblRecipientEmail.AutoSize = True
        Me.lblRecipientEmail.Location = New System.Drawing.Point(6, 252)
        Me.lblRecipientEmail.Name = "lblRecipientEmail"
        Me.lblRecipientEmail.Size = New System.Drawing.Size(259, 13)
        Me.lblRecipientEmail.TabIndex = 24
        Me.lblRecipientEmail.Text = "Recipient email address (TO addr). Blank=no emailing"
        '
        'txtAcctEmailAddr
        '
        Me.txtAcctEmailAddr.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "TxtEmailSenderAddr", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtAcctEmailAddr.Location = New System.Drawing.Point(27, 227)
        Me.txtAcctEmailAddr.Name = "txtAcctEmailAddr"
        Me.txtAcctEmailAddr.Size = New System.Drawing.Size(249, 20)
        Me.txtAcctEmailAddr.TabIndex = 6
        Me.txtAcctEmailAddr.Text = Global.WindowsApplication1.My.MySettings.Default.TxtEmailSenderAddr
        '
        'lblAcctEmailAddr
        '
        Me.lblAcctEmailAddr.AutoSize = True
        Me.lblAcctEmailAddr.Location = New System.Drawing.Point(6, 210)
        Me.lblAcctEmailAddr.Name = "lblAcctEmailAddr"
        Me.lblAcctEmailAddr.Size = New System.Drawing.Size(174, 13)
        Me.lblAcctEmailAddr.TabIndex = 22
        Me.lblAcctEmailAddr.Text = "Sender Email Address (FROM addr)"
        '
        'ckShowPassword
        '
        Me.ckShowPassword.AutoSize = True
        Me.ckShowPassword.Location = New System.Drawing.Point(45, 187)
        Me.ckShowPassword.Name = "ckShowPassword"
        Me.ckShowPassword.Size = New System.Drawing.Size(102, 17)
        Me.ckShowPassword.TabIndex = 5
        Me.ckShowPassword.Text = "Show Password"
        Me.ckShowPassword.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtEmailPasword", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtPassword.Location = New System.Drawing.Point(27, 163)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(249, 20)
        Me.txtPassword.TabIndex = 4
        Me.txtPassword.Text = Global.WindowsApplication1.My.MySettings.Default.txtEmailPasword
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(6, 146)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 19
        Me.lblPassword.Text = "Password"
        '
        'txtAcctName
        '
        Me.txtAcctName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtEmailAcctName", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtAcctName.Location = New System.Drawing.Point(27, 122)
        Me.txtAcctName.Name = "txtAcctName"
        Me.txtAcctName.Size = New System.Drawing.Size(249, 20)
        Me.txtAcctName.TabIndex = 3
        Me.txtAcctName.Text = Global.WindowsApplication1.My.MySettings.Default.txtEmailAcctName
        '
        'lblAcctName
        '
        Me.lblAcctName.AutoSize = True
        Me.lblAcctName.Location = New System.Drawing.Point(6, 105)
        Me.lblAcctName.Name = "lblAcctName"
        Me.lblAcctName.Size = New System.Drawing.Size(185, 13)
        Me.lblAcctName.TabIndex = 17
        Me.lblAcctName.Text = "Account Name (likely the email addr..)"
        '
        'txtServerPort
        '
        Me.txtServerPort.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtEmailServerPort", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtServerPort.Location = New System.Drawing.Point(27, 81)
        Me.txtServerPort.Name = "txtServerPort"
        Me.txtServerPort.Size = New System.Drawing.Size(45, 20)
        Me.txtServerPort.TabIndex = 2
        Me.txtServerPort.Text = Global.WindowsApplication1.My.MySettings.Default.txtEmailServerPort
        '
        'lblServerPort
        '
        Me.lblServerPort.AutoSize = True
        Me.lblServerPort.Location = New System.Drawing.Point(6, 64)
        Me.lblServerPort.Name = "lblServerPort"
        Me.lblServerPort.Size = New System.Drawing.Size(116, 13)
        Me.lblServerPort.TabIndex = 15
        Me.lblServerPort.Text = "Server Outgoing Port #"
        '
        'txtServerURL
        '
        Me.txtServerURL.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtEmailServerURL", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtServerURL.Location = New System.Drawing.Point(27, 41)
        Me.txtServerURL.Name = "txtServerURL"
        Me.txtServerURL.Size = New System.Drawing.Size(249, 20)
        Me.txtServerURL.TabIndex = 1
        Me.txtServerURL.Text = Global.WindowsApplication1.My.MySettings.Default.txtEmailServerURL
        '
        'lblServerURL
        '
        Me.lblServerURL.AutoSize = True
        Me.lblServerURL.Location = New System.Drawing.Point(6, 25)
        Me.lblServerURL.Name = "lblServerURL"
        Me.lblServerURL.Size = New System.Drawing.Size(156, 13)
        Me.lblServerURL.TabIndex = 13
        Me.lblServerURL.Text = "Server URL: ex mail.yahoo.com"
        '
        'grpCloudConfig
        '
        Me.grpCloudConfig.Controls.Add(Me.btnEmailFolderDialog2)
        Me.grpCloudConfig.Controls.Add(Me.SyncFolderPath2)
        Me.grpCloudConfig.Controls.Add(Me.Label1)
        Me.grpCloudConfig.Controls.Add(Me.syncPostLabel)
        Me.grpCloudConfig.Controls.Add(Me.btnPostViewFinder)
        Me.grpCloudConfig.Controls.Add(Me.syncPostPath)
        Me.grpCloudConfig.Controls.Add(Me.btnEmailFolderDialog1)
        Me.grpCloudConfig.Controls.Add(Me.lblSyncLa)
        Me.grpCloudConfig.Controls.Add(Me.SyncFolderPath1)
        Me.grpCloudConfig.Location = New System.Drawing.Point(328, 9)
        Me.grpCloudConfig.Name = "grpCloudConfig"
        Me.grpCloudConfig.Size = New System.Drawing.Size(345, 223)
        Me.grpCloudConfig.TabIndex = 86
        Me.grpCloudConfig.TabStop = False
        Me.grpCloudConfig.Text = "Cloud Config"
        '
        'SyncFolderPath2
        '
        Me.SyncFolderPath2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "SyncFolderPth2", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncFolderPath2.Location = New System.Drawing.Point(9, 86)
        Me.SyncFolderPath2.Name = "SyncFolderPath2"
        Me.SyncFolderPath2.Size = New System.Drawing.Size(283, 20)
        Me.SyncFolderPath2.TabIndex = 93
        Me.SyncFolderPath2.Text = Global.WindowsApplication1.My.MySettings.Default.SyncFolderPth2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(310, 13)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "Path to Dropbox/Slideshow Folder #2 for KIOSK. Blank=no path"
        '
        'syncPostLabel
        '
        Me.syncPostLabel.AutoSize = True
        Me.syncPostLabel.Location = New System.Drawing.Point(6, 146)
        Me.syncPostLabel.Name = "syncPostLabel"
        Me.syncPostLabel.Size = New System.Drawing.Size(257, 13)
        Me.syncPostLabel.TabIndex = 91
        Me.syncPostLabel.Text = "Post View Email Copy-to-Cloud folder. Blank=no copy"
        '
        'btnPostViewFinder
        '
        Me.btnPostViewFinder.Location = New System.Drawing.Point(295, 164)
        Me.btnPostViewFinder.Name = "btnPostViewFinder"
        Me.btnPostViewFinder.Size = New System.Drawing.Size(33, 20)
        Me.btnPostViewFinder.TabIndex = 90
        Me.btnPostViewFinder.Text = "..."
        Me.btnPostViewFinder.UseVisualStyleBackColor = True
        '
        'syncPostPath
        '
        Me.syncPostPath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "syncPostPathvalue", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.syncPostPath.Location = New System.Drawing.Point(6, 165)
        Me.syncPostPath.Name = "syncPostPath"
        Me.syncPostPath.Size = New System.Drawing.Size(283, 20)
        Me.syncPostPath.TabIndex = 89
        Me.syncPostPath.Text = Global.WindowsApplication1.My.MySettings.Default.syncPostPathvalue
        '
        'btnEmailFolderDialog1
        '
        Me.btnEmailFolderDialog1.Location = New System.Drawing.Point(298, 40)
        Me.btnEmailFolderDialog1.Name = "btnEmailFolderDialog1"
        Me.btnEmailFolderDialog1.Size = New System.Drawing.Size(33, 20)
        Me.btnEmailFolderDialog1.TabIndex = 88
        Me.btnEmailFolderDialog1.Text = "..."
        Me.btnEmailFolderDialog1.UseVisualStyleBackColor = True
        '
        'lblSyncLa
        '
        Me.lblSyncLa.AutoSize = True
        Me.lblSyncLa.Location = New System.Drawing.Point(6, 25)
        Me.lblSyncLa.Name = "lblSyncLa"
        Me.lblSyncLa.Size = New System.Drawing.Size(310, 13)
        Me.lblSyncLa.TabIndex = 87
        Me.lblSyncLa.Text = "Path to Dropbox/Slideshow Folder #1 for KIOSK. Blank=no path"
        '
        'SyncFolderPath1
        '
        Me.SyncFolderPath1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "SyncFolderPth", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncFolderPath1.Location = New System.Drawing.Point(9, 41)
        Me.SyncFolderPath1.Name = "SyncFolderPath1"
        Me.SyncFolderPath1.Size = New System.Drawing.Size(283, 20)
        Me.SyncFolderPath1.TabIndex = 9
        Me.SyncFolderPath1.Text = Global.WindowsApplication1.My.MySettings.Default.SyncFolderPth
        '
        'btnOKay
        '
        Me.btnOKay.Location = New System.Drawing.Point(598, 348)
        Me.btnOKay.Name = "btnOKay"
        Me.btnOKay.Size = New System.Drawing.Size(75, 23)
        Me.btnOKay.TabIndex = 88
        Me.btnOKay.Text = "OK"
        Me.btnOKay.UseVisualStyleBackColor = True
        '
        'btnEmailFolderDialog2
        '
        Me.btnEmailFolderDialog2.Location = New System.Drawing.Point(298, 85)
        Me.btnEmailFolderDialog2.Name = "btnEmailFolderDialog2"
        Me.btnEmailFolderDialog2.Size = New System.Drawing.Size(33, 20)
        Me.btnEmailFolderDialog2.TabIndex = 94
        Me.btnEmailFolderDialog2.Text = "..."
        Me.btnEmailFolderDialog2.UseVisualStyleBackColor = True
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 391)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOKay)
        Me.Controls.Add(Me.grpCloudConfig)
        Me.Controls.Add(Me.grpEmailConfig)
        Me.Name = "Form4"
        Me.Text = "Email & Cloud Configuration"
        Me.grpEmailConfig.ResumeLayout(False)
        Me.grpEmailConfig.PerformLayout()
        Me.grpCloudConfig.ResumeLayout(False)
        Me.grpCloudConfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpEmailConfig As System.Windows.Forms.GroupBox
    Friend WithEvents chkMakeEmailLog As System.Windows.Forms.CheckBox
    Friend WithEvents txtEmailRecipient As System.Windows.Forms.TextBox
    Friend WithEvents lblRecipientEmail As System.Windows.Forms.Label
    Friend WithEvents txtAcctEmailAddr As System.Windows.Forms.TextBox
    Friend WithEvents lblAcctEmailAddr As System.Windows.Forms.Label
    Friend WithEvents ckShowPassword As System.Windows.Forms.CheckBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtAcctName As System.Windows.Forms.TextBox
    Friend WithEvents lblAcctName As System.Windows.Forms.Label
    Friend WithEvents txtServerPort As System.Windows.Forms.TextBox
    Friend WithEvents lblServerPort As System.Windows.Forms.Label
    Friend WithEvents txtServerURL As System.Windows.Forms.TextBox
    Friend WithEvents lblServerURL As System.Windows.Forms.Label
    Friend WithEvents grpCloudConfig As System.Windows.Forms.GroupBox
    Friend WithEvents SyncFolderPath1 As System.Windows.Forms.TextBox
    Friend WithEvents lblSyncLa As System.Windows.Forms.Label
    Friend WithEvents btnOKay As System.Windows.Forms.Button
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents lblSubject As System.Windows.Forms.Label
    Friend WithEvents btnEmailFolderDialog1 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnPostViewFinder As System.Windows.Forms.Button
    Friend WithEvents syncPostPath As System.Windows.Forms.TextBox
    Friend WithEvents syncPostLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SyncFolderPath2 As System.Windows.Forms.TextBox
    Friend WithEvents btnEmailFolderDialog2 As System.Windows.Forms.Button
End Class