using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter
{
    internal class Ip
    {
        protected string[] oct;
        protected string decimalIp;

        protected Ip()
        {
        }

        public Ip(string ipv4)
        {
            decimalIp = ipv4;
            oct = ToBits(ipv4);
        }

        protected string[] ToBits(string ipv4)
        {
            string[] split = ipv4.Split('.');
            int temp;
            string octet ="";
            int index = 0;
            string[] bits = new string[4];
            foreach (string s in split)
            {
                int.TryParse(s, out temp);
                while (temp > 0)
                {
                    octet += temp % 2;
                    temp /= 2;
                }
                octet = FillWith0(octet);
                bits[index]=Reverse(octet);
                index++;
                octet = "";
            }
            return bits;
        }

        protected string ToDecimal(string[] binaryOct)
        {
            string decimalIp = "";
            int octValue = 0;
            int powerOfTwo = 7;
            foreach (string oct in binaryOct)
            {
                for (int i = 0; i < oct.Length; i++)
                {
                    if (oct[i] == '1')
                    {
                        octValue += (int)Math.Pow(2, powerOfTwo);
                    }
                    powerOfTwo--;
                }
                decimalIp += octValue + ".";
                octValue = 0;
                powerOfTwo = 7;
            }
            decimalIp = decimalIp.Remove(decimalIp.Length - 1, 1);
            return decimalIp;
        }

        private string FillWith0(string s)
        {
            for(int i=s.Length; i!=8; i++)
            {
                s += '0';
            }
            return s;
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        protected string DecimaleEbinario()
        {
            return $"DECIMALE: {Ipv4Decimal} ---- BINARIO: {Ipv4Binary}";
        }

        public string Ipv4Decimal
        {
            get { return decimalIp; }
        }
        
        public string Ipv4Binary
        {
            get { return $"{oct[0]}.{ oct[1]}.{ oct[2]}.{ oct[3]}"; }
        }

        public override string ToString()
        {
            return "IP VALUE: "+DecimaleEbinario();
        }

        public static Ip operator &(Ip ip1, Ip ip2)
        {
            string[] newIpOct = new string[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ip1.oct[i][j] == '0')
                        newIpOct[i] += '0';
                    else
                    {
                        if (ip2.oct[i][j] == '0')
                            newIpOct[i] += '0';
                        else
                            newIpOct[i] += '1';
                    }
                }
            }
            Ip temp = new Ip();
            return new Ip(temp.ToDecimal(newIpOct));
        }
    }
}
