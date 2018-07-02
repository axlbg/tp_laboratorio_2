using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace Test
{
    [TestClass]
    public class TestUnitarios
    {
        [TestMethod]
        public void TestListaDePaquetesDelCorreoInstanciada()
        {
            Correo c = new Correo();
            Paquete p = new Paquete("Direccion de prueba", "000-000-0001");

            try
            {
                c = c + p;
            }
            catch(NullReferenceException e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TrackingIdRepetidoException))]
        public void TestDosPaquetesConElMismoTrackingId()
        {
            Correo c = new Correo();
            Paquete p1 = new Paquete("Direccion de prueba uno", "000-000-0001");
            Paquete p2 = new Paquete("Direccion de prueba dos", "000-000-0001");
            
            c = c + p1;
            c = c + p2;
        }
    }
}
