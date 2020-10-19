using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ConsumoApi
{
    [Activity(Label = "RestActivity")]
    public class RestActivity : Activity
    {
        List<Usuario> usuarioList = new List<Usuario>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rest);
            ListView listView = FindViewById<ListView>(Resource.Id.list_rest_usuario);
        }
    }
}