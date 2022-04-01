using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int opcion;

                Console.WriteLine("\t\t\t Opciones del programa \n\n");
                Console.WriteLine("1.Agregar un usuario \n");
                Console.WriteLine("2.Actualizar un usuario \n");
                Console.WriteLine("3.Ver todos los usuarios \n");
                Console.WriteLine("4.Buscar un solo usuario \n");
                Console.WriteLine("5.Eliminar un usuario \n");
                Console.WriteLine("6.CargaEmpresas txt \n");
                Console.WriteLine("7.CargaEmpresas xlsx \n");
                Console.WriteLine("8.Suma con WCF\n");
                Console.WriteLine("9.Resta con WCF\n");
                Console.WriteLine("10.Multipliacion con WCF\n");
                Console.WriteLine("11.Division con WCF\n");
                Console.WriteLine("Ingrese la opción deseada:");
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Usuario.Add();
                        break;
                    case 2:
                        Usuario.UpdateById();
                        break;
                    case 3:
                        Usuario.GetAll();
                        break;
                    case 4:
                        Usuario.GetById();
                        break;
                    case 5:
                        Usuario.DeleteById();
                        break;
                    case 6:
                        Empresa.ReadFile();
                        break;
                    case 7:
                        //Empresa.ReadExcel();
                        break;
                    case 8:
                        TestOperaciones.OperacionesClient prueba = new TestOperaciones.OperacionesClient();
                        var resultSuma = prueba.Suma(4, 3);
                        Console.WriteLine(resultSuma);
                        break;
                    case 9:
                        TestOperaciones.OperacionesClient prueba1 = new TestOperaciones.OperacionesClient();
                        var resultResta = prueba1.Resta(4, 3);
                        Console.WriteLine(resultResta);
                        break;
                    case 10:
                        TestOperaciones.OperacionesClient prueba2 = new TestOperaciones.OperacionesClient();
                        var resultMultiplicacion = prueba2.Multiplicacion(4, 3);
                        Console.WriteLine(resultMultiplicacion);
                        break;
                    case 11:
                        TestOperaciones.OperacionesClient prueba3 = new TestOperaciones.OperacionesClient();
                        var resultDivision = prueba3.Division(4, 3);
                        Console.WriteLine(resultDivision);
                        break;

                    default:
                        Console.WriteLine("La opción digitada no es correcta \n Ingrese una opción correcta:");
                        opcion = int.Parse(Console.ReadLine());
                        break;
                }
            }
        }
    }
}
