﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("c:\onsite\capture\")>  _
        Public Property SourcePath() As String
            Get
                Return CType(Me("SourcePath"),String)
            End Get
            Set
                Me("SourcePath") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("c:\onsite\")>  _
        Public Property DestinationPath1() As String
            Get
                Return CType(Me("DestinationPath1"),String)
            End Get
            Set
                Me("DestinationPath1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DestinationPath2() As String
            Get
                Return CType(Me("DestinationPath2"),String)
            End Get
            Set
                Me("DestinationPath2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property Thumbnail_Location() As Global.System.Drawing.Point
            Get
                Return CType(Me("Thumbnail_Location"),Global.System.Drawing.Point)
            End Get
            Set
                Me("Thumbnail_Location") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property Cfg_Location() As Global.System.Drawing.Point
            Get
                Return CType(Me("Cfg_Location"),Global.System.Drawing.Point)
            End Get
            Set
                Me("Cfg_Location") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("632, 446")>  _
        Public Property img_size() As Global.System.Drawing.Size
            Get
                Return CType(Me("img_size"),Global.System.Drawing.Size)
            End Get
            Set
                Me("img_size") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property Pic2Print_Loc() As Global.System.Drawing.Point
            Get
                Return CType(Me("Pic2Print_Loc"),Global.System.Drawing.Point)
            End Get
            Set
                Me("Pic2Print_Loc") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property CheckBoxSave() As Boolean
            Get
                Return CType(Me("CheckBoxSave"),Boolean)
            End Get
            Set
                Me("CheckBoxSave") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property RadioBtn() As Boolean
            Get
                Return CType(Me("RadioBtn"),Boolean)
            End Get
            Set
                Me("RadioBtn") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property Print1PrintTime() As String
            Get
                Return CType(Me("Print1PrintTime"),String)
            End Get
            Set
                Me("Print1PrintTime") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Print1Sheets() As String
            Get
                Return CType(Me("Print1Sheets"),String)
            End Get
            Set
                Me("Print1Sheets") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property Print2PrintTime() As String
            Get
                Return CType(Me("Print2PrintTime"),String)
            End Get
            Set
                Me("Print2PrintTime") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Print2Sheets() As String
            Get
                Return CType(Me("Print2Sheets"),String)
            End Get
            Set
                Me("Print2Sheets") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Print2EnabledBool() As Boolean
            Get
                Return CType(Me("Print2EnabledBool"),Boolean)
            End Get
            Set
                Me("Print2EnabledBool") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property LoadBalanceEnabled() As Boolean
            Get
                Return CType(Me("LoadBalanceEnabled"),Boolean)
            End Get
            Set
                Me("LoadBalanceEnabled") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property LoadBalanceMaxP() As Boolean
            Get
                Return CType(Me("LoadBalanceMaxP"),Boolean)
            End Get
            Set
                Me("LoadBalanceMaxP") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Print1DownCount() As String
            Get
                Return CType(Me("Print1DownCount"),String)
            End Get
            Set
                Me("Print1DownCount") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Print2DownCount() As String
            Get
                Return CType(Me("Print2DownCount"),String)
            End Get
            Set
                Me("Print2DownCount") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property PSEnableOverlay() As Boolean
            Get
                Return CType(Me("PSEnableOverlay"),Boolean)
            End Get
            Set
                Me("PSEnableOverlay") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnaLoadBalancing() As Boolean
            Get
                Return CType(Me("EnaLoadBalancing"),Boolean)
            End Get
            Set
                Me("EnaLoadBalancing") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property MaxPerf() As Boolean
            Get
                Return CType(Me("MaxPerf"),Boolean)
            End Get
            Set
                Me("MaxPerf") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnaGreenScreen() As Boolean
            Get
                Return CType(Me("EnaGreenScreen"),Boolean)
            End Get
            Set
                Me("EnaGreenScreen") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnaMultBkgnds() As Boolean
            Get
                Return CType(Me("EnaMultBkgnds"),Boolean)
            End Get
            Set
                Me("EnaMultBkgnds") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Prt1Profile() As String
            Get
                Return CType(Me("Prt1Profile"),String)
            End Get
            Set
                Me("Prt1Profile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Prt2Profile() As String
            Get
                Return CType(Me("Prt2Profile"),String)
            End Get
            Set
                Me("Prt2Profile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property P1InfoDS40() As Boolean
            Get
                Return CType(Me("P1InfoDS40"),Boolean)
            End Get
            Set
                Me("P1InfoDS40") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P1InfoSony() As Boolean
            Get
                Return CType(Me("P1InfoSony"),Boolean)
            End Get
            Set
                Me("P1InfoSony") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property P1Siz4x6() As Boolean
            Get
                Return CType(Me("P1Siz4x6"),Boolean)
            End Get
            Set
                Me("P1Siz4x6") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P1Siz5x7() As Boolean
            Get
                Return CType(Me("P1Siz5x7"),Boolean)
            End Get
            Set
                Me("P1Siz5x7") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P1Siz8x10() As Boolean
            Get
                Return CType(Me("P1Siz8x10"),Boolean)
            End Get
            Set
                Me("P1Siz8x10") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property P2InfoDS40() As Boolean
            Get
                Return CType(Me("P2InfoDS40"),Boolean)
            End Get
            Set
                Me("P2InfoDS40") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P2InfoSony() As Boolean
            Get
                Return CType(Me("P2InfoSony"),Boolean)
            End Get
            Set
                Me("P2InfoSony") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property P2Siz4x6() As Boolean
            Get
                Return CType(Me("P2Siz4x6"),Boolean)
            End Get
            Set
                Me("P2Siz4x6") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P2Siz5x7() As Boolean
            Get
                Return CType(Me("P2Siz5x7"),Boolean)
            End Get
            Set
                Me("P2Siz5x7") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property P2Siz8x10() As Boolean
            Get
                Return CType(Me("P2Siz8x10"),Boolean)
            End Get
            Set
                Me("P2Siz8x10") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SyncFolderEna() As Boolean
            Get
                Return CType(Me("SyncFolderEna"),Boolean)
            End Get
            Set
                Me("SyncFolderEna") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SyncFolderPth() As String
            Get
                Return CType(Me("SyncFolderPth"),String)
            End Get
            Set
                Me("SyncFolderPth") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtEmailServerURL() As String
            Get
                Return CType(Me("txtEmailServerURL"),String)
            End Get
            Set
                Me("txtEmailServerURL") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtEmailServerPort() As String
            Get
                Return CType(Me("txtEmailServerPort"),String)
            End Get
            Set
                Me("txtEmailServerPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtEmailAcctName() As String
            Get
                Return CType(Me("txtEmailAcctName"),String)
            End Get
            Set
                Me("txtEmailAcctName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtEmailPasword() As String
            Get
                Return CType(Me("txtEmailPasword"),String)
            End Get
            Set
                Me("txtEmailPasword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property TxtEmailSenderAddr() As String
            Get
                Return CType(Me("TxtEmailSenderAddr"),String)
            End Get
            Set
                Me("TxtEmailSenderAddr") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtEmailRcvAddr() As String
            Get
                Return CType(Me("txtEmailRcvAddr"),String)
            End Get
            Set
                Me("txtEmailRcvAddr") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property chkEmailLog() As Boolean
            Get
                Return CType(Me("chkEmailLog"),Boolean)
            End Get
            Set
                Me("chkEmailLog") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property txtSubjectMsg() As String
            Get
                Return CType(Me("txtSubjectMsg"),String)
            End Get
            Set
                Me("txtSubjectMsg") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property NoPrintCheck() As Boolean
            Get
                Return CType(Me("NoPrintCheck"),Boolean)
            End Get
            Set
                Me("NoPrintCheck") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property Prev_Location() As Global.System.Drawing.Point
            Get
                Return CType(Me("Prev_Location"),Global.System.Drawing.Point)
            End Get
            Set
                Me("Prev_Location") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Printer1Select() As String
            Get
                Return CType(Me("Printer1Select"),String)
            End Get
            Set
                Me("Printer1Select") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Printer2Select() As String
            Get
                Return CType(Me("Printer2Select"),String)
            End Get
            Set
                Me("Printer2Select") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DebugLog() As Boolean
            Get
                Return CType(Me("DebugLog"),Boolean)
            End Get
            Set
                Me("DebugLog") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnableEmailLog() As Boolean
            Get
                Return CType(Me("EnableEmailLog"),Boolean)
            End Get
            Set
                Me("EnableEmailLog") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("4")>  _
        Public Property LayersPerOutput() As String
            Get
                Return CType(Me("LayersPerOutput"),String)
            End Get
            Set
                Me("LayersPerOutput") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property LayersPerCustom() As String
            Get
                Return CType(Me("LayersPerCustom"),String)
            End Get
            Set
                Me("LayersPerCustom") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property bkFgSelection() As String
            Get
                Return CType(Me("bkFgSelection"),String)
            End Get
            Set
                Me("bkFgSelection") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property txtAutoPrtCnt() As String
            Get
                Return CType(Me("txtAutoPrtCnt"),String)
            End Get
            Set
                Me("txtAutoPrtCnt") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property resBkFgAnimated() As Boolean
            Get
                Return CType(Me("resBkFgAnimated"),Boolean)
            End Get
            Set
                Me("resBkFgAnimated") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ckSavePSDFile() As Boolean
            Get
                Return CType(Me("ckSavePSDFile"),Boolean)
            End Get
            Set
                Me("ckSavePSDFile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("16")>  _
        Public Property txtRGBValue() As String
            Get
                Return CType(Me("txtRGBValue"),String)
            End Get
            Set
                Me("txtRGBValue") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property txtFontIndex() As String
            Get
                Return CType(Me("txtFontIndex"),String)
            End Get
            Set
                Me("txtFontIndex") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Printer1RadioSelect() As Boolean
            Get
                Return CType(Me("Printer1RadioSelect"),Boolean)
            End Get
            Set
                Me("Printer1RadioSelect") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("001")>  _
        Public Property MachineName() As String
            Get
                Return CType(Me("MachineName"),String)
            End Get
            Set
                Me("MachineName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property resGifDelay() As Boolean
            Get
                Return CType(Me("resGifDelay"),Boolean)
            End Get
            Set
                Me("resGifDelay") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoFollowRight() As Boolean
            Get
                Return CType(Me("AutoFollowRight"),Boolean)
            End Get
            Set
                Me("AutoFollowRight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property KIOSKdateQual() As Boolean
            Get
                Return CType(Me("KIOSKdateQual"),Boolean)
            End Get
            Set
                Me("KIOSKdateQual") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("01/08/2015 22:46:18")>  _
        Public Property dtEarliestDateSelected() As Date
            Get
                Return CType(Me("dtEarliestDateSelected"),Date)
            End Get
            Set
                Me("dtEarliestDateSelected") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property KioskPrintAnyway() As Boolean
            Get
                Return CType(Me("KioskPrintAnyway"),Boolean)
            End Get
            Set
                Me("KioskPrintAnyway") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property txtboxFilter1() As String
            Get
                Return CType(Me("txtboxFilter1"),String)
            End Get
            Set
                Me("txtboxFilter1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property txtboxFilter2() As String
            Get
                Return CType(Me("txtboxFilter2"),String)
            End Get
            Set
                Me("txtboxFilter2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property txtboxFilter3() As String
            Get
                Return CType(Me("txtboxFilter3"),String)
            End Get
            Set
                Me("txtboxFilter3") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property syncPostPathvalue() As String
            Get
                Return CType(Me("syncPostPathvalue"),String)
            End Get
            Set
                Me("syncPostPathvalue") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SyncFolderPth2() As String
            Get
                Return CType(Me("SyncFolderPth2"),String)
            End Get
            Set
                Me("SyncFolderPth2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property pvAutoScroll() As Boolean
            Get
                Return CType(Me("pvAutoScroll"),Boolean)
            End Get
            Set
                Me("pvAutoScroll") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property cbAutoViewTracking() As Boolean
            Get
                Return CType(Me("cbAutoViewTracking"),Boolean)
            End Get
            Set
                Me("cbAutoViewTracking") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.WindowsApplication1.My.MySettings
            Get
                Return Global.WindowsApplication1.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
