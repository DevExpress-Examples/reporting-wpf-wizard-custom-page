using DevExpress.XtraReports.Wizards;
using System.Drawing.Printing;

namespace CustomReportWizard {
    class CustomReportModel : XtraReportModel {
        public PaperKind PaperKind { get; set; }
        public Margins PageMargins { get; set; }

        public CustomReportModel() {
            PaperKind = PaperKind.Letter;
            PageMargins = new Margins(100, 100, 100, 100);
        }
        public CustomReportModel(CustomReportModel model) : base(model) {
            PaperKind = model.PaperKind;
            PageMargins = new Margins(model.PageMargins.Left, model.PageMargins.Right, model.PageMargins.Top, model.PageMargins.Bottom);
        }

        public override object Clone() {
            return new CustomReportModel(this);
        }

        public override bool Equals(object obj) {
            var other = obj as CustomReportModel;
            return other != null
                && base.Equals(obj)
                && PaperKind == other.PaperKind
                && PageMargins == other.PageMargins;
        }

        public override int GetHashCode() {
            return base.GetHashCode() ^ PaperKind.GetHashCode() ^ PageMargins.GetHashCode();
        }
    }
}
