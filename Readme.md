<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128627004/19.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2801)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to customize the default Find Panel filtering algorithm


<p>In 19.1 we've implemented two new options that specify how our Find Panel works:</p>
<p>1) <a href="https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Base.ColumnViewOptionsFind.ParserKind"><u>ParserKind</u></a>  - If a search string contains whitespaces, the control splits it into separate logical blocks. This option specifies how these blocks are combined into a search query.</p>
<p>2) <a href="https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Base.ColumnViewOptionsFind.Condition"><u>Filter Condition</u></a> - Use the Condition property to specify the search operator (Contains, StartsWith, Equals, Like).</p>
<p>So starting with 19.1 you can use these options to manage the Find Panel behavior without using descendants. 
In case of using SearchLookUpEdit you can find these options in the SearchLookUpEdit.Properties.PopupView.OptionsFind section.</p>

<p>See also:</p>
<p><a href="https://community.devexpress.com/blogs/winforms/archive/2019/03/20/winforms-data-grid-new-find-panel-options-v19-1-ctp.aspx?CommentPosted=true"><u>WinForms - Data Grid - New Find Panel Options</u></a></p>

[DevExpress WinForms Troubleshooting - Grid Control](https://go.devexpress.com/CheatSheets_WinForms_Examples_T934742.aspx)

<br/>


