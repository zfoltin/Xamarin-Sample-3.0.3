using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsApp
{
	public partial class App : Application
	{
		public App()
		{

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                int i = 0;
                string exception = args.Exception.StackTrace;
            };

			InitializeComponent();

			MainPage = new NavigationPage(new FormsAppPage());
		}

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}