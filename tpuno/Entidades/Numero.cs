using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Numero
    {
        private double _numero;

        private double ValidarNumero(string strNumero)
        {
            double retorno;
            if (double.TryParse(strNumero, out retorno))
            {
                return retorno;
            }
            return 0;
        }

        private string SetNumero
        {
            set { this._numero = ValidarNumero(value); }
        }

        public string BinarioDecimal(string binario)
        {
            int exponente = binario.Length - 1;
            int num_decimal = 0;

            for (int i = 0; i < binario.Length; i++)
            {
                if (int.Parse(binario.Substring(i, 1)) == 1)
                {
                    num_decimal = num_decimal + int.Parse(System.Math.Pow(2, double.Parse(exponente.ToString())).ToString());
                }
                exponente--;
            }
            return num_decimal.ToString();
        }

        public string DecimalBinario(double numero)
        {
            return DecimalBinario(numero.ToString());
        }

        public string DecimalBinario(string numero)
        {
            string retorno = "";
            int n = int.Parse(numero.ToString());
            string binario = "";
            int l;
            if (n!=1)
            {
                for(l=n; l !=0 && l != 1; l = l/2)
                {
                    binario = (l % 2) + binario;
                }
                if (l == 0)
                {
                    retorno += "0";
                }
                else
                {
                    binario = 1 + binario;
                    retorno += binario;
                }
            }
            else
            {
                retorno += "1";
            }
            return retorno;
        }

        public Numero()
        {
            this._numero = 0;
        }

        public Numero(double numero)
        {
            this._numero = numero;
        }

        public Numero(string strNumero)
        {
            this._numero = int.Parse(strNumero);
        }

        public static double operator -(Numero n1, Numero n2)
        {
            return n1._numero - n2._numero;
        }

        public static double operator *(Numero n1, Numero n2)
        {
            return n1._numero * n2._numero;
        }

        public static double operator /(Numero n1, Numero n2)
        {
            return n1._numero / n2._numero;
        }

        public static double operator +(Numero n1, Numero n2)
        {
            return n1._numero + n2._numero;
        }

    }
}
