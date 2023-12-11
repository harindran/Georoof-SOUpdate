Namespace SOUpdate

    Public Class clsRightClickEvent
        Dim objform As SAPbouiCOM.Form
        Dim objglobalmethods As New clsGlobalMethods
        Dim ocombo As SAPbouiCOM.ComboBox
        Dim objmatrix As SAPbouiCOM.Matrix
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset

        Public Sub RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Select Case objaddon.objapplication.Forms.ActiveForm.TypeEx
                    Case "65211"
                        'ProductionOrder_RightClickEvent(eventInfo, BubbleEvent)

                End Select
            Catch ex As Exception
            End Try
        End Sub

        Private Sub RightClickMenu_Add(ByVal MainMenu As String, ByVal NewMenuID As String, ByVal NewMenuName As String, ByVal position As Integer)
            Dim omenus As SAPbouiCOM.Menus
            Dim omenuitem As SAPbouiCOM.MenuItem
            Dim oCreationPackage As SAPbouiCOM.MenuCreationParams
            oCreationPackage = objaddon.objapplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            omenuitem = objaddon.objapplication.Menus.Item(MainMenu) 'Data'
            If Not omenuitem.SubMenus.Exists(NewMenuID) Then
                oCreationPackage.UniqueID = NewMenuID
                oCreationPackage.String = NewMenuName
                oCreationPackage.Position = position
                oCreationPackage.Enabled = True
                omenus = omenuitem.SubMenus
                omenus.AddEx(oCreationPackage)
            End If
        End Sub

        Private Sub RightClickMenu_Delete(ByVal MainMenu As String, ByVal NewMenuID As String)
            Dim omenuitem As SAPbouiCOM.MenuItem
            omenuitem = objaddon.objapplication.Menus.Item(MainMenu) 'Data'
            If omenuitem.SubMenus.Exists(NewMenuID) Then
                objaddon.objapplication.Menus.RemoveEx(NewMenuID)
            End If
        End Sub

        Private Sub ProductionOrder_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                'If eventInfo.BeforeAction Then
                '    If eventInfo.ItemUID <> "" Then
                '        Try
                '            objmatrix = objform.Items.Item(eventInfo.ItemUID).Specific
                '            If objmatrix.Item.Type = SAPbouiCOM.BoFormItemTypes.it_MATRIX Then
                '                If objmatrix.Columns.Item(eventInfo.ColUID).Cells.Item(eventInfo.Row).Specific.String <> "" Then
                '                    objform.EnableMenu("772", True)  'Copy
                '                Else
                '                    objform.EnableMenu("772", False)
                '                End If
                '            End If
                '        Catch ex As Exception
                '            If objform.Items.Item(eventInfo.ItemUID).Specific.String <> "" Then
                '                objform.EnableMenu("772", True)  'Copy
                '            Else
                '                objform.EnableMenu("772", False)
                '            End If
                '        End Try
                '    Else
                '        objform.EnableMenu("772", False)
                '        objform.EnableMenu("1283", False)

                '    End If


                '    objform.EnableMenu("1287", False)
                '    'If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                '    '    objform.EnableMenu("1287", True)  'Duplicate
                '    'Else
                '    '    objform.EnableMenu("1287", False)
                '    'End If
                'Else
                '    objform.EnableMenu("1283", False)
                '    objform.EnableMenu("784", False)
                'End If
            Catch ex As Exception
            End Try

        End Sub

    End Class

End Namespace
