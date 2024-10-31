using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestjugadorProg;
using Moq;


namespace TestjugadorProg1
{


    [TestClass]
    public class JugadorTests
    {
        // Pruebas de creación de jugador
        [TestMethod]
        public void Jugador_IniciaConValoresCorrectos()
        {
            var jugador = new Jugador("Carlos");
            Assert.AreEqual("Carlos", jugador.Nombre);
            Assert.AreEqual(0, jugador.Puntos);
            Assert.AreEqual(3, jugador.Vidas);
            Assert.AreEqual(1, jugador.Nivel);
            Assert.IsFalse(jugador.HaGanado);
        }

        [TestMethod]
        public void Jugador_IniciaConTresVidas()
        {
            var jugador = new Jugador("Carlos");
            Assert.AreEqual(3, jugador.Vidas);
        }

        [TestMethod]
        public void Jugador_IniciaSinHaberGanado()
        {
            var jugador = new Jugador("Carlos");
            Assert.IsFalse(jugador.HaGanado);
        }

        // Pruebas de ganar puntos
        [TestMethod]
        public void GanarPuntos_IncrementaCorrectamente()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(50);
            Assert.AreEqual(50, jugador.Puntos);
        }

        [TestMethod]
        public void GanarPuntos_NoPermiteNegativos()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(-10);
            // No debe cambiar los puntos
            Assert.AreEqual(0, jugador.Puntos);
        }

        [TestMethod]
        public void GanarPuntos_100Puntos_AvanceDeNivel()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(100);
            Assert.AreEqual(2, jugador.Nivel);
            Assert.AreEqual(0, jugador.Puntos);
            // Los puntos se resetean al cambiar de nivel
        }

        [TestMethod]
        public void GanarPuntos_MultiplesSuma()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(60);
            jugador.GanarPuntos(50);
            Assert.AreEqual(10, jugador.Puntos);
            // Suma total pasa los 100, y los puntos restantes quedan
            Assert.AreEqual(2, jugador.Nivel);
        }

        [TestMethod]
        public void GanarPuntos_AlAcumularVariosNiveles()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(250);
            Assert.AreEqual(3, jugador.Nivel);
            // Avanza dos niveles (250/100 = 2 niveles)
            Assert.AreEqual(50, jugador.Puntos);
            // Sobran 50 puntos
        }

        // Pruebas de perder vidas
        [TestMethod]
        public void PerderVida_DecrementaCorrectamente()
        {
            var jugador = new Jugador("Carlos");
            jugador.PerderVida();
            Assert.AreEqual(2, jugador.Vidas);
        }

        [TestMethod]
        public void PerderVida_NoDeberiaBajarDeCero()
        {
            var jugador = new Jugador("Carlos");
            jugador.PerderVida();
            jugador.PerderVida();
            jugador.PerderVida();
            jugador.PerderVida();
            // Intento de pérdida extra
            Assert.AreEqual(0, jugador.Vidas);
        }

        [TestMethod]
        public void PerderVida_ConVidasMinimas()
        {
            var jugador = new Jugador("Carlos");
            jugador.PerderVida();
            jugador.PerderVida();
            jugador.PerderVida();
            // Todas las vidas se pierden
            Assert.IsFalse(jugador.EstaVivo());
        }

        // Pruebas de ganar vida
        [TestMethod]
        public void GanarVida_IncrementaCorrectamente()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarVida();
            Assert.AreEqual(4, jugador.Vidas);
            // Pasa de 3 a 4
        }

        [TestMethod]
        public void GanarVida_MultiplesVeces()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarVida();
            jugador.GanarVida();
            Assert.AreEqual(5, jugador.Vidas);
            // Incrementa correctamente
        }

        // Pruebas de avance de nivel
        [TestMethod]
        public void AvanzarNivel_IncrementaCorrectamente()
        {
            var jugador = new Jugador("Carlos");
            jugador.AvanzarNivel();
            Assert.AreEqual(2, jugador.Nivel);
        }

        [TestMethod]
        public void AvanzarNivel_10NivelesJugadorGana()
        {
            var jugador = new Jugador("Carlos");
            for (int i = 0; i < 9; i++)
            {
                jugador.AvanzarNivel();
            }
            Assert.AreEqual(10, jugador.Nivel);
            Assert.IsTrue(jugador.HaGanado);
        }

        [TestMethod]
        public void NoAvanzarNivel_HastaGanar()
        {
            var jugador = new Jugador("Carlos");
            jugador.AvanzarNivel();
            Assert.IsFalse(jugador.HaGanado);
            // Jugador no debe ganar aún
        }

        // Pruebas de reinicio de juego
        [TestMethod]
        public void ReiniciarJuego_RestableceValoresIniciales()
        {
            var jugador = new Jugador("Carlos");
            jugador.GanarPuntos(150);
            jugador.PerderVida();
            jugador.ReiniciarJuego();

            Assert.AreEqual(0, jugador.Puntos);
            Assert.AreEqual(3, jugador.Vidas);
            Assert.AreEqual(1, jugador.Nivel);
            Assert.IsFalse(jugador.HaGanado);
        }

        [TestMethod]
        public void ReiniciarJuego_DespuesDeGanar()
        {
            var jugador = new Jugador("Carlos");
            for (int i = 0; i < 9; i++)
            {
                jugador.AvanzarNivel();
            }
            jugador.ReiniciarJuego();

            Assert.AreEqual(0, jugador.Puntos);
            Assert.AreEqual(3, jugador.Vidas);
            Assert.AreEqual(1, jugador.Nivel);
            Assert.IsFalse(jugador.HaGanado);
        }


        [TestMethod]
        public void Jugador_EstaVivoConVidas()
        {
            var jugador = new Jugador("Carlos");
            Assert.IsTrue(jugador.EstaVivo());
        }

        [TestMethod]
        public void Jugador_NoEstaVivoSinVidas()
        {
            var jugador = new Jugador("Carlos");
            jugador.PerderVida();
            jugador.PerderVida();
            jugador.PerderVida();
            Assert.IsFalse(jugador.EstaVivo());
        }

        [TestMethod]
        public void Jugador_MantieneNombreTrasReiniciar()
        {
            var jugador = new Jugador("Carlos");
            jugador.ReiniciarJuego();
            Assert.AreEqual("Carlos", jugador.Nombre);
            // El nombre no cambia tras reiniciar
        }
    }
    [TestClass]
    public class BancoTests
    {
        [TestMethod]
        public void RealizarPago_DeberiaLlamarAProcesadorDePagosConMontoCorrecto()
        {

            var mockProcesadorDePagos = new Mock<IProcesadorDePagos>();
            mockProcesadorDePagos.Setup(p => p.ProcesarPago(It.IsAny<decimal>())).Returns(true);

            var banco = new Banco(mockProcesadorDePagos.Object);


            banco.RealizarPago(100);


            mockProcesadorDePagos.Verify(p => p.ProcesarPago(100), Times.Once);
        }

        [TestMethod]
        public void RealizarPago_NoDeberiaLlamarAProcesadorDePagosSiMontoEsCero()
        {

            var mockProcesadorDePagos = new Mock<IProcesadorDePagos>();
            var banco = new Banco(mockProcesadorDePagos.Object);


            banco.RealizarPago(0);


            mockProcesadorDePagos.Verify(p => p.ProcesarPago(It.IsAny<decimal>()), Times.Never);
        }
    }


    [TestClass]
    public class CarritoIntegrationTests
    {
        [TestMethod]
        public void AgregarArticulo_DeberiaAumentarElTotal()
        {
            // Arrange
            var carrito = new Carrito();

            // Act
            carrito.AgregarArticulo("Manzana", 1.50m);
            carrito.AgregarArticulo("Pan", 2.00m);

            // Assert
            Assert.AreEqual(3.50m, carrito.Total);
        }

        [TestMethod]
        public void ProcesarPago_DeberiaSerExitosoConTotalPositivo()
        {
            // Arrange
            var carrito = new Carrito();
            var procesadorDePagos = new ProcesadorDePagos();

            carrito.AgregarArticulo("Manzana", 1.50m);
            carrito.AgregarArticulo("Pan", 2.00m);

            // Act
            var resultadoPago = procesadorDePagos.ProcesarPago(carrito.Total);

            // Assert
            Assert.IsTrue(resultadoPago);
        }

        [TestMethod]
        public void ProcesarPago_DeberiaFallarConTotalCero()
        {
            // Arrange
            var carrito = new Carrito();
            var procesadorDePagos = new ProcesadorDePagos();

            // Act
            var resultadoPago = procesadorDePagos.ProcesarPago(carrito.Total); // Total es 0

            // Assert
            Assert.IsFalse(resultadoPago);
        }

        [TestMethod]
        public void AgregarVariosArticulos_DeberiaRegistrarArticulosCorrectamente()
        {
            // Arrange
            var carrito = new Carrito();

            // Act
            carrito.AgregarArticulo("Manzana", 1.50m);
            carrito.AgregarArticulo("Pan", 2.00m);
            carrito.AgregarArticulo("Leche", 1.20m);

            // Assert
            Assert.AreEqual(3, carrito.Articulos.Count);
            CollectionAssert.AreEqual(new List<string> { "Manzana", "Pan", "Leche" }, carrito.Articulos);
        }

        [TestMethod]
        public void AgregarArticulo_DeberiaActualizarTotalCorrectamente()
        {
            // Arrange
            var carrito = new Carrito();

            // Act
            carrito.AgregarArticulo("Manzana", 1.50m);

            // Assert
            Assert.AreEqual(1.50m, carrito.Total);
        }
    }
}




