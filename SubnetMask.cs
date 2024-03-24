using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter
{
    internal class SubnetMask : Ip
    {
        public int Cidr { get; private set; }

        public SubnetMask(int cidr)
        {
            Cidr = cidr;
            oct = BinaryOct(cidr);
            decimalIp = ToDecimal(oct);
        }

        string[] BinaryOct(int cidr)
        {
            string[] oct = new string[4];
            int index = 0;
            for(int i=0;i<cidr;i++)
            {
                if(i==8 || i==16 || i==24)
                {
                    index++;
                }
                oct[index] += '1';
            }
            return FillWith0(cidr,oct,ref index);
        }

        string[] FillWith0(int lastPosition, string[] oct,ref int lastIndex)
        {
            for(int i=lastPosition; i<32 ;i++)
            {
                if(i==8 || i==16 || i==24)
                {
                    lastIndex++;
                }
                oct[lastIndex] += '0';
            }
            return oct;
        }

        string[] FillWith0(string[] oct,ref int lastIndex,int upToWhere)
        {
            for (int i = 0; i < upToWhere; i++)
            {
                if (i == 8 || i == 16 || i == 24)
                {
                    lastIndex++;
                }
                oct[lastIndex] += '0';
            }
            return oct;
        }

        string[] FillWith1(int lastPosition, string[] oct, int lastIndex)
        {
            for (int i = lastPosition; i < 32; i++)
            {
                if (i == 8 || i == 16 || i == 24)
                {
                    lastIndex++;
                }
                oct[lastIndex] += '1';
            }
            return oct;
        }

        public Ip WildCardMask()
        {
            string[] wildOct = new string[4];
            int index=0;
            wildOct = FillWith0(wildOct,ref index, Cidr);
            wildOct = FillWith1(Cidr,wildOct,index);
            return new Ip(ToDecimal(wildOct));
        }

        public override string ToString()
        {
            return "SUBNET VALUE: "+base.DecimaleEbinario();
        }
    }
}
