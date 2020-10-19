using Android.Database.Sqlite;

namespace ConsumoApi
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string senha { get; set; }
        public int funcaoId { get; set; }
    }
}