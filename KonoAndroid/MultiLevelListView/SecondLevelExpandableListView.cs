using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace KonoAndroid
{
    public class SecondLevelExpandableListView : ExpandableListView
    {
        public SecondLevelExpandableListView(Context context) : base(context)
        {
        }

        public SecondLevelExpandableListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public SecondLevelExpandableListView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public SecondLevelExpandableListView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected SecondLevelExpandableListView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            heightMeasureSpec = MeasureSpec.MakeMeasureSpec(999999, MeasureSpecMode.AtMost);

            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }
    }
}