using System;
using System.Collections.Generic;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.Entity.ProjectModel;
using DevExpress.XtraReports.Wizards;
using DevExpress.XtraReports.Wizards.Presenters;
using DevExpress.XtraReports.Wizards.Views;

namespace CustomReportWizard {
    class CustomChooseReportTypePage : ChooseReportTypePage<XtraReportModel> {
        public CustomChooseReportTypePage(IChooseReportTypePageView view, IConnectionStorageService connectionStorageService,
            DataSourceTypes dataSourceTypes, IWizardRunnerContext context, ISolutionTypesProvider solutionTypesProvider) : 
            base(view, connectionStorageService, dataSourceTypes, context, solutionTypesProvider) { }

        public override bool MoveNextEnabled {
            get { return true; }
        }

        public override Type GetNextPageType() {
            if(View.ReportType == ReportType.Standard || View.ReportType == ReportType.Empty)
                return typeof(ChoosePageSettingsPage<CustomReportModel>);
            return base.GetNextPageType();
        }
    }
}
