# How to customize the default Find Panel filtering algorithm


<p>Find Panel uses an extended syntax that allows you to specify complex criteria. You can find detailed information here:<br /> - <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument8869">Find Panel</a></p>
<p>However, sometimes it is necessary to exclude/include some rows in the resulting list according to your custom logic. In this case, you can filter rows manually by handling <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraGridViewsBaseColumnView_CustomRowFiltertopic">View.CustomRowFilter Event</a>. This example demonstrates how to construct your own filter criteria and use <strong>ExpressionEvaluator </strong>to check whether a row should be hidden.</p>
<p>For demo purposes, this example just shows how to find exact matches of a typed string. The same result can be accomplished with the default behavior if you enclose your text in double quotes in Find Panel. To provide your own logic, just modify the <strong>MyConvertFindPanelTextToCriteriaOperator </strong>method as your needs dictate.</p>

<br/>


