
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage="Ingrese el Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el Apellido Paterno")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Ingrese el email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingrese la Fecha de Nacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Ingrese un UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Seleccione el sexo")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "Ingrese un Numero Telefonico")]
        public string Telefono { get; set; }
        public string Celular { get; set; }
        [Required(ErrorMessage = "Ingrese el CURP")]
        public string CURP { get; set; }
        public ML.Direccion Direccion { get; set; }
        public byte[] Imagen { get; set; }
        [Required(ErrorMessage = "Seleccione un rol")]
        public ML.Rol Rol { get; set; }
        [Required(ErrorMessage = "Ingrese un password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,15}$",ErrorMessage = "Ingrese un password valido")]
        public string Password { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public List<object> Usuarios { get; set; }

    }
}
