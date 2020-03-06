using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace KonoAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private Android.Support.V4.App.Fragment fragment;
        private NavigationView navigationView;

        public void AddItemToNavView(string item)
        {
            if (!(navigationView == null))
            {
                navigationView.Menu.Add(item);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            for (int i = 0; i < 2; i++)
            {
                navigationView.Menu.Add(i.ToString());
            }

            navigationView.Menu.FindItem(Resource.Id.nav_group_main);

            // DaKa
            //Button button = FindViewById<Button>(Resource.Id.action_yo);

            //button.Click += (o, e) => {
            //    Toast.MakeText(this, "Beep Boop", ToastLength.Short).Show();
            //};
            toggle.Dispose();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                System.Diagnostics.Debug.WriteLine("action_settings");
                Toast.MakeText(this, "action_settings", ToastLength.Short).Show();
                return true;
            }
            else if (id == Resource.Id.action_Test)
            {
                System.Diagnostics.Debug.WriteLine("action_Test");
                Toast.MakeText(this, "action_Test", ToastLength.Short).Show();
                return true;
            }
            else if (id == Resource.Id.action_yo)
            {
                System.Diagnostics.Debug.WriteLine("action_yo");
                Toast.MakeText(this, "action_yo", ToastLength.Short).Show();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.nav_inbox)
            {
                fragment = new FragmentInbox();
                Title = Resources.GetString(Resource.String.title_fragment_inbox);
            }
            else if (id == Resource.Id.nav_today)
            {
                fragment = new FragmentToday();
                Title = Resources.GetString(Resource.String.title_fragment_today);
            }
            else if (id == Resource.Id.nav_next_7_days)
            {
                fragment = new FragmentNext7Days();
                Title = Resources.GetString(Resource.String.title_fragment_next_7_days);
            }
            else if (id == Resource.Id.nav_manage)
            {
                fragment = new FragmentSettings();
                Title = Resources.GetString(Resource.String.title_fragment_settings);
            }
            else if (id == Resource.Id.nav_share)
            {
            }
            else if (id == Resource.Id.nav_send)
            {
            }

            if (!(fragment is null))
            {
                Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
                Android.Support.V4.App.FragmentTransaction fragmentTransaction = fragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.fragment_container, fragment);
                fragmentTransaction.Commit();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}