using System.Windows;
using ViewLayerWpfApp.ViewModels;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DIConfig.Initialize();
            MapperStarter.CreateMapper();
        }        
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.WaitLogWriting();
        }
    }
}
