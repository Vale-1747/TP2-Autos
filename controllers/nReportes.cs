using System;
using System.Collections.Generic;
using System.Linq;
using alquiler_de_autos.models;
using Libreria2026;

namespace alquiler_de_autos.controllers
{
    public class nReportes
    {
        private static Exportador exportador = new Exportador();

        public static void VehiculosMasUsados()
        {
            Console.Clear();

            List<Reserva> reservas = nReserva.GetReservas();

            Console.WriteLine("     --Vehiculos mas usados--     \n");

            if (reservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas.");
            }
        
            var consulta = reservas
                .GroupBy(r => r.vehiculo.patente)
                .Select(grupo => new { 
                    Patente = grupo.Key, 
                    Cant = grupo.Count(), 
                    Auto = grupo.First().vehiculo 
                })
                .OrderByDescending(x => x.Cant);
            
            foreach (var item in consulta)
            {
                Console.WriteLine($"Patente: {item.Patente} | Cantidad de reservas: {item.Cant} | Marca: {item.Auto.marca} | Modelo: {item.Auto.modelo}");
            }
            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void ClientesQueMasAlquilan()
        {
            Console.Clear();

            List<Reserva> reservas = nReserva.GetReservas();

            Console.WriteLine("     --Clientes Frecuentes--      \n");
            if (reservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas.");
            }

            var consulta = reservas
                .GroupBy(r => r.cliente.DNI)
                .Select(grupo => new { 
                    DNI = grupo.Key, 
                    Cant = grupo.Count(), 
                    Nombre = $"{grupo.First().cliente.Apellido}, {grupo.First().cliente.Nombre}"
                })
                .OrderByDescending(x => x.Cant);

            foreach (var item in consulta)
            {
                Console.WriteLine($"DNI: {item.DNI} | Cantidad de reservas: {item.Cant} | Nombre: {item.Nombre}");
            }
            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void ExportarReporte()
        {
            Console.Clear();

            List<Reserva> reservas = nReserva.GetReservas();

            List<string> lineas = new List<string>();
            lineas.Add("Reporte de reservas");
            lineas.Add($"Fecha en la que se genera: {DateTime.Now}");

            foreach (var r in reservas)
            {
                lineas.Add($"Auto: {r.vehiculo.patente} | Cliente: {r.cliente.DNI} | Desde: {r.fechaInicio.ToShortDateString()}");
            }
            
            string nombreCarpeta = "exports";
            
            if (!Directory.Exists(nombreCarpeta))  
            {
                Directory.CreateDirectory(nombreCarpeta);
            }

            using (StreamWriter writer = new StreamWriter("exports/reporteAlquileres.csv"))
            {
                foreach(string linea in lineas)
                {
                    writer.WriteLine(linea);
                }
            }
    
            Console.WriteLine("\nArchivo 'reporteAlquileres.csv' exportado con éxito.");
            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = {"Vehiculos más usados","Clientes que más alquilan","Exportar reporte","Volver"};
            int seleccion = Herramienta.MenuSeleccionar(opciones,1,"Gestión deReportes");
            switch(seleccion)
            {
                case 1: VehiculosMasUsados(); Menu(); break;
                case 2: ClientesQueMasAlquilan(); Menu(); break;
                case 3: ExportarReporte(); Menu(); break;
                case 4: return;
            }
        }
    }
}