using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var polizas = context.PolizaGetAll().ToList();
                    if (polizas != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objPoliza in polizas)
                        {
                            ML.Poliza poliza = new ML.Poliza();
                            poliza.IdPoliza = objPoliza.IdPoliza;
                            poliza.Nombre = objPoliza.NombrePoliza;
                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = objPoliza.IdSubPoliza.Value;
                            poliza.SubPoliza.Nombre = objPoliza.NombreSubPoliza;
                            poliza.NumeroPoliza = objPoliza.NumeroPoliza;
                            poliza.FechaCreacion = objPoliza.FechaCreacion.Value.ToString();
                            poliza.FechaModificacion = objPoliza.FechaModificacion.Value.ToString();
                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = objPoliza.IdUsuario.Value;
                            poliza.Usuario.UserName = objPoliza.UserName;

                            result.Objects.Add(poliza);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener los datos de la Tabla Poliza";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objPoliza = context.PolizaGetById(IdPoliza).First();
                    if (objPoliza != null)
                    {
                        ML.Poliza poliza = new ML.Poliza();
                        poliza.IdPoliza = objPoliza.IdPoliza;
                        poliza.Nombre = objPoliza.NombrePoliza;
                        poliza.SubPoliza = new ML.SubPoliza();
                        poliza.SubPoliza.IdSubPoliza = objPoliza.IdSubPoliza.Value;
                        poliza.SubPoliza.Nombre = objPoliza.NombreSubPoliza;
                        poliza.NumeroPoliza = objPoliza.NumeroPoliza;
                        poliza.FechaCreacion = objPoliza.FechaCreacion.Value.ToString();
                        poliza.FechaModificacion = objPoliza.FechaModificacion.Value.ToString();
                        poliza.Usuario = new ML.Usuario();
                        poliza.Usuario.IdUsuario = objPoliza.IdUsuario.Value;
                        poliza.Usuario.UserName = objPoliza.UserName;
                        result.Object = poliza;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener la Poliza";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.PolizaAdd(poliza.Nombre, poliza.SubPoliza.IdSubPoliza, poliza.NumeroPoliza, poliza.Usuario.IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al Agregar la Poliza en la tabla Poliza";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.PolizaUpdate(poliza.IdPoliza, poliza.Nombre, poliza.SubPoliza.IdSubPoliza, poliza.NumeroPoliza, poliza.Usuario.IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al actualizar la poliza";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.PolizaDelete(IdPoliza);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar la Poliza";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }

}
