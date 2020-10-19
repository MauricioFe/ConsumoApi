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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Util;
using Org.Apache.Http.Conn;

namespace ConsumoApi
{
    [Activity(Label = "CadatrarUsuarioActivity")]
    public class CadatrarUsuarioActivity : Activity
    {
        EditText edtNome;
        EditText edtEmail;
        EditText edtTelefone;
        EditText edtSenha;
        Spinner spnfuncao;
        Button btnSalvar;
        int idFuncao = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_cadastrar_usuario);
            edtNome = FindViewById<EditText>(Resource.Id.cadastrar_usuario_edt_nome);
            edtEmail = FindViewById<EditText>(Resource.Id.cadastrar_usuario_edt_email);
            edtTelefone = FindViewById<EditText>(Resource.Id.cadastrar_usuario_edt_telefone);
            edtSenha = FindViewById<EditText>(Resource.Id.cadastrar_usuario_edt_senha);
            spnfuncao = FindViewById<Spinner>(Resource.Id.cadastrar_usuario_spinner);
            btnSalvar = FindViewById<Button>(Resource.Id.cadastrar_usuario_btn_salvar);
            List<string> funcaoList = new List<string>();
            funcaoList.Add("Administrador");
            funcaoList.Add("Usuário");
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, funcaoList);
            spnfuncao.Adapter = adapter;
            spnfuncao.ItemSelected += Spinner_ItemSelected;

            string jsonUsuario = Intent.GetStringExtra("usuario");
            
            if (jsonUsuario == null)
            {

                btnSalvar.Click += delegate
                {
                    Usuario usuario = new Usuario();
                    usuario.nome = edtNome.Text;
                    usuario.email = edtEmail.Text;
                    usuario.telefone = edtTelefone.Text;
                    usuario.senha = edtSenha.Text;
                    usuario.funcaoId = idFuncao;
                    CadatrarUsuario(usuario);
                };
            }
            else
            {
                var parseJson = new DataContractJsonSerializer(typeof(Usuario));
                MemoryStream memory = new MemoryStream(Encoding.UTF8.GetBytes(jsonUsuario));
                Usuario usuarioEditar = (Usuario)parseJson.ReadObject(memory);
                edtNome.Text = usuarioEditar.nome;
                edtEmail.Text = usuarioEditar.email;
                edtTelefone.Text = usuarioEditar.telefone;
                edtSenha.Text = usuarioEditar.senha;
                btnSalvar.Click += delegate
                {
                    Usuario usuario = new Usuario();
                    usuario.id = usuarioEditar.id;
                    usuario.nome = edtNome.Text;
                    usuario.email = edtEmail.Text;
                    usuario.telefone = edtTelefone.Text;
                    usuario.senha = edtSenha.Text;
                    usuario.funcaoId = idFuncao;
                    EditarUsuario(usuario);
                };
            }


        }

        private async void EditarUsuario(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                var parseJson = new DataContractJsonSerializer(typeof(Usuario));
                MemoryStream memory = new MemoryStream();
                parseJson.WriteObject(memory, usuario);
                var jsonString = Encoding.UTF8.GetString(memory.ToArray());
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"http://192.168.0.109:5000/api/usuarios/{usuario.id}", content);
                if (response.IsSuccessStatusCode)
                {
                    StartActivity(new Intent(this, typeof(RestActivity)));
                }
                else
                {
                    Toast.MakeText(this, "Erro ao editar usuário", ToastLength.Long);
                }
            }
        }

        private async void CadatrarUsuario(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                var parseJson = new DataContractJsonSerializer(typeof(Usuario));
                MemoryStream memory = new MemoryStream();
                parseJson.WriteObject(memory, usuario);
                var jsonString = Encoding.UTF8.GetString(memory.ToArray());
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://192.168.0.109:5000/api/usuarios", content);

                if (response.IsSuccessStatusCode)
                {
                    StartActivity(new Intent(this, typeof(RestActivity)));
                }
                else
                {
                    Toast.MakeText(this, "Erro ao cadastrar usuário", ToastLength.Long);
                }
            }
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            idFuncao = e.Position + 1;
        }
    }
}