using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using DevExpress.Data.WizardFramework;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.Drawing.Printing;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraReports.Wizards;

namespace CustomReportWizard {
    class ChoosePageSettingsPage<TModel> : WizardPageBase<IChoosePageSettingsPageView, TModel>, IWizardPage<XtraReportModel> 
        where TModel : CustomReportModel { XtraReportModel IWizardPage<XtraReportModel>.Model 
            { get { return Model; } set { Model = (TModel)value; } }

        public ChoosePageSettingsPage(IChoosePageSettingsPageView view) : base(view) { }

        public override bool FinishEnabled { get { return Model.ReportType == ReportType.Empty; } }
        public override bool MoveNextEnabled { get { return Model.ReportType != ReportType.Empty; } }

        public override Type GetNextPageType() { return Model.ReportType == ReportType.Empty 
                ? null 
                : typeof(ChooseDataSourceTypePage<XtraReportModel>); }

        public override void Begin() {
            View.PaperKind = Model.PaperKind;
            View.Portrait = Model.Portrait;
            View.PageMargins = Model.PageMargins;
        }

        public override void Commit() {
            Model.PaperKind = View.PaperKind;
            Model.Portrait = View.Portrait;
            Model.PageMargins = View.PageMargins;
        }
    }

    interface IChoosePageSettingsPageView {
        DXPaperKind PaperKind { get; set; }
        bool Portrait { get; set; }
        Margins PageMargins { get; set; }
    }
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class ChoosePageSettingsPageViewModel : DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase, 
        IChoosePageSettingsPageView {

        public override string Description { get { return "Choose the required page settings."; } }

        public virtual IEnumerable<PaperKindViewInfo> AvailablePaperKinds {get; protected set;}
        public virtual PaperKindViewInfo SelectedPaperKind { get; set; }

        public virtual bool Portrait { get; set; }

        [Range(0,300)]
        public virtual int LeftMargin { get; set; }
        [Range(0, 300)]
        public virtual int RightMargin { get; set; }
        [Range(0, 300)]
        public virtual int TopMargin { get; set; }
        [Range(0, 300)]
        public virtual int BottomMargin { get; set; }

        DXPaperKind IChoosePageSettingsPageView.PaperKind {
            get { return (DXPaperKind)SelectedPaperKind.Id; }
            set { SetPaperKind(value); }
        }
        Margins IChoosePageSettingsPageView.PageMargins {
            get { return new Margins(LeftMargin, RightMargin, TopMargin, BottomMargin); }
            set { SetMargins(value); }
        }

        public ChoosePageSettingsPageViewModel() {
            var printerSettings = new PrinterSettings();
            AvailablePaperKinds = printerSettings.PaperSizes.Cast<PaperSize>().Select(paperSize => {
                return new PaperKindViewInfo() {
                    Id = (int)paperSize.Kind,
                    DisplayName = paperSize.PaperName,
                    SizeText = string.Format("{0}x{1}", paperSize.Width, paperSize.Height) };
            }).ToArray();
        }

        void SetPaperKind(DXPaperKind value) {
            var info = AvailablePaperKinds.SingleOrDefault(x => (DXPaperKind)x.Id == value)
                ?? AvailablePaperKinds.Single(x => (DXPaperKind)x.Id == DXPaperKind.Letter);
            SelectedPaperKind = info;
        }

        void SetMargins(Margins value) {
            LeftMargin = value.Left;
            RightMargin = value.Right;
            TopMargin = value.Top;
            BottomMargin = value.Bottom;
        }
    }
}
