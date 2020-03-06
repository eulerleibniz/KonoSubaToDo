using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using KonoAndroid.Model;
using System;
using System.Collections.Generic;

namespace KonoAndroid
{
    public class FragmentInbox : Android.Support.V4.App.Fragment
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private TextView textView1;
        private TextView textView2;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_inbox, container, false);

            button1 = view.FindViewById<Button>(Resource.Id.button1);
            button2 = view.FindViewById<Button>(Resource.Id.button2);
            button3 = view.FindViewById<Button>(Resource.Id.fragment1_button3);

            textView1 = view.FindViewById<TextView>(Resource.Id.textView1);
            textView2 = view.FindViewById<TextView>(Resource.Id.textView2);

            textView1.Text = Constants.DatabasePath;
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            return view;

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private async void Button2_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button2_Click");

            Random random = new Random();
            var todoItem = new TodoItem() { Title = random.NextDouble().ToString(), Completed = false, Comment = "Yp" };
            await SplashActivity.Database.SaveItemAsync(todoItem).ConfigureAwait(true);
        }

        private async void Button3_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button3_Click");

            Random random = new Random();
            var todoItem = new TodoItem() { Title = random.NextDouble().ToString(), Completed = false, Comment = "Yp" };
            List<TodoItem> todoItems = await SplashActivity.Database.GetItemsAsync().ConfigureAwait(false);
            if (todoItems == null)
            {
                System.Diagnostics.Debug.WriteLine("todoItems==null");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(todoItems.Count);
            }
            await SplashActivity.Database.SaveItemAsync(todoItem).ConfigureAwait(false);
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Fragment parentFragment = ParentFragment;

            //NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            //navigationView.SetNavigationItemSelectedListener(this);

            //for (int i = 0; i < 10; i++)
            //{
            //    navigationView.Menu.Add(i.ToString());
            //}

            //navigationView.Menu.FindItem(Resource.Id.nav_group_main);
            //NavigationView navigationView = View.FindViewById<NavigationView>(Resource.Id.nav_view);

            ((MainActivity)Activity).AddItemToNavView("Get Rekt");

            //Android.Support.V4.Widget.DrawerLayout mylayout = (Android.Support.V4.Widget.DrawerLayout)LayoutInflater.Inflate(Resource.Layout.activity_main, null);
            //NavigationView navigationView = mylayout.FindViewById<NavigationView>(Resource.Id.nav_view);

            ////NavigationView navigationView = (NavigationView)LayoutInflater.Inflate(Resource.Menu.activity_main_drawer, null);
            //if (navigationView == null)
            //{
            //    System.Diagnostics.Debug.WriteLine("damn null");
            //}
            //else
            //{
            //    navigationView.Menu.Add("ffs please");
            //    int i = ((ViewGroup)View.Parent).Id;
            //    // i = ((ViewGroup)View.Parent).RootView.Id;
            //    System.Diagnostics.Debug.WriteLine(i);
            //}

            textView2.Text = Constants.DatabasePath;
        }
    }
}