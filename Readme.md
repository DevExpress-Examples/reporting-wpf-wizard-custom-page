<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128605288/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T600080)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# WPF Report Designer - How to register a custom page in the Report Wizard


<p>This example demonstrates how to extend the <a href="https://documentation.devexpress.com/XtraReports/114104/Creating-End-User-Reporting-Applications/WPF-Reporting/Report-Designer">End-User Report Designer</a>'s <a href="https://documentation.devexpress.com/XtraReports/114841/Creating-End-User-Reporting-Applications/WPF-Reporting/Report-Designer/GUI/Report-Wizard">Report Wizard</a> with a custom page that allows you to edit the report page settings. This page is displayed after selecting the report type (for empty and data-bound reports).</p>
<p><br>To accomplish this task, perform the following steps:<br><br>1. Create a custom page Presenter by inheriting from the <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.Data.WizardFramework.WizardPageBase~TView~TModel~.class">WizardPageBase<TView, TModel></a> class (from the DevExpress.Data.WizardFramework namespace) . Implement the logic to pass data between a Model and View, specify the next wizard page type and define which page buttons should be available. <br>2. Declare an interface identifying the wizard page View. <br>3. Create the page ViewModel by inheriting from the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase.members">WizardPageBase</a> class (from the DevExpress.Xpf.DataAccess.DataSourceWizard namespace) and implementing the interface declared above. This ViewModel processes data for displaying it in the user interface.   <br>4. Write a XAML template with the ViewModel type referenced by a Key to define the page's visual appearance and layout. The specified Key is used to automatically locate the corresponding template.<br>5. Create an <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Wizards.XtraReportModel.class">XtraReportModel</a> class descendant, add custom fields storing the report page settings and override the <strong>Equals </strong>method to take into account the added fields.<br>6. Override the existing <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Reports.UserDesigner.ReportWizard.Pages.ChooseReportTypePage.class">ChooseReportTypePage</a> Presenter to set the next page to your custom one. <br>7. Implement the <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Reports.UserDesigner.ReportWizard.IWizardCustomizationService.class">IWizardCustomizationService</a> interface, which provides four methods for wizard customization. In this implementation, register the previously created Presenters, ViewModel and Model as well as write the logic for building a report.</p>
<p> </p>
<p><strong>See Also<br></strong><a href="https://www.devexpress.com/Support/Center/p/T456882">WPF Report Designer - How to customize the list of data providers in the Data Source Wizard</a></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-wpf-wizard-custom-page&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-wpf-wizard-custom-page&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
