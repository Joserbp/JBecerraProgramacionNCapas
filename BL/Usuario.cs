using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Globalization;

namespace BL
{
    public class Usuario
    {
        //METODOS CON LINQ

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objUsuarios = (from obj in context.Usuarios
                                       select obj).ToList();

                    if (objUsuarios != null)
                    {
                        result.Objects = new List<Object>();
                        foreach (var obj in objUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Direccion = new ML.Direccion();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString();
                            usuario.UserName = obj.UserName;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.CURP;
                            usuario.Direccion.IdDireccion = obj.IdDireccion.Value;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los usuario";
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
        public static ML.Result GetByIdLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objUsuario = (from obj in context.Usuarios
                                      where obj.IdUsuario == IdUsuario
                                      select obj).Single();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Email = objUsuario.Email;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.ToString();
                        usuario.UserName = objUsuario.UserName;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.CURP = objUsuario.CURP;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se econtro el usuario";
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
        public static ML.Result DeleteByIdLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = (from itemUsuario in context.Usuarios
                                 where itemUsuario.IdUsuario == IdUsuario
                                 select itemUsuario).Single();

                    context.Usuarios.Remove(query);
                    if (query != null)
                    {
                        int RowsAffected = context.SaveChanges();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de borrar al usuario";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
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
        public static ML.Result UpdateByIdLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var usuarioLinq = (from obj in context.Usuarios
                                       where obj.IdUsuario == usuario.IdUsuario
                                       select obj).FirstOrDefault();

                    if (usuarioLinq != null)
                    {
                        usuarioLinq.Nombre = usuario.Nombre;
                        usuarioLinq.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuarioLinq.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuarioLinq.Email = usuario.Email;
                        usuarioLinq.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        usuarioLinq.UserName = usuario.UserName;
                        usuarioLinq.Sexo = usuario.Sexo;
                        usuarioLinq.Telefono = usuario.Telefono;
                        usuarioLinq.Celular = usuario.Celular;
                        usuarioLinq.CURP = usuario.CURP;
                        usuarioLinq.IdDireccion = usuario.Direccion.IdDireccion;

                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de actualizar el usuario";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
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
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioLinq = new DL_EF.Usuario();

                    usuarioLinq.Nombre = usuario.Nombre;
                    usuarioLinq.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioLinq.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioLinq.Email = usuario.Email;
                    usuarioLinq.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    usuarioLinq.UserName = usuario.UserName;
                    usuarioLinq.Sexo = usuario.Sexo;
                    usuarioLinq.Telefono = usuario.Telefono;
                    usuarioLinq.Celular = usuario.Celular;
                    usuarioLinq.CURP = usuario.CURP;
                    usuarioLinq.IdDireccion = usuario.Direccion.IdDireccion;

                    context.Usuarios.Add(usuarioLinq);

                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de agregar el usuario";
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

        //METODOS ENTITY FRAMEWORK

        public static ML.Result GetAllEF(ML.Usuario usuarioBusqueda)
        {
            ML.Result result = new ML.Result();
            if (usuarioBusqueda.Nombre == null)
            {
                usuarioBusqueda.Nombre = "";
            }
            if (usuarioBusqueda.ApellidoPaterno == null)
            {
                usuarioBusqueda.ApellidoPaterno = "";
            }
            if (usuarioBusqueda.ApellidoMaterno == null)
            {
                usuarioBusqueda.ApellidoMaterno = "";
            }
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objUsuarios = context.UsuarioGetAll(usuarioBusqueda.Nombre, usuarioBusqueda.ApellidoPaterno, usuarioBusqueda.ApellidoMaterno).ToList();

                    if (objUsuarios != null)
                    {
                        result.Objects = new List<Object>();
                        foreach (var obj in objUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.UsuarioNombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.FechaNacimiento = (obj.FechaNacimiento.Value).ToString("dd-MM-yyyy");
                            usuario.UserName = obj.UserName;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.CURP;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion.Value;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia.Value;
                            usuario.Direccion.Colonia.Nombre = obj.ColoniaNombre;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre;
                            usuario.Imagen = obj.Imagen;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.RolNombre;
                            usuario.Password = obj.Password;
                            usuario.Estatus = obj.Estatus;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los usuarios";
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
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objUsuario = context.UsuarioGetByID(IdUsuario).FirstOrDefault();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.UsuarioNombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Email = objUsuario.Email;
                        usuario.FechaNacimiento = (objUsuario.FechaNacimiento.Value).ToString("dd-MM-yyyy");
                        usuario.UserName = objUsuario.UserName;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.CURP = objUsuario.CURP;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion.Value;
                        usuario.Direccion.Calle = objUsuario.Calle;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = objUsuario.ColoniaNombre;
                        usuario.Direccion.Colonia.CodigoPostal = objUsuario.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.MunicipioNombre;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.EstadoNombre;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.PaisNombre;
                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        usuario.Rol.Nombre = objUsuario.RolNombre;
                        usuario.Password = objUsuario.Password;
                        usuario.Estatus = objUsuario.Estatus;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener el usuario";
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
        public static ML.Result GetByUserNameEF(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var objUsuario = context.UsuarioGetByUserName(userName).SingleOrDefault();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Email = objUsuario.Email;
                        usuario.FechaNacimiento = (objUsuario.FechaNacimiento.Value).ToString("dd-MM-yyyy");
                        usuario.UserName = objUsuario.UserName;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.CURP = objUsuario.CURP;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion.Value;                   
                        usuario.Imagen = objUsuario.Imagen;                        
                        usuario.Password = objUsuario.Password;
                        usuario.Estatus = objUsuario.Estatus;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener el usuario";
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
        public static ML.Result DeleteByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.UsuarioDeleteById(IdUsuario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de eliminar el usuario";
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
        public static ML.Result UpdateByIdEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.UsuarioUpdateById(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.FechaNacimiento, usuario.UserName, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Direccion.IdDireccion, usuario.Imagen, usuario.Rol.IdRol, usuario.Password, usuario.Estatus);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de actualizar el usuario";
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
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.FechaNacimiento, usuario.UserName, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Direccion.IdDireccion, usuario.Imagen, usuario.Rol.IdRol, usuario.Password, true);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de agregar el usuario";
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


        //METODOS STORED PROCEDURE
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string NameStoredProcedure = "UsuarioGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = NameStoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();

                            da.Fill(tableUsuario);

                            cmd.Connection.Open();

                            if (tableUsuario.Rows.Count > 0)
                            {
                                foreach (DataRow row in tableUsuario.Rows) //
                                {
                                    ML.Usuario usuario = new ML.Usuario();
                                    usuario.IdUsuario = int.Parse(row[0].ToString());
                                    usuario.Nombre = row[1].ToString();
                                    usuario.ApellidoPaterno = row[2].ToString();
                                    usuario.ApellidoMaterno = row[3].ToString();
                                    usuario.Email = row[4].ToString();
                                    usuario.FechaNacimiento = row[5].ToString();
                                    usuario.UserName = row[6].ToString();
                                    usuario.Sexo = row[7].ToString();
                                    usuario.Telefono = row[8].ToString();
                                    usuario.Celular = row[9].ToString();
                                    usuario.CURP = row[10].ToString();

                                    result.Objects.Add(usuario);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros en la tabla Usuario";
                            }
                        }
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
        public static ML.Result GetByIdSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string NameStoredProcedure = "UsuarioGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = NameStoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.VarChar);
                        collection[0].Value = IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();

                            da.Fill(tableUsuario);
                            cmd.Connection.Open();

                            if (tableUsuario.Rows.Count > 0)
                            {
                                DataRow row = tableUsuario.Rows[0];

                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.FechaNacimiento = row[5].ToString();
                                usuario.UserName = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                result.Object = usuario;
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros en la tabla Usuario";
                            }
                        }
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
        public static ML.Result DeleteByIdSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UsuarioDeleteById";
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de eliminar el usuario";
                        }

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
        public static ML.Result UpdateByIdSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UsuarioUpdateById";
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[11];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = usuario.IdUsuario;
                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = usuario.Nombre;
                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoPaterno;
                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = usuario.ApellidoMaterno;
                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = usuario.Email;
                        collection[5] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[5].Value = usuario.FechaNacimiento;
                        collection[6] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[6].Value = usuario.UserName;
                        collection[7] = new SqlParameter("Sexo", SqlDbType.VarChar);
                        collection[7].Value = usuario.Sexo;
                        collection[8] = new SqlParameter("Telefono", SqlDbType.VarChar);
                        collection[8].Value = usuario.Telefono;
                        collection[9] = new SqlParameter("Celular", SqlDbType.VarChar);
                        collection[9].Value = usuario.Celular;
                        collection[10] = new SqlParameter("CURP", SqlDbType.VarChar);
                        collection[10].Value = usuario.CURP;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de actualizar el usuario";
                        }

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
        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UsuarioAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[11];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuario.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoMaterno;
                        collection[3] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[3].Value = usuario.Email;
                        collection[4] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[4].Value = usuario.FechaNacimiento;
                        collection[5] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[5].Value = usuario.UserName;
                        collection[6] = new SqlParameter("Sexo", SqlDbType.VarChar);
                        collection[6].Value = usuario.Sexo;
                        collection[7] = new SqlParameter("Telefono", SqlDbType.VarChar);
                        collection[7].Value = usuario.Telefono;
                        collection[8] = new SqlParameter("Celular", SqlDbType.VarChar);
                        collection[8].Value = usuario.Celular;
                        collection[9] = new SqlParameter("CURP", SqlDbType.VarChar);
                        collection[9].Value = usuario.CURP;
                        collection[10] = new SqlParameter("IdDireccion", SqlDbType.Int);
                        collection[10].Value = usuario.Direccion.IdDireccion;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de agregar el usuario";
                        }

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

        //METODOS SQL QUERY
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,Email,FechaNacimiento,UserName,Sexo,Telefono,Celular,CURP FROM Usuario";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();

                            da.Fill(tableUsuario);

                            cmd.Connection.Open();

                            if (tableUsuario.Rows.Count > 0)
                            {
                                foreach (DataRow row in tableUsuario.Rows) //
                                {
                                    ML.Usuario usuario = new ML.Usuario();
                                    usuario.IdUsuario = int.Parse(row[0].ToString());
                                    usuario.Nombre = row[1].ToString();
                                    usuario.ApellidoPaterno = row[2].ToString();
                                    usuario.ApellidoMaterno = row[3].ToString();
                                    usuario.Email = row[4].ToString();
                                    usuario.FechaNacimiento = row[5].ToString();
                                    usuario.UserName = row[6].ToString();
                                    usuario.Sexo = row[7].ToString();
                                    usuario.Telefono = row[8].ToString();
                                    usuario.Celular = row[9].ToString();
                                    usuario.CURP = row[10].ToString();

                                    result.Objects.Add(usuario);

                                }

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros en la tabla Usuario";
                            }
                        }
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,Email,FechaNacimiento,UserName,Sexo,Telefono,Celular,CURP FROM Usuario" + "WHERE IdUsuario = @IdUsuario";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();

                            da.Fill(tableUsuario);
                            cmd.Connection.Open();

                            if (tableUsuario.Rows.Count > 0)
                            {
                                DataRow row = tableUsuario.Rows[0];

                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.FechaNacimiento = row[5].ToString();
                                usuario.UserName = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                result.Object = usuario;
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros en la tabla Usuario";
                            }
                        }
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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO Usuario (Nombre,ApellidoPaterno,ApellidoMaterno,Email,FechaNacimiento,UserName,Sexo,Telefono,Celular,CURP,IdDireccion) VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,@Email,@FechaNacimiento,@UserName,@Sexo,@Telefono,@Celular,@CURP,@IdDireccion)";
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[11];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuario.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoMaterno;
                        collection[3] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[3].Value = usuario.Email;
                        collection[4] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[4].Value = usuario.FechaNacimiento;
                        collection[5] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[5].Value = usuario.UserName;
                        collection[6] = new SqlParameter("Sexo", SqlDbType.VarChar);
                        collection[6].Value = usuario.Sexo;
                        collection[7] = new SqlParameter("Telefono", SqlDbType.VarChar);
                        collection[7].Value = usuario.Telefono;
                        collection[8] = new SqlParameter("Celular", SqlDbType.VarChar);
                        collection[8].Value = usuario.Celular;
                        collection[9] = new SqlParameter("CURP", SqlDbType.VarChar);
                        collection[9].Value = usuario.CURP;
                        collection[10] = new SqlParameter("IdDireccion", SqlDbType.Int);
                        collection[10].Value = usuario.Direccion.IdDireccion;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de insertar el usuario";
                        }

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
        public static ML.Result UpdateById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UPDATE Usuario [Nombre] = @Nombre,[ApellidoPaterno] = @ApellidoPaterno,[ApellidoMaterno] = @ApellidoMaterno,[Email] = @Email ,[FechaNacimiento] = @FechaNacimiento, [UserName] = @UserName, [Sexo] = @Sexo, [Telefono] = @Telefono ,[Celular] = @Celular, [CURP] = @CURP,[IdDireccion] = @IdDireccion" + "WHERE IdUsuario = @IdUsuario";
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[11];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuario.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoMaterno;
                        collection[3] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[3].Value = usuario.Email;
                        collection[4] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[4].Value = usuario.FechaNacimiento;
                        collection[5] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[5].Value = usuario.UserName;
                        collection[6] = new SqlParameter("Sexo", SqlDbType.VarChar);
                        collection[6].Value = usuario.Sexo;
                        collection[7] = new SqlParameter("Telefono", SqlDbType.VarChar);
                        collection[7].Value = usuario.Telefono;
                        collection[8] = new SqlParameter("Celular", SqlDbType.VarChar);
                        collection[8].Value = usuario.Celular;
                        collection[9] = new SqlParameter("CURP", SqlDbType.VarChar);
                        collection[9].Value = usuario.CURP;
                        collection[10] = new SqlParameter("IdDireccion", SqlDbType.Int);
                        collection[10].Value = usuario.Direccion.IdDireccion;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de actualizar el usuario";
                        }

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
        public static ML.Result DeleteById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DELETE FROM Usuario " + "WHERE IdUsuario = @IdUsuario";
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = usuario.IdUsuario;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de eliminar al usuario";
                        }

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

        //METODOS EXCEL

        public static ML.Result GetAllByExcel(string connectionString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.Connection.Open();

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                        DataTable tablaUsuario = new DataTable();
                        da.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaUsuario.Rows) //
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.Email = row[3].ToString();
                                usuario.FechaNacimiento = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                usuario.Sexo = row[6].ToString();
                                usuario.Telefono = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                usuario.CURP = row[9].ToString();
                                usuario.Password = row[10].ToString();

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron registros en el Archivo de Excel";
                        }
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
        public static ML.Result Validacion(List<object> Object)
        {
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();
            string errorMessage;
            int contador = 2;
            foreach (ML.Usuario usuario in Object) //
            {
                errorMessage = "";
                errorMessage += (usuario.Nombre == "") ? "Falta Nombre " : "";
                errorMessage += (usuario.ApellidoPaterno == "") ? "Falta ApellidoPaterno " : "";
                errorMessage += (usuario.ApellidoMaterno == "") ? "Falta ApellidoMaterno " : "";
                errorMessage += (usuario.Email == "") ? "Falta Email " : "";
                errorMessage += (usuario.FechaNacimiento == "") ? "Falta FechaNacimiento " : "";
                errorMessage += (usuario.UserName == "") ? "Falta UserName " : "";
                errorMessage += (usuario.Sexo == "") ? "Falta Sexo " : "";
                errorMessage += (usuario.Telefono == "") ? "Falta Telefono " : "";
                errorMessage += (usuario.Celular == "") ? "Falta Celular " : "";
                errorMessage += (usuario.CURP == "") ? "Falta CURP " : "";
                errorMessage += (usuario.Password == "") ? "Falta Password " : "";

                if (errorMessage != "")
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = contador;
                    error.Message = errorMessage;
                    result.Objects.Add(error);
                }

                contador++;
            }
            return result;
        }
    }
}
