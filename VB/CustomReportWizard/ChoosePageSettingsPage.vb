Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Drawing.Printing
Imports System.Linq
Imports DevExpress.Data.WizardFramework
Imports DevExpress.DataAccess.Wizard.Presenters
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.XtraReports.Wizards

Namespace CustomReportWizard
    Friend Class ChoosePageSettingsPage(Of TModel As CustomReportModel)
        Inherits WizardPageBase(Of IChoosePageSettingsPageView, TModel)
        Implements IWizardPage(Of XtraReportModel)

        Public Sub New(ByVal view As IChoosePageSettingsPageView)
            MyBase.New(view)
        End Sub

        Public Overrides ReadOnly Property FinishEnabled() As Boolean
            Get
                Return Model.ReportType = ReportType.Empty
            End Get
        End Property
        Public Overrides ReadOnly Property MoveNextEnabled() As Boolean
            Get
                Return Model.ReportType <> ReportType.Empty
            End Get
        End Property

        Private Property IWizardPage_Model As XtraReportModel Implements IWizardPage(Of XtraReportModel).Model
            Get
                Return Model
            End Get
            Set(ByVal value As XtraReportModel)
                Model = CType(value, TModel)
            End Set
        End Property

        Public Overrides Function GetNextPageType() As Type
            Return If(Model.ReportType = ReportType.Empty, Nothing, GetType(ChooseDataSourceTypePage(Of XtraReportModel)))
        End Function

        Public Overrides Sub Begin()
            View.PaperKind = Model.PaperKind
            View.Portrait = Model.Portrait
            View.PageMargins = Model.PageMargins
        End Sub

        Public Overrides Sub Commit()
            Model.PaperKind = View.PaperKind
            Model.Portrait = View.Portrait
            Model.PageMargins = View.PageMargins
        End Sub

#Region "IWizardPage"
        Private ReadOnly Property IWizardPage_MoveNextEnabled As Boolean Implements IWizardPage(Of XtraReportModel).MoveNextEnabled
            Get
                Return MoveNextEnabled
            End Get
        End Property

        Private ReadOnly Property IWizardPage_FinishEnabled As Boolean Implements IWizardPage(Of XtraReportModel).FinishEnabled
            Get
                Return FinishEnabled
            End Get
        End Property

        Private ReadOnly Property IWizardPage_PageContent As Object Implements IWizardPage(Of XtraReportModel).PageContent
            Get
                Return PageContent
            End Get
        End Property

        Private Function IWizardPage_GetNextPageType() As Type Implements IWizardPage(Of XtraReportModel).GetNextPageType
            Return GetNextPageType()
        End Function

        Private Function IWizardPage_Validate(ByRef errorMessage As String) As Boolean Implements IWizardPage(Of XtraReportModel).Validate
            Return Validate(errorMessage)
        End Function

        Private Sub IWizardPage_Begin() Implements IWizardPage(Of XtraReportModel).Begin
            Begin()
        End Sub

        Private Event IWizardPage_Changed As EventHandler Implements IWizardPage(Of XtraReportModel).Changed
        Private Event IWizardPage_Error As EventHandler(Of WizardPageErrorEventArgs) Implements IWizardPage(Of XtraReportModel).Error

        Private Sub IWizardPage_Commit() Implements IWizardPage(Of XtraReportModel).Commit
            Commit()
        End Sub
#End Region

    End Class

    Friend Interface IChoosePageSettingsPageView
        Property PaperKind() As PaperKind
        Property Portrait() As Boolean
        Property PageMargins() As Margins
    End Interface
    <POCOViewModel(ImplementIDataErrorInfo:=True)>
    Public Class ChoosePageSettingsPageViewModel
        Inherits DevExpress.Xpf.DataAccess.DataSourceWizard.WizardPageBase
        Implements IChoosePageSettingsPageView

        Public Overrides ReadOnly Property Description() As String
            Get
                Return "Choose the required page settings."
            End Get
        End Property

        Private privateAvailablePaperKinds As IEnumerable(Of PaperKindViewInfo)
        Public Overridable Property AvailablePaperKinds() As IEnumerable(Of PaperKindViewInfo)
            Get
                Return privateAvailablePaperKinds
            End Get
            Protected Set(ByVal value As IEnumerable(Of PaperKindViewInfo))
                privateAvailablePaperKinds = value
            End Set
        End Property
        Public Overridable Property SelectedPaperKind() As PaperKindViewInfo

        Public Overridable Property Portrait() As Boolean Implements IChoosePageSettingsPageView.Portrait

        <Range(0, 300)>
        Public Overridable Property LeftMargin() As Integer
        <Range(0, 300)>
        Public Overridable Property RightMargin() As Integer
        <Range(0, 300)>
        Public Overridable Property TopMargin() As Integer
        <Range(0, 300)>
        Public Overridable Property BottomMargin() As Integer

        Private Property IChoosePageSettingsPageView_PaperKind() As PaperKind Implements IChoosePageSettingsPageView.PaperKind
            Get
                Return CType(SelectedPaperKind.Id, PaperKind)
            End Get
            Set(ByVal value As PaperKind)
                SetPaperKind(value)
            End Set
        End Property
        Private Property IChoosePageSettingsPageView_PageMargins() As Margins Implements IChoosePageSettingsPageView.PageMargins
            Get
                Return New Margins(LeftMargin, RightMargin, TopMargin, BottomMargin)
            End Get
            Set(ByVal value As Margins)
                SetMargins(value)
            End Set
        End Property

        Public Sub New()
            Dim printerSettings = New PrinterSettings()
            AvailablePaperKinds = printerSettings.PaperSizes.Cast(Of PaperSize)().Select(Function(paperSize)
                                                                                             Return New PaperKindViewInfo() With {
                                                                                                 .Id = CInt((paperSize.Kind)),
                                                                                                 .DisplayName = paperSize.PaperName,
                                                                                                 .SizeText = String.Format("{0}x{1}", paperSize.Width, paperSize.Height)
                                                                                             }
                                                                                         End Function).ToArray()
        End Sub

        Private Sub SetPaperKind(ByVal value As PaperKind)
            Dim info = If(AvailablePaperKinds.SingleOrDefault(Function(x) CType(x.Id, PaperKind) = value), AvailablePaperKinds.Single(Function(x) CType(x.Id, PaperKind) = PaperKind.Letter))
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