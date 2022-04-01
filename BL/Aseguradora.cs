using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BL
{
    public class Aseguradora
    {
       public static ML.Result GetAllEF()
       {
           ML.Result result = new ML.Result();
           try
           {
               using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
               {
                   var objAseguradoras = context.AseguradoraGetAll().ToList();
                   if (objAseguradoras != null)
                   {
                       result.Objects = new List<Object>();
                       foreach (var obj in objAseguradoras)
                       {
                           ML.Aseguradora aseguradora = new ML.Aseguradora();
                           aseguradora.IdAseguradora = obj.IdAseguradora;
                           aseguradora.Nombre = obj.AseguradoraNombre;
                           aseguradora.FechaCreacion = obj.FechaCreacion.Value.ToString();
                           aseguradora.FechaModificacion = obj.FechaModificacion.Value.ToString();
                           aseguradora.Imagen = obj.Imagen;
                           aseguradora.Usuario = new ML.Usuario();
                           aseguradora.Usuario.IdUsuario = obj.IdUsuario;
                           aseguradora.Usuario.UserName = obj.nombreUsuario;

                           result.Objects.Add(aseguradora);
                       }
                       result.Correct = true;
                   }
                   else
                   {
                       result.Correct = false;
                       result.ErrorMessage = "No se encontraron registros en la tabla Aseguradora";
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
       public static ML.Result GetByIdEF(int idAseguradora)
       {
           ML.Result result = new ML.Result();
           try
           {
               using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
               {
                   var objAseguradora = context.AseguradoraGetById(idAseguradora).FirstOrDefault();
                   if(objAseguradora != null)
                   {
                       ML.Aseguradora aseguradora = new ML.Aseguradora();
                       aseguradora.IdAseguradora = objAseguradora.IdAseguradora;
                       aseguradora.Nombre = objAseguradora.AseguradoraNombre;
                       aseguradora.FechaCreacion = objAseguradora.FechaCreacion.Value.ToString();
                       aseguradora.FechaModificacion = objAseguradora.FechaModificacion.Value.ToString();
                       aseguradora.Imagen = objAseguradora.Imagen;
                       aseguradora.Usuario = new ML.Usuario();
                       aseguradora.Usuario.IdUsuario = objAseguradora.IdUsuario.Value;

                       result.Object = aseguradora;
                       result.Correct = true;
                   }
                   else
                   { 
                       result.Correct = false;
                       result.ErrorMessage = "No se encontro la aseguradora";
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
       public static ML.Result UpdateByIdEF(ML.Aseguradora aseguradora)
       {
           ML.Result result = new ML.Result();
           try
           {
               using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
               {
                   var query = context.AseguradoraUpdateById(aseguradora.IdAseguradora,aseguradora.Nombre,aseguradora.Imagen,aseguradora.Usuario.IdUsuario);
                   if (query >= 1)
                   {
                       result.Correct = true;
                   }
                   else
                   {
                       result.Correct = false;
                       result.ErrorMessage = "Error al actualizar la aseguradora";
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
        public static ML.Result DeleteEF(int IdAseguradora)
       {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.AseguradoraDeleteById(IdAseguradora);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar la aseguradora";
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
        public static ML.Result AddEF(ML.Aseguradora aseguradora)
       {
           ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.AseguradoraAdd(aseguradora.Nombre,aseguradora.Imagen, aseguradora.Usuario.IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar la aseguradora";
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
