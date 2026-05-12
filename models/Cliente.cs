using System;

namespace TP2Autos.Models
{
    public class Cliente
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"DNI: {DNI} | {Apellido}, {Nombre} | Email: {Email}";
        }
    }
}