using System;
Console.WriteLine("Hello, World!");

namespace TP2Autos
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionClientes gestion = new GestionClientes();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- SISTEMA DE GESTIÓN DE CLIENTES ---");
                Console.WriteLine("1. Dar de Alta Cliente");
                Console.WriteLine("2. Listar Clientes");
                Console.WriteLine("3. Exportar a CSV");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("DNI: "); string DNI = Console.ReadLine();
                        Console.Write("Nombre: "); string Nombre = Console.ReadLine();
                        Console.Write("Apellido: "); string Apellido = Console.ReadLine();
                        Console.Write("Email: "); string Email = Console.ReadLine();

                        if(gestion.AgregarCliente(DNI, Nombre, Apellido, Email)) 
                            Console.WriteLine("Cliente agregado correctamente.");
                        break;
                    case "2":
                        gestion.ListarClientes();
                        break;
                    case "3":
                        gestion.ExportarAArchivo();
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}