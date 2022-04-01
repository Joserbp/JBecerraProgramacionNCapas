using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var dependientes = context.DependienteGetAll().ToList();
                    if (dependientes != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDependiente in dependientes)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = objDependiente.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = objDependiente.NumeroEmpleado;
                            dependiente.Nombre = objDependiente.NombreDependiente;
                            dependiente.ApellidoPaterno = objDependiente.ApellidoPaterno;
                            dependiente.ApellidoMaterno = objDependiente.ApellidoMaterno;
                            dependiente.FechaNacimiento = objDependiente.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = objDependiente.EstadoCivil;
                            dependiente.Genero = objDependiente.Genero;
                            dependiente.Telefono = objDependiente.Telefono;
                            dependiente.RFC = objDependiente.RFC;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = objDependiente.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = objDependiente.NombreDependienteTipo;

                            result.Objects.Add(dependiente);
                        }
                        result.Correct = true;
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
                    var dependientes = context.DependienteGetByNumeroEmpleado(numeroEmpleado).ToList();
                    if (dependientes != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDependiente in dependientes)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = objDependiente.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = objDependiente.NumeroEmpleado;
                            dependiente.Nombre = objDependiente.NombreDependiente;
                            dependiente.ApellidoPaterno = objDependiente.ApellidoPaterno;
                            dependiente.ApellidoMaterno = objDependiente.ApellidoMaterno;
                            dependiente.FechaNacimiento = objDependiente.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = objDependiente.EstadoCivil;
                            dependiente.Genero = objDependiente.Genero;
                            dependiente.Telefono = objDependiente.Telefono;
                            dependiente.RFC = objDependiente.RFC;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = objDependiente.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = objDependiente.NombreDependienteTipo;

                            result.Objects.Add(dependiente);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la Tabla Dependiente";
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
        public static ML.Result GetById(int IdDependiente)
        {
            ///////////////////////////////////
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objDependiente = context.DependienteGetById(IdDependiente).FirstOrDefault();
                    if (objDependiente != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();
                        dependiente.IdDependiente = objDependiente.IdDependiente;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = objDependiente.NumeroEmpleado;
                        dependiente.Nombre = objDependiente.NombreDependiente;
                        dependiente.ApellidoPaterno = objDependiente.ApellidoPaterno;
                        dependiente.ApellidoMaterno = objDependiente.ApellidoMaterno;
                        dependiente.FechaNacimiento = objDependiente.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        dependiente.EstadoCivil = objDependiente.EstadoCivil;
                        dependiente.Genero = objDependiente.Genero;
                        dependiente.Telefono = objDependiente.Telefono;
                        dependiente.RFC = objDependiente.RFC;
                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo = objDependiente.IdDependienteTipo.Value;
                        dependiente.DependienteTipo.Nombre = objDependiente.NombreDependienteTipo;

                        result.Object = dependiente;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro registro en la Tabla Dependiente";
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
        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.DependienteAdd(dependiente.Empleado.NumeroEmpleado, dependiente.Nombre, dependiente.ApellidoPaterno, dependiente.ApellidoMaterno, dependiente.FechaNacimiento, dependiente.EstadoCivil, dependiente.Genero, dependiente.Telefono, dependiente.RFC, dependiente.DependienteTipo.IdDependienteTipo);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar el Dependiente en la tabla Dependiente";
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
        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.DependienteUpdate(dependiente.IdDependiente, dependiente.Empleado.NumeroEmpleado, dependiente.Nombre, dependiente.ApellidoPaterno, dependiente.ApellidoMaterno, dependiente.FechaNacimiento, dependiente.EstadoCivil, dependiente.Genero, dependiente.Telefono, dependiente.RFC, dependiente.DependienteTipo.IdDependienteTipo);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al actualizar el Dependiente en la tabla Dependiente";
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
        public static ML.Result Delete(int idDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.DependienteDelete(idDependiente);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar el Dependiente en la tabla Dependiente";
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
