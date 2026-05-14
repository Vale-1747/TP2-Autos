using alquiler_de_autos.models;
using System;
using System.Collections.Generic;
using System.Data;

namespace alquiler_de_autos.controllers
{
    public class nReserva
    {
        private List<Reserva> listaReservas { get; set;} = new List<Reserva>();

        public List<Reserva> GetReservas()
        {
            return listaReservas;
        }
        public void listarReservas()
        {
            if(listaReservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas.");
                return;
            }

            foreach (Reserva r in listaReservas)
            {
                Console.WriteLine(r);
            }
        }

        public bool validarFechas(Vehiculo vehiculo, DateTime desde, DateTime hasta)
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
        public void crearReserva(GestionClientes gestion, nVehiculo gestionVehiculos)
        {
            Console.Write ("Ingrese DNI del cliente: ");
            string dni = Console.ReadLine();
            Cliente cliente = gestion.BuscarPorDNI(dni);

            if (cliente == null)
            {
                Console.WriteLine("No se encontró un cliente con ese DNI.");
                return;
            }

            Console.Write("Ingrese patente del vehículo: ");
            string patente = Console.ReadLine();
            Vehiculo vehiculo = gestionVehiculos.buscarVehiculo(patente);

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
        

        public void vehiculosDisponibles(List<Vehiculo> listaVehiculos)
        {
            Console.Write("Ingrese fecha: ");
            DateTime fecha;

            while(!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.WriteLine("Fecha inválida. Intente nuevamente.");
                return;
            }

            foreach (Vehiculo v in listaVehiculos)
            {
                bool ocupado = false;

                foreach (Reserva r in listaReservas)
                {
                    if (r.vehiculo.patente == v.patente && fecha >= r.fechaInicio && fecha <= r.fechaFin)
                    {
                        ocupado = true;
                    }
                }

                if (!ocupado)
                {
                    Console.WriteLine(v);
                }
            }
        }

    }
}