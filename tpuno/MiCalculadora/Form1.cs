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

namespace MiCalculadora
{
    public partial class LaCalculadora : Form
    {
        public LaCalculadora()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            lblResultado.Text = "";
        }

        private static double Operar(string numero1, string numero2, string operador)
        {
            Numero n1 = new Numero(numero1);
            Numero n2 = new Numero(numero2);

            return Calculadora.Operar(n1, n2, operador);
        }
        
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            Numero n1 = new Numero();

            string resultado = lblResultado.Text;

            if (resultado != "")
            {
                lblResultado.Text = n1.DecimalBinario(resultado);
            }
        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            Numero n1 = new Numero();

            string resultado = lblResultado.Text;

            if (resultado != "")
            {
                lblResultado.Text = n1.BinarioDecimal(resultado);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            lblResultado.Text = Operar(txtNumero1.Text, txtNumero2.Text, cmbOperador.Text).ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
