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
            listAdapter = new ExpandableListAdapter(Activity, listDataHeader, listDataChild);
            expListView.SetAdapter(listAdapter);

            FnClickEvents();

            return view;

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void FnClickEvents()
        {
            //Listening to child item selection
            expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
            {
                Toast.MakeText(Context, "child clicked", ToastLength.Short).Show();
            };

            //Listening to group expand
            //modified so that on selection of one group other opened group has been closed
            expListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e)
            {
                if (e.GroupPosition != previousGroup)
                    expListView.CollapseGroup(previousGroup);
                previousGroup = e.GroupPosition;
            };

            //Listening to group collapse
            expListView.GroupCollapse += delegate (object sender, ExpandableListView.GroupCollapseEventArgs e)
            {
                Toast.MakeText(Context, "group collapsed", ToastLength.Short).Show();
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
            var lstCS = new List<string>
            {
                "Data structure",
                "C# Programming",
                "Java programming",
                "ADA",
                "Operation reserach",
                "OOPS with C",
                "Data structure",
                "C# Programming",
                "Java programming",
                "ADA",
                "Operation reserach",
                "OOPS with C",
                "Data structure",
                "C# Programming",
                "Java programming",
                "ADA",
                "Operation reserach",
                "OOPS with C",
                "Data structure",
                "C# Programming",
                "Java programming",
                "ADA",
                "Operation reserach",
                "OOPS with C",
                "Data structure",
                "C# Programming",
                "Java programming",
                "ADA",
                "Operation reserach",
                "OOPS with C",
                "C++ Programming"
            };

            var lstEC = new List<string>
            {
                "Field Theory",
                "Logic Design",
                "Analog electronics",
                "Network analysis",
                "Micro controller",
                "Field Theory",
                "Logic Design",
                "Analog electronics",
                "Network analysis",
                "Micro controller",
                "Field Theory",
                "Logic Design",
                "Analog electronics",
                "Network analysis",
                "Micro controller",
                "Field Theory",
                "Logic Design",
                "Analog electronics",
                "Network analysis",
                "Micro controller",
                "Field Theory",
                "Logic Design",
                "Analog electronics",
                "Network analysis",
                "Micro controller",
                "Signals and system"
            };

            var lstMech = new List<string>
            {
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Instrumentation technology",
                "Dynamics of machinnes",
                "Energy engineering",
                "Design of machine",
                "Turbo machine",
                "Energy conversion"
            };

            // Header, Child data
            listDataChild.Add(listDataHeader[0], lstCS);
            listDataChild.Add(listDataHeader[1], lstEC);
            listDataChild.Add(listDataHeader[2], lstMech);
            listDataChild.Add(listDataHeader[3], lstEC);
            listDataChild.Add(listDataHeader[4], lstMech);
        }
    }
}