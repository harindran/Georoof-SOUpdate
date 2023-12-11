'Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports SAPbobsCOM
Imports SAPbouiCOM.Framework
Namespace SOUpdate
    Public Class ClsProdOrder
        Public Const Formtype = "65211"
        Dim objform As SAPbouiCOM.Form
        Dim objmatrix As SAPbouiCOM.Matrix
        Dim strSQL As String
        Dim objRs As SAPbobsCOM.Recordset

        Public Sub ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean)
            Try
                objform = objaddon.objapplication.Forms.Item(FormUID)
                objmatrix = objform.Items.Item("37").Specific
                Dim GetValue As String
                Dim UDFForm As SAPbouiCOM.Form
                objRs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                If pVal.BeforeAction Then
                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_CLICK
                            If pVal.ItemUID = "1" And objform.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then
                                GetValue = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                                If GetValue = "" Then Exit Sub
                                strSQL = "Select T0.""U_ClubNo"",T0.""U_SO_MachineAllocated"",T0.""U_SubTable"",T0.""U_Sequence"",T1.""U_AltQty3"",T1.""U_altqty"" from ORDR T0 left join RDR1 T1 on T0.""DocEntry"" =T1.""DocEntry"" where T0.""DocEntry""=" & GetValue & ""
                                objRs.DoQuery(strSQL)
                                'GetValue = objaddon.objglobalmethods.getSingleValue("Select ""U_ClubNo"" from ORDR where ""DocEntry""='" & objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0) & "' ")

                                If objaddon.objapplication.Menus.Item("6913").Checked = False Then
                                    objaddon.objapplication.SendKeys("^+U")
                                End If
                                If objRs.RecordCount > 0 Then
                                    UDFForm = objaddon.objapplication.Forms.Item(objform.UDFFormUID)

                                    If CInt(UDFForm.Items.Item("U_clubbing").Specific.String) <> CInt(objRs.Fields.Item("U_ClubNo").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Clubbing Number is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Clubbing Number is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                    If Trim(UDFForm.Items.Item("U_machname").Specific.String) <> Trim(objRs.Fields.Item("U_SO_MachineAllocated").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Machine Name is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Machine Name is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                    If CInt(UDFForm.Items.Item("U_SubTable").Specific.String) <> CInt(objRs.Fields.Item("U_SubTable").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Sub Table is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Sub Table is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                    If Trim(UDFForm.Items.Item("U_Sequence").Specific.String) <> Trim(objRs.Fields.Item("U_Sequence").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Sequence No. is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Sequence No. is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                    If CDbl(UDFForm.Items.Item("U_AltQty").Specific.String) <> CDbl(objRs.Fields.Item("U_AltQty3").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Alt Qty (KGS) is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Alt Qty (KGS) is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                    If CDbl(UDFForm.Items.Item("U_AltQty1").Specific.String) <> CDbl(objRs.Fields.Item("U_altqty").Value.ToString) Then
                                        objaddon.objapplication.MessageBox("Alt Qty (NOS) is Mismatching with the Sales Order and Production Order.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Alt Qty (NOS) is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                        BubbleEvent = False : Exit Sub
                                    End If
                                End If
                                'UDFForm = objaddon.objapplication.Forms.Item(objform.UDFFormUID)
                                'If GetValue <> "" Then
                                '    If GetValue <> UDFForm.Items.Item("U_clubbing").Specific.String Then
                                '        objaddon.objapplication.MessageBox("Clubbing Number is Mismatching with the Sales Order and Production Order.", , "OK")
                                '        objaddon.objapplication.StatusBar.SetText("Clubbing Number is Mismatching with the Sales Order and Production Order.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                '        BubbleEvent = False : Exit Sub
                                '    End If
                                'End If
                            End If
                    End Select
                Else
                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_VALIDATE
                            If pVal.ItemUID = "32" And pVal.ActionSuccess = True And pVal.InnerEvent = False Then
                                Try
                                    GetValue = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                                    If GetValue = "" Then Exit Sub
                                    Dim objRecset As SAPbobsCOM.Recordset
                                    objRecset = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                                    strSQL = "Select T0.""U_ClubNo"",T0.""U_SO_MachineAllocated"",T0.""U_SubTable"",T0.""U_Sequence"",T1.""U_AltQty3"",T1.""U_altqty"" from ORDR T0 left join RDR1 T1 on T0.""DocEntry"" =T1.""DocEntry"" where T0.""DocEntry""=" & GetValue & ";"
                                    objRecset.DoQuery(strSQL)
                                    If objaddon.objapplication.Menus.Item("6913").Checked = False Then
                                        objaddon.objapplication.SendKeys("^+U")
                                    End If
                                    If objRecset.RecordCount > 0 Then
                                        objRecset.MoveFirst()
                                        UDFForm = objaddon.objapplication.Forms.Item(objform.UDFFormUID)
                                        If objRecset.Fields.Item("U_ClubNo").Value.ToString <> "" Then UDFForm.Items.Item("U_clubbing").Specific.String = objRecset.Fields.Item("U_ClubNo").Value
                                        If objRecset.Fields.Item("U_SO_MachineAllocated").Value.ToString <> "" Then UDFForm.Items.Item("U_machname").Specific.String = objRecset.Fields.Item("U_SO_MachineAllocated").Value.ToString
                                        If objRecset.Fields.Item("U_SubTable").Value.ToString <> "" Then UDFForm.Items.Item("U_SubTable").Specific.String = objRecset.Fields.Item("U_SubTable").Value.ToString
                                        If objRecset.Fields.Item("U_Sequence").Value.ToString <> "" Then UDFForm.Items.Item("U_Sequence").Specific.String = objRecset.Fields.Item("U_Sequence").Value.ToString
                                        If objRecset.Fields.Item("U_AltQty3").Value.ToString <> "" Then UDFForm.Items.Item("U_AltQty").Specific.String = objRecset.Fields.Item("U_AltQty3").Value.ToString
                                        If objRecset.Fields.Item("U_altqty").Value.ToString <> "" Then UDFForm.Items.Item("U_AltQty1").Specific.String = objRecset.Fields.Item("U_altqty").Value.ToString
                                    End If
                                    objRecset = Nothing
                                    'GetValue = objaddon.objglobalmethods.getSingleValue("Select ""U_ClubNo"" from ORDR where ""DocEntry""='" & objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0) & "' ")

                                    'UDFForm = objaddon.objapplication.Forms.Item(objform.UDFFormUID)
                                    'If GetValue = UDFForm.Items.Item("U_clubbing").Specific.String Then Exit Sub
                                    'If GetValue <> "" Then UDFForm.Items.Item("U_clubbing").Specific.String = GetValue
                                Catch ex As Exception
                                    objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                End Try

                            End If
                    End Select

                End If
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Public Sub FormDataEvent(ByRef BusinessObjectInfo As SAPbouiCOM.BusinessObjectInfo, ByRef BubbleEvent As Boolean)
            Try
                objform = objaddon.objapplication.Forms.Item(BusinessObjectInfo.FormUID)
                objRs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                Dim SOEntry, SONum As String
                If BusinessObjectInfo.BeforeAction = True Then
                    Select Case BusinessObjectInfo.EventType


                    End Select
                Else
                    Select Case BusinessObjectInfo.EventType
                        Case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD
                            If BusinessObjectInfo.ActionSuccess = True And BusinessObjectInfo.BeforeAction = False Then
                                If objform.Items.Item("32").Specific.String = "" Then Exit Sub
                                SOEntry = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                                SONum = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginNum", 0)
                                If SOEntry = "" Then Exit Sub
                                If UpdateSalesOrder(SOEntry, "Production Order Created") Then
                                    objaddon.objapplication.MessageBox("Order Tracking field in the sales order """ & SONum & """ has been updated with the value of Production Order Created.", , "OK")
                                    objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """ has been updated with the value of Production Order Created.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                                Else
                                    objaddon.objapplication.MessageBox("Order Tracking field in the sales order " & SONum & " not updated. Please contact your administrator.", , "OK")
                                    objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """ not updated. Please contact your administrator.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                End If
                                'DocDate = objaddon.objglobalmethods.getSingleValue("Select To_Varchar(""DocDate"",'yyyyMMdd') as ""DocDate"" from ORDR where ""DocNum""='" & objform.Items.Item("32").Specific.String & "'")
                                'If GetSalesOrder(objform.Items.Item("32").Specific.String, DocDate, "Production Order Created") Then
                                '    objaddon.objapplication.StatusBar.SetText("Sales Order Updated Successfully...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                                'Else
                                '    objaddon.objapplication.StatusBar.SetText("Error Occurred while updating Sales Order...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                'End If
                            End If
                        Case SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE
                            If BusinessObjectInfo.ActionSuccess = True And BusinessObjectInfo.BeforeAction = False Then
                                If objform.Items.Item("32").Specific.String = "" Then Exit Sub
                                If objform.Items.Item("10").Specific.Selected.Value = "R" Then
                                    SOEntry = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                                    SONum = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginNum", 0)
                                    If SOEntry = "" Then Exit Sub
                                    Dim Status As String = objaddon.objglobalmethods.getSingleValue("select 1 as ""Status"" from ORDR where ""DocEntry""='" & SOEntry & "' and ""U_OrderTracking""='Prod. Order Released'")
                                    If Status = "1" Then Exit Sub
                                    If UpdateSalesOrder(SOEntry, "Prod. Order Released") Then
                                        objaddon.objapplication.MessageBox("Order Tracking field in the sales order """ & SONum & """  has been updated with the value of Production Order Released.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """  has been updated with the value of Production Order Released.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                                    Else
                                        objaddon.objapplication.MessageBox("Order Tracking field in the sales order """ & SONum & """ not updated. Please contact your administrator.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """ not updated. Please contact your administrator.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                    End If
                                    'DocDate = objaddon.objglobalmethods.getSingleValue("select To_Varchar(""DocDate"",'yyyyMMdd') as ""DocDate"" from ORDR where ""DocNum""='" & objform.Items.Item("32").Specific.String & "'")

                                    'If GetSalesOrder(objform.Items.Item("32").Specific.String, DocDate, "Prod. Order Released") Then
                                    '    objaddon.objapplication.StatusBar.SetText("Sales Order Updated Successfully...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                                    'Else
                                    '    objaddon.objapplication.StatusBar.SetText("Error Occurred while updating Sales Order...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)

                                    'End If
                                ElseIf objform.Items.Item("10").Specific.Selected.Value = "L" Then
                                    SOEntry = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                                    SONum = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginNum", 0)
                                    If SOEntry = "" Then Exit Sub
                                    Dim Status As String = objaddon.objglobalmethods.getSingleValue("select 1 as ""Status"" from ORDR where ""DocEntry""='" & SOEntry & "' and ""U_OrderTracking""='Prod. Order Closed'")
                                    If Status = "1" Then Exit Sub
                                    If UpdateSalesOrder(SOEntry, "Prod. Order Closed") Then
                                        objaddon.objapplication.MessageBox("Order Tracking field in the sales order """ & SONum & """  has been updated with the value of Production Order Closed.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """  has been updated with the value of Production Order Closed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                                    Else
                                        objaddon.objapplication.MessageBox("Order Tracking field in the sales order """ & SONum & """ not updated. Please contact your administrator.", , "OK")
                                        objaddon.objapplication.StatusBar.SetText("Order Tracking field in the sales order """ & SONum & """ not updated. Please contact your administrator.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                    End If
                                End If
                            End If
                        Case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD

                    End Select
                End If

            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            End Try

        End Sub

        Private Function GetSalesOrder(ByVal DocNum As String, ByVal SODate As String, ByVal FieldValue As String) As Boolean
            Dim oForm, oUDFForm As SAPbouiCOM.Form
            Try
                objaddon.objapplication.Menus.Item("2050").Activate()  'AR Invoice
                oForm = objaddon.objapplication.Forms.ActiveForm
                oForm.Resize(10, 10)
                oForm.Top = objform.Top
                oForm.Left = objform.Left
                oForm.MaxHeight = 10
                oForm.MaxWidth = 10
                oForm.ClientHeight = 10
                oForm.ClientWidth = 10
                oForm.Width = 10
                oForm.Height = 10
                oForm.Freeze(True)
                oForm.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                oForm.Items.Item("8").Specific.String = DocNum ' Matrix1.Columns.Item("originno").Cells.Item(pVal.Row).Specific.String
                oForm.Items.Item("10").Specific.String = SODate 'Matrix1.Columns.Item("date").Cells.Item(pVal.Row).Specific.String
                oForm.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                If objaddon.objapplication.Menus.Item("6913").Checked = False Then
                    objaddon.objapplication.SendKeys("^+U")
                End If
                oUDFForm = objaddon.objapplication.Forms.Item(oForm.UDFFormUID)
                oUDFForm.Items.Item("U_OrderTracking").Specific.Select(FieldValue, SAPbouiCOM.BoSearchKey.psk_ByValue)
                If oForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then oForm.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                oForm.Freeze(False)
                oForm.Close()
                Return True
            Catch ex As Exception
                oForm.Freeze(False)
                oForm.Close()
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                Return False
            End Try
        End Function

        Private Function UpdateSalesOrder_Old(ByVal DocEntry As String, ByVal FieldValue As String) As Boolean
            Try
                Dim objSalesOrder As SAPbobsCOM.Documents
                Dim Retval As Integer
                objSalesOrder = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)

                If objSalesOrder.GetByKey(DocEntry) Then
                    objSalesOrder.UserFields.Fields.Item("U_OrderTracking").Value = FieldValue
                End If

                Retval = objSalesOrder.Update()
                If Retval <> 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                Return False
            End Try
        End Function

        Public Function UpdateSalesOrder(ByVal DocEntry As String, ByVal FieldValue As String) As Boolean
            Dim objSalesOrder As SAPbobsCOM.Documents
            Try
                If DocEntry = "" Then Return False
                Dim Retval As Integer
                If IndSapUser = "" Or IndSapPass = "" Then
                    If objaddon.HANA Then
                        IndSapUser = objaddon.objglobalmethods.getSingleValue("select ""U_IndSAPUser"" from OUSR where ""USER_CODE""='" & objaddon.objcompany.UserName & "' and ifnull(""U_IndSAPUser"",'')<>''")
                        IndSapPass = objaddon.objglobalmethods.getSingleValue("select ""U_IndSAPPass"" from OUSR where ""USER_CODE""='" & objaddon.objcompany.UserName & "' and ifnull(""U_IndSAPPass"",'')<>''")
                    Else
                        IndSapUser = objaddon.objglobalmethods.getSingleValue("select U_IndSAPUser from OUSR where USER_CODE='" & objaddon.objcompany.UserName & "'")
                        IndSapPass = objaddon.objglobalmethods.getSingleValue("select U_IndSAPPass from OUSR where USER_CODE='" & objaddon.objcompany.UserName & "'")
                    End If
                End If

                oCompany = ConnectToCompany(IndSapUser, IndSapPass)
                If oCompany Is Nothing Then Return False
                objSalesOrder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)

                If objSalesOrder.GetByKey(DocEntry) Then
                    objSalesOrder.UserFields.Fields.Item("U_OrderTracking").Value = FieldValue
                End If

                Retval = objSalesOrder.Update()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objSalesOrder)
                If Retval <> 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                Return False
            Finally
                'oCompany = Nothing
                GC.Collect()
            End Try
        End Function

        Public Function ConnectToCompany(ByVal ISAPUser As String, ByVal ISAPPass As String) As SAPbobsCOM.Company
            Dim lRetCode As Integer, lErrCode As Integer
            Dim sErrMsg As String = ""

            Dim objcompanynew As SAPbobsCOM.Company
            Try
                If ISAPUser = "" Or ISAPPass = "" Then Return Nothing

                If Not oCompany Is Nothing Then
                    If oCompany.Connected = True Then
                        Return oCompany
                    End If
                End If
                objaddon.objapplication.SetStatusBarMessage("Connecting to Indirect SAP B1 User. Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                objcompanynew = New SAPbobsCOM.Company
                objcompanynew.Server = "GEO@gcsapserver:30013" ' Getvalue_webconfig("SAPServername")
                objcompanynew.LicenseServer = "gcsapserver:40000" 'Getvalue_webconfig("SAPLicenseName")
                objcompanynew.SLDServer = "gcsapserver:40000" ' Getvalue_webconfig("SLDSERVER")
                objcompanynew.language = SAPbobsCOM.BoSuppLangs.ln_English
                objcompanynew.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
                objcompanynew.DbUserName = "SYSTEM" 'Getvalue_webconfig("DBUserName")
                objcompanynew.DbPassword = "Indiaf$eedom@!947" ' Getvalue_webconfig("DBPassword")
                objcompanynew.CompanyDB = objaddon.objcompany.CompanyDB '"GEORGE_LIVE" '"POS_WEB_DB" ' Getvalue_webconfig("Database")
                objcompanynew.UserName = ISAPUser ' "PRODUCTION ADDON" 'Getvalue_webconfig("SAPUsername")
                objcompanynew.Password = ISAPPass ' "Mipl@1234" ' Getvalue_webconfig("SAPPassword")
                'objcompanynew.UseTrusted = False

                'objcompanynew.Server = objaddon.objcompany.Server  'Trim(objRecordset.Fields.Item("U_Server").Value)      '"192.168.168.244:30015"
                'objcompanynew.LicenseServer = objaddon.objcompany.LicenseServer  'Trim(objRecordset.Fields.Item("U_LicServer").Value)                '"https://HANADEV:40000"
                'objcompanynew.SLDServer = objaddon.objcompany.SLDServer   'Trim(objRecordset.Fields.Item("LSRV").Value)
                'objcompanynew.language = SAPbobsCOM.BoSuppLangs.ln_English
                'objcompanynew.UseTrusted = False
                'objcompanynew.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
                'objcompanynew.DbUserName = "OECDBBR"  '"SYSTEM" '
                'objcompanynew.DbPassword = "India@1947" '"Miplive2017" '
                'objcompanynew.CompanyDB = objaddon.objcompany.CompanyDB ' "OEC_TEST"        '"DB_SRMTEST" '
                'objcompanynew.UserName = ISAPUser '"tmicloud\chitra" 'objaddon.objcompany.UserName 'Trim(objRecordset.Fields.Item("U_UserName").Value)      '"nave\srmprof1" '  
                'objcompanynew.Password = ISAPPass ' "N%wt$n@19%6Nqw" 'Trim(objRecordset.Fields.Item("U_Password").Value)         '"Mukesh@2010@" '

                If objcompanynew.Connected = True Then
                    Return objcompanynew
                End If
                lRetCode = objcompanynew.Connect()
                If lRetCode <> 0 Then
                    objcompanynew.GetLastError(lErrCode, sErrMsg)
                    objaddon.objapplication.SetStatusBarMessage("Error in Connection:" & sErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    'MsgBox("Error in Connection: " & sErrMsg)
                Else
                    ' objAddOn.objApplication.SetStatusBarMessage("Connected to " & objRecordset.Fields.Item("Name").Value, SAPbouiCOM.BoMessageTime.bmt_Short, False)
                    objaddon.objapplication.SetStatusBarMessage("Connected to Indirect SAP B1 User...", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                    'oCompany = objcompanynew
                    Return objcompanynew
                End If

            Catch ex As Exception
                'MsgBox(ex.ToString)
                objaddon.objapplication.MessageBox(ex.Message, , "OK")
            End Try
            Return Nothing
        End Function

        Public Function Getvalue_webconfig(ByVal key As String) As String
            Try
                Dim strConnectionString As String = Configuration.ConfigurationManager.AppSettings(key)
                Return strConnectionString
            Catch ex As Exception
                'MsgBox(ex.ToString)
                Return ""
            End Try
        End Function
    End Class
End Namespace