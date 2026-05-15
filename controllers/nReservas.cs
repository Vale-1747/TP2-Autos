using alquiler_de_autos.models;
using System;
using System.Collections.Generic;
using System.Data;
using Libreria2026;

namespace alquiler_de_autos.controllers
{
    public class nReserva
    {
        private static List<Reserva> listaReservas { get; set;} = new List<Reserva>();

        public static List<Reserva> GetReservas()
        {
            return listaReservas;
        }
        public static void listarReservas()
        {
            Console.Clear();
            Console.WriteLine("      --Lista de reservas--      \n");
            if(listaReservas.Count == 0)
            {
                Console.WriteLine("\nNo hay reservas registradas.");
            }

            foreach (Reserva r in listaReservas)
            {
                Console.WriteLine(r+"\n");

            }

            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static bool validarFechas(Vehiculo vehiculo, DateTime desde, DateTime hasta)
        {
            foreach (Reserva r in listaReservas)
            {
                if (r.vehiculo.patente == vehiculo.patente)
                {
                    if (desde <= r.fechaFin && hasta >= r.fechaInicio)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
            //hago un cambio aca poruqe program no tiene los objetos vehiculo y cliente, solo tiene las listas.
        public static void crearReserva()
        {
            Console.Clear();
            Console.Write ("Ingrese DNI del cliente: ");
            string dni = Console.ReadLine();
            Cliente cliente = nCliente.BuscarPorDNI(dni);

            if (cliente == null)
            {
                Console.WriteLine("No se encontró un cliente con ese DNI.");
                return;
            }

            Console.Write("Ingrese patente del vehículo: ");
            string patente = Console.ReadLine();
            Vehiculo vehiculo = nVehiculo.buscarVehiculo(patente);

            if (vehiculo == null)
            {
                Console.WriteLine("No se encontró un vehículo con esa patente.");
                return;
            }
    
            
            Console.Write("Fecha desde: ");
            DateTime desde = DateTime.Parse(Console.ReadLine());

            Console.Write("Fecha hasta: ");
            DateTime hasta = DateTime.Parse(Console.ReadLine());

            if (desde > hasta)
            {
                Console.WriteLine("La fecha de inicio no puede ser mayor a la fecha de fin.");
                return;
            }
            
            if (!validarFechas(vehiculo, desde, hasta))
            {
                Console.WriteLine("Ese vehiculo ya aesta reservado en esas fechas.");
                return;
            }   

            Reserva nueva = new Reserva(desde, hasta, cliente, vehiculo);
            listaReservas.Add(nueva);

            Console.WriteLine("Reserva creada."); 
        }
        

        public static void vehiculosDisponibles()
        {
            Console.Clear();
            Console.Write("Ingrese fecha: ");
            DateTime fecha;

            while(!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.WriteLine("Fecha inválida. Intente nuevamente.");
                Console.WriteLine("\nPresione una tecla para volver...");
                Console.ReadKey();
                return;
            }

            bool hayDisponibles = false;

            foreach (Vehiculo v in nVehiculo.obtenerVehiculos())
            {
                bool ocupado = false;

                foreach (Reserva r in listaReservas)
                {
                    if (r.vehiculo.patente == v.patente && fecha.Date >= r.fechaInicio.Date && fecha.Date <= r.fechaFin.Date)
                    {
                        ocupado = true;
                        break;
                    }
                }

                if (!ocupado)
                {
                    Console.WriteLine(v);
                    hayDisponibles = true;
                }
            }

            if(!hayDisponibles)
            {
                Console.WriteLine("No hay vehículos disponibles para esa fecha.");
            }

            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void exportarReservas()
        {
            Console.Clear();

            string carpeta = "exports";

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            using (StreamWriter writer = new StreamWriter("exports/reservas.csv"))
            {
                writer.WriteLine("DNI;Patente;FechaInicio;FechaFin");

                foreach (Reserva r in listaReservas)
                {
                    writer.WriteLine($"{r.cliente.DNI};{r.vehiculo.patente};{r.fechaInicio};{r.fechaFin}");
                }
            }

            Console.WriteLine("\nLas reservas se han exportado con éxito.");
            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void cargarDesdeArchivo()
        {
            string ruta = "exports/reservas.csv";

            if (!File.Exists(ruta))
            {
                return;
            }

            listaReservas.Clear();

            try
            {
                using (StreamReader reader = new StreamReader(ruta))
                {
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string linea = reader.ReadLine();
                        string[] datos = linea.Split(';');

                        if (datos.Length >= 4)
                        {
                            string dni = datos[0];
                            string patente = datos[1];

                            DateTime fechaInicio = DateTime.Parse(datos[2]);
                            DateTime fechaFin = DateTime.Parse(datos[3]);

                            Cliente cliente = nCliente.BuscarPorDNI(dni);
                            Vehiculo vehiculo = nVehiculo.buscarVehiculo(patente);

                            if (cliente != null || vehiculo != null) {
                                Reserva reserva = new Reserva(fechaInicio, fechaFin, cliente, vehiculo);
                                listaReservas.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar reservas: " + ex.Message);
            }
        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = {"Crear","Listar","Ver Disponibilidad por fecha","Exportar","Volver"};
            int seleccion = Herramienta.MenuSeleccionar(opciones,1,"Gestión de Reservas");
            switch(seleccion)
            {
                case 1: crearReserva(); Menu(); break;
                case 2: listarReservas(); Menu(); break;
                case 3: vehiculosDisponibles(); Menu(); break;
                case 4: exportarReservas(); Menu(); break;
                case 5: return;
            }
        }

    }
}