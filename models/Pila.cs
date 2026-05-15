using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquiler_de_autos.models
{

    public class Pila<T>
    {
        private Nodo<T>? tope;

        public void Push(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
        }

        public T Pop()
        {
            if (tope == null)
                throw new InvalidOperationException("Pila vacía");

            T dato = tope.Dato;
            tope = tope.Siguiente;
            return dato;
        }

        public T Peek()
        {
            if (tope == null)
                throw new InvalidOperationException("Pila vacía");

            return tope.Dato;
        }

        public bool EstaVacia()
        {
            return tope == null;
        }
    }
}
