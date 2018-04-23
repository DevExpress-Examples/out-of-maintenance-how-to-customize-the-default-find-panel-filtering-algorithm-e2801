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
using System.Collections;

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


        private static ICollection GetFindToColumnsCollection(GridView view)
        {
            System.Reflection.MethodInfo mi = typeof(ColumnView).GetMethod("GetFindToColumnsCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return mi.Invoke(view, null) as ICollection;
        }

        public static CriteriaOperator MyConvertFindPanelTextToCriteriaOperator(GridView view)
        {
            return ConvertFindPanelTextToCriteriaOperator(String.Format("\"{0}\"", view.FindFilterText), view, false);
        }
    }
}