<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mmsForm
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
        Me.lblEnterPhoneNum = New System.Windows.Forms.Label()
        Me.txtPhoneNum = New System.Windows.Forms.TextBox()
        Me.mmsSend = New System.Windows.Forms.Button()
        Me.mmsCancel = New System.Windows.Forms.Button()
        Me.mmsCarrierCB = New System.Windows.Forms.ComboBox()
        Me.mmsSelCarrier = New System.Windows.Forms.Label()
        Me.email2label = New System.Windows.Forms.Label()
        Me.usrEmail1 = New System.Windows.Forms.TextBox()
        Me.lblOrOther = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.useButton = New System.Windows.Forms.Button()
        Me.lblLastEmail = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblEnterPhoneNum
        '
        Me.lblEnterPhoneNum.AutoSize = True
        Me.lblEnterPhoneNum.Location = New System.Drawing.Point(14, 228)
        Me.lblEnterPhoneNum.Name = "lblEnterPhoneNum"
        Me.lblEnterPhoneNum.Size = New System.Drawing.Size(166, 13)
        Me.lblEnterPhoneNum.TabIndex = 0
        Me.lblEnterPhoneNum.Text = "Enter Your Phone # (xxx-xxx-xxxx)"
        '
        'txtPhoneNum
        '
        Me.txtPhoneNum.Location = New System.Drawing.Point(17, 244)
        Me.txtPhoneNum.Name = "txtPhoneNum"
        Me.txtPhoneNum.Size = New System.Drawing.Size(216, 20)
        Me.txtPhoneNum.TabIndex = 1
        '
        'mmsSend
        '
        Me.mmsSend.Location = New System.Drawing.Point(17, 315)
        Me.mmsSend.Name = "mmsSend"
        Me.mmsSend.Size = New System.Drawing.Size(75, 23)
        Me.mmsSend.TabIndex = 2
        Me.mmsSend.Text = "Send"
        Me.mmsSend.UseVisualStyleBackColor = True
        '
        'mmsCancel
        '
        Me.mmsCancel.Location = New System.Drawing.Point(158, 315)
        Me.mmsCancel.Name = "mmsCancel"
        Me.mmsCancel.Size = New System.Drawing.Size(75, 23)
        Me.mmsCancel.TabIndex = 3
        Me.mmsCancel.Text = "Cancel"
        Me.mmsCancel.UseVisualStyleBackColor = True
        '
        'mmsCarrierCB
        '
        Me.mmsCarrierCB.FormattingEnabled = True
        Me.mmsCarrierCB.Location = New System.Drawing.Point(17, 283)
        Me.mmsCarrierCB.Name = "mmsCarrierCB"
        Me.mmsCarrierCB.Size = New System.Drawing.Size(216, 21)
        Me.mmsCarrierCB.TabIndex = 4
        '
        'mmsSelCarrier
        '
        Me.mmsSelCarrier.AutoSize = True
        Me.mmsSelCarrier.Location = New System.Drawing.Point(14, 267)
        Me.mmsSelCarrier.Name = "mmsSelCarrier"
        Me.mmsSelCarrier.Size = New System.Drawing.Size(95, 13)
        Me.mmsSelCarrier.TabIndex = 6
        Me.mmsSelCarrier.Text = "Select Your Carrier"
        '
        'email2label
        '
        Me.email2label.AutoSize = True
        Me.email2label.Location = New System.Drawing.Point(12, 115)
        Me.email2label.Name = "email2label"
        Me.email2label.Size = New System.Drawing.Size(129, 13)
        Me.email2label.TabIndex = 8
        Me.email2label.Text = "Enter Your Email Address:"
        '
        'usrEmail1
        '
        Me.usrEmail1.Location = New System.Drawing.Point(15, 136)
        Me.usrEmail1.Name = "usrEmail1"
        Me.usrEmail1.Size = New System.Drawing.Size(216, 20)
        Me.usrEmail1.TabIndex = 9
        '
        'lblOrOther
        '
        Me.lblOrOther.AutoSize = True
        Me.lblOrOther.Location = New System.Drawing.Point(64, 202)
        Me.lblOrOther.Name = "lblOrOther"
        Me.lblOrOther.Size = New System.Drawing.Size(122, 13)
        Me.lblOrOther.TabIndex = 10
        Me.lblOrOther.Text = "--------------- OR  ---------------"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Add Your Message"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(16, 34)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(216, 20)
        Me.txtMessage.TabIndex = 12
        '
        'useButton
        '
        Me.useButton.Location = New System.Drawing.Point(172, 110)
        Me.useButton.Name = "useButton"
        Me.useButton.Size = New System.Drawing.Size(59, 23)
        Me.useButton.TabIndex = 13
        Me.useButton.Text = "Use Last"
        Me.useButton.UseVisualStyleBackColor = True
        '
        'lblLastEmail
        '
        Me.lblLastEmail.Location = New System.Drawing.Point(14, 159)
        Me.lblLastEmail.Name = "lblLastEmail"
        Me.lblLastEmail.Size = New System.Drawing.Size(215, 18)
        Me.lblLastEmail.TabIndex = 14
        Me.lblLastEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mmsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(257, 365)
        Me.Controls.Add(Me.lblLastEmail)
        Me.Controls.Add(Me.useButton)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblOrOther)
        Me.Controls.Add(Me.email2label)
        Me.Controls.Add(Me.usrEmail1)
        Me.Controls.Add(Me.mmsSelCarrier)
        Me.Controls.Add(Me.mmsCarrierCB)
        Me.Controls.Add(Me.mmsCancel)
        Me.Controls.Add(Me.mmsSend)
        Me.Controls.Add(Me.txtPhoneNum)
        Me.Controls.Add(Me.lblEnterPhoneNum)
        Me.Name = "mmsForm"
        Me.Text = "mmsForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblEnterPhoneNum As System.Windows.Forms.Label
    Friend WithEvents txtPhoneNum As System.Windows.Forms.TextBox
    Friend WithEvents mmsSend As System.Windows.Forms.Button
    Friend WithEvents mmsCancel As System.Windows.Forms.Button
    Friend WithEvents mmsCarrierCB As System.Windows.Forms.ComboBox
    Friend WithEvents mmsSelCarrier As System.Windows.Forms.Label
    Friend WithEvents email2label As System.Windows.Forms.Label
    Friend WithEvents usrEmail1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOrOther As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents useButton As System.Windows.Forms.Button
    Friend WithEvents lblLastEmail As System.Windows.Forms.Label
End Class
