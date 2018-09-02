Imports System
Imports System.IO
Imports System.Threading
Imports System.Drawing.Printing

Public Class Form5

    Dim print1CountUpdated As Boolean = False

    Private Sub Form5_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer

        ' if the user sent in a reset, then reposition on the desktop
        If Globals.cmdLineReset Then

            ' move the form.
            SetDesktopLocation(20, 20)

        End If

        ' makes the version string visible in the dialog box

        VersionBox.Text = Globals.Version

        ' pick up the selected printers, fonts, and layer set, from application storage
        'If prtrSelect1.Text = "" Then prtrSelect1.Text = "0"
        'If prtrSelect2.Text = "" Then prtrSelect2.Text = "0"
        'If tbBKFG.Text = "" Then tbBKFG.Text = "0"
        'If txtFontListIndex.Text = "" Then txtFontListIndex.Text = "0"
        'If tbFilter1.Text = "" Then tbFilter1.Text = "0"
        'If tbFilter2.Text = "" Then tbFilter2.Text = "0"
        'If tbFilter3.Text = "" Then tbFilter3.Text = "0"

        i = My.Settings.bkFgSelection
        If i >= ComboBoxBKFG.Items.Count Then i = 0
        ComboBoxBKFG.SelectedIndex = i

        i = My.Settings.Printer1Select
        If i >= Printer1LB.Items.Count Then i = 0
        Printer1LB.SelectedIndex = i

        'i = prtrSelect2.Text
        'If i >= Printer2LB.Items.Count Then i = 0
        'Printer2LB.SelectedIndex = i

        i = My.Settings.txtboxFilter1
        If i >= cbFilter1.Items.Count Then i = 0
        cbFilter1.SelectedIndex = i

        i = My.Settings.txtboxFilter2
        If i >= cbFilter2.Items.Count Then i = 0
        cbFilter2.SelectedIndex = i

        i = My.Settings.txtboxFilter3
        If i >= cbFilter3.Items.Count Then i = 0
        cbFilter3.SelectedIndex = i

        'Call CreateFamilyFontList()
        'i = txtFontListIndex.Text
        'If i >= cbFontList.Items.Count Then i = 0
        'cbFontList.SelectedIndex = i

        ' false until the user changes these fields
        print1CountUpdated = False
        'print2CountUpdated = False

        Globals.Form3Loading = False

    End Sub
    Private Sub OKay_Click(sender As System.Object, e As System.EventArgs) Handles OKay.Click

        If print1CountUpdated = True Then
            print1CountUpdated = False
            Globals.fPic2Print.PrintCount1.Text = Printer1PaperCount.Text
            Globals.Printer1Remaining = Printer1PaperCount.Text
        End If

        Call Globals.fPic2Print.CopyForm5ToForm3()
        Call Globals.fForm3.form3IsClosing()

        Call Globals.fForm3.IsConfigCorrect()

        Call Globals.fPic2Print.ShowConfigPanel(False)

    End Sub

    Private Sub ExpertButton_Click(sender As System.Object, e As System.EventArgs) Handles ExpertButton.Click
        ' go into expert mode
        Globals.fPic2Print.ToggleConfigPanel(True)
    End Sub

    Private Sub cbFilter1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter1.SelectedIndexChanged
        Globals.fForm3.cbFilter1.SelectedIndex = cbFilter1.SelectedIndex
        My.Settings.txtboxFilter1 = cbFilter1.SelectedIndex
    End Sub

    Private Sub cbFilter2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter2.SelectedIndexChanged
        Globals.fForm3.cbFilter2.SelectedIndex = cbFilter2.SelectedIndex
        My.Settings.txtboxFilter2 = cbFilter2.SelectedIndex
    End Sub

    Private Sub cbFilter3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter3.SelectedIndexChanged
        Globals.fForm3.cbFilter3.SelectedIndex = cbFilter3.SelectedIndex
        My.Settings.txtboxFilter3 = cbFilter3.SelectedIndex
    End Sub

    Private Sub Printer1LB_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Printer1LB.SelectedIndexChanged
        Globals.fForm3.Printer1LB.SelectedIndex = Printer1LB.SelectedIndex
        My.Settings.Printer1Select = Printer1LB.SelectedIndex
    End Sub

    Private Sub ComboBoxBKFG_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxBKFG.SelectedIndexChanged
        Globals.fForm3.ComboBoxBKFG.SelectedIndex = ComboBoxBKFG.SelectedIndex
        My.Settings.bkFgSelection = ComboBoxBKFG.SelectedIndex
    End Sub

    Private Sub Printer1PaperCount_TextChanged(sender As System.Object, e As System.EventArgs) Handles Printer1PaperCount.TextChanged
        print1CountUpdated = True
    End Sub

End Class