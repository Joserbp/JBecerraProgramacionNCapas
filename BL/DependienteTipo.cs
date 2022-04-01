using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var dependiestesTipo = context.DependienteTipoGetAll().ToList();
                    if (dependiestesTipo != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDependienteTipo in dependiestesTipo)
                        {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();
                            dependienteTipo.IdDependienteTipo = objDependienteTipo.IdDependienteTipo;
                            dependienteTipo.Nombre = objDependienteTipo.Nombre;

                            result.Objects.Add(dependienteTipo);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener los registros de la Tabla DependienteTipo";
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
