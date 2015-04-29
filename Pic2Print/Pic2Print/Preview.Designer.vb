<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Preview
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
        Me.PrevClose = New System.Windows.Forms.Button()
        Me.Form2PictureBox = New System.Windows.Forms.PictureBox()
        Me.lblPrintMsg = New System.Windows.Forms.Label()
        Me.txtPrintMsg = New System.Windows.Forms.TextBox()
        Me.BtnSaveTxt = New System.Windows.Forms.Button()
        Me.lblRemaining = New System.Windows.Forms.Label()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.btnEmailDlg = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnLeftMost = New System.Windows.Forms.Button()
        Me.btnRightMost = New System.Windows.Forms.Button()
        Me.btnProcessOne = New System.Windows.Forms.Button()
        Me.btnRightOne = New System.Windows.Forms.Button()
        Me.btnLeftOne = New System.Windows.Forms.Button()
        Me.PreEmailGroup = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CarrierCB = New System.Windows.Forms.ComboBox()
        Me.tbPhoneNum = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.btnPostSend = New System.Windows.Forms.Button()
        Me.usrEmail2 = New System.Windows.Forms.TextBox()
        Me.emaillabel = New System.Windows.Forms.Label()
        Me.btnApply = New System.Windows.Forms.Button()
        CType(Me.Form2PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOptions.SuspendLayout()
        Me.PreEmailGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrevClose
        '
        Me.PrevClose.Location = New System.Drawing.Point(516, 39)
        Me.PrevClose.Name = "PrevClose"
        Me.PrevClose.Size = New System.Drawing.Size(75, 23)
        Me.PrevClose.TabIndex = 3
        Me.PrevClose.Text = "Close"
        Me.PrevClose.UseVisualStyleBackColor = True
        '
        'Form2PictureBox
        '
        Me.Form2PictureBox.InitialImage = Global.WindowsApplication1.My.Resources.Resources.blank
        Me.Form2PictureBox.Location = New System.Drawing.Point(12, 0)
        Me.Form2PictureBox.Name = "Form2PictureBox"
        Me.Form2PictureBox.Size = New System.Drawing.Size(608, 372)
        Me.Form2PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Form2PictureBox.TabIndex = 0
        Me.Form2PictureBox.TabStop = False
        '
        'lblPrintMsg
        '
        Me.lblPrintMsg.AutoSize = True
        Me.lblPrintMsg.Location = New System.Drawing.Point(6, 35)
        Me.lblPrintMsg.Name = "lblPrintMsg"
        Me.lblPrintMsg.Size = New System.Drawing.Size(86, 13)
        Me.lblPrintMsg.TabIndex = 4
        Me.lblPrintMsg.Text = "Printed Message"
        '
        'txtPrintMsg
        '
        Me.txtPrintMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintMsg.Location = New System.Drawing.Point(98, 36)
        Me.txtPrintMsg.MaxLength = 64
        Me.txtPrintMsg.Name = "txtPrintMsg"
        Me.txtPrintMsg.Size = New System.Drawing.Size(396, 29)
        Me.txtPrintMsg.TabIndex = 5
        '
        'BtnSaveTxt
        '
        Me.BtnSaveTxt.Location = New System.Drawing.Point(516, 10)
        Me.BtnSaveTxt.Name = "BtnSaveTxt"
        Me.BtnSaveTxt.Size = New System.Drawing.Size(75, 23)
        Me.BtnSaveTxt.TabIndex = 6
        Me.BtnSaveTxt.Text = "Save"
        Me.BtnSaveTxt.UseVisualStyleBackColor = True
        '
        'lblRemaining
        '
        Me.lblRemaining.AutoSize = True
        Me.lblRemaining.Location = New System.Drawing.Point(6, 52)
        Me.lblRemaining.Name = "lblRemaining"
        Me.lblRemaining.Size = New System.Drawing.Size(66, 13)
        Me.lblRemaining.TabIndex = 7
        Me.lblRemaining.Text = "Remaining ="
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.btnEmailDlg)
        Me.gbOptions.Controls.Add(Me.btnRefresh)
        Me.gbOptions.Controls.Add(Me.btnLeftMost)
        Me.gbOptions.Controls.Add(Me.btnRightMost)
        Me.gbOptions.Controls.Add(Me.btnProcessOne)
        Me.gbOptions.Controls.Add(Me.btnRightOne)
        Me.gbOptions.Controls.Add(Me.btnLeftOne)
        Me.gbOptions.Controls.Add(Me.lblPrintMsg)
        Me.gbOptions.Controls.Add(Me.lblRemaining)
        Me.gbOptions.Controls.Add(Me.PrevClose)
        Me.gbOptions.Controls.Add(Me.BtnSaveTxt)
        Me.gbOptions.Controls.Add(Me.txtPrintMsg)
        Me.gbOptions.Location = New System.Drawing.Point(12, 368)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(608, 75)
        Me.gbOptions.TabIndex = 8
        Me.gbOptions.TabStop = False
        '
        'btnEmailDlg
        '
        Me.btnEmailDlg.Location = New System.Drawing.Point(98, 10)
        Me.btnEmailDlg.Name = "btnEmailDlg"
        Me.btnEmailDlg.Size = New System.Drawing.Size(53, 23)
        Me.btnEmailDlg.TabIndex = 14
        Me.btnEmailDlg.Text = "Email"
        Me.btnEmailDlg.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(441, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(53, 23)
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnLeftMost
        '
        Me.btnLeftMost.Location = New System.Drawing.Point(209, 10)
        Me.btnLeftMost.Name = "btnLeftMost"
        Me.btnLeftMost.Size = New System.Drawing.Size(29, 23)
        Me.btnLeftMost.TabIndex = 12
        Me.btnLeftMost.Text = "<<"
        Me.btnLeftMost.UseVisualStyleBackColor = True
        '
        'btnRightMost
        '
        Me.btnRightMost.Location = New System.Drawing.Point(349, 10)
        Me.btnRightMost.Name = "btnRightMost"
        Me.btnRightMost.Size = New System.Drawing.Size(29, 23)
        Me.btnRightMost.TabIndex = 11
        Me.btnRightMost.Text = ">>"
        Me.btnRightMost.UseVisualStyleBackColor = True
        '
        'btnProcessOne
        '
        Me.btnProcessOne.Location = New System.Drawing.Point(279, 10)
        Me.btnProcessOne.Name = "btnProcessOne"
        Me.btnProcessOne.Size = New System.Drawing.Size(29, 23)
        Me.btnProcessOne.TabIndex = 10
        Me.btnProcessOne.Text = "1"
        Me.btnProcessOne.UseVisualStyleBackColor = True
        '
        'btnRightOne
        '
        Me.btnRightOne.Location = New System.Drawing.Point(314, 10)
        Me.btnRightOne.Name = "btnRightOne"
        Me.btnRightOne.Size = New System.Drawing.Size(29, 23)
        Me.btnRightOne.TabIndex = 9
        Me.btnRightOne.Text = ">"
        Me.btnRightOne.UseVisualStyleBackColor = True
        '
        'btnLeftOne
        '
        Me.btnLeftOne.Location = New System.Drawing.Point(244, 10)
        Me.btnLeftOne.Name = "btnLeftOne"
        Me.btnLeftOne.Size = New System.Drawing.Size(29, 23)
        Me.btnLeftOne.TabIndex = 8
        Me.btnLeftOne.Text = "<"
        Me.btnLeftOne.UseVisualStyleBackColor = True
        '
        'PreEmailGroup
        '
        Me.PreEmailGroup.Controls.Add(Me.btnApply)
        Me.PreEmailGroup.Controls.Add(Me.Button1)
        Me.PreEmailGroup.Controls.Add(Me.CarrierCB)
        Me.PreEmailGroup.Controls.Add(Me.tbPhoneNum)
        Me.PreEmailGroup.Controls.Add(Me.lblPhone)
        Me.PreEmailGroup.Controls.Add(Me.btnPostSend)
        Me.PreEmailGroup.Controls.Add(Me.usrEmail2)
        Me.PreEmailGroup.Controls.Add(Me.emaillabel)
        Me.PreEmailGroup.Location = New System.Drawing.Point(35, 141)
        Me.PreEmailGroup.Name = "PreEmailGroup"
        Me.PreEmailGroup.Size = New System.Drawing.Size(563, 134)
        Me.PreEmailGroup.TabIndex = 9
        Me.PreEmailGroup.TabStop = False
        Me.PreEmailGroup.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(482, 84)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 21)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CarrierCB
        '
        Me.CarrierCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CarrierCB.FormattingEnabled = True
        Me.CarrierCB.Location = New System.Drawing.Point(263, 70)
        Me.CarrierCB.Name = "CarrierCB"
        Me.CarrierCB.Size = New System.Drawing.Size(213, 45)
        Me.CarrierCB.TabIndex = 11
        '
        'tbPhoneNum
        '
        Me.tbPhoneNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPhoneNum.Location = New System.Drawing.Point(63, 70)
        Me.tbPhoneNum.Name = "tbPhoneNum"
        Me.tbPhoneNum.Size = New System.Drawing.Size(191, 44)
        Me.tbPhoneNum.TabIndex = 10
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(4, 84)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(53, 15)
        Me.lblPhone.TabIndex = 9
        Me.lblPhone.Text = "Phone#:"
        '
        'btnPostSend
        '
        Me.btnPostSend.Location = New System.Drawing.Point(482, 30)
        Me.btnPostSend.Name = "btnPostSend"
        Me.btnPostSend.Size = New System.Drawing.Size(75, 21)
        Me.btnPostSend.TabIndex = 8
        Me.btnPostSend.Text = "Send"
        Me.btnPostSend.UseVisualStyleBackColor = True
        '
        'usrEmail2
        '
        Me.usrEmail2.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usrEmail2.Location = New System.Drawing.Point(64, 20)
        Me.usrEmail2.Name = "usrEmail2"
        Me.usrEmail2.Size = New System.Drawing.Size(412, 44)
        Me.usrEmail2.TabIndex = 7
        '
        'emaillabel
        '
        Me.emaillabel.AutoSize = True
        Me.emaillabel.Location = New System.Drawing.Point(7, 27)
        Me.emaillabel.Name = "emaillabel"
        Me.emaillabel.Size = New System.Drawing.Size(35, 13)
        Me.emaillabel.TabIndex = 6
        Me.emaillabel.Text = "Email:"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(482, 57)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 21)
        Me.btnApply.TabIndex = 13
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = Global.WindowsApplication1.My.MySettings.Default.img_size
        Me.ControlBox = False
        Me.Controls.Add(Me.PreEmailGroup)
        Me.Controls.Add(Me.gbOptions)
        Me.Controls.Add(Me.Form2PictureBox)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApplication1.My.MySettings.Default, "Thumbnail_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.WindowsApplication1.My.MySettings.Default, "img_size", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.WindowsApplication1.My.MySettings.Default.Thumbnail_Location
        Me.Name = "Preview"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview"
        CType(Me.Form2PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.PreEmailGroup.ResumeLayout(False)
        Me.PreEmailGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Form2PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents PrevClose As System.Windows.Forms.Button
    Friend WithEvents lblPrintMsg As System.Windows.Forms.Label
    Friend WithEvents txtPrintMsg As System.Windows.Forms.TextBox
    Friend WithEvents BtnSaveTxt As System.Windows.Forms.Button
    Friend WithEvents lblRemaining As System.Windows.Forms.Label
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents btnRightOne As System.Windows.Forms.Button
    Friend WithEvents btnLeftOne As System.Windows.Forms.Button
    Friend WithEvents btnProcessOne As System.Windows.Forms.Button
    Friend WithEvents btnEmailDlg As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnLeftMost As System.Windows.Forms.Button
    Friend WithEvents btnRightMost As System.Windows.Forms.Button
    Friend WithEvents PreEmailGroup As System.Windows.Forms.GroupBox
    Friend WithEvents CarrierCB As System.Windows.Forms.ComboBox
    Friend WithEvents tbPhoneNum As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents btnPostSend As System.Windows.Forms.Button
    Friend WithEvents usrEmail2 As System.Windows.Forms.TextBox
    Friend WithEvents emaillabel As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
End Class
