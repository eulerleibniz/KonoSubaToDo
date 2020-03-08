using Android.OS;
using Android.Views;
using System;

namespace KonoAndroid
{
    public class FragmentToday : Android.Support.V4.App.Fragment
    {
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
            return inflater.Inflate(Resource.Layout.fragment_today, container, false);

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}