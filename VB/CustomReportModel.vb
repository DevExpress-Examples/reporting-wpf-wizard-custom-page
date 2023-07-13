Imports DevExpress.XtraReports.Wizards
Imports System.Drawing.Printing

Namespace CustomReportWizard
    Friend Class CustomReportModel
        Inherits XtraReportModel

        Public Property PaperKind() As PaperKind
        Public Property PageMargins() As Margins

        Public Sub New()
            PaperKind = PaperKind.Letter
            PageMargins = New Margins(100, 100, 100, 100)
        End Sub
        Public Sub New(ByVal model As CustomReportModel)
            MyBase.New(model)
            PaperKind = model.PaperKind
            PageMargins = New Margins(model.PageMargins.Left, model.PageMargins.Right, model.PageMargins.Top, model.PageMargins.Bottom)
        End Sub

        Public Overrides Function Clone() As Object
            Return New CustomReportModel(Me)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim other = TryCast(obj, CustomReportModel)
            Return other IsNot Nothing AndAlso MyBase.Equals(obj) AndAlso PaperKind = other.PaperKind AndAlso PageMargins Is other.PageMargins
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode() Xor PaperKind.GetHashCode() Xor PageMargins.GetHashCode()
        End Function
    End Class
End Namespace
