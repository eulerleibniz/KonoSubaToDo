using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace KonoAndroid
{
    internal class ThreeLevelListAdapter : BaseExpandableListAdapter, ExpandableListView.IOnChildClickListener
    {
        private Context context;
        private List<string> parentHeaders; // header titles
        private List<string[]> secondLevel;
        private List<Dictionary<string, string[]>> data;
        public ThreeLevelListViewListener mThreeLevelListViewListener;

        public ThreeLevelListAdapter(Context context, List<string> listDataHeader, List<string[]> secondLevel,
                             List<Dictionary<string, string[]>> data,
                             ThreeLevelListViewListener listener)
        {
            this.context = context;
            parentHeaders = listDataHeader;
            this.secondLevel = secondLevel;
            this.data = data;
            mThreeLevelListViewListener = listener;
        }

        public override int GroupCount
        {
            get
            {
                return parentHeaders.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
                // TODO: WTF is this?
                throw new NotImplementedException();
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return childPosition;
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            // TODO: WTF is this?
            SecondLevelExpandableListView secondLevelELV = new SecondLevelExpandableListView(this.context);
            string[] headers = secondLevel[groupPosition];
            List<string[]> childData = new List<string[]>();
            Dictionary<string, string[]> secondLevelData = data[groupPosition];
            foreach (var key in secondLevelData.Keys)
            {
                childData.Add(secondLevelData[key]);
            }
            secondLevelELV.SetAdapter(new SecondLevelAdapter(context, headers, childData));
            secondLevelELV.SetGroupIndicator(null);

            secondLevelELV.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e)
            {
                int previousGroup = -1;
                if (groupPosition != previousGroup)
                    secondLevelELV.CollapseGroup(previousGroup);
                previousGroup = groupPosition;
            };

            secondLevelELV.Tag = groupPosition;

            return secondLevelELV;

            throw new NotImplementedException();
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return parentHeaders[groupPosition];
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string a = this.parentHeaders[groupPosition];
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            convertView = inflater.Inflate(Resource.Layout.row_first, null);
            TextView text = (TextView)convertView.FindViewById(Resource.Id.row_parent_text);
            text.Text = parentHeaders[groupPosition];
            return convertView;
            // TODO: WTF is this?
            throw new NotImplementedException();
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            // TODO: WTF is this?
            return true;
            throw new NotImplementedException();
        }

        public interface ThreeLevelListViewListener
        {
            void OnFinalChildClick(int plpos, int slpos, int tlpos);

            void OnFinalItemClick(string plItem, string slItem, string tlItem);
        }

        public bool OnChildClick(ExpandableListView parent, View clickedView, int groupPosition, int childPosition, long id)
        {
            int ppos = (int)parent.Tag;
            mThreeLevelListViewListener.OnFinalChildClick(ppos, groupPosition, childPosition);
            string plItem = (string)GetGroup(ppos);
            SecondLevelAdapter adapter = (SecondLevelAdapter)parent.ExpandableListAdapter;
            string slItem = (string)adapter.GetGroup(groupPosition);
            string tlItem = (string)adapter.GetChild(groupPosition, childPosition);
            mThreeLevelListViewListener.OnFinalItemClick(plItem, slItem, tlItem);
            return true;

            throw new NotImplementedException();
        }
    }
}