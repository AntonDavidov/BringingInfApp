using AutoMapper;
using ClientEntities;
using NLog;
using System;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using ViewLayerWpfApp.ViewModels.Employees;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.ProjLead;

namespace ViewLayerWpfApp.ViewModels.SupportingClasses
{
    public static class DIConfig
    {
        public static CompositionContainer ComposeContainer { get; set; }
        public static void Initialize()
        {
            AssemblyCatalog assemCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

			DirectoryCatalog dirCatalog = new DirectoryCatalog("BusinessLogic");
			AggregateCatalog aggregateCatalog = new AggregateCatalog(assemCatalog, dirCatalog);
			ComposeContainer = new CompositionContainer(aggregateCatalog);
		}
    }

    public static class MapperStarter
    {
        public static void CreateMapper()
        {
            Mapper.Initialize(cfg =>
            {
                #region Employee & EmployeeView

                cfg.CreateMap<Employee, EmployeeView>().ForMember("EmplProjViews", opt => opt.MapFrom(s => s.Projects))
                                                       .ForMember("LeadProjViews", opt => opt.MapFrom(s => s.LeadProjects));
                cfg.CreateMap<EmployeeView, Employee>();
                #endregion


                #region Project & EmplProjView
                cfg.CreateMap<Project, EmplProjView>().ForMember("IsChecked", opt => opt.MapFrom(s => true)).PreserveReferences();
                cfg.CreateMap<EmplProjView, Project>();
                #endregion


                #region Project & ProjectView
                cfg.CreateMap<Project, ProjectView>().ForMember("ProjEmplViews", opt => opt.MapFrom(s => s.Employees))
                                                     .ForMember("ProjLeadView", opt => opt.MapFrom(s => s.Leader));
                cfg.CreateMap<ProjectView, Project>();
                #endregion


                #region Employee & ProjEmplView
                cfg.CreateMap<Employee, ProjEmplView>().ForMember("IsChecked", opt => opt.MapFrom(s => true)).PreserveReferences();
                cfg.CreateMap<ProjEmplView, Employee>();
                #endregion


                #region Employee & ProjLeadView
                cfg.CreateMap<Employee, ProjLeadView>().ForMember("IsChecked", opt => opt.MapFrom(s => true)).PreserveReferences();
                cfg.CreateMap<ProjLeadView, Employee>();
                #endregion


                #region EmployeeView To ProjEmplView
                cfg.CreateMap<EmployeeView, ProjEmplView>();
                #endregion


                #region EmployeeView To ProjLeadView
                cfg.CreateMap<EmployeeView, ProjLeadView>();
                #endregion


                #region ProjectView To EmplProjView
                cfg.CreateMap<ProjectView, EmplProjView>();
                #endregion
            });
        }
    }

    public static class Log
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        static Task logTask;
        public static void WaitLogWriting()
        {
            if (logTask != null)
                logTask.Wait();
        }
        public static Task WriteLogAsync(Exception ex)
        {
            logTask = Task.Run(() => logger.Error(ex));
            return logTask;
        }
    }

	public static class Credentials
	{
		public static string Login { get; set; } = "";
		public static string Password { get; set; } = "";
	}
}
