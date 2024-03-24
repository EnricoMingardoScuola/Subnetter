namespace Subnetter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ip ip1 = new Ip("10.0.15.0");
            Console.WriteLine(ip1.ToString());
            SubnetMask s1 = new SubnetMask(17);
            Console.WriteLine(s1.ToString());
            Network LAN1 = null;
            PrintMenu();
            try
            {
                LAN1 = new Network('c', 1);
                Console.WriteLine(LAN1.ToString());
                Console.WriteLine(LAN1.MaxHost);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine(s1.WildCardMask().ToString());
            Console.WriteLine(LAN1.FirstIp.ToString());
            Console.ReadLine();
        }

        static void PrintMenu()
        {
            string[] options = { "Inserisci IP", "Inserisci SUBNET" };
            for(int i=0;i<options.Length;i++)
                Console.WriteLine($"[{i+1}] "+options[i]);
        }

        static void PrintError(string message)
        {

        }
    }
}