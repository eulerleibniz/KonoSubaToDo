using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace KonoAndroid
{
    internal class SecondLevelAdapter : BaseExpandableListAdapter
    {
        private Context context;

        private List<string[]> data;

        private string[] headers;

        private ImageView ivGroupIndicator;

        public SecondLevelAdapter(Context context, string[] headers, List<string[]> data)
        {
            this.context = context;
            this.data = data;
            this.headers = headers;
        }

        public override int GroupCount
        {
            get
            {
                return headers.Length;
                throw new NotImplementedException();
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
                throw new NotImplementedException();
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            string[] childData;

            childData = data[groupPosition];

            return childData[childPosition];

            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
            throw new NotImplementedException();
        }

        public override int GetChildrenCount(int groupPosition)
        {
            string[] children = data[groupPosition];

            return children.Length;

            throw new NotImplementedException();
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            convertView = inflater.Inflate(Resource.Layout.row_third, null);

            TextView textView = (TextView)convertView.FindViewById(Resource.Id.row_third_text);

            string[] childArray = data[groupPosition];

            string text = childArray[childPosition];

            textView.Text = text;

            return convertView;

            throw new NotImplementedException();
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return headers[groupPosition];
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
            throw new NotImplementedException();
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            convertView = inflater.Inflate(Resource.Layout.row_second, null);
            TextView text = (TextView)convertView.FindViewById(Resource.Id.row_second_text);
            string groupText = GetGroup(groupPosition).ToString();
            text.Text = groupText;
            return convertView;
            throw new NotImplementedException();
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
            throw new NotImplementedException();
        }
    }
}