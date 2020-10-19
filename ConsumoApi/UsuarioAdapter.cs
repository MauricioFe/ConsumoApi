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
    class UsuarioAdapter : BaseAdapter<Usuario>
    {
        Context context;
        List<Usuario> usuarioList;
        public UsuarioAdapter(Context context, List<Usuario> usuarioList)
        {
            this.context = context;
            this.usuarioList = usuarioList;
        }

        public override Usuario this[int position] => usuarioList[position];

        public override int Count => usuarioList.Count;

        public override long GetItemId(int position)
        {
            return usuarioList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
           convertView = LayoutInflater.From(context).Inflate(Resource.Layout.)
        }
    }
}