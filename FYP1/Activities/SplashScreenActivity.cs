using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using FYP_Droid.Utilities;
using System.Threading.Tasks;

namespace FYP_Droid.Activities
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        #region Fields

        #endregion //Fields

        #region Property Accessors


        #endregion //Property Accessors

        #region Methods

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

        }

        protected override void OnResume()
        {
            base.OnResume();
            //delay splash
            Task startupWork = new Task(() =>
            {

                Task.Delay(2000);  // Simulate a bit of startup work.

            });
            //start next activity
            startupWork.ContinueWith(t =>
            {
                // GlobalUtilities.UserManager.DeleteUser();
                StartActivity(new Intent(Application.Context, typeof(NaivgationActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
        #endregion //Methods

    }
}