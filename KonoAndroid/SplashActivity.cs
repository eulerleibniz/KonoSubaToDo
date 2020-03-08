using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using KonoAndroid.Model;
using System.Threading.Tasks;

namespace KonoAndroid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        //private static TodoItemDatabase database;
        private static DatabaseToDo databaseToDo;

        private static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        public static DatabaseToDo DatabaseToDo
        {
            get
            {
                if (databaseToDo == null)
                {
                    databaseToDo = new DatabaseToDo(Constants.DatabasePath);
                }
                return databaseToDo;
            }
        }

        // Simulates background work that happens behind the splash screen
        private async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(1000).ConfigureAwait(false); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            Intent intent = new Intent(Application.Context, typeof(MainActivity));
            StartActivity(intent);
            intent.Dispose();
        }
    }
}