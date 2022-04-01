using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Colonia
    {
        [Required(ErrorMessage = "Seleccione la Colonia")]
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public ML.Municipio Municipio { get; set; }
        public List<object> Colonias { get; set; }

        public void Saludar(string nombre)
        {

        }
    }

}

