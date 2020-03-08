using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace KonoAndroid
{
    public class FragmentNext7Days : Android.Support.V4.App.Fragment, ThreeLevelListAdapter.ThreeLevelListViewListener
    {
        private ExpandableListView expandableListView;

        private List<string> parent = new List<string>() { "group 1", "group 2" };
        private string[] q1 = new string[] { "Child Level 1", "Child level 2" };
        private string[] q2 = new string[] { "Child Level 1B", "Child Level 2B" };
        private string[] q3 = new string[] { "Child Level 1C" };
        private string[] des1 = new string[] { "A", "B", "C" };
        private string[] des2 = new string[] { "D", "E", "F" };
        private string[] des3 = new string[] { "G" };
        private string[] des4 = new string[] { "H", "J" };
        private string[] des5 = new string[] { "U.", " R", " V" };

        private Dictionary<string, string[]> thirdLevelq1 = new Dictionary<string, string[]>();
        private Dictionary<string, string[]> thirdLevelq2 = new Dictionary<string, string[]>();
        private Dictionary<string, string[]> thirdLevelq3 = new Dictionary<string, string[]>();
        /**
         * Second level array list
         */
        private List<string[]> secondLevel = new List<string[]>();
        /**
         * Inner level data
         */
        private List<Dictionary<string, string[]>> data = new List<Dictionary<string, string[]>>();

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
            View view;
            view = inflater.Inflate(Resource.Layout.fragment_next_7_days, container, false);
            SetUpAdapter(view, container, savedInstanceState);
            return view;

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void OnFinalChildClick(int plpos, int slpos, int tlpos)
        {
            throw new NotImplementedException();
        }

        public void OnFinalItemClick(string plItem, string slItem, string tlItem)
        {
            throw new NotImplementedException();
        }

        private void SetUpAdapter(View view, ViewGroup container, Bundle savedInstanceState)
        {
            secondLevel.Add(q1);
            secondLevel.Add(q2);
            secondLevel.Add(q3);
            thirdLevelq1.Add(q1[0], des1);
            thirdLevelq1.Add(q1[1], des2);
            thirdLevelq2.Add(q2[0], des3);
            thirdLevelq2.Add(q2[1], des4);
            thirdLevelq3.Add(q3[0], des5);

            data.Add(thirdLevelq1);
            data.Add(thirdLevelq2);
            data.Add(thirdLevelq3);
            expandableListView = (ExpandableListView)view.FindViewById(Resource.Id.expandible_listview);
            //passing three level of information to constructor
            ThreeLevelListAdapter threeLevelListAdapterAdapter = new ThreeLevelListAdapter(view.Context, parent, secondLevel, data, this);
            expandableListView.SetAdapter(threeLevelListAdapterAdapter);

            expandableListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e)
            {
                int previousGroup = -1;
                if (e.GroupPosition != previousGroup)
                    expandableListView.CollapseGroup(previousGroup);
                previousGroup = e.GroupPosition;
            };

            // ExpandableListView on child click listener
        }
    }
}