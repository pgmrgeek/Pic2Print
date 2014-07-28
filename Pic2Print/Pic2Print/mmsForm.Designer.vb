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
        Me.CarrierLB = New System.Windows.Forms.ComboBox()
        Me.mmsSelCarrier = New System.Windows.Forms.Label()
        Me.email2label = New System.Windows.Forms.Label()
        Me.usrEmail1 = New System.Windows.Forms.TextBox()
        Me.lblOrOther = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblEnterPhoneNum
        '
        Me.lblEnterPhoneNum.AutoSize = True
        Me.lblEnterPhoneNum.Location = New System.Drawing.Point(12, 83)
        Me.lblEnterPhoneNum.Name = "lblEnterPhoneNum"
        Me.lblEnterPhoneNum.Size = New System.Drawing.Size(166, 13)
        Me.lblEnterPhoneNum.TabIndex = 0
        Me.lblEnterPhoneNum.Text = "Enter Your Phone # (xxx-xxx-xxxx)"
        '
        'txtPhoneNum
        '
        Me.txtPhoneNum.Location = New System.Drawing.Point(15, 99)
        Me.txtPhoneNum.Name = "txtPhoneNum"
        Me.txtPhoneNum.Size = New System.Drawing.Size(216, 20)
        Me.txtPhoneNum.TabIndex = 1
        '
        'mmsSend
        '
        Me.mmsSend.Location = New System.Drawing.Point(15, 170)
        Me.mmsSend.Name = "mmsSend"
        Me.mmsSend.Size = New System.Drawing.Size(75, 23)
        Me.mmsSend.TabIndex = 2
        Me.mmsSend.Text = "Send"
        Me.mmsSend.UseVisualStyleBackColor = True
        '
        'mmsCancel
        '
        Me.mmsCancel.Location = New System.Drawing.Point(156, 170)
        Me.mmsCancel.Name = "mmsCancel"
        Me.mmsCancel.Size = New System.Drawing.Size(75, 23)
        Me.mmsCancel.TabIndex = 3
        Me.mmsCancel.Text = "Cancel"
        Me.mmsCancel.UseVisualStyleBackColor = True
        '
        'CarrierLB
        '
        Me.CarrierLB.FormattingEnabled = True
        Me.CarrierLB.Location = New System.Drawing.Point(15, 138)
        Me.CarrierLB.Name = "CarrierLB"
        Me.CarrierLB.Size = New System.Drawing.Size(216, 21)
        Me.CarrierLB.TabIndex = 4
        '
        'mmsSelCarrier
        '
        Me.mmsSelCarrier.AutoSize = True
        Me.mmsSelCarrier.Location = New System.Drawing.Point(12, 122)
        Me.mmsSelCarrier.Name = "mmsSelCarrier"
        Me.mmsSelCarrier.Size = New System.Drawing.Size(95, 13)
        Me.mmsSelCarrier.TabIndex = 6
        Me.mmsSelCarrier.Text = "Select Your Carrier"
        '
        'email2label
        '
        Me.email2label.AutoSize = True
        Me.email2label.Location = New System.Drawing.Point(12, 9)
        Me.email2label.Name = "email2label"
        Me.email2label.Size = New System.Drawing.Size(129, 13)
        Me.email2label.TabIndex = 8
        Me.email2label.Text = "Enter Your Email Address:"
        '
        'usrEmail1
        '
        Me.usrEmail1.Location = New System.Drawing.Point(15, 25)
        Me.usrEmail1.Name = "usrEmail1"
        Me.usrEmail1.Size = New System.Drawing.Size(216, 20)
        Me.usrEmail1.TabIndex = 9
        '
        'lblOrOther
        '
        Me.lblOrOther.AutoSize = True
        Me.lblOrOther.Location = New System.Drawing.Point(62, 57)
        Me.lblOrOther.Name = "lblOrOther"
        Me.lblOrOther.Size = New System.Drawing.Size(116, 13)
        Me.lblOrOther.TabIndex = 10
        Me.lblOrOther.Text = "--------------- OR  -------------"
        '
        'mmsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(255, 205)
        Me.Controls.Add(Me.lblOrOther)
        Me.Controls.Add(Me.email2label)
        Me.Controls.Add(Me.usrEmail1)
        Me.Controls.Add(Me.mmsSelCarrier)
        Me.Controls.Add(Me.CarrierLB)
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
    Friend WithEvents CarrierLB As System.Windows.Forms.ComboBox
    Friend WithEvents mmsSelCarrier As System.Windows.Forms.Label
    Friend WithEvents email2label As System.Windows.Forms.Label
    Friend WithEvents usrEmail1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOrOther As System.Windows.Forms.Label
End Class
