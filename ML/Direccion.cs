using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [Required(ErrorMessage="Ingrese la Calle")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "Ingrese el Numero Exterior")]

        public string NumeroExterior { get; set; }
        [Required(ErrorMessage = "Ingrese el Numero Interior")]

        public string NumeroInterior { get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
