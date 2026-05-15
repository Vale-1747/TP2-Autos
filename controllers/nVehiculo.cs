using alquiler_de_autos.models;
using System;
using System.Collections.Generic;
using System.IO;
using Libreria2026;

namespace alquiler_de_autos.controllers
{
    public class nVehiculo
    {
        public static List<Vehiculo> listaVehiculos = new List<Vehiculo>();

        public static void altaVehiculo()
        {
            Console.Clear();

            Console.Write("Patente: ");
            string patente = Console.ReadLine();
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Año: ");
            string textoAnio = Console.ReadLine();
            int anio;

            if (!int.TryParse(textoAnio, out anio))
            {
                Console.WriteLine("El año ingresado no es valido, por favor ingrese otro.");
                return;
            }

            if (!validarCampos(patente, marca, modelo))
            {
                Console.WriteLine("Los campos no pueden estar vacios.");
                return;
            }

            if (!validarAnio(anio))
            {
                Console.WriteLine("El año ingresado no es valido, por favor ingrese otro.");
                return;
            }

            if (existePatente(patente))
            {
                Console.WriteLine("Ya existe esta patente, por favor ingrese una nueva.");
                return;
            }

            Vehiculo v = new Vehiculo(patente, marca, modelo, anio);

            listaVehiculos.Add(v);

            Console.WriteLine("El vehículo se ha agregado con exito.");
        }

        public static void bajaVehiculo()
        {
            Console.Clear();

            Console.Write("Patente del vehículo a dar de baja: ");
            string patente = Console.ReadLine();

            if (patente == "")
            {
                Console.WriteLine("El campo no puede estar vacío.");
                return;
            }

            Vehiculo vehiculoEncontrado = buscarVehiculo(patente);

            if (vehiculoEncontrado == null)
            {
                Console.WriteLine("No se encontró un vehículo con esa patente.");
                return;
            }

            listaVehiculos.Remove(vehiculoEncontrado);

            Console.WriteLine("El vehículo se ha dado de baja con éxito.");
        }

        public static void modificarVehiculo()
        {
            Console.Clear();

            Console.Write("Ingrese la patente del vehículo: ");
            string patente = Console.ReadLine();

            //validar que el campo de la patente no este vacio
            if (string.IsNullOrWhiteSpace(patente))
            {
                Console.WriteLine("Debe ingresar una patente, el campo no puede estar vacío");
                return;
            }

            Vehiculo vehiculoEncontrado = buscarVehiculo(patente);

            if (vehiculoEncontrado == null)
            {
                Console.WriteLine("No se encontró un vehículo con esa patente.");
                return;
            }

            //mostramos los datos que ya estan cargados
            Console.WriteLine("Datos actuales del vehículo: ");
            Console.WriteLine($"Marca: {vehiculoEncontrado.marca}");
            Console.WriteLine($"Modelo: {vehiculoEncontrado.modelo}");
            Console.WriteLine($"Año: {vehiculoEncontrado.anio}");

            //pedimos los nuevos datos para cargar
            Console.Write("Nueva marca: ");
            string nuevaMarca = Console.ReadLine();
            Console.Write("Nuevo modelo: ");
            string nuevoModelo = Console.ReadLine();
            Console.Write("Nuevo año: ");
            string textoNuevoAnio = Console.ReadLine();
            int nuevoAnio;

            if (!int.TryParse(textoNuevoAnio, out nuevoAnio))
            {
                Console.WriteLine("El año ingresado no es valido, por favor ingrese otro.");
                return;
            }

            if (!validarCampos(vehiculoEncontrado.patente, nuevaMarca, nuevoModelo))
            {
                Console.WriteLine("Los campos no pueden estar vacíos.");
                return;
            }

            if (!validarAnio(nuevoAnio))
            {
                Console.WriteLine("El año ingresado no es válido, por favor ingrese otro.");
                return; 
            }

            //actualiza los datos del objeto
            vehiculoEncontrado.marca = nuevaMarca;
            vehiculoEncontrado.modelo = nuevoModelo;
            vehiculoEncontrado.anio = nuevoAnio;

            Console.WriteLine("El vehículo se ha modificado con éxito.");
        }

        public static void listarVehiculos()
        {
            Console.Clear();
            Console.WriteLine("      --Lista de vehículos--      \n");

            if (listaVehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos registrados.");
            }
            else
            {
                foreach (Vehiculo v in listaVehiculos)
                {
                    Console.WriteLine(v);
                }
            }
            Console.WriteLine("\nPresione una tecla para volver...");
            Console.ReadKey();
        }

        public static void exportarVehiculos()
        {
            if (listaVehiculos.Count == 0)
            {
                Console.Write("No hay vehículos cargados para exportar.");
                return;
            }
            //agrego esto para que si no tiene la carpeta, se cree una asi no falla el programa.
            string nombreCarpeta = "exports";
            if (!Directory.Exists(nombreCarpeta))
            {
                Directory.CreateDirectory(nombreCarpeta);
            }

            using (StreamWriter archivo = new StreamWriter("exports/vehiculos.csv"))
            {
                archivo.WriteLine("Patente;Marca;Modelo;Año");

                foreach (Vehiculo v in listaVehiculos)
                {
                    archivo.WriteLine($"{v.patente};{v.marca};{v.modelo};{v.anio}");
                }
            }

            Console.WriteLine("Los vehículos se han exportado con éxito.");
            Console.ReadKey();
        }

        public static void cargarDesdeArchivo()
        {
            string ruta = "exports/vehiculos.csv";

            if(!File.Exists(ruta))
            {
                return;                
            }

            listaVehiculos.Clear();

            try
            {
                using (StreamReader reader = new StreamReader(ruta))
                {
                    //salteal el encabezado
                    reader.ReadLine();

                    while(!reader.EndOfStream)
                    {
                        string linea = reader.ReadLine();
                        string[] datos = linea.Split(';');

                        if(datos.Length == 4)
                        {
                            Vehiculo v = new Vehiculo
                            (
                                datos[0],
                                datos[1],
                                datos[2],
                                int.Parse(datos[3])
                            );

                            listaVehiculos.Add(v);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar vehículos: " + ex.Message);
            }
        }

        //este metodo verifica que los campos no esten vacios
        public static bool validarCampos(string patente, string marca, string modelo)
        {
            if(string.IsNullOrWhiteSpace(patente) || string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo))
            {
                return false;
            }
            return true;
        }

        //este método verifica que haya patenta para que no haya patentes duplicadas
        public static bool existePatente(string patente)
        {
            foreach (Vehiculo v in listaVehiculos)
            {
                if(v.patente == patente)
                {
                    return true;
                }
            }
            return false;
        }

        //este metodo valida que el año que se ingresa tiene sentido para que no se guarde cualquier valor
        public static bool validarAnio(int anio)
        {
            //decidimos que sean validos años mayores a 1950 hasta el año actual
            if(anio < 1950 || anio > DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }


        public static Vehiculo buscarVehiculo(string patente)
        {
            foreach (Vehiculo v in listaVehiculos)
            {
                if(v.patente == patente)
                {
                    return v;
                }
            }
            return null;
        }

        public static List<Vehiculo> obtenerVehiculos()
        {
            return listaVehiculos;
        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = { "Agregar", "Eliminar", "Modificar", "Listar", "Exportar", "Volver" };
            int seleccion = Herramienta.MenuSeleccionar(opciones, 1, "Gestión de Vehículos");
            switch (seleccion)
            {
                case 1: altaVehiculo(); Menu(); break;
                case 2: bajaVehiculo(); Menu(); break;
                case 3: modificarVehiculo(); Menu(); break;
                case 4: listarVehiculos(); Menu(); break;
                case 5: exportarVehiculos(); Menu(); break;
                case 6: return;
            }
        }       
    }
}