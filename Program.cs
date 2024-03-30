namespace Subnetter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ip ip1 = new Ip("255.255.255.255");
            Console.WriteLine("IP1 : "+ip1.ToString());
            SubnetMask s1 = new SubnetMask(17);
            Console.WriteLine("SUBNET MASK "+ s1.ToString());
            Network LAN1 = null;
            PrintMenu();
            try
            {
                LAN1 = new Network("LAN1",'c', 254);
                Console.WriteLine(LAN1.ToString());
                Console.WriteLine("MAXHOST LAN1 "+LAN1.MaxHost);
            }
            catch (Exception ex) { PrintError(ex.Message); }
            Console.WriteLine("WILDCARD MASK "+s1.WildCardMask().ToString());
            Console.WriteLine("FIRST IP LAN1 "+LAN1.FirstIp.ToString());
            Console.WriteLine("BROADCAST LAN1 "+LAN1.Broadcast);
            Ip NextLan = LAN1.Broadcast;
            try
            {
                NextLan++;
            }catch (Exception ex) { PrintError(ex.Message); }
            Console.WriteLine("NEXT LAN: " + (NextLan.ToString()));
            try
            {
                Console.WriteLine((++ip1).ToString());
            }catch (Exception ex) { PrintError(ex.Message); }

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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}