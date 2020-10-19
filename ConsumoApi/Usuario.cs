using Android.Database.Sqlite;

namespace ConsumoApi
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int FuncaoId { get; set; }
    }
}