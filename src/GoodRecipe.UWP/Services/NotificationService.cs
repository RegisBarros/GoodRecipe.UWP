using GoodRecipe.UWP.Models;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace GoodRecipe.UWP.Services
{
    public static class NotificationService
    {
        public static void ShowToastNotification(Recipe recipe)
        {
            XmlDocument toastXml = new XmlDocument();

            string toastXmlString =
            $@"<toast>
                <visual>
                  <binding template='ToastGeneric'>
                    <text>Receita Cadastrada</text>
                  </binding>
                </visual>
            </toast>";

            toastXml.LoadXml(toastXmlString);

            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
