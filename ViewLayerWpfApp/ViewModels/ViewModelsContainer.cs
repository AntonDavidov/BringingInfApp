using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.ProjLead;
using ViewLayerWpfApp.ViewModels.Employees;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.LeadProjs;

namespace ViewLayerWpfApp.ViewModels
{

    public static class ViewModelsContainer
    {
        public static ProjectsViewModel ProjectsViewModel { get; set; }
        public static ProjEmplsViewModel ProjEmplsViewModel { get; set; }
        public static ProjLeadViewModel ProjLeadViewModel { get; set; }
        public static EmployeesViewModel EmployeesViewModel { get; set; }
        public static EmplProjsViewModel EmplProjsViewModel { get; set; }
        public static LeadProjsViewModel LeadProjsViewModel { get; set; }
    }
}
