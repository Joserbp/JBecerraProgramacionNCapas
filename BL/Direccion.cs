using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace BL
{
    public class Direccion
    {
        public static ML.Result AddSP(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DireccionAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[5];

                        collection[0] = new SqlParameter("Calle", SqlDbType.VarChar);
                        collection[0].Value = direccion.Calle;
                        collection[1] = new SqlParameter("NumeroExterior", SqlDbType.VarChar);
                        collection[1].Value = direccion.NumeroExterior;
                        collection[2] = new SqlParameter("NumeroInterior", SqlDbType.VarChar);
                        collection[2].Value = direccion.NumeroInterior;
                        collection[3] = new SqlParameter("IdColonia", SqlDbType.Int);
                        collection[3].Value = direccion.Colonia.IdColonia;
                        collection[4] = new SqlParameter("IdDireccion", SqlDbType.Int);
                        collection[4].Direction = ParameterDirection.Output;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        result.Object = Convert.ToInt32(cmd.Parameters["IdDireccion"].Value);

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al momento de insertar la dirección ";
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
        public static ML.Result AddEF(ML.Direccion direccion)
        {
            ML.Result result= new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    ObjectParameter output = new ObjectParameter("IdDireccion", typeof(int));
                    var query = context.DireccionAdd(direccion.Calle, direccion.NumeroExterior, direccion.NumeroInterior, direccion.Colonia.IdColonia, output);
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Object = Convert.ToInt32(output.Value);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de insertar la dirección";
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
        public static ML.Result AddLINQ(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    DL_EF.Direccion direccionLinq = new DL_EF.Direccion();

                    direccionLinq.Calle = direccion.Calle;
                    direccionLinq.NumeroExterior = direccion.NumeroExterior;
                    direccionLinq.NumeroInterior = direccion.NumeroInterior;
                    direccionLinq.IdColonia = direccion.Colonia.IdColonia;

                    var query = context.Direccions.Add(direccionLinq);
                    
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Object = Convert.ToInt32(direccionLinq.IdDireccion.ToString());
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de insertar la dirección";
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
        public static ML.Result UpdateById(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.DireccionUpdateById(direccion.IdDireccion, direccion.Calle, direccion.NumeroExterior, direccion.NumeroInterior, direccion.Colonia.IdColonia);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar la Dirección";
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
        public static ML.Result DeleteById(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JBecerraProgramacionNCapasEntities context = new DL_EF.JBecerraProgramacionNCapasEntities())
                {
                    var query = context.DireccionDeleteById(IdDireccion);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar la direccion";
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
