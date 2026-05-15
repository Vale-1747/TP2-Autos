using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO;   // Para StreamWriter
using System.Linq;
using alquiler_de_autos.models;
using Libreria2026;

namespace alquiler_de_autos.controllers
{
    public class nCliente
    {
        private static List<Cliente> clientes = new List<Cliente>();
        private static string rutaArchivo = "clientes_exportados.csv";

        //para agregar un cliente nuevo
        public static void AgregarCliente()
        {
            Console.Clear();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("DNI: ");
            string dni = Console.ReadLine();
            
            // datos que si o si precisamos
            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("Error: DNI, Nombre y Apellido son obligatorios.");
                return;
            }

            // con esto validas el DNI para que no se repita
            if (clientes.Any(c => c.DNI == dni))
            {
                Console.WriteLine("Error: Ya existe un cliente con ese DNI.");
                return;
            }

            clientes.Add(new Cliente { DNI = dni, Nombre = nombre, Apellido = apellido, Email = email });
            Console.WriteLine("Se ha agregado el nuevo cliente con exito.");
            return;
        }

        // el listado de clientes
        public static void ListarClientes()
        {
            Console.Clear();
            if (clientes.Count == 0) Console.WriteLine("No hay clientes registrados.");
            else clientes.ForEach(c => Console.WriteLine(c.ToString()));
        }

        // para exportar a un archivo de texto
        public static void ExportarAArchivo()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(rutaArchivo))
                {
                    writer.WriteLine("DNI;Nombre;Apellido;Email");
                    foreach (var c in clientes)
                    {
                        writer.WriteLine($"{c.DNI};{c.Nombre};{c.Apellido};{c.Email}");
                    }
                }
                Console.WriteLine($"\nÉxito: Datos exportados a {rutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al exportar: " + ex.Message);
            }
        }

        public static Cliente BuscarPorDNI(string dni)
        {
            return clientes.Find(c => c.DNI == dni);
        }
        
        public static void Menu()
        {
            Console.Clear();
            string[] opciones = {"Agregar","Listar","Exportar","Salir"};
            int seleccion = Herramienta.MenuSeleccionar(opciones,1,"Gestión de Clientes");
            switch (seleccion)
            {
                case 1: AgregarCliente(); Menu(); break;
                case 2: ListarClientes(); Menu(); break;
                case 3: ExportarAArchivo(); Menu(); break;
                case 4: return;
            }
        }
    }
}