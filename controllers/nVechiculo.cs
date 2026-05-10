using alquiler_de_autos.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace alquiler_de_autos.controllers
{
    class nVehiculo
    {
        public List<Vehiculo> listaVehiculos = new List<Vehiculo>();

        public void altaVehiculo()
        {
            Console.Write("Patente: ");
            string patente = Console.ReadLine();
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Año: ");
            int anio = int.Parse(Console.ReadLine());

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

            listarVehiculos.add(nuevo);

            Console.WriteLine("El vehículo se ha agregado con exito.");
        }

        public void bajaVehiculo()
        {
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

            listarVehiculos.remove(vehiculoEncontrado);

            Console.WriteLine("El vehículo se ha dado de baja con éxito.");
        }

        public void modificarVehiculo()
        {
            Console.Write("Ingrese la patente del vehículo: ");
            string patente = Console.ReadLine();

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
            int nuevoAnio = int.Parse(Console.ReadLine());

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

        public void listarVehiculos()
        {
            if (listaVehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos registrados.");
                return;
            }

            Console.WriteLine("      --Lista de vehículos--      ");
            foreach (Vehiculo v in listaVehiculos)
            {
                Console.WriteLine(v);
            }
        }

        public void exportarVehiculos() {}

        //este metodo verifica que los campos no esten vacios
        public bool validarCampos(string patente, string marca, string modelo)
        {
            if(patente == "" || marca == "" || modelo == "")
            {
                return false;
            }
            return true;
        }

        //este método verifica que haya patenta para que no haya patentes duplicadas
        public bool existePatente(string patente)
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
        public bool validarAnio(int anio)
        {
            //decidimos que sean validos años mayores a 1950 hasta el año actual
            if(anio < 1950 || anio > DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }


        public Vehiculo buscarVehiculo(string patente)
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

        public override string toString()
        {
            return
                "Patente: " + patente +
                "\nMarca: " + marca +
                "\nModelo: " + modelo +
                "\nAño: " + anio +
                "\nDisponible: " + 
                (disponible ? "Si" : "No");
        }
    }
}