namespace alquiler_de_autos.models
{
    class Vehiculo
    {
        public string patente {get; set;}
        public string marca {get; set;}
        public string modelo {get; set;}
        public int anio {get; set;}
        public bool disponible {get; set;}

        public Vehiculo(string patente, string marca, string modelo, int anio)
        {
            this.patente = patente;
            this.marca = marca;
            this.modelo = modelo;
            this.anio = anio;
            disponible = true;
        }
    }
}