using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var empresas = context.EmpresaGetAll().ToList();

                    if(empresas != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objEmpresa in empresas)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = objEmpresa.IdEmpresa;
                            empresa.Nombre = objEmpresa.Nombre;
                            empresa.Telefono = objEmpresa.Telefono;
                            empresa.Email = objEmpresa.Email;
                            empresa.DireccionWeb = objEmpresa.DireccionWeb;
                            empresa.Logo = objEmpresa.Logo;

                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Empresa";
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
        public static ML.Result GetByID(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objEmpresa = context.EmpresaGetById(IdEmpresa).FirstOrDefault();
                    if(objEmpresa != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = objEmpresa.IdEmpresa;
                        empresa.Nombre = objEmpresa.Nombre;
                        empresa.Telefono = objEmpresa.Telefono;
                        empresa.Email = objEmpresa.Email;
                        empresa.DireccionWeb = objEmpresa.DireccionWeb;
                        empresa.Logo = objEmpresa.Logo;

                        result.Object = empresa;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro en la tabla Empresa";
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
        public static ML.Result UpdateById(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objEmpresa = context.EmpresaUpdateById(empresa.IdEmpresa,empresa.Nombre,empresa.Telefono,empresa.Email,empresa.DireccionWeb,empresa.Logo);
                    if(objEmpresa >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro registro en la tabla Empresa";
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
        public static ML.Result DeleteById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.EmpresaDeleteById(IdEmpresa);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminiar no se encontro el registro en la tabla Empresa";
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
        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.EmpresaAdd(empresa.Nombre, empresa.Telefono, empresa.Email, empresa.DireccionWeb, empresa.Logo);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar la empresa en la tabla Empresa";
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
