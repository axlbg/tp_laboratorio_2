using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{
    public enum EEstado { Ingresado, EnViaje, Entregado }

    public class Paquete : IMostrar<Paquete>
    {
        public delegate void DelegadoEstado(object sender, EventArgs e);
        public event DelegadoEstado InformaEstado;

        private string _direccionEntrega;
        private EEstado _estado;
        private string _trackingID;
        
        public string DireccionEntrega
        {
            get { return this._direccionEntrega; }
            set { this._direccionEntrega = value; }
        }

        public EEstado Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        public string TrackingID
        {
            get { return this._trackingID; }
            set { this._trackingID = value; }
        }

        public void MockCicloDeVida()
        {
            bool ok = false;
            while (!ok)
            {
                Thread.Sleep(10000);
                if(this._estado == EEstado.Ingresado)
                {
                    this._estado = EEstado.EnViaje;
                }
                else if(this._estado == EEstado.EnViaje)
                {
                    this._estado = EEstado.Entregado;
                }
                else if(this._estado == EEstado.Entregado)
                {
                    try
                    {
                        PaqueteDAO.Insertar(this);
                    }
                    catch (Exception e)
                    {
                        InformaEstado(e, null);
                    }
                    finally
                    {
                        ok = true;
                    }
                }
                InformaEstado(this, null);
            };
        }

        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            Paquete p = (Paquete)elemento;

            return string.Format("{0} para {1}", p.TrackingID, p.DireccionEntrega);
        }
        
        public static bool operator ==(Paquete p1, Paquete p2)
        {
            bool retorno = false;
            if(p1._trackingID == p2._trackingID)
            {
                retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Paquete p1, Paquete p2)
        {
            return !(p1 == p2);
        }

        public Paquete(string direccionEntrega, string trackingID)
        {
            this._direccionEntrega = direccionEntrega;
            this._trackingID = trackingID;
        }

        public override string ToString()
        {
            return MostrarDatos(this);
        }
    }
}
