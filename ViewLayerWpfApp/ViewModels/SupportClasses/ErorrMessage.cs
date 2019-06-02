using System;
using System.IO;

namespace ViewLayerWpfApp.ViewModels.SupportingClasses
{
    public static class ErrorMessage
    {
        public static string MakingMessageForMessageBox(Exception ex)
        {
            string errMessage = "{0}\n" +
                                "Имя объекта или приложения, вызвавшего ошибку: {1}\n\n" +
                                "Если ошибка повторяется, необходимо связаться со службой технический поддержки.\n\n";
            return string.Format(errMessage, ex.Message, ex.Source);
        }
    }
}
