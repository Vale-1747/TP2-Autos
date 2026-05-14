using System;
using alquiler_de_autos.controllers;
using alquiler_de_autos.models;


namespace alquiler_de_autos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            GestionClientes gestion = new GestionClientes();
            nVehiculo gestionVehiculos = new nVehiculo();
            nReserva gestionReservas = new nReserva();
            nReportes gestionReportes = new nReportes();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE ALQUILER DE AUTOS ===");
                Console.WriteLine("1. Gestión de Clientes");
                Console.WriteLine("2. Gestión de Vehículos");
                Console.WriteLine("3. Gestión de Reservas");
                Console.WriteLine("4. Reportes");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        MenuClientes(gestion);
                        break;
                    case "2":
                        MenuVehiculos(gestionVehiculos);
                        break;
                    case "3":
                        MenuReservas(gestionReservas, gestion, gestionVehiculos);
                        break;
                    case "4":
                        MenuReportes(gestionReportes, gestionReservas);
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
                

        static void MenuClientes(GestionClientes gestion)
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE CLIENTES ===");
                Console.WriteLine("1. Agregar Cliente");
                Console.WriteLine("2. Listar Clientes");
                Console.WriteLine("3. Exportar a Archivo");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== AGREGAR CLIENTE ===");
                        Console.Write("DNI: "); string DNI = Console.ReadLine();
                        Console.Write("Nombre: "); string Nombre = Console.ReadLine();
                        Console.Write("Apellido: "); string Apellido = Console.ReadLine();
                        Console.Write("Email: "); string Email = Console.ReadLine();

                        if (gestion.AgregarCliente(DNI, Nombre, Apellido, Email))
                            Console.WriteLine("Cliente agregado correctamente.");
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== LISTA DE CLIENTES ===");
                        gestion.ListarClientes();
                        break;
                    case "3":
                        gestion.ExportarAArchivo();
                        break;
                    case "4":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no valda.");
                        break;
                }
                if (!volver) { Console.WriteLine("\nPresione una tecla para volver atras..."); Console.ReadKey(); }
            }
        }

        static void MenuVehiculos(nVehiculo gestionVehiculos)
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE VEHÍCULOS ===");
                Console.WriteLine("1. Alta de Vehículo");
                Console.WriteLine("2. Baja de Vehículo");
                Console.WriteLine("3. Modificar Vehículo");
                Console.WriteLine("4. Listar Vehículos");
                Console.WriteLine("5. Exportar Vehículos");
                Console.WriteLine("6. Volver");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": gestionVehiculos.altaVehiculo(); break;
                    case "2": gestionVehiculos.bajaVehiculo(); break;
                    case "3": gestionVehiculos.modificarVehiculo(); break;
                    case "4": gestionVehiculos.listarVehiculos(); break;
                    case "5": gestionVehiculos.exportarVehiculos(); break;
                    case "6": volver = true; break;
                }
                if (!volver) { Console.WriteLine("\nPresione una tecla para volver atras..."); Console.ReadKey(); }
            }
        }

        static void MenuReservas(nReserva gestionReservas, GestionClientes gestion, nVehiculo gestionVehiculos)
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE RESERVAS ===");
                Console.WriteLine("1. Crear Reserva");
                Console.WriteLine("2. Listar Reservas");
                Console.WriteLine("3. Ver Disponibilidad por fecha");
                Console.WriteLine("4. Volver");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== CREAR RESERVA ===");
                        // Por ahora lo dejamos marcado para conectar después
                        gestionReservas.crearReserva(gestion, gestionVehiculos);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== LISTA DE RESERVAS ===");
                        gestionReservas.listarReservas();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== DISPONIBILIDAD POR FECHA ===");
                        gestionReservas.vehiculosDisponibles(gestionVehiculos.listaVehiculos);
                        break;
                    case "4":
                        volver = true;
                        break;
                }
                if (!volver) { Console.WriteLine("\nPresione una tecla para volver atras..."); Console.ReadKey(); }
            }
        }

        static void MenuReportes(nReportes gestionReportes, nReserva gestionarReservas)
        {
            string opcion;
            var reservas = gestionarReservas.GetReservas();

            do
            {
                Console.Clear();

                Console.WriteLine("=== REPORTES ===");
                Console.WriteLine("1- Vehiculos mas usados");
                Console.WriteLine("2- Clientes que mas alquilan");
                Console.WriteLine("3- Exportar reporte");
                Console.WriteLine("0- Volver");
                Console.Write("\nSeleccione una opción: ");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        gestionReportes.VehiculosMasUsados(reservas);
                        break;

                    case "2":
                        gestionReportes.ClientesQueMasAlquilan(reservas);
                        break;

                    case "3":
                        gestionReportes.ExportarReporte(reservas);
                        break;
                    case "4":
                        break;
                        
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                
                if (opcion != "0")
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }


            } while (opcion != "0");
        }


    }
}