using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace FrmPpal
{
    public partial class FrmPpal : Form
    {
        private Correo correo = new Correo();

        public FrmPpal()
        {
            InitializeComponent();
            lstEstadoEntregado.ContextMenuStrip = cmsListas;
        }

        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            if(!Object.ReferenceEquals(elemento, null))
            {
                IMostrar<T> imostrarElemento = (IMostrar<T>)elemento;
                string texto = imostrarElemento.MostrarDatos(elemento);
                rtbMostrar.Text = texto;
                try
                {
                    texto.Guardar("salida.txt");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void ActualizarEstados()
        {
            lstEstadoIngresado.Items.Clear();
            lstEstadoEnviaje.Items.Clear();
            lstEstadoEntregado.Items.Clear();

            foreach (Paquete paquete in correo.Paquetes)
            {
                if(paquete.Estado == EEstado.Ingresado)
                {
                    lstEstadoIngresado.Items.Add(paquete);
                }
                else if(paquete.Estado == EEstado.EnViaje)
                {
                    lstEstadoEnviaje.Items.Add(paquete);
                }
                else if(paquete.Estado == EEstado.Entregado)
                {
                    lstEstadoEntregado.Items.Add(paquete);
                }
            }
        }

        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                if(sender is Exception)
                {
                    Exception excepcion = (Exception)sender;
                    MessageBox.Show(excepcion.Message);
                }
                else
                {
                    ActualizarEstados();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Paquete p = new Paquete(txtDireccion.Text, mtxtTrackingID.Text);
            p.InformaEstado += paq_InformaEstado;

            try
            {
                correo = correo + p;
            }
            catch(TrackingIdRepetidoException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
            catch(Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
            ActualizarEstados();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            correo.FinEntregas();
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }
    }
}
