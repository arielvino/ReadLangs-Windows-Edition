using System.Configuration;
using System.Data;
using System.Windows;

namespace ReadLangs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ITranslator Translator { get; private set; } = new GoogleApiTranslator();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Settings.Init();
        }
    }

}