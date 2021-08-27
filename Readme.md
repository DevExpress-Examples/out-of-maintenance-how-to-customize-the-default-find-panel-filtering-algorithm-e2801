<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128627004/10.2.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2801)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to customize the default Find Panel filtering algorithm


<p>Find Panel uses an extended syntax that allows you to specify complex criteria. You can find detailed information here:<br /> - <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument8869">Find Panel</a></p>
<p>However, sometimes it is necessary to exclude/include some rows in the resulting list according to your custom logic. In this case, you can filter rows manually by handling <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraGridViewsBaseColumnView_CustomRowFiltertopic">View.CustomRowFilter Event</a>. This example demonstrates how to construct your own filter criteria and use <strong>ExpressionEvaluator </strong>to check whether a row should be hidden.</p>
<p>For demo purposes, this example just shows how to find exact matches of a typed string. The same result can be accomplished with the default behavior if you enclose your text in double quotes in Find Panel. To provide your own logic, just modify the <strong>MyConvertFindPanelTextToCriteriaOperator </strong>method as your needs dictate.</p>

<br/>


