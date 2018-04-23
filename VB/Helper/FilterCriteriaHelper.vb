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
Imports System.Collections

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

				Return DxFtsContainsHelperAlt.Create(parseResult, FilterCondition.Contains, False)
			End If
			Return Nothing
		End Function



		Private Shared Function GetFindToColumnsCollection(ByVal view As GridView) As ICollection
			Dim mi As System.Reflection.MethodInfo = GetType(ColumnView).GetMethod("GetFindToColumnsCollection", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
			Return TryCast(mi.Invoke(view, Nothing), ICollection)
		End Function

		Public Shared Function MyConvertFindPanelTextToCriteriaOperator(ByVal view As GridView) As CriteriaOperator
			Return ConvertFindPanelTextToCriteriaOperator(String.Format("""{0}""", view.FindFilterText), view, False)
		End Function
	End Class
End Namespace