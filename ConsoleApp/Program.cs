using System;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var n = Console.ReadLine();
                string nf = "";
                try
                {
                    nf = NumericFormConverter.Convert(n);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(nf + '\n');

                if (!string.IsNullOrWhiteSpace(nf))
                    Speech.Speak(nf);
            }
        }
    }
}
