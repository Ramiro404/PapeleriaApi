using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;
using System.Security.Claims;

namespace PapeleriaApi.Servicios
{
    
    public class Jwt
    {
		public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public string? Token { get; set; }
    }
}
