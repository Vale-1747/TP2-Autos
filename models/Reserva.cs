<<<<<<< HEAD
using TP2Autos.Models;

namespace alquiler_de_autos.models
{
    public class Reserva
    {
         public Vehiculo vehiculo { get; set; }
         public Cliente cliente { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public Reserva(Vehiculo vehiculo, Cliente cliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            this.vehiculo = vehiculo;
            this.cliente = cliente;
            this.FechaDesde = fechaDesde;
            this.FechaHasta = fechaHasta;
        }
    }
}
=======
using System;

namespace alquiler_de_autos.models
{
    class Reserva
    {
        public DateTime fechaInicio {get; set;}
        public DateTime fechaFin {get; set;}
        public Cliente cliente {get; set;}
        public Vehiculo vehiculo {get; set;}

        public Reserva(DateTime fechaInicio, DateTime fechafin, Cliente cliente, Vehiculo vehiculo)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechafin;
            this.cliente = cliente;
            this.vehiculo = vehiculo;    
        }

        public override string ToString()
        {
            return "Cliente: " + cliente +
                   "\nVehiculo: " + vehiculo.marca + " " + vehiculo.modelo + " (" + vehiculo.patente + ")" +
                   "\nFecha desde: " + fechaInicio.ToShortDateString() +
                   "\nFecha hasta: " + fechaFin.ToShortDateString();
        }
    }
}   
>>>>>>> 08837f89a9ca6527db935f302391cd8414547dc5
