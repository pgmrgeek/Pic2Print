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
        Me.pbPostView = New System.Windows.Forms.PictureBox()
        Me.PostGroup = New System.Windows.Forms.GroupBox()
        Me.CarrierCB = New System.Windows.Forms.ComboBox()
        Me.tbPhoneNum = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.btnPostSend = New System.Windows.Forms.Button()
        Me.usrEmail2 = New System.Windows.Forms.TextBox()
        Me.emaillabel = New System.Windows.Forms.Label()
        Me.PostCloseButton1 = New System.Windows.Forms.Button()
        Me.pbThumb1 = New System.Windows.Forms.PictureBox()
        Me.pbThumb2 = New System.Windows.Forms.PictureBox()
        Me.PbThumb3 = New System.Windows.Forms.PictureBox()
        Me.pbThumb4 = New System.Windows.Forms.PictureBox()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnButtonRtEnd = New System.Windows.Forms.Button()
        Me.btnLeftEnd = New System.Windows.Forms.Button()
        Me.chkAutoScroll = New System.Windows.Forms.CheckBox()
        Me.btnReprint = New System.Windows.Forms.Button()
        CType(Me.pbPostView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PostGroup.SuspendLayout()
        CType(Me.pbThumb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbThumb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PbThumb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbThumb4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbPostView
        '
        Me.pbPostView.Location = New System.Drawing.Point(116, 108)
        Me.pbPostView.Name = "pbPostView"
        Me.pbPostView.Size = New System.Drawing.Size(480, 320)
        Me.pbPostView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPostView.TabIndex = 0
        Me.pbPostView.TabStop = False
        Me.pbPostView.Tag = ""
        '
        'PostGroup
        '
        Me.PostGroup.Controls.Add(Me.CarrierCB)
        Me.PostGroup.Controls.Add(Me.tbPhoneNum)
        Me.PostGroup.Controls.Add(Me.lblPhone)
        Me.PostGroup.Controls.Add(Me.btnPostSend)
        Me.PostGroup.Controls.Add(Me.usrEmail2)
        Me.PostGroup.Controls.Add(Me.emaillabel)
        Me.PostGroup.Location = New System.Drawing.Point(138, 435)
        Me.PostGroup.Name = "PostGroup"
        Me.PostGroup.Size = New System.Drawing.Size(443, 93)
        Me.PostGroup.TabIndex = 7
        Me.PostGroup.TabStop = False
        '
        'CarrierCB
        '
        Me.CarrierCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CarrierCB.FormattingEnabled = True
        Me.CarrierCB.Location = New System.Drawing.Point(222, 46)
        Me.CarrierCB.Name = "CarrierCB"
        Me.CarrierCB.Size = New System.Drawing.Size(213, 32)
        Me.CarrierCB.TabIndex = 11
        '
        'tbPhoneNum
        '
        Me.tbPhoneNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPhoneNum.Location = New System.Drawing.Point(59, 49)
        Me.tbPhoneNum.Name = "tbPhoneNum"
        Me.tbPhoneNum.Size = New System.Drawing.Size(162, 29)
        Me.tbPhoneNum.TabIndex = 10
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(7, 52)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(53, 15)
        Me.lblPhone.TabIndex = 9
        Me.lblPhone.Text = "Phone#:"
        '
        'btnPostSend
        '
        Me.btnPostSend.Location = New System.Drawing.Point(360, 19)
        Me.btnPostSend.Name = "btnPostSend"
        Me.btnPostSend.Size = New System.Drawing.Size(75, 21)
        Me.btnPostSend.TabIndex = 8
        Me.btnPostSend.Text = "Send"
        Me.btnPostSend.UseVisualStyleBackColor = True
        '
        'usrEmail2
        '
        Me.usrEmail2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usrEmail2.Location = New System.Drawing.Point(59, 14)
        Me.usrEmail2.Name = "usrEmail2"
        Me.usrEmail2.Size = New System.Drawing.Size(295, 29)
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
        'PostCloseButton1
        '
        Me.PostCloseButton1.Location = New System.Drawing.Point(614, 505)
        Me.PostCloseButton1.Name = "PostCloseButton1"
        Me.PostCloseButton1.Size = New System.Drawing.Size(75, 23)
        Me.PostCloseButton1.TabIndex = 11
        Me.PostCloseButton1.Text = "Close"
        Me.PostCloseButton1.UseVisualStyleBackColor = True
        '
        'pbThumb1
        '
        Me.pbThumb1.Location = New System.Drawing.Point(72, 12)
        Me.pbThumb1.Name = "pbThumb1"
        Me.pbThumb1.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb1.TabIndex = 8
        Me.pbThumb1.TabStop = False
        '
        'pbThumb2
        '
        Me.pbThumb2.Location = New System.Drawing.Point(214, 12)
        Me.pbThumb2.Name = "pbThumb2"
        Me.pbThumb2.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb2.TabIndex = 9
        Me.pbThumb2.TabStop = False
        '
        'PbThumb3
        '
        Me.PbThumb3.Location = New System.Drawing.Point(356, 12)
        Me.PbThumb3.Name = "PbThumb3"
        Me.PbThumb3.Size = New System.Drawing.Size(135, 90)
        Me.PbThumb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PbThumb3.TabIndex = 10
        Me.PbThumb3.TabStop = False
        '
        'pbThumb4
        '
        Me.pbThumb4.Location = New System.Drawing.Point(497, 12)
        Me.pbThumb4.Name = "pbThumb4"
        Me.pbThumb4.Size = New System.Drawing.Size(135, 90)
        Me.pbThumb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbThumb4.TabIndex = 11
        Me.pbThumb4.TabStop = False
        '
        'btnLeft
        '
        Me.btnLeft.Location = New System.Drawing.Point(45, 34)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(17, 23)
        Me.btnLeft.TabIndex = 12
        Me.btnLeft.Text = "<"
        Me.btnLeft.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        Me.btnRight.Location = New System.Drawing.Point(639, 34)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(17, 23)
        Me.btnRight.TabIndex = 13
        Me.btnRight.Text = ">"
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'btnButtonRtEnd
        '
        Me.btnButtonRtEnd.Location = New System.Drawing.Point(662, 34)
        Me.btnButtonRtEnd.Name = "btnButtonRtEnd"
        Me.btnButtonRtEnd.Size = New System.Drawing.Size(27, 23)
        Me.btnButtonRtEnd.TabIndex = 14
        Me.btnButtonRtEnd.Text = ">>"
        Me.btnButtonRtEnd.UseVisualStyleBackColor = True
        '
        'btnLeftEnd
        '
        Me.btnLeftEnd.Location = New System.Drawing.Point(12, 34)
        Me.btnLeftEnd.Name = "btnLeftEnd"
        Me.btnLeftEnd.Size = New System.Drawing.Size(27, 23)
        Me.btnLeftEnd.TabIndex = 15
        Me.btnLeftEnd.Text = "<<"
        Me.btnLeftEnd.UseVisualStyleBackColor = True
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.AutoSize = True
        Me.chkAutoScroll.Location = New System.Drawing.Point(615, 482)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(74, 17)
        Me.chkAutoScroll.TabIndex = 16
        Me.chkAutoScroll.Text = "AutoScroll"
        Me.chkAutoScroll.UseVisualStyleBackColor = True
        Me.chkAutoScroll.Visible = False
        '
        'btnReprint
        '
        Me.btnReprint.Location = New System.Drawing.Point(614, 453)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(75, 23)
        Me.btnReprint.TabIndex = 17
        Me.btnReprint.Text = "Reprint "
        Me.btnReprint.UseVisualStyleBackColor = True
        '
        'PostView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(696, 534)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnReprint)
        Me.Controls.Add(Me.PostCloseButton1)
        Me.Controls.Add(Me.chkAutoScroll)
        Me.Controls.Add(Me.btnLeftEnd)
        Me.Controls.Add(Me.btnButtonRtEnd)
        Me.Controls.Add(Me.btnRight)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.pbThumb4)
        Me.Controls.Add(Me.PbThumb3)
        Me.Controls.Add(Me.pbThumb2)
        Me.Controls.Add(Me.pbThumb1)
        Me.Controls.Add(Me.PostGroup)
        Me.Controls.Add(Me.pbPostView)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApplication1.My.MySettings.Default, "Prev_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.WindowsApplication1.My.MySettings.Default.Prev_Location
        Me.Name = "PostView"
        Me.Text = "Post View of Processed Images"
        CType(Me.pbPostView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PostGroup.ResumeLayout(False)
        Me.PostGroup.PerformLayout()
        CType(Me.pbThumb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbThumb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PbThumb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbThumb4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbPostView As System.Windows.Forms.PictureBox
    Friend WithEvents PostGroup As System.Windows.Forms.GroupBox
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
End Class
