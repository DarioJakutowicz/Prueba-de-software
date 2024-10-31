using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestjugadorProg
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

    }
    public class Jugador
    {
        public string Nombre { get; private set; }
        public int Puntos { get; private set; }
        public int Vidas { get; private set; }
        public int Nivel { get; private set; }
        public bool HaGanado { get; private set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Puntos = 0;
            Vidas = 3;
            Nivel = 1;
            HaGanado = false;
        }

        public void GanarPuntos(int puntos)
        {
            if (puntos < 0) return; // Los puntos no pueden ser negativos
            Puntos += puntos;
            if (Puntos >= 100)
            {
                AvanzarNivel();
                Puntos -= 100; // Se ajustan los puntos para avanzar nivel
            }
        }

        public void PerderVida()
        {
            if (Vidas > 0)
            {
                Vidas--;
            }
        }

        public void AvanzarNivel()
        {
            Nivel++;
            if (Nivel >= 10)
            {
                HaGanado = true;
            }
        }

        public void ReiniciarJuego()
        {
            Puntos = 0;
            Vidas = 3;
            Nivel = 1;
            HaGanado = false;
        }

        public void GanarVida()
        {
            Vidas++;
        }

        public bool EstaVivo()
        {
            return Vidas > 0;
        }
    }
    public interface IProcesadorDePagos
    {
        bool ProcesarPago(decimal monto);
    }

    public class Banco
    {
        private readonly IProcesadorDePagos _procesadorDePagos;

        public Banco(IProcesadorDePagos procesadorDePagos)
        {
            _procesadorDePagos = procesadorDePagos;
        }

        public bool RealizarPago(decimal monto)
        {
            if (monto <= 0)
                return false;

            return _procesadorDePagos.ProcesarPago(monto);
        }
    }
    public class Carrito
    {
        public List<string> Articulos { get; private set; }
        public decimal Total { get; private set; }

        public Carrito()
        {
            Articulos = new List<string>();
            Total = 0;
        }

        public void AgregarArticulo(string articulo, decimal precio)
        {
            Articulos.Add(articulo);
            Total += precio;
        }
    }
    public class ProcesadorDePagos
    {
        public bool ProcesarPago(decimal monto)
        {
            // Simula un procesamiento de pago que siempre es exitoso si el monto es mayor que 0.
            return monto > 0;
        }
    }


}
