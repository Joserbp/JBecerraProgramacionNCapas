using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Usuario
    {
        public static void GetAll()
        {
            //ML.Result result = BL.Usuario.GetAllSP();
            //ML.Result result = BL.Usuario.GetAllEF();
            //ML.Result result = BL.Usuario.GetAllLINQ();
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            TestUsuario.UsuarioClient prueba = new TestUsuario.UsuarioClient();
            
            var result = prueba.GetAll(usuario);

            if (result.Correct)
            {
                foreach (ML.Usuario usuario2 in result.Objects.ToList())
                {
                    Console.WriteLine("IdUsuario: " + usuario2.IdUsuario +
                                "\n  Nombre: " + usuario2.Nombre +
                                "\n  ApellidoPaterno: " + usuario2.ApellidoPaterno +
                                "\n  ApellidoMaterno: " + usuario2.ApellidoMaterno +
                                "\n  Email: " + usuario2.Email +
                                "\n  Fecha de nacimiento: " + usuario2.FechaNacimiento +
                                "\n  UserName: " + usuario2.UserName +
                                "\n  Sexo: " + usuario2.Sexo +
                                "\n  Telefono: " + usuario2.Telefono +
                                "\n  Celular: " + usuario2.Celular +
                                "\n  CURP: " + usuario2.CURP
                        );
                    Console.WriteLine("\n ------------------------ \n");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Ocurrio un error al trae la informacion " + result.ErrorMessage);
            }
        }
        public static void GetById()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Id del usuario");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.GetByIdSP(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdLINQ(usuario.IdUsuario);
            TestUsuario.UsuarioClient prueba = new TestUsuario.UsuarioClient();
            var result = prueba.GetById(usuario.IdUsuario);

            if (result.Correct)
            {
                Console.WriteLine("IdUsuario: " + ((ML.Usuario)result.Object).IdUsuario +
                                "\n  Nombre: " + ((ML.Usuario)result.Object).Nombre +
                                "\n  ApellidoPaterno: " + ((ML.Usuario)result.Object).ApellidoPaterno +
                                "\n  ApellidoMaterno: " + ((ML.Usuario)result.Object).ApellidoMaterno +
                                "\n  Email: " + ((ML.Usuario)result.Object).Email +
                                "\n  Fecha de nacimiento: " + ((ML.Usuario)result.Object).FechaNacimiento +
                                "\n  UserName: " + ((ML.Usuario)result.Object).UserName +
                                "\n  Sexo: " + ((ML.Usuario)result.Object).Sexo +
                                "\n  Telefono: " + ((ML.Usuario)result.Object).Telefono +
                                "\n  Celular: " + ((ML.Usuario)result.Object).Celular +
                                "\n  CURP: " + ((ML.Usuario)result.Object).CURP
                        );
                Console.WriteLine("\n ------------------------ \n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Ocurrio un error al trae la informacion " + result.ErrorMessage);
            }
        }
        public static void DeleteById()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Id del usuario que desea eliminar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.DeleteByIdSP(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.DeleteByIdEF(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.DeleteByIdLINQ(usuario.IdUsuario);
            TestUsuario.UsuarioClient prueba = new TestUsuario.UsuarioClient();
            var result = prueba.Delete(usuario.IdUsuario);
            if (result.Correct)
            {
                Console.WriteLine("El usuario ha sido eliminado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al eliminar el usuario " + result.ErrorMessage);
            }
        }
        public static void UpdateById()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion = new ML.Direccion();

            Console.WriteLine("Ingrese el Id del usuario que requiere actualizar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el nuevo nombre del usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo email del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva fecha de nacimiento del usuario (Formato DD-MM-AAAA)");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo UserName del usuario");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo Sexo del usuario");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo Telefono del usuario");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo celular del usuario");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo CURP del usuario");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo IdDireccion del usuario");
            usuario.Direccion.IdDireccion = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.UpdateByIdSP(usuario);
            //ML.Result result = BL.Usuario.UpdateByIdEF(usuario);
            //ML.Result result = BL.Usuario.UpdateByIdLINQ(usuario);
            TestUsuario.UsuarioClient prueba = new TestUsuario.UsuarioClient();
            var result = prueba.Update(usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usuario ha sido actualizado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al actualizar el usuario " + result.ErrorMessage);
            }

        }
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion = new ML.Direccion();
            usuario.Rol = new ML.Rol();
            usuario.Direccion.Colonia = new ML.Colonia();

            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el email del usuario");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de nacimiento del usuario (Formato DD-MM-AAAA)");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el celular del usuario");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese el CURP del usuario");
            usuario.CURP = Console.ReadLine();

            Console.WriteLine("Ingrese el Id de rol del usuario");
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la calle del usuario");
            usuario.Direccion.Calle = Console.ReadLine();

            Console.WriteLine("Ingrese el Numero Exterior del usuario");
            usuario.Direccion.NumeroExterior = Console.ReadLine();

            Console.WriteLine("Ingrese el Numero Interior del usuario");
            usuario.Direccion.NumeroInterior = Console.ReadLine();

            Console.WriteLine("Ingrese el Id Colonia del usuario");
            usuario.Direccion.Colonia.IdColonia = int.Parse(Console.ReadLine());

            //ML.Result resultDireccion = BL.Direccion.AddSP(usuario.Direccion);
            //ML.Result resultDireccion = BL.Direccion.AddEF(usuario.Direccion);
            ML.Result resultDireccion = BL.Direccion.AddLINQ(usuario.Direccion);
            if(resultDireccion.Correct){
                Console.WriteLine("La direccion ha sido insertada correctamente");

                usuario.Direccion.IdDireccion = ((int)resultDireccion.Object);  //GUARDAR ID DIRECCION

                //ML.Result result = BL.Usuario.AddSP(usuario);
                //ML.Result result = BL.Usuario.AddEF(usuario);
                //ML.Result result = BL.Usuario.AddLINQ(usuario);
                TestUsuario.UsuarioClient prueba = new TestUsuario.UsuarioClient();
                var result = prueba.Add(usuario);
                if (result.Correct)
                {
                    Console.WriteLine("El usuario ha sido insertado correctamente");
                }
                else
                {
                    Console.WriteLine("Ocurrió un error al insertar el usuario" + result.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine("Ocurrió un error al insertar la direccion" + resultDireccion.ErrorMessage);
            }
        }
    }
}
