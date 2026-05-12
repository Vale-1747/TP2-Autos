using alquiler_de_autos.models;
using System;
using System.Collections.Generic;

namespace alquiler_de_autos.controllers
{
    class nReserva
    {
        public List<Reserva> listaReservas = new List<Reserva>();

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

        public void crearReserva(Cliente cliente, Vehiculo vehiculo)
        {
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
            DateTime fecha = DateTime.Parse(Console.ReadLine());

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