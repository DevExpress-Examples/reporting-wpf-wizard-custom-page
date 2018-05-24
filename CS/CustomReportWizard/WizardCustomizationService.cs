using System;
using DevExpress.DataAccess;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.Xpf.DataAccess.DataSourceWizard;
using DevExpress.Xpf.Reports.UserDesigner.ReportWizard;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Wizards;
using DevExpress.XtraReports.Wizards.Presenters;


namespace CustomReportWizard {
    class WizardCustomizationService : IWizardCustomizationService {
        void IDataSourceWizardCustomizationService.CustomizeDataSourceWizard(DataSourceWizardCustomizationModel customization, 
            ViewModelSourceIntegrityContainer container) { }

        void IWizardCustomizationService.CustomizeReportWizard(ReportWizardCustomizationModel customization, 
            ViewModelSourceIntegrityContainer container) {
            customization.Model = new CustomReportModel();
            container.RegisterType<ChoosePageSettingsPage<CustomReportModel>>();
            container.RegisterType<ChooseReportTypePage<XtraReportModel>, CustomChooseReportTypePage>();
            container.RegisterViewModel<IChoosePageSettingsPageView, ChoosePageSettingsPageViewModel>();
        }

        bool IDataSourceWizardCustomizationService.TryCreateDataSource(IDataSourceModel model, out object dataSource, 
            out string dataMember) {
            dataSource = null;
            dataMember = null;
            return false;
        }

        bool IWizardCustomizationService.TryCreateReport(XtraReportModel model, out XtraReport report) {
            var customModel = model as CustomReportModel;
            if(customModel == null || model.ReportType == ReportType.Template || model.ReportType == ReportType.Label) {
                report = null;
                return false;
            }
            IDataComponent dataSource = null;
            string dataMember = null;
            if(customModel.ReportType != ReportType.Empty) {
                var dataComponentCreator = new DataComponentCreator();
                dataSource = dataComponentCreator.CreateDataComponent(model);
                dataMember = dataSource.DataMember;
            }
            var builder = new DevExpress.Xpf.Reports.UserDesigner.ReportWizard.ReportBuilder(dataSource, dataMember);

            report = new XtraReport();
            report.PaperKind = customModel.PaperKind;
            report.Margins = customModel.PageMargins;
            builder.Build(report, customModel);
            return true;
        }
    }
}
