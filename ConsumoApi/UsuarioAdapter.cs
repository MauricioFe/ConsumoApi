using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Org.Apache.Http.Conn;

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
            return usuarioList[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = LayoutInflater.From(context).Inflate(Resource.Layout.usuario_item_list, parent, false);

            TextView txtNome = convertView.FindViewById<TextView>(Resource.Id.item_list_nome);
            TextView txtTelefone = convertView.FindViewById<TextView>(Resource.Id.item_list_telefone);
            ImageView btnEdit = convertView.FindViewById<ImageView>(Resource.Id.item_list_edit);
            ImageView btnDelete = convertView.FindViewById<ImageView>(Resource.Id.item_list_delete);


            txtNome.Text = usuarioList[position].nome;
            txtTelefone.Text = usuarioList[position].telefone;
            btnEdit.Click += delegate
            {
                Intent intent = new Intent(context, typeof(CadatrarUsuarioActivity));
                var parseJson = new DataContractJsonSerializer(typeof(Usuario));
                MemoryStream memory = new MemoryStream();
                parseJson.WriteObject(memory, usuarioList[position]);
                var jsonString = Encoding.UTF8.GetString(memory.ToArray());
                intent.PutExtra("usuario", jsonString);
                context.StartActivity(intent);

            };

            btnDelete.Click += delegate
            {
                new AlertDialog.Builder(context).SetTitle("Excluindo dados").SetMessage("Você realmente deseja excluir esse registro?")
                .SetPositiveButton("Sim", (sender, events) =>
                {
                    DeletarUsuario(GetItemId(position));
                })
                .SetNegativeButton("Não", (sender, events) =>
                {

                })
                .Show();
            };
            return convertView;
        }

        private async void DeletarUsuario(long id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://192.168.0.109:5000/api/usuarios/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(context, "Exluído com sucesso", ToastLength.Short);
                    var index = usuarioList.FindIndex(u => u.id == id);
                    usuarioList.RemoveAt(index);
                    this.NotifyDataSetChanged();
                }
            }
        }
    }
}