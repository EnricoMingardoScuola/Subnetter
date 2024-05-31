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
        public string Name { get; private set; }
        static public Dictionary<char, Ip> classes { get; private set; }
        Ip netId;
        Ip broadcast;
        SubnetMask mask;
        int cidr;
        public int requiredHost { get; private set; }
        static Network()
        {
            classes = new Dictionary<char, Ip>()
            {
                ['a'] = new Ip("10.0.0.0"),
                ['b'] = new Ip("172.16.0.0"),
                ['c'] = new Ip("192.168.0.0")
            };
        }

        public Network(string nome,char ipClass, int numeroHost) //inizializzazione della prima lan
        {
            if (!classes.ContainsKey(ipClass))
                throw new Exception("Classe invalida");
            cidr = 32 - (int)Math.Round(Math.Log2(numeroHost + 2), MidpointRounding.ToPositiveInfinity);
            Mask = new SubnetMask(cidr);
            char.ToLower(ipClass);
            NetId = classes[ipClass] & mask;
            Broadcast = NetId | mask.WildCardMask();
            requiredHost = numeroHost;
            Name = nome;
        }

        public Network(string nome, Ip previusBroadcast, int numeroHost) //inizializzazione per le lan successive
        {
            cidr = 32 - (int)Math.Round(Math.Log2(numeroHost + 2), MidpointRounding.ToPositiveInfinity);
            Mask = new SubnetMask(cidr);
            try
            {
                NetId = ++previusBroadcast;
            } catch (Exception e) { throw e; }
            Broadcast = NetId | mask.WildCardMask();
            requiredHost = numeroHost;
            Name = nome;
        }

        public Ip NetId 
        {
            get { return netId.Copy(); }
            private set { netId = value; }
        }

        public Ip Broadcast
        {
            get { return broadcast.Copy(); } 
            private set {  broadcast = value; }
        }

        public SubnetMask Mask
        {
            get
            {
                return new SubnetMask(mask.Cidr);
            }
            private set { mask = value; }
        }

        public Ip FirstIp
        {
            get { Ip FirstIp = ++NetId; return FirstIp; }
        }

        public int MaxHost
        {
            get { return (int)Math.Pow(2, 32 - cidr) - 2; }
        }

        public override string ToString()
        {
            return $"{Name} - NetId: {NetId.Ipv4Decimal}/{cidr} --- Broadcast: {Broadcast.Ipv4Decimal}/{cidr} --- Numero richiesto di host: {requiredHost} --- Numero max di host: {MaxHost}";
        }
    }
}
