Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Drawing.Printing
Imports System.Linq
Imports DevExpress.Data.WizardFramework
Imports DevExpress.DataAccess.Wizard.Presenters
Imports DevExpress.Drawing.Printing
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.XtraReports.Wizards

Namespace CustomReportWizard

    Friend Class ChoosePageSettingsPage(Of TModel As CustomReportModel)
        Inherits WizardPageBase(Of IChoosePageSettingsPageView, TModel)
        Implements IWizardPage(Of XtraReportModel)

        Private Property IWizardPage_Model As XtraReportModel Implements IWizardPage(Of XtraReportModel).Model
            Get
                Return Model
            End Get

            Set(ByVal value As XtraReportModel)
                Model = CType(value, TModel)
            End Set
        End Property

        Public Sub New(ByVal view As IChoosePageSettingsPageView)
            MyBase.New(view)
        End Sub

        Public Overrides ReadOnly Property FinishEnabled As Boolean Implements IWizardPage(Of XtraReportModel).FinishEnabled
            Get
                Return Model.ReportType = ReportType.Empty
            End Get
        End Property

        Public Overrides ReadOnly Property MoveNextEnabled As Boolean Implements IWizardPage(Of XtraReportModel).MoveNextEnabled
            Get
                Return Model.ReportType <> ReportType.Empty
            End Get
        End Property

        Public Overrides Function GetNextPageType() As Type Implements IWizardPage(Of XtraReportModel).GetNextPageType
            Return If(Model.ReportType = ReportType.Empty, Nothing, GetType(ChooseDataSourceTypePage(Of XtraReportModel)))
        End Function

        Public Overrides Sub Begin() Implements IWizardPage(Of XtraReportModel).Begin
            View.PaperKind = Model.PaperKind
            View.Portrait = Model.Portrait
            View.PageMargins = Model.PageMargins
        End Sub

        Public Overrides Sub Commit() Implements IWizardPage(Of XtraReportModel).Commit
            Model.PaperKind = View.PaperKind
            Model.Portrait = View.Portrait
            Model.PageMargins = View.PageMargins
        End Sub

        Public Shadows Sub Validate(ByVal errorMessage As String) Implements DevExpress.Data.WizardFramework.IWizardPage(Of XtraReportModel).Validate
            MyBase.Validate(errorMessage)
        End Sub
    End Class

    Friend Interface IChoosePageSettingsPageView

        Property PaperKind As DXPaperKind

        Property Portrait As Boolean

        Property PageMargins As Margins

    End Interface

    <POCOViewModel(ImplementIDataErrorInfo:=True)>
    Public Class ChoosePageSettingsPageViewModel
        Inherits DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase
        Implements IChoosePageSettingsPageView

        Public Overrides ReadOnly Property Description As String
            Get
                Return "Choose the required page settings."
            End Get
        End Property

        Public Overridable Property AvailablePaperKinds As IEnumerable(Of PaperKindViewInfo)

        Public Overridable Property SelectedPaperKind As PaperKindViewInfo

        Public Overridable Property Portrait As Boolean Implements IChoosePageSettingsPageView.Portrait

        <Range(0, 300)>
        Public Overridable Property LeftMargin As Integer

        <Range(0, 300)>
        Public Overridable Property RightMargin As Integer

        <Range(0, 300)>
        Public Overridable Property TopMargin As Integer

        <Range(0, 300)>
        Public Overridable Property BottomMargin As Integer

        Private Property PaperKind As DXPaperKind Implements IChoosePageSettingsPageView.PaperKind
            Get
                Return CType(SelectedPaperKind.Id, DXPaperKind)
            End Get

            Set(ByVal value As DXPaperKind)
                SetPaperKind(value)
            End Set
        End Property

        Private Property PageMargins As Margins Implements IChoosePageSettingsPageView.PageMargins
            Get
                Return New Margins(LeftMargin, RightMargin, TopMargin, BottomMargin)
            End Get

            Set(ByVal value As Margins)
                SetMargins(value)
            End Set
        End Property

        Public Sub New()
            Dim printerSettings = New PrinterSettings()
            AvailablePaperKinds = printerSettings.PaperSizes.Cast(Of PaperSize)().[Select](Function(paperSize) New PaperKindViewInfo() With {.Id = CInt(paperSize.Kind), .DisplayName = paperSize.PaperName, .SizeText = String.Format("{0}x{1}", paperSize.Width, paperSize.Height)}).ToArray()
        End Sub

        Private Sub SetPaperKind(ByVal value As DXPaperKind)
            Dim info = If(AvailablePaperKinds.SingleOrDefault(Function(x) CType(x.Id, DXPaperKind) = value), AvailablePaperKinds.[Single](Function(x) CType(x.Id, DXPaperKind) = DXPaperKind.Letter))
            SelectedPaperKind = info
        End Sub

        Private Sub SetMargins(ByVal value As Margins)
            LeftMargin = value.Left
            RightMargin = value.Right
            TopMargin = value.Top
            BottomMargin = value.Bottom
        End Sub
    End Class
End Namespace
