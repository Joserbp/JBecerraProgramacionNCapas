using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var empleados = context.EmpleadoGetAll().ToList();
                    if (empleados != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objEmpleado in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
                            empleado.RFC = objEmpleado.RFC;
                            empleado.Nombre = objEmpleado.NombreEmpleado;
                            empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                            empleado.ApellidoMaterno = objEmpleado.ApellidoPaterno;
                            empleado.Email = objEmpleado.Email;
                            empleado.Telefono = objEmpleado.Telefono;
                            empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.NSS = objEmpleado.NSS;
                            empleado.FechaIngreso = objEmpleado.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Foto = objEmpleado.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
                            empleado.Empresa.Nombre = objEmpleado.NombreEmpresa;
                            empleado.Poliza = new ML.Poliza();
                            if (objEmpleado.IdPoliza != null)
                            {
                                empleado.Poliza.IdPoliza = objEmpleado.IdPoliza.Value;
                            }
                            else
                            {
                                empleado.Poliza.IdPoliza = 0;
                            }

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener los registros de la tabla Empleado";
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
        public static ML.Result GetByNumeroEmpleado(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objEmpleado = context.EmpleadoGetByNumeroEmpreado(numeroEmpleado).FirstOrDefault();
                    if (objEmpleado != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
                        empleado.RFC = objEmpleado.RFC;
                        empleado.Nombre = objEmpleado.NombreEmpleado;
                        empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                        empleado.ApellidoMaterno = objEmpleado.ApellidoPaterno;
                        empleado.Email = objEmpleado.Email;
                        empleado.Telefono = objEmpleado.Telefono;
                        empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        empleado.NSS = objEmpleado.NSS;
                        empleado.FechaIngreso = objEmpleado.FechaIngreso.ToString("dd-MM-yyyy");
                        empleado.Foto = objEmpleado.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
                        empleado.Empresa.Nombre = objEmpleado.NombreEmpresa;
                        empleado.Poliza = new ML.Poliza();
                        if (objEmpleado.IdPoliza != null)
                        {
                            empleado.Poliza.IdPoliza = objEmpleado.IdPoliza.Value;
                        }
                        else
                        {
                            empleado.Poliza.IdPoliza = 0;
                        }

                        result.Object = empleado;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener la informacion de la tabla Empleado";
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
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.EmpleadoAdd(empleado.NumeroEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.FechaIngreso, empleado.Foto, empleado.Empresa.IdEmpresa, null);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al agregar el Empleado";
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
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.EmpleadoUpdate(empleado.NumeroEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.FechaIngreso, empleado.Foto, empleado.Empresa.IdEmpresa, null);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al actulizar el Empleado";
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
        public static ML.Result Delete(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.EmpleadoDelete(numeroEmpleado);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar el Empleado";
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
        public static ML.Result GetByIdEmpresa(int idEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var empleados = context.EmpleadoGetAllByEmpresa(idEmpresa).ToList();
                    if (empleados != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objEmpleado in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
                            empleado.RFC = objEmpleado.RFC;
                            empleado.Nombre = objEmpleado.NombreEmpleado;
                            empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                            empleado.ApellidoMaterno = objEmpleado.ApellidoPaterno;
                            empleado.Email = objEmpleado.Email;
                            empleado.Telefono = objEmpleado.Telefono;
                            empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.NSS = objEmpleado.NSS;
                            empleado.FechaIngreso = objEmpleado.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Foto = objEmpleado.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
                            empleado.Empresa.Nombre = objEmpleado.NombreEmpresa;
                            empleado.Poliza = new ML.Poliza();
                            if (objEmpleado.IdPoliza != null)
                            {
                                empleado.Poliza.IdPoliza = objEmpleado.IdPoliza.Value;
                            }
                            else
                            {
                                empleado.Poliza.IdPoliza = 0;
                            }

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al obtener los registros de la tabla Empleado";
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
