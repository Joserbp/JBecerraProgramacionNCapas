using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubPoliza
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var subPolizas = context.SubPolizaGetAll().ToList();
                    if (subPolizas != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var objPoliza in subPolizas)
                        {
                            ML.SubPoliza subPoliza = new ML.SubPoliza();
                            subPoliza.IdSubPoliza = objPoliza.IdSubPoliza;
                            subPoliza.Nombre = objPoliza.Nombre;

                            result.Objects.Add(subPoliza);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener los registros de la Tabla SubPoliza";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
