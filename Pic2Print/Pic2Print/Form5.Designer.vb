<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form5
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gbDateQuals = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cbPrintNoDates = New System.Windows.Forms.CheckBox()
        Me.lblImageNewer = New System.Windows.Forms.Label()
        Me.cbDateQualified = New System.Windows.Forms.CheckBox()
        Me.dtEarliestDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblAutoPrintCount = New System.Windows.Forms.Label()
        Me.txtAutoPrintCnt = New System.Windows.Forms.TextBox()
        Me.Printer1Info = New System.Windows.Forms.GroupBox()
        Me.lblPprCnt = New System.Windows.Forms.Label()
        Me.lblPrtrHlp = New System.Windows.Forms.Label()
        Me.Printer1PaperCount = New System.Windows.Forms.TextBox()
        Me.Printer1LB = New System.Windows.Forms.ComboBox()
        Me.GroupBoxBKFG = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxBKFG = New System.Windows.Forms.ComboBox()
        Me.lblFilterSelection = New System.Windows.Forms.Label()
        Me.FilterDescription = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbFilter3 = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbFilter2 = New System.Windows.Forms.ComboBox()
        Me.gbFilter1 = New System.Windows.Forms.GroupBox()
        Me.cbFilter1 = New System.Windows.Forms.ComboBox()
        Me.ExpertButton = New System.Windows.Forms.Button()
        Me.OKay = New System.Windows.Forms.Button()
        Me.VersionBox = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grpOperatorMd = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbIncomingHlp = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ckSavePSD = New System.Windows.Forms.CheckBox()
        Me.gbDateQuals.SuspendLayout()
        Me.Printer1Info.SuspendLayout()
        Me.GroupBoxBKFG.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gbFilter1.SuspendLayout()
        Me.grpOperatorMd.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Label9.Location = New System.Drawing.Point(-245, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1199, 13)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "                                                                                 " & _
    "                                                     "
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.MediumSpringGreen
        Me.Label8.Location = New System.Drawing.Point(-245, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1199, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "                                                                                 " & _
    "                                                     "
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Black
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.Window
        Me.Label10.Location = New System.Drawing.Point(-25, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1199, 13)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "            Bay Area Event Photography Onsite Printing                           " & _
    "                                                                                " & _
    "        "
        '
        'gbDateQuals
        '
        Me.gbDateQuals.Controls.Add(Me.Label2)
        Me.gbDateQuals.Controls.Add(Me.Label19)
        Me.gbDateQuals.Controls.Add(Me.cbPrintNoDates)
        Me.gbDateQuals.Controls.Add(Me.lblImageNewer)
        Me.gbDateQuals.Controls.Add(Me.cbDateQualified)
        Me.gbDateQuals.Controls.Add(Me.dtEarliestDate)
        Me.gbDateQuals.Controls.Add(Me.Label13)
        Me.gbDateQuals.Controls.Add(Me.lblAutoPrintCount)
        Me.gbDateQuals.Controls.Add(Me.txtAutoPrintCnt)
        Me.gbDateQuals.Location = New System.Drawing.Point(29, 171)
        Me.gbDateQuals.Name = "gbDateQuals"
        Me.gbDateQuals.Size = New System.Drawing.Size(283, 210)
        Me.gbDateQuals.TabIndex = 121
        Me.gbDateQuals.TabStop = False
        Me.gbDateQuals.Text = "KIOSK mode rules"
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(13, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(264, 30)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "Incoming images go directly to the printer, set the # of prints per image, and qu" & _
    "alify images created today."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label19.Location = New System.Drawing.Point(12, 95)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(186, 13)
        Me.Label19.TabIndex = 121
        Me.Label19.Text = "Check this to print only today's images"
        '
        'cbPrintNoDates
        '
        Me.cbPrintNoDates.AutoSize = True
        Me.cbPrintNoDates.Checked = Global.WindowsApplication1.My.MySettings.Default.KioskPrintAnyway
        Me.cbPrintNoDates.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.WindowsApplication1.My.MySettings.Default, "KioskPrintAnyway", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.cbPrintNoDates.Location = New System.Drawing.Point(35, 178)
        Me.cbPrintNoDates.Name = "cbPrintNoDates"
        Me.cbPrintNoDates.Size = New System.Drawing.Size(163, 17)
        Me.cbPrintNoDates.TabIndex = 123
        Me.cbPrintNoDates.Text = "Print anyway if no date found"
        Me.cbPrintNoDates.UseVisualStyleBackColor = True
        '
        'lblImageNewer
        '
        Me.lblImageNewer.AutoSize = True
        Me.lblImageNewer.ForeColor = System.Drawing.Color.Black
        Me.lblImageNewer.Location = New System.Drawing.Point(32, 134)
        Me.lblImageNewer.Name = "lblImageNewer"
        Me.lblImageNewer.Size = New System.Drawing.Size(132, 13)
        Me.lblImageNewer.TabIndex = 122
        Me.lblImageNewer.Text = "Image must be newer than"
        '
        'cbDateQualified
        '
        Me.cbDateQualified.AutoSize = True
        Me.cbDateQualified.Checked = Global.WindowsApplication1.My.MySettings.Default.KIOSKdateQual
        Me.cbDateQualified.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.WindowsApplication1.My.MySettings.Default, "KIOSKdateQual", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.cbDateQualified.Location = New System.Drawing.Point(16, 111)
        Me.cbDateQualified.Name = "cbDateQualified"
        Me.cbDateQualified.Size = New System.Drawing.Size(168, 17)
        Me.cbDateQualified.TabIndex = 119
        Me.cbDateQualified.Text = "KIOSK will date qualify images"
        Me.cbDateQualified.UseVisualStyleBackColor = True
        '
        'dtEarliestDate
        '
        Me.dtEarliestDate.Checked = False
        Me.dtEarliestDate.DataBindings.Add(New System.Windows.Forms.Binding("Value", Global.WindowsApplication1.My.MySettings.Default, "dtEarliestDateSelected", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.dtEarliestDate.Location = New System.Drawing.Point(35, 152)
        Me.dtEarliestDate.Name = "dtEarliestDate"
        Me.dtEarliestDate.Size = New System.Drawing.Size(215, 20)
        Me.dtEarliestDate.TabIndex = 0
        Me.dtEarliestDate.Value = Global.WindowsApplication1.My.MySettings.Default.dtEarliestDateSelected
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label13.Location = New System.Drawing.Point(13, 53)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(155, 13)
        Me.Label13.TabIndex = 99
        Me.Label13.Text = "# of prints for automatic printing"
        '
        'lblAutoPrintCount
        '
        Me.lblAutoPrintCount.AutoSize = True
        Me.lblAutoPrintCount.Location = New System.Drawing.Point(47, 74)
        Me.lblAutoPrintCount.Name = "lblAutoPrintCount"
        Me.lblAutoPrintCount.Size = New System.Drawing.Size(179, 13)
        Me.lblAutoPrintCount.TabIndex = 96
        Me.lblAutoPrintCount.Text = "# of Prints per image in KIOSK mode"
        '
        'txtAutoPrintCnt
        '
        Me.txtAutoPrintCnt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "txtAutoPrtCnt", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtAutoPrintCnt.Location = New System.Drawing.Point(14, 70)
        Me.txtAutoPrintCnt.Name = "txtAutoPrintCnt"
        Me.txtAutoPrintCnt.Size = New System.Drawing.Size(27, 20)
        Me.txtAutoPrintCnt.TabIndex = 97
        Me.txtAutoPrintCnt.Text = Global.WindowsApplication1.My.MySettings.Default.txtAutoPrtCnt
        Me.txtAutoPrintCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Printer1Info
        '
        Me.Printer1Info.Controls.Add(Me.lblPprCnt)
        Me.Printer1Info.Controls.Add(Me.lblPrtrHlp)
        Me.Printer1Info.Controls.Add(Me.Printer1PaperCount)
        Me.Printer1Info.Controls.Add(Me.Printer1LB)
        Me.Printer1Info.Location = New System.Drawing.Point(359, 98)
        Me.Printer1Info.Name = "Printer1Info"
        Me.Printer1Info.Size = New System.Drawing.Size(296, 62)
        Me.Printer1Info.TabIndex = 122
        Me.Printer1Info.TabStop = False
        Me.Printer1Info.Text = "Printer #1 Model && Paper Selection"
        '
        'lblPprCnt
        '
        Me.lblPprCnt.AutoSize = True
        Me.lblPprCnt.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblPprCnt.Location = New System.Drawing.Point(227, 15)
        Me.lblPprCnt.Name = "lblPprCnt"
        Me.lblPprCnt.Size = New System.Drawing.Size(66, 13)
        Me.lblPprCnt.TabIndex = 8
        Me.lblPprCnt.Text = "Paper Count"
        '
        'lblPrtrHlp
        '
        Me.lblPrtrHlp.AutoSize = True
        Me.lblPrtrHlp.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblPrtrHlp.Location = New System.Drawing.Point(11, 15)
        Me.lblPrtrHlp.Name = "lblPrtrHlp"
        Me.lblPrtrHlp.Size = New System.Drawing.Size(144, 13)
        Me.lblPrtrHlp.TabIndex = 7
        Me.lblPrtrHlp.Text = "Select the Printer && Print Size"
        '
        'Printer1PaperCount
        '
        Me.Printer1PaperCount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.WindowsApplication1.My.MySettings.Default, "Print1Sheets", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Printer1PaperCount.Location = New System.Drawing.Point(235, 33)
        Me.Printer1PaperCount.Name = "Printer1PaperCount"
        Me.Printer1PaperCount.Size = New System.Drawing.Size(42, 20)
        Me.Printer1PaperCount.TabIndex = 6
        Me.Printer1PaperCount.Text = Global.WindowsApplication1.My.MySettings.Default.Print1Sheets
        '
        'Printer1LB
        '
        Me.Printer1LB.FormattingEnabled = True
        Me.Printer1LB.Location = New System.Drawing.Point(15, 33)
        Me.Printer1LB.Name = "Printer1LB"
        Me.Printer1LB.Size = New System.Drawing.Size(209, 21)
        Me.Printer1LB.TabIndex = 1
        '
        'GroupBoxBKFG
        '
        Me.GroupBoxBKFG.Controls.Add(Me.Label4)
        Me.GroupBoxBKFG.Controls.Add(Me.ComboBoxBKFG)
        Me.GroupBoxBKFG.Location = New System.Drawing.Point(359, 168)
        Me.GroupBoxBKFG.Name = "GroupBoxBKFG"
        Me.GroupBoxBKFG.Size = New System.Drawing.Size(296, 69)
        Me.GroupBoxBKFG.TabIndex = 123
        Me.GroupBoxBKFG.TabStop = False
        Me.GroupBoxBKFG.Text = "Layout Selection"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(12, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(165, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Select the layout you wish to print"
        '
        'ComboBoxBKFG
        '
        Me.ComboBoxBKFG.FormattingEnabled = True
        Me.ComboBoxBKFG.Location = New System.Drawing.Point(15, 36)
        Me.ComboBoxBKFG.Name = "ComboBoxBKFG"
        Me.ComboBoxBKFG.Size = New System.Drawing.Size(272, 21)
        Me.ComboBoxBKFG.TabIndex = 0
        '
        'lblFilterSelection
        '
        Me.lblFilterSelection.AutoSize = True
        Me.lblFilterSelection.Location = New System.Drawing.Point(356, 243)
        Me.lblFilterSelection.Name = "lblFilterSelection"
        Me.lblFilterSelection.Size = New System.Drawing.Size(81, 13)
        Me.lblFilterSelection.TabIndex = 130
        Me.lblFilterSelection.Text = "Filter Selections"
        '
        'FilterDescription
        '
        Me.FilterDescription.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.FilterDescription.Location = New System.Drawing.Point(370, 261)
        Me.FilterDescription.Name = "FilterDescription"
        Me.FilterDescription.Size = New System.Drawing.Size(285, 28)
        Me.FilterDescription.TabIndex = 129
        Me.FilterDescription.Text = "Select up to three filters for custom effects.  This is applied to the midground " & _
    "and background layers."
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cbFilter3)
        Me.GroupBox4.Location = New System.Drawing.Point(359, 399)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(296, 50)
        Me.GroupBox4.TabIndex = 128
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Filter #3"
        '
        'cbFilter3
        '
        Me.cbFilter3.FormattingEnabled = True
        Me.cbFilter3.Location = New System.Drawing.Point(7, 16)
        Me.cbFilter3.Name = "cbFilter3"
        Me.cbFilter3.Size = New System.Drawing.Size(280, 21)
        Me.cbFilter3.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbFilter2)
        Me.GroupBox3.Location = New System.Drawing.Point(359, 346)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(296, 50)
        Me.GroupBox3.TabIndex = 127
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter #2"
        '
        'cbFilter2
        '
        Me.cbFilter2.FormattingEnabled = True
        Me.cbFilter2.Location = New System.Drawing.Point(7, 16)
        Me.cbFilter2.Name = "cbFilter2"
        Me.cbFilter2.Size = New System.Drawing.Size(280, 21)
        Me.cbFilter2.TabIndex = 0
        '
        'gbFilter1
        '
        Me.gbFilter1.Controls.Add(Me.cbFilter1)
        Me.gbFilter1.Location = New System.Drawing.Point(359, 292)
        Me.gbFilter1.Name = "gbFilter1"
        Me.gbFilter1.Size = New System.Drawing.Size(296, 50)
        Me.gbFilter1.TabIndex = 126
        Me.gbFilter1.TabStop = False
        Me.gbFilter1.Text = "Filter #1"
        '
        'cbFilter1
        '
        Me.cbFilter1.FormattingEnabled = True
        Me.cbFilter1.Location = New System.Drawing.Point(7, 16)
        Me.cbFilter1.Name = "cbFilter1"
        Me.cbFilter1.Size = New System.Drawing.Size(280, 21)
        Me.cbFilter1.TabIndex = 0
        '
        'ExpertButton
        '
        Me.ExpertButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpertButton.Location = New System.Drawing.Point(29, 400)
        Me.ExpertButton.Name = "ExpertButton"
        Me.ExpertButton.Size = New System.Drawing.Size(121, 79)
        Me.ExpertButton.TabIndex = 131
        Me.ExpertButton.Text = "Expert Mode"
        Me.ExpertButton.UseVisualStyleBackColor = True
        '
        'OKay
        '
        Me.OKay.Location = New System.Drawing.Point(207, 456)
        Me.OKay.Name = "OKay"
        Me.OKay.Size = New System.Drawing.Size(75, 23)
        Me.OKay.TabIndex = 132
        Me.OKay.Text = "OK"
        Me.OKay.UseVisualStyleBackColor = True
        '
        'VersionBox
        '
        Me.VersionBox.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.VersionBox.Location = New System.Drawing.Point(335, 495)
        Me.VersionBox.Name = "VersionBox"
        Me.VersionBox.Size = New System.Drawing.Size(97, 18)
        Me.VersionBox.TabIndex = 134
        Me.VersionBox.Text = "Version"
        Me.VersionBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label11.Location = New System.Drawing.Point(12, 497)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(317, 13)
        Me.Label11.TabIndex = 133
        Me.Label11.Text = "Copyright (c) -  Bay Area Event Photography - All Rights Reserved"
        '
        'grpOperatorMd
        '
        Me.grpOperatorMd.Controls.Add(Me.Label1)
        Me.grpOperatorMd.Location = New System.Drawing.Point(29, 98)
        Me.grpOperatorMd.Name = "grpOperatorMd"
        Me.grpOperatorMd.Size = New System.Drawing.Size(283, 67)
        Me.grpOperatorMd.TabIndex = 135
        Me.grpOperatorMd.TabStop = False
        Me.grpOperatorMd.Text = "Operator Mode rules"
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(13, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 42)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Easy enough. Incoming images appear on the Control Panel. Click an image, then cl" & _
    "ick # of prints."
        '
        'tbIncomingHlp
        '
        Me.tbIncomingHlp.BackColor = System.Drawing.SystemColors.ControlText
        Me.tbIncomingHlp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbIncomingHlp.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.tbIncomingHlp.Location = New System.Drawing.Point(29, 63)
        Me.tbIncomingHlp.Name = "tbIncomingHlp"
        Me.tbIncomingHlp.Size = New System.Drawing.Size(283, 27)
        Me.tbIncomingHlp.TabIndex = 136
        Me.tbIncomingHlp.Text = "Incoming Images"
        Me.tbIncomingHlp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label3.Location = New System.Drawing.Point(359, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(293, 27)
        Me.Label3.TabIndex = 137
        Me.Label3.Text = "Outgoing Images"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ckSavePSD
        '
        Me.ckSavePSD.AutoSize = True
        Me.ckSavePSD.Checked = Global.WindowsApplication1.My.MySettings.Default.ckSavePSDFile
        Me.ckSavePSD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckSavePSD.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.WindowsApplication1.My.MySettings.Default, "ckSavePSDFile", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ckSavePSD.Location = New System.Drawing.Point(418, 459)
        Me.ckSavePSD.Name = "ckSavePSD"
        Me.ckSavePSD.Size = New System.Drawing.Size(183, 17)
        Me.ckSavePSD.TabIndex = 124
        Me.ckSavePSD.Text = "Also save a layered .PSD version"
        Me.ckSavePSD.UseVisualStyleBackColor = True
        '
        'Form5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 519)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbIncomingHlp)
        Me.Controls.Add(Me.grpOperatorMd)
        Me.Controls.Add(Me.VersionBox)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.OKay)
        Me.Controls.Add(Me.ExpertButton)
        Me.Controls.Add(Me.lblFilterSelection)
        Me.Controls.Add(Me.FilterDescription)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.gbFilter1)
        Me.Controls.Add(Me.ckSavePSD)
        Me.Controls.Add(Me.GroupBoxBKFG)
        Me.Controls.Add(Me.Printer1Info)
        Me.Controls.Add(Me.gbDateQuals)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Name = "Form5"
        Me.Text = "Easy Mode Config Panel"
        Me.gbDateQuals.ResumeLayout(False)
        Me.gbDateQuals.PerformLayout()
        Me.Printer1Info.ResumeLayout(False)
        Me.Printer1Info.PerformLayout()
        Me.GroupBoxBKFG.ResumeLayout(False)
        Me.GroupBoxBKFG.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.gbFilter1.ResumeLayout(False)
        Me.grpOperatorMd.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents gbDateQuals As System.Windows.Forms.GroupBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cbPrintNoDates As System.Windows.Forms.CheckBox
    Friend WithEvents lblImageNewer As System.Windows.Forms.Label
    Friend WithEvents cbDateQualified As System.Windows.Forms.CheckBox
    Friend WithEvents dtEarliestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblAutoPrintCount As System.Windows.Forms.Label
    Friend WithEvents txtAutoPrintCnt As System.Windows.Forms.TextBox
    Friend WithEvents Printer1Info As System.Windows.Forms.GroupBox
    Friend WithEvents Printer1LB As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBoxBKFG As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBoxBKFG As System.Windows.Forms.ComboBox
    Friend WithEvents ckSavePSD As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilterSelection As System.Windows.Forms.Label
    Friend WithEvents FilterDescription As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbFilter3 As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbFilter2 As System.Windows.Forms.ComboBox
    Friend WithEvents gbFilter1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbFilter1 As System.Windows.Forms.ComboBox
    Friend WithEvents ExpertButton As System.Windows.Forms.Button
    Friend WithEvents OKay As System.Windows.Forms.Button
    Friend WithEvents Printer1PaperCount As System.Windows.Forms.TextBox
    Friend WithEvents VersionBox As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblPprCnt As System.Windows.Forms.Label
    Friend WithEvents lblPrtrHlp As System.Windows.Forms.Label
    Friend WithEvents grpOperatorMd As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbIncomingHlp As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
