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
    [Activity(Label = "CadatrarUsuarioActivity")]
    public class CadatrarUsuarioActivity : Activity
    {
        EditText edtNome;
        EditText edtEmail;
        EditText edtTelefone;
        EditText edtSenha;
        Spinner spnfuncao;
        Button btnSalvar;
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

        }
    }
}