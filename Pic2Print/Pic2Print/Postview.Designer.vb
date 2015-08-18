<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PostView
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
        Me.PostEmailGroup = New System.Windows.Forms.GroupBox()
        Me.btnPaste = New System.Windows.Forms.Button()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnEmailClose = New System.Windows.Forms.Button()
        Me.CarrierCB = New System.Windows.Forms.ComboBox()
        Me.tbPhoneNum = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.btnPostSend = New System.Windows.Forms.Button()
        Me.usrEmail2 = New System.Windows.Forms.TextBox()
        Me.emaillabel = New System.Windows.Forms.Label()
        Me.btnReprint = New System.Windows.Forms.Button()
        Me.PostCloseButton1 = New System.Windows.Forms.Button()
        Me.chkAutoScroll = New System.Windows.Forms.CheckBox()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnButtonRtEnd = New System.Windows.Forms.Button()
        Me.btnLeftEnd = New System.Windows.Forms.Button()
        Me.gbThumbBox = New System.Windows.Forms.GroupBox()
        Me.pbThumb1 = New System.Windows.Forms.PictureBox()
        Me.pbThumb2 = New System.Windows.Forms.PictureBox()
        Me.PbThumb3 = New System.Windows.Forms.PictureBox()
        Me.pbThumb4 = New System.Windows.Forms.PictureBox()
        Me.grpButtons = New System.Windows.Forms.GroupBox()
        Me.lbl_Frame = New System.Windows.Forms.Label()
        Me.btnEmailPopup = New System.Windows.Forms.Button()
        Me.pbPostView = New System.Windows.Forms.PictureBox()
        Me.ckb_PostOptin = New System.Windows.Forms.CheckBox()
        Me.ckb_PostPermit = New System.Windows.Forms.CheckBox()
        Me.PostEmailGroup.SuspendLayout()
        Me.gbThumbBox.SuspendLayout()
        CType(Me.pbThumb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbThumb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PbThumb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbThumb4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpButtons.SuspendLayout()
        CType(Me.pbPostView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PostEmailGroup
        '
        Me.PostEmailGroup.Controls.Add(Me.ckb_PostPermit)
        Me.PostEmailGroup.Controls.Add(Me.ckb_PostOptin)
        Me.PostEmailGroup.Controls.Add(Me.btnPaste)
        Me.PostEmailGroup.Controls.Add(Me.btnCopy)
        Me.PostEmailGroup.Controls.Add(Me.btnEmailClose)
        Me.PostEmailGroup.Controls.Add(Me.CarrierCB)
        Me.PostEmailGroup.Controls.Add(Me.tbPhoneNum)
        Me.PostEmailGroup.Controls.Add(Me.lblPhone)
        Me.PostEmailGroup.Controls.Add(Me.btnPostSend)
        Me.PostEmailGroup.Controls.Add(Me.usrEmail2)
        Me.PostEmailGroup.Controls.Add(Me.emaillabel)
        Me.PostEmailGroup.Location = New System.Drawing.Point(64, 191)
        Me.PostEmailGroup.Name = "PostEmailGroup"
        Me.PostEmailGroup.Size = New System.Drawing.Size(583, 170)
        Me.PostEmailGroup.TabIndex = 7
        Me.PostEmailGroup.TabStop = False
        '
        'btnPaste
        '
        Me.btnPaste.Image = Global.WindowsApplication1.My.Resources.Resources.paste
        Me.btnPaste.Location = New System.Drawing.Point(513, 76)
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Size = New System.Drawing.Size(28, 28)
        Me.btnPaste.TabIndex = 17
        Me.btnPaste.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Image = Global.WindowsApplication1.My.Resources.Resources.copy
        Me.btnCopy.Location = New System.Drawing.Point(479, 76)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(28, 28)
        Me.btnCopy.TabIndex = 16
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnEmailClose
        '
        Me.btnEmailClose.Location = New System.Drawing.Point(473, 48)
        Me.btnEmailClose.Name = "btnEmailClose"
        Me.btnEmailClose.Size = New System.Drawing.Size(75, 21)
        Me.btnEmailClose.TabIndex = 12
        Me.btnEmailClose.Text = "Cancel"
        Me.btnEmailClose.UseVisualStyleBackColor = True
        '
        'CarrierCB
        '
        Me.CarrierCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CarrierCB.FormattingEnabled = True
        Me.CarrierCB.Location = New System.Drawing.Point(229, 63)
        Me.CarrierCB.Name = "CarrierCB"
        Me.CarrierCB.Size = New System.Drawing.Size(213, 45)
        Me.CarrierCB.TabIndex = 11
        '
        'tbPhoneNum
        '
        Me.tbPhoneNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPhoneNum.Location = New System.Drawing.Point(65, 63)
        Me.tbPhoneNum.Name = "tbPhoneNum"
        Me.tbPhoneNum.Size = New System.Drawing.Size(162, 44)
        Me.tbPhoneNum.TabIndex = 10
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(7, 66)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(53, 15)
        Me.lblPhone.TabIndex = 9
        Me.lblPhone.Text = "Phone#:"
        '
        'btnPostSend
        '
        Me.btnPostSend.Location = New System.Drawing.Point(473, 19)
        Me.btnPostSend.Name = "btnPostSend"
        Me.btnPostSend.Size = New System.Drawing.Size(75, 21)
        Me.btnPostSend.TabIndex = 8
        Me.btnPostSend.Text = "Send"
        Me.btnPostSend.UseVisualStyleBackColor = True
        '
        'usrEmail2
        '
        Me.usrEmail2.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usrEmail2.Location = New System.Drawing.Point(66, 14)
        Me.usrEmail2.Name = "usrEmail2"
        Me.usrEmail2.Size = New System.Drawing.Size(376, 44)
        Me.usrEmail2.TabIndex = 7
        '
        'emaillabel
        '
        Me.emaillabel.AutoSize = True
        Me.emaillabel.Location = New System.Drawing.Point(7, 16)
        Me.emaillabel.Name = "emaillabel"
        Me.emaillabel.Size = New System.Drawing.Size(35, 13)
        Me.emaillabel.TabIndex = 6
        Me.emaillabel.Text = "Email:"
        '
        'btnReprint
        '
        Me.btnReprint.Location = New System.Drawing.Point(225, 19)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(75, 23)
        Me.btnReprint.TabIndex = 17
        Me.btnReprint.Text = "Reprint "
        Me.btnReprint.UseVisualStyleBackColor = True
        '
        'PostCloseButton1
        '
        Me.PostCloseButton1.Location = New System.Drawing.Point(324, 19)
        Me.PostCloseButton1.Name = "PostCloseButton1"
        Me.PostCloseButton1.Size = New System.Drawing.Size(75, 23)
        Me.PostCloseButton1.TabIndex = 11
        Me.PostCloseButton1.Text = "Close"
        Me.PostCloseButton1.UseVisualStyleBackColor = True
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.AutoSize = True
        Me.chkAutoScroll.Location = New System.Drawing.Point(438, 23)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(74, 17)
        Me.chkAutoScroll.TabIndex = 16
        Me.chkAutoScroll.Text = "AutoScroll"
        Me.chkAutoScroll.UseVisualStyleBackColor = True
        Me.chkAutoScroll.Visible = False
        '
        'btnLeft
        '
        Me.btnLeft.Location = New System.Drawing.Point(37, 30)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(17, 23)
        Me.btnLeft.TabIndex = 12
        Me.btnLeft.Text = "<"
        Me.btnLeft.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        Me.btnRight.Location = New System.Drawing.Point(627, 30)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(17, 23)
        Me.btnRight.TabIndex = 13
        Me.btnRight.Text = ">"
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'btnButtonRtEnd
        '
        Me.btnButtonRtEnd.Location = New System.Drawing.Point(650, 30)
        Me.btnButtonRtEnd.Name = "btnButtonRtEnd"
        Me.btnButtonRtEnd.Size = New System.Drawing.Size(27, 23)
        Me.btnButtonRtEnd.TabIndex = 14
        Me.btnButtonRtEnd.Text = ">>"
        Me.btnButtonRtEnd.UseVisualStyleBackColor = True
        '
        'btnLeftEnd
        '
        Me.btnLeftEnd.Location = New System.Drawing.Point(6, 30)
        Me.btnLeftEnd.Name = "btnLeftEnd"
        Me.btnLeftEnd.Size = New System.Drawing.Size(27, 23)
        Me.btnLeftEnd.TabIndex = 15
        Me.btnLeftEnd.Text = "<<"
        Me.btnLeftEnd.UseVisualStyleBackColor = True
        '
        'gbThumbBox
        '
        Me.gbThumbBox.Controls.Add(Me.btnLeftEnd)
        Me.gbThumbBox.Controls.Add(Me.pbThumb1)
        Me.gbThumbBox.Controls.Add(Me.btnButtonRtEnd)
        Me.gbThumbBox.Controls.Add(Me.pbThumb2)
        Me.gbThumbBox.Controls.Add(Me.btnRight)
        Me.gbThumbBox.Controls.Add(Me.PbThumb3)
        Me.gbThumbBox.Controls.Add(Me.btnLeft)
        Me.gbThumbBox.Controls.Add(Me.pbThumb4)
        Me.gbThumbBox.Location = New System.Drawing.Point(4, 0)
        Me.gbThumbBox.Name = "gbThumbBox"
        Me.gbThumbBox.Size = New System.Drawing.Size(683, 102)
        Me.gbThumbBox.TabIndex = 16
        Me.gbThumbBox.TabStop = False
        '
        'pbThumb1
        '
        Me.pbThumb1.Location = New System.Drawing.Point(60, 8)
        Me.pbThumb1.Name = "pbThumb1"
        Me.pbThumb1.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb1.TabIndex = 8
        Me.pbThumb1.TabStop = False
        '
        'pbThumb2
        '
        Me.pbThumb2.Location = New System.Drawing.Point(202, 8)
        Me.pbThumb2.Name = "pbThumb2"
        Me.pbThumb2.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb2.TabIndex = 9
        Me.pbThumb2.TabStop = False
        '
        'PbThumb3
        '
        Me.PbThumb3.Location = New System.Drawing.Point(344, 8)
        Me.PbThumb3.Name = "PbThumb3"
        Me.PbThumb3.Size = New System.Drawing.Size(135, 90)
        Me.PbThumb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PbThumb3.TabIndex = 10
        Me.PbThumb3.TabStop = False
        '
        'pbThumb4
        '
        Me.pbThumb4.Location = New System.Drawing.Point(485, 8)
        Me.pbThumb4.Name = "pbThumb4"
        Me.pbThumb4.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb4.TabIndex = 11
        Me.pbThumb4.TabStop = False
        '
        'grpButtons
        '
        Me.grpButtons.Controls.Add(Me.lbl_Frame)
        Me.grpButtons.Controls.Add(Me.btnEmailPopup)
        Me.grpButtons.Controls.Add(Me.PostCloseButton1)
        Me.grpButtons.Controls.Add(Me.chkAutoScroll)
        Me.grpButtons.Controls.Add(Me.btnReprint)
        Me.grpButtons.Location = New System.Drawing.Point(84, 477)
        Me.grpButtons.Name = "grpButtons"
        Me.grpButtons.Size = New System.Drawing.Size(540, 56)
        Me.grpButtons.TabIndex = 17
        Me.grpButtons.TabStop = False
        '
        'lbl_Frame
        '
        Me.lbl_Frame.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Frame.Location = New System.Drawing.Point(6, 19)
        Me.lbl_Frame.Name = "lbl_Frame"
        Me.lbl_Frame.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Frame.TabIndex = 19
        Me.lbl_Frame.Text = "Frame #00000"
        '
        'btnEmailPopup
        '
        Me.btnEmailPopup.Location = New System.Drawing.Point(124, 19)
        Me.btnEmailPopup.Name = "btnEmailPopup"
        Me.btnEmailPopup.Size = New System.Drawing.Size(75, 23)
        Me.btnEmailPopup.TabIndex = 18
        Me.btnEmailPopup.Text = "Email "
        Me.btnEmailPopup.UseVisualStyleBackColor = True
        '
        'pbPostView
        '
        Me.pbPostView.Location = New System.Drawing.Point(116, 108)
        Me.pbPostView.Name = "pbPostView"
        Me.pbPostView.Size = New System.Drawing.Size(480, 363)
        Me.pbPostView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPostView.TabIndex = 0
        Me.pbPostView.TabStop = False
        Me.pbPostView.Tag = ""
        '
        'ckb_PostOptin
        '
        Me.ckb_PostOptin.Location = New System.Drawing.Point(65, 122)
        Me.ckb_PostOptin.Name = "ckb_PostOptin"
        Me.ckb_PostOptin.Size = New System.Drawing.Size(160, 24)
        Me.ckb_PostOptin.TabIndex = 18
        Me.ckb_PostOptin.Text = "I OPT-IN for future emails"
        Me.ckb_PostOptin.UseVisualStyleBackColor = True
        '
        'ckb_PostPermit
        '
        Me.ckb_PostPermit.Location = New System.Drawing.Point(229, 122)
        Me.ckb_PostPermit.Name = "ckb_PostPermit"
        Me.ckb_PostPermit.Size = New System.Drawing.Size(295, 24)
        Me.ckb_PostPermit.TabIndex = 19
        Me.ckb_PostPermit.Text = "I give permission to use my image for promotional use"
        Me.ckb_PostPermit.UseVisualStyleBackColor = True
        '
        'PostView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(696, 534)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpButtons)
        Me.Controls.Add(Me.gbThumbBox)
        Me.Controls.Add(Me.PostEmailGroup)
        Me.Controls.Add(Me.pbPostView)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApplication1.My.MySettings.Default, "Prev_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.WindowsApplication1.My.MySettings.Default.Prev_Location
        Me.Name = "PostView"
        Me.Text = "Post View of Processed Images"
        Me.PostEmailGroup.ResumeLayout(False)
        Me.PostEmailGroup.PerformLayout()
        Me.gbThumbBox.ResumeLayout(False)
        CType(Me.pbThumb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbThumb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PbThumb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbThumb4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpButtons.ResumeLayout(False)
        Me.grpButtons.PerformLayout()
        CType(Me.pbPostView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbPostView As System.Windows.Forms.PictureBox
    Friend WithEvents PostEmailGroup As System.Windows.Forms.GroupBox
    Friend WithEvents btnPostSend As System.Windows.Forms.Button
    Friend WithEvents usrEmail2 As System.Windows.Forms.TextBox
    Friend WithEvents emaillabel As System.Windows.Forms.Label
    Friend WithEvents PostCloseButton1 As System.Windows.Forms.Button
    Friend WithEvents pbThumb1 As System.Windows.Forms.PictureBox
    Friend WithEvents pbThumb2 As System.Windows.Forms.PictureBox
    Friend WithEvents PbThumb3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbThumb4 As System.Windows.Forms.PictureBox
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents btnButtonRtEnd As System.Windows.Forms.Button
    Friend WithEvents btnLeftEnd As System.Windows.Forms.Button
    Friend WithEvents chkAutoScroll As System.Windows.Forms.CheckBox
    Friend WithEvents tbPhoneNum As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents CarrierCB As System.Windows.Forms.ComboBox
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents gbThumbBox As System.Windows.Forms.GroupBox
    Friend WithEvents grpButtons As System.Windows.Forms.GroupBox
    Friend WithEvents btnEmailPopup As System.Windows.Forms.Button
    Friend WithEvents btnEmailClose As System.Windows.Forms.Button
    Friend WithEvents btnPaste As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents lbl_Frame As System.Windows.Forms.Label
    Friend WithEvents ckb_PostOptin As System.Windows.Forms.CheckBox
    Friend WithEvents ckb_PostPermit As System.Windows.Forms.CheckBox
End Class
