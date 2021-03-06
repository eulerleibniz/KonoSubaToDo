﻿using Android.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

// TODO: remember to check for empty lists and expandable category and stuff

namespace KonoAndroid
{
    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        #region Data
        private readonly Activity _context;
        private class GroupViewItem : Java.Lang.Object
        {
            public GroupViewItem(int position)
            {
                Position = position;
            }
            public Guid Guid { get; } = Guid.NewGuid(); // C# 6 or higher
            public int Position { get; set; }
            public string Text { get; set; }
            public bool Checked { get; set; }
        }
        private class ChildViewItem : Java.Lang.Object
        {
            public ChildViewItem(int groupPosition, int childPosition)
            {
                Position = (groupPosition, childPosition);
            }
            public Guid Guid { get; } = Guid.NewGuid(); // C# 6 or higher
            public (int, int) Position { get; set; }
            public string Text { get; set; }
            public bool Checked { get; set; }
        }
        private readonly List<GroupViewItem> listGroupViewItem;
        private readonly List<ChildViewItem> listChildViewItem;
        #endregion

        #region Public Methods
        public ExpandableListAdapter(Activity context)
        {
            listGroupViewItem = new List<GroupViewItem>();
            listChildViewItem = new List<ChildViewItem>();
            _context = context;
        }

        public void AddHeader(string text, bool isCheked)
        {
            GroupViewItem groupViewItem = new GroupViewItem(listGroupViewItem.Count)
            {
                Text = text,
                Checked = isCheked
            };
            listGroupViewItem.Add(groupViewItem);
        }

        public void AddChild(string text, bool isCheked, int parentPosition)
        {
            ChildViewItem childViewItem = new ChildViewItem(parentPosition, listChildViewItem.Count(x => x.Position.Item1 == parentPosition))
            {
                Text = text,
                Checked = isCheked
            };
            listChildViewItem.Add(childViewItem);
        }


        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            // Find doesn't throw exeption when condition returns a Null List
            // instead it returns null on result
            // Using First we get an Exception when condition returns a Null List
            ChildViewItem childViewItem = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition)));
            if (childViewItem == null)
            {
                return null;
            }
            else
            {
                return childViewItem;
            }
        }
        #endregion

        #region overrides
        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.custom_layout_list_item, null);
            }
            CheckBox checkBoxChild = (CheckBox)convertView.FindViewById(Resource.Id.ExpandableListView_Child_CheckBox);
            TextView textViewChild = (TextView)convertView.FindViewById(Resource.Id.ExpandableListView_Child_TextView);

            // Remove Events
            checkBoxChild.CheckedChange -= CheckBoxChild_CheckedChange;


            checkBoxChild.Checked = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition))).Checked;
            textViewChild.Text = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition))).Text;

            listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition))).Checked = checkBoxChild.Checked;
            listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition))).Text = textViewChild.Text;

            checkBoxChild.Tag = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition)));
            //textViewChild.Tag = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition)));
            convertView.Tag = listChildViewItem.Find(x => (x.Position == (groupPosition, childPosition)));

            // Add Events Again
            checkBoxChild.CheckedChange += CheckBoxChild_CheckedChange;

            NotifyDataSetChanged();
            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return listChildViewItem.Count(x => x.Position.Item1 == groupPosition);
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            // Find doesn't throw exeption when condition returns a Null List
            // instead it returns null on result
            // Using First we get an Exception when condition returns a Null List
            GroupViewItem groupViewItem = listGroupViewItem.Find(x => x.Position == groupPosition);
            if (groupViewItem == null)
            {
                return null;
            }
            else
            {
                return groupViewItem;
            }
        }

        public override int GroupCount
        {
            get
            {
                return listGroupViewItem.Count;
            }
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.custom_layout_list_header, null);
            }


            CheckBox checkBoxGroup = (CheckBox)convertView.FindViewById(Resource.Id.ExpandableListView_Header_CheckBox);
            TextView textViewGroup = (TextView)convertView.FindViewById(Resource.Id.ExpandableListView_Header_TextView);

            // Remove Events
            checkBoxGroup.CheckedChange -= CheckBoxGroup_CheckedChange;

            checkBoxGroup.Checked = listGroupViewItem[groupPosition].Checked;
            textViewGroup.Text = listGroupViewItem[groupPosition].Text;

            listGroupViewItem[groupPosition].Checked = checkBoxGroup.Checked;
            listGroupViewItem[groupPosition].Text = textViewGroup.Text;

            checkBoxGroup.Tag = listGroupViewItem[groupPosition];
            //textViewGroup.Tag = listGroupViewItem[groupPosition];

            // Add Events Again
            checkBoxGroup.CheckedChange += CheckBoxGroup_CheckedChange;

            NotifyDataSetChanged();
            return convertView;
        }


        public override bool HasStableIds
        {
            get
            {
                return false; // was default
                //return true;
            }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }
        #endregion

        #region Event Handlers
        private void CheckBoxGroup_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            GroupViewItem groupViewItem = (GroupViewItem)(checkBox.Tag);
            listGroupViewItem.Find(x => (x.Position == groupViewItem.Position)).Checked = checkBox.Checked;
            //NotifyDataSetChanged();
            System.Diagnostics.Debug.WriteLine("CheckBoxGroup_CheckedChange");
        }
        private void CheckBoxChild_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            ChildViewItem childViewItem = (ChildViewItem)(checkBox.Tag);

            listChildViewItem.Find(x => (x.Position == childViewItem.Position)).Checked = checkBox.Checked;
            //NotifyDataSetChanged();

            System.Diagnostics.Debug.WriteLine("CheckBoxChild_CheckedChange");
        }
        
        #endregion
    }
}