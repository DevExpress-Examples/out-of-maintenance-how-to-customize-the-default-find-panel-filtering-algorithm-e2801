# How to customize the default Find Panel filtering algorithm


<p>In 19.1 we've implemented two new options that specify how our Find Panel works:</p>
<p>1) <a href="http://newdoc.devexpress.devx/WindowsForms/DevExpress.XtraGrid.Views.Base.ColumnViewOptionsFind.ParserKind?v=19.1"><u>ParserKind</u></a>  - If a search string contains whitespaces, the control splits it into separate logical blocks. This option specifies how these blocks are combined into a search query.</p>
<p>2) <a href="http://newdoc.devexpress.devx/WindowsForms/DevExpress.XtraGrid.Views.Base.ColumnViewOptionsFind.Condition?v=19.1"><u>Filter Condition</u></a> - Use the Condition property to specify the search operator (Contains, StartsWith, Equals, Like).</p>
<p>So starting with 19.1 you can use these options to manage the Find Panel behavior without using descendants. 
In case of using SearchLookUpEdit you can find these options in the SearchLookUpEdit.Properties.PopupView.OptionsFind section.</p>

<p>See also:</p>
<p><a href="https://community.devexpress.com/blogs/winforms/archive/2019/03/20/winforms-data-grid-new-find-panel-options-v19-1-ctp.aspx?CommentPosted=true"><u>WinForms - Data Grid - New Find Panel Options</u></a></p>
<br/>


