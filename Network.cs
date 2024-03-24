using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter
{
    internal class Network
    {
        static Dictionary<char, Ip> classes;
        public Ip NetId { get; private set; }
        public Ip Broadcast { get; private set; }
        int cidr;

        static Network()
        {
            classes = new Dictionary<char, Ip>()
            {
                ['a'] = new Ip("10.0.0.0"),
                ['b'] = new Ip("172.16.0.0"),
                ['c'] = new Ip("192.168.0.0")
            };
        }

        public Network(char ipClass,int numeroHost)
        {
            if (!classes.ContainsKey(ipClass))
                throw new Exception("Classe invalida");
            cidr = 32 - (int)Math.Round(Math.Log2(numeroHost+2), MidpointRounding.ToPositiveInfinity);
            SubnetMask mask = new SubnetMask(cidr);
            NetId = classes[ipClass] & mask;
            //TODO: BROADCAST,WILDCARD SUBNET & VARIUS ERROR HANDLING
        }

        public int MaxHost
        {
            get { return (int)Math.Pow(2, 32 - cidr); }
        }

        public override string ToString()
        {
            return $"NetId: {NetId.Ipv4Decimal}/{cidr}";
        }
    }
}
