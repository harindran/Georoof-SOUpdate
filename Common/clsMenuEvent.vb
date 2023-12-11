Imports SAPbouiCOM
Namespace SOUpdate

    Public Class clsMenuEvent
        Dim objform As SAPbouiCOM.Form
        Dim objglobalmethods As New clsGlobalMethods
        Public SOEntry As String
        Public Sub MenuEvent_For_StandardMenu(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean)
            Try
                Select Case objaddon.objapplication.Forms.ActiveForm.TypeEx

                    Case "65211"
                        Default_Sample_MenuEvent(pVal, BubbleEvent)

                End Select
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Default_Sample_MenuEvent(ByVal pval As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean)
            Try
                objform = objaddon.objapplication.Forms.ActiveForm

                Dim oUDFForm As SAPbouiCOM.Form
                If pval.BeforeAction = True Then
                    Select Case pval.MenuUID
                        Case "6005"
                        Case "1284" 'Cancel
                            If objform.TypeEx = "65211" Then
                                SOEntry = objform.DataSources.DBDataSources.Item("OWOR").GetValue("OriginAbs", 0)
                            End If

                    End Select
                Else

                    oUDFForm = objaddon.objapplication.Forms.Item(objform.UDFFormUID)
                    Select Case pval.MenuUID
                        Case "1284" 'Cancel
                            If objform.TypeEx = "65211" Then
                                If TempForm Then

                                    If SOEntry = "" Then Exit Sub
                                    If objaddon.objProdOrder.UpdateSalesOrder(SOEntry, "Prod. Order Cancelled") Then
                                        TempForm = False
                                        SOEntry = ""
                                    End If
                                End If
                            End If


                        Case Else

                    End Select
                End If
            Catch ex As Exception
                'objaddon.objapplication.SetStatusBarMessage("Error in Standart Menu Event" + ex.ToString, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub



        Sub DeleteRow(ByVal objMatrix As SAPbouiCOM.Matrix, ByVal TableName As String)
            Try
                Dim DBSource As SAPbouiCOM.DBDataSource
                'objMatrix = objform.Items.Item("20").Specific
                objMatrix.FlushToDataSource()
                DBSource = objform.DataSources.DBDataSources.Item(TableName) '"@MIREJDET1"
                For i As Integer = 1 To objMatrix.VisualRowCount
                    objMatrix.GetLineData(i)
                    DBSource.Offset = i - 1
                    DBSource.SetValue("LineId", DBSource.Offset, i)
                    objMatrix.SetLineData(i)
                    objMatrix.FlushToDataSource()
                Next
                DBSource.RemoveRecord(DBSource.Size - 1)
                objMatrix.LoadFromDataSource()

            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText("DeleteRow  Method Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_None)
            Finally
            End Try
        End Sub
    End Class
End Namespace