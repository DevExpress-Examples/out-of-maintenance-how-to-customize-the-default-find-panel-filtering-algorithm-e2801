using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Data.Filtering.Helpers;

namespace WindowsApplication1
{
    public static class FilterCriteriaHelper
    {

        public static CriteriaOperator ReplaceFilterCriteria(CriteriaOperator source, CriteriaOperator prevOperand, CriteriaOperator newOperand)
        {
            GroupOperator groupOperand = source as GroupOperator;
            if (ReferenceEquals(groupOperand, null))
                return newOperand;
            GroupOperator clone = groupOperand.Clone();
            clone.Operands.Remove(prevOperand);
            if (clone.Equals(source))
                return newOperand;
            clone.Operands.Add(newOperand);
            return clone;
        }

        public static CriteriaOperator ReplaceFindPanelCriteria(CriteriaOperator source, GridView view, CriteriaOperator newOperand)
        {
            return ReplaceFilterCriteria(source, ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, true), newOperand);
        }

        public static CriteriaOperator ConvertFindPanelTextToCriteriaOperator(string findPanelText, GridView view, bool applyPrefixes)
        {
            if (!string.IsNullOrEmpty(findPanelText))
            {
                FindSearchParserResults parseResult = new FindSearchParser().Parse(findPanelText, GetFindToColumnsCollection(view));
                if (applyPrefixes)
                    parseResult.AppendColumnFieldPrefixes();
                return DxFtsContainsHelper.Create(parseResult);
            }
            return null;
        }


        static List<IDataColumnInfo> GetFindToColumnsCollection(GridView view)
        {
            List<IDataColumnInfo> res = new List<IDataColumnInfo>();
            foreach (GridColumn column in view.VisibleColumns)
            {
                if (IsAllowFindColumn(column))
                {
                    DataColumnInfo dataColumn = view.DataController.Columns[column.FieldName];
                    res.Add(dataColumn);
                }
            }
            foreach (GridColumn column in view.GroupedColumns)
            {
                DataColumnInfo dataColumn = view.DataController.Columns[column.FieldName];
                if (res.Contains(dataColumn) || !IsAllowFindColumn(column)) continue;
                res.Add(dataColumn);
            }
            return res;
        }

        internal static bool IsAllowFindColumn(GridColumn col)
        {
            if (col == null || string.IsNullOrEmpty(col.FieldName)) return false;
            if (col.ColumnEdit is RepositoryItemPictureEdit ||
                col.ColumnEdit is RepositoryItemImageEdit) return false;
            GridView view = col.View as GridView;
            if (view.OptionsFind.FindFilterColumns == "*") return true;
            return string.Concat(";", view.OptionsFind.FindFilterColumns, ";").Contains(string.Concat(";", col.FieldName, ";"));
        }

        public static CriteriaOperator MyConvertFindPanelTextToCriteriaOperator(GridView view)
        {
            return ConvertFindPanelTextToCriteriaOperator(String.Format("\"{0}\"", view.FindFilterText), view, false);
        }
    }
}
