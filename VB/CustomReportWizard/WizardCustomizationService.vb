Imports DevExpress.DataAccess
Imports DevExpress.DataAccess.Wizard
Imports DevExpress.DataAccess.Wizard.Model
Imports DevExpress.Xpf.DataAccess.DataSourceWizard
Imports DevExpress.Xpf.Reports.UserDesigner.ReportWizard
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Wizards
Imports DevExpress.XtraReports.Wizards.Presenters
Imports System
Imports System.Collections.Generic
Imports System.Drawing.Printing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace CustomReportWizard
    Friend Class WizardCustomizationService
        Implements IWizardCustomizationService

        Private Sub IDataSourceWizardCustomizationService_CustomizeDataSourceWizard(ByVal customization As DataSourceWizardCustomizationModel, ByVal container As ViewModelSourceIntegrityContainer) Implements IDataSourceWizardCustomizationService.CustomizeDataSourceWizard
        End Sub

        Private Sub IWizardCustomizationService_CustomizeReportWizard(ByVal customization As ReportWizardCustomizationModel, ByVal container As ViewModelSourceIntegrityContainer) Implements IWizardCustomizationService.CustomizeReportWizard
            customization.Model = New CustomReportModel()
            container.RegisterType(Of ChoosePageSettingsPage(Of CustomReportModel))()
            container.RegisterType(Of ChooseReportTypePage(Of XtraReportModel), CustomChooseReportTypePage)()
            container.RegisterViewModel(Of IChoosePageSettingsPageView, ChoosePageSettingsPageViewModel)()
        End Sub

        Private Function IDataSourceWizardCustomizationService_TryCreateDataSource(ByVal model As IDataSourceModel, ByRef dataSource As Object, ByRef dataMember As String) As Boolean Implements IDataSourceWizardCustomizationService.TryCreateDataSource
            dataSource = Nothing
            dataMember = Nothing
            Return False
        End Function

        Private Function IWizardCustomizationService_TryCreateReport(ByVal model As XtraReportModel, ByRef report As XtraReport) As Boolean Implements IWizardCustomizationService.TryCreateReport
            Dim customModel = TryCast(model, CustomReportModel)
            If customModel Is Nothing OrElse model.ReportType = ReportType.Template OrElse model.ReportType = ReportType.Label Then
                report = Nothing
                Return False
            End If
            Dim dataSource As IDataComponent = Nothing
            Dim dataMember As String = Nothing
            If customModel.ReportType <> ReportType.Empty Then
                Dim dataComponentCreator = New DataComponentCreator()
                dataSource = dataComponentCreator.CreateDataComponent(model)
                dataMember = dataSource.DataMember
            End If
            Dim builder = New DevExpress.Xpf.Reports.UserDesigner.ReportWizard.ReportBuilder(dataSource, dataMember)

            report = New XtraReport()
            report.PaperKind = customModel.PaperKind
            report.Margins = customModel.PageMargins
            builder.Build(report, customModel)
            Return True
        End Function
    End Class
End Namespace
