using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{
    public class Correo : IMostrar<List<Paquete>>
    {
        private List<Thread> _mockPaquetes;
        private List<Paquete> _paquetes;

        public List<Paquete> Paquetes
        {
            get { return this._paquetes; }
            set { this._paquetes = value; }
        }

        public Correo()
        {
            this._paquetes = new List<Paquete>();
            this._mockPaquetes = new List<Thread>();
        }

        public void FinEntregas()
        {
            foreach(Thread t in this._mockPaquetes)
            {
                t.Abort();
            }
            this._mockPaquetes.Clear();
        }

        public string MostrarDatos(IMostrar<List<Paquete>> elementos)
        {
            string retorno = "";

            foreach(Paquete p in ((Correo)elementos).Paquetes)
            {
                retorno += string.Format("{0} para {1} ({2})\n", p.TrackingID, p.DireccionEntrega, p.Estado.ToString());
            }

            return retorno;
        }

        public static Correo operator +(Correo c, Paquete p)
        {
            foreach(Paquete i in c.Paquetes)
            {
                if (i == p)
                {
                    throw new TrackingIdRepetidoException(string.Format("El tracking ID {0} ya figura en la lista de envios.", p.TrackingID));
                }
            }

            try
            {
                Thread t = new Thread(p.MockCicloDeVida);

                c._paquetes.Add(p);
                c._mockPaquetes.Add(t);
                t.Start();
            }
            catch (Exception e)
            {
                throw e;
            }

            return c;
        }
    }
}
