using Microsoft.AspNetCore.Identity;
using PapeleriaApi.Utilidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PapeleriaApi.Modelos
{
    public class Usuario: IdentityUser
    {
        public Usuario() { 
            this.OrdenesDeCompra = new HashSet<OrdenDeCompra>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        [MaxLength(6)]
        public string Codigo { get; set; }
        //public string PasswordHash
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = BCrypt.Net.BCrypt.HashPassword(value); }
		public override string Email { get => base.Email; set => base.Email = value; }
		public string Telefono { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenDeCompra> OrdenesDeCompra { get; set; }

        public Nullable<int> RolId { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
