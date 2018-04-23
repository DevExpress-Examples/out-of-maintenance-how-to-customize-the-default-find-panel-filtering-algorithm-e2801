Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Helpers
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data.Filtering.Helpers

Namespace WindowsApplication1
	Public NotInheritable Class FilterCriteriaHelper

		Private Sub New()
		End Sub
		Public Shared Function ReplaceFilterCriteria(ByVal source As CriteriaOperator, ByVal prevOperand As CriteriaOperator, ByVal newOperand As CriteriaOperator) As CriteriaOperator
			Dim groupOperand As GroupOperator = TryCast(source, GroupOperator)
			If ReferenceEquals(groupOperand, Nothing) Then
				Return newOperand
			End If
			Dim clone As GroupOperator = groupOperand.Clone()
			clone.Operands.Remove(prevOperand)
			If clone.Equals(source) Then
				Return newOperand
			End If
			clone.Operands.Add(newOperand)
			Return clone
		End Function

		Public Shared Function ReplaceFindPanelCriteria(ByVal source As CriteriaOperator, ByVal view As GridView, ByVal newOperand As CriteriaOperator) As CriteriaOperator
			Return ReplaceFilterCriteria(source, ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, True), newOperand)
		End Function

		Public Shared Function ConvertFindPanelTextToCriteriaOperator(ByVal findPanelText As String, ByVal view As GridView, ByVal applyPrefixes As Boolean) As CriteriaOperator
			If (Not String.IsNullOrEmpty(findPanelText)) Then
				Dim parseResult As FindSearchParserResults = New FindSearchParser().Parse(findPanelText, GetFindToColumnsCollection(view))
				If applyPrefixes Then
					parseResult.AppendColumnFieldPrefixes()
				End If
				Return DxFtsContainsHelper.Create(parseResult)
			End If
			Return Nothing
		End Function


		Private Shared Function GetFindToColumnsCollection(ByVal view As GridView) As List(Of IDataColumnInfo)
			Dim res As New List(Of IDataColumnInfo)()
			For Each column As GridColumn In view.VisibleColumns
				If IsAllowFindColumn(column) Then
					Dim dataColumn As DataColumnInfo = view.DataController.Columns(column.FieldName)
					res.Add(dataColumn)
				End If
			Next column
			For Each column As GridColumn In view.GroupedColumns
				Dim dataColumn As DataColumnInfo = view.DataController.Columns(column.FieldName)
				If res.Contains(dataColumn) OrElse (Not IsAllowFindColumn(column)) Then
					Continue For
				End If
				res.Add(dataColumn)
			Next column
			Return res
		End Function

		Friend Shared Function IsAllowFindColumn(ByVal col As GridColumn) As Boolean
			If col Is Nothing OrElse String.IsNullOrEmpty(col.FieldName) Then
				Return False
			End If
			If TypeOf col.ColumnEdit Is RepositoryItemPictureEdit OrElse TypeOf col.ColumnEdit Is RepositoryItemImageEdit Then
				Return False
			End If
			Dim view As GridView = TryCast(col.View, GridView)
			If view.OptionsFind.FindFilterColumns = "*" Then
				Return True
			End If
			Return String.Concat(";", view.OptionsFind.FindFilterColumns, ";").Contains(String.Concat(";", col.FieldName, ";"))
		End Function

		Public Shared Function MyConvertFindPanelTextToCriteriaOperator(ByVal view As GridView) As CriteriaOperator
			Return ConvertFindPanelTextToCriteriaOperator(String.Format("""{0}""", view.FindFilterText), view, False)
		End Function
	End Class
End Namespace
