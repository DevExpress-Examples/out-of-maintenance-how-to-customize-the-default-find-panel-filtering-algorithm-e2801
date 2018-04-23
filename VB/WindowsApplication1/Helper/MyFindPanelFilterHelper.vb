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
	Public Class MyFindPanelFilterHelper

		Public Sub New(ByVal view As GridView)
			_View = view
			AddHandler view.CustomRowFilter, AddressOf view_CustomRowFilter
		End Sub

		Private lastCriteria As String
		Private lastEvaluator As ExpressionEvaluator
		Private _View As GridView
		Private Function GetExpressionEvaluator(ByVal criteria As CriteriaOperator) As ExpressionEvaluator
			If criteria.ToString() = lastCriteria Then
				Return lastEvaluator
			End If
			lastCriteria = criteria.ToString()
			Dim pdc As PropertyDescriptorCollection = (CType(_View.DataSource, ITypedList)).GetItemProperties(Nothing)
			lastEvaluator = New ExpressionEvaluator(pdc, criteria, False)
			Return lastEvaluator
		End Function

		Private Function GetFindPanelCriteria() As CriteriaOperator
			Dim criteria As CriteriaOperator = FilterCriteriaHelper.MyConvertFindPanelTextToCriteriaOperator(_View)
			Return criteria
		End Function
		Private Sub view_CustomRowFilter(ByVal sender As Object, ByVal e As RowFilterEventArgs)
			If String.IsNullOrEmpty(_View.FindFilterText) Then
				Return
			End If
			Dim criteria As CriteriaOperator = FilterCriteriaHelper.ReplaceFindPanelCriteria(_View.DataController.FilterCriteria, _View, GetFindPanelCriteria())
			Dim evaluator As ExpressionEvaluator = GetExpressionEvaluator(criteria)
			e.Handled = True
			e.Visible = evaluator.Fit((TryCast(_View.DataSource, DataView))(e.ListSourceRow))
		End Sub
	End Class
End Namespace
