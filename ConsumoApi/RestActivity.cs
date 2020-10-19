using System.Collections.Generic;
using System.Net.Http;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Web;
using Newtonsoft.Json;

namespace ConsumoApi
{
    [Activity(Label = "RestActivity")]
    public class RestActivity : Activity
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

        private async void GetUsuariosAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://192.168.0.109:5000/api/usuarios");
                var content = await response.Content.ReadAsStringAsync();

                usuarioList = JsonConvert.DeserializeObject<List<Usuario>>(content);

                UsuarioAdapter adapter = new UsuarioAdapter(this, usuarioList);

                listView.Adapter = adapter;
            }

        }
    }
}