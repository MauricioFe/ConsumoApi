using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ConsumoApi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class RestActivity : AppCompatActivity
    {
        List<Usuario> usuarioList = new List<Usuario>();
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_rest);
            GetUsuariosAsync();
            listView = FindViewById<ListView>(Resource.Id.list_rest_usuario);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            MenuInflater inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.adicionar_dados)
            {
                StartActivity(new Intent(this, typeof(CadatrarUsuarioActivity)));
            }
            return base.OnOptionsItemSelected(item);
        }
        private async void GetUsuariosAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://192.168.0.109:5000/api/usuarios");
                var content = await response.Content.ReadAsStringAsync();
                var parseJson = new DataContractJsonSerializer(typeof(List<Usuario>));
                MemoryStream memory = new MemoryStream(Encoding.UTF8.GetBytes(content));
                usuarioList = (List<Usuario>)parseJson.ReadObject(memory);

                UsuarioAdapter adapter = new UsuarioAdapter(this, usuarioList);

                listView.Adapter = adapter;
            }

        }
    }
}