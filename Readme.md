<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128605288/23.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T600080)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Reporting for WPF - How to Register a Custom Page in the Report Wizard for the End-User Report Designer

This example demonstrates how to extend the [Report Wizard](https://docs.devexpress.com/XtraReports/114841/desktop-reporting/wpf-reporting/end-user-report-designer-for-wpf/gui/report-wizard) in the [End-User Report Designer](https://docs.devexpress.com/XtraReports/114104/desktop-reporting/wpf-reporting/end-user-report-designer-for-wpf) with a custom page that allows you to edit the report page settings. This page is displayed after selecting the report type (for empty and data-bound reports).

## Implementation Details

To accomplish this task, perform the following steps:

1. Create the `Presenter` class as the [DevExpress.Data.WizardFramework.WizardPageBase<TView, TModel>](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.WizardFramework.WizardPageBase-2) class descendant. Implement the logic to pass data between a Model and View, specify the next wizard page type, and define which page buttons should be available.
2. Declare an interface that identifies the wizard page View.
3. Create the `ViewModel` class as the [DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase](https://docs.devexpress.com/WPF/DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase) class descendant that implements the interface declared above. This ViewModel class processes data to display it in the User Interface.
4. Write an XAML template with the `ViewModel` type referenced by a `Key` to define the page's visual appearance and layout. The specified `Key` is used to automatically locate the corresponding template.
5. Create an [XtraReportModel](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Wizards.XtraReportModel) class descendant. Add custom fields to store the report page settings and override the `Equals` method to take into account the added fields.
6. Override the existing [ChooseReportTypePage](https://docs.devexpress.com/WPF/DevExpress.Xpf.Reports.UserDesigner.ReportWizard.Pages.ChooseReportTypePage) to set the next page to your custom one.
7. Implement the [IWizardCustomizationService](https://docs.devexpress.com/WPF/DevExpress.Xpf.Reports.UserDesigner.ReportWizard.IWizardCustomizationService) interface, which provides four methods for wizard customization. In this implementation, register the previously created `Presenters`, `ViewModel`, and `Model`,  and write the logic to build a report.


## Files to Review

* [CustomReportModel.cs](./CS/CustomReportModel.cs) (VB:[CustomReportModel.vb](./VB/CustomReportModel.vb))
* [ChoosePageSettingsPage.cs](./CS/ChoosePageSettingsPage.cs) (VB:[ChoosePageSettingsPage.vb](./VB/ChoosePageSettingsPage.vb))
* [CustomChooseReportTypePage.cs](./CS/CustomChooseReportTypePage.cs) (VB:[CustomChooseReportTypePage.vb](./VB/CustomChooseReportTypePage.vb))
* [WizardCustomizationService.cs](./CS/WizardCustomizationService.cs) (VB:[WizardCustomizationService.vb](./VB/WizardCustomizationService.vb))

## More Examples

* [WPF Report Designer - How to customize the list of data providers in the Data Source Wizard](https://github.com/DevExpress-Examples/reporting-wpf-designer-data-provider-list)

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-wpf-wizard-custom-page&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-wpf-wizard-custom-page&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
