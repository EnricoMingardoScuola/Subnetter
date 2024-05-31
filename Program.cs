using System.IO.Pipes;
using System.Xml.Linq;

namespace Subnetter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int nNetworks;
            string nome;
            char privateIpClass;
            List<int> nHost = new List<int>();
            Dictionary<string, int> nameAndHosts = new Dictionary<string, int>();
            Console.WriteLine("Quante sottoreti con tecnica VLSM hai bisogno?");
            try
            {
                Console.Write("Risposta: ");
                nNetworks = int.Parse(Console.ReadLine());
            }
            catch (Exception e) { PrintError(e.Message); Console.ReadLine(); return; }
            Console.WriteLine("Che IP privato vuoi utilizzare?");
            foreach (var item in Network.classes)
            {
                Console.WriteLine("- " + item.Key + " : " + item.Value.Ipv4Decimal);
            }
            try
            {
                Console.Write("Risposta: ");
                privateIpClass = char.Parse(Console.ReadLine());
                privateIpClass = char.ToUpper(privateIpClass);
                if (!Network.classes.ContainsKey(privateIpClass)) throw new Exception("Classe non valida");
            }
            catch (Exception e) { PrintError(e.Message); Console.ReadLine(); return; }
            for (int i = 0; i < nNetworks; i++)
            {
                Console.Clear();
                Console.WriteLine($"Come si chiama la network numero {i + 1} ?");
                Console.Write("Risposta: ");
                nome = Console.ReadLine();
                Console.WriteLine("Di quanti host hai bisogno?");
                try
                {
                    Console.Write("Risposta: ");

                    nHost.Add(int.Parse(Console.ReadLine()));
                    nameAndHosts.Add(nome, nHost[i]); //salvo il nome con la chiave che è il numero degli host
                }
                catch (Exception e) { PrintError(e.Message); }
            }
            nHost.Sort();
            nHost.Reverse();
            Network[] networks = new Network[nNetworks];
            networks[0] = new Network(SearchFirstValueInDictionary(nameAndHosts, nHost[0]), privateIpClass, nHost[0]);
            for (int i = 1; i < nNetworks; i++)
            {
                try
                {
                    networks[i] = new Network(SearchFirstValueInDictionary(nameAndHosts, nHost[i]), networks[i - 1].Broadcast, nHost[i]);
                }catch(Exception e) { PrintError(e.Message); return; }
            }
            Console.Clear();
            Console.WriteLine("------------SUBNETTING CALCOLATO------------");
            foreach (var network in networks)
            {
                Console.WriteLine(network);
            }
            Console.ReadLine();
        }
        static string SearchFirstValueInDictionary(Dictionary<string, int> dictionary, int value)
        {
            string key = "";
            foreach (var item in dictionary)
            {
                if (item.Value == value)
                {
                    key = item.Key;
                    dictionary.Remove(key);
                    break;
                }
            }
            return key;
        }
        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}