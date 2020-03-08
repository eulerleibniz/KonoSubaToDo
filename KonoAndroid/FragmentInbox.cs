using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace KonoAndroid
{
    public class FragmentInbox : Fragment
    {
        private ExpandableListAdapter listAdapter;
        private ExpandableListView expListView;
        private ThreeLevelListAdapter threeLevelListAdapter;
        private List<string> listDataHeader;
        private Dictionary<string, List<string>> listDataChild;
        private int previousGroup = -1;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (inflater == null)
            {
                throw new NullReferenceException();
            }
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_inbox, container, false);

            expListView = view.FindViewById<ExpandableListView>(Resource.Id.lvExp);
            // Prepare list data
            FnGetListData();

            //Bind list
            listAdapter = new ExpandableListAdapter(Activity);

            Random random = new Random();
            double numHeader, numChild;
            for (int i = 0; i < 15; i++)
            {
                //numHeader = random.NextDouble();
                //listAdapter.AddHeader(numHeader.ToString(), numHeader >= 0.5);
                //for (int j = 0; j < 15; j++)
                //{
                //    numChild = random.NextDouble();
                //    listAdapter.AddChild(numChild.ToString(), numChild >= 0.5, i);
                //}
                listAdapter.AddHeader(i.ToString(), i >= 5);
                for (int j = 0; j < 15; j++)
                {
                    listAdapter.AddChild(j.ToString(), j >= 5, i);
                }
            }

            expListView.SetAdapter(listAdapter);
            //expListView.SetGroupIndicator(Android.Support.V4.Content.ContextCompat.GetDrawable(Context, Resource.Drawable.group_indicator));

            FnClickEvents();

            return view;

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void FnClickEvents()
        {
            //Listening to child item selection
            expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
            {
                Toast.MakeText(Context, "ChildClick", ToastLength.Short).Show();
            };

            //Listening to group expand
            //modified so that on selection of one group other opened group has been closed
            expListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e)
            {
                Toast.MakeText(Context, "GroupExpand", ToastLength.Short).Show();
                if (e.GroupPosition != previousGroup)
                    expListView.CollapseGroup(previousGroup);
                previousGroup = e.GroupPosition;
            };

            //Listening to group collapse
            expListView.GroupCollapse += delegate (object sender, ExpandableListView.GroupCollapseEventArgs e)
            {
                Toast.MakeText(Context, "GroupCollapse", ToastLength.Short).Show();
            };
        }

        private void FnGetListData()
        {
            listDataHeader = new List<string>();
            listDataChild = new Dictionary<string, List<string>>();

            // Adding child data
            listDataHeader.Add("Computer science");
            listDataHeader.Add("Electrocs & comm.");
            listDataHeader.Add("Mechanical");
            listDataHeader.Add("Mechateronics");
            listDataHeader.Add("NeuroScience");




            // Adding child data
            var list1 = new List<string>();
            var list2 = new List<string>();
            var list3 = new List<string>();
            var list4 = new List<string>();
            var list5 = new List<string>();

            for (int i = 0; i < 50; i++)
            {
                list1.Add(i.ToString());
                list2.Add(Math.IEEERemainder(i, 5).ToString());
                list3.Add(Math.IEEERemainder(i, 10).ToString());
                list4.Add(new Random().NextDouble().ToString());
            }



            // Header, Child data
            listDataChild.Add(listDataHeader[0], list1);
            listDataChild.Add(listDataHeader[1], list2);
            listDataChild.Add(listDataHeader[2], list3);
            listDataChild.Add(listDataHeader[3], list4);
            listDataChild.Add(listDataHeader[4], list5);
        }
    }
}