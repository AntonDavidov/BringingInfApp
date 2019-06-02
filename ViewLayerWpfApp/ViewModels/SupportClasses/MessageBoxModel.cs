using System.Windows;

namespace ViewLayerWpfApp.ViewModels.SupportingClasses
{
    public class MessageBoxModel
    {
        public string Message { get; set; }
        public string Caption { get; set; }
        public MessageBoxButton Buttons { get; set; }
        public MessageBoxResult Result { get; set; }
    }
}
