using System;
using System.Collections.Generic;
using System.Text;

namespace godrok
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("melyseg.txt");
            int[] melysegek = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                melysegek[i] = Convert.ToInt32(lines[i]);
            }

            Console.WriteLine("1. feladat");

            Console.WriteLine($"A fájl adatainak száma: {melysegek.Length}\n");

            Console.Write("Adjon meg egy távolságértéket! ");

            int tavolsag = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Ezen a helyen a felszín {melysegek[tavolsag-1]} méter mélyen van.\n");

            double erintetlen = 0;

            foreach (var item in melysegek)
            {
                if (item == 0)
                {
                    erintetlen++;
                }
            }

            double szazalek = erintetlen / melysegek.Length * 100;

            Console.WriteLine("3. feladat");
            Console.WriteLine($"Az érintetlen terület aránya {Math.Round(szazalek, 2)}%\n");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < melysegek.Length; i++)
            {
                if(melysegek[i] != 0)
                {
                    sb.Append(melysegek[i] + " ");

                    if (melysegek[i+1] == 0)
                    {
                        sb.Append("\n");
                    }
                }
            }

            System.IO.File.WriteAllText("godrok.txt", sb.ToString());

            int godrokSzama = 0;

            for (int i = 0; i < sb.Length; i++)
            {
               if(sb[i].ToString() == "\n")
                {
                    godrokSzama++;
                }
            }

            Console.WriteLine("5. feladat");
            Console.WriteLine($"A gödrök száma: {godrokSzama}\n");

            if (melysegek[tavolsag-1] == 0)
            {
                Console.WriteLine("Az adott helyen nincs gödör.\n");
            } else
            {
                int poz = tavolsag - 1;

                while (melysegek[poz] > 0)
                {
                    poz--;            
                }

                int balSzel = poz + 2;

                poz = tavolsag - 1;

                while (melysegek[poz] > 0)
                {
                    poz++;
                }

                int jobbSzel = poz;

                Console.WriteLine("6. feladat");
                Console.WriteLine("a)");
                Console.WriteLine($"A gödör kezdete: {balSzel}, a gödör vége: {jobbSzel}.");

                poz = balSzel;

                while (melysegek[poz] >= melysegek[poz - 1 ] && poz <= jobbSzel)
                {
                    poz++;
                }

                while (melysegek[poz] <= melysegek[poz - 1] && poz <= jobbSzel)
                {
                    poz++;
                }

                Console.WriteLine("b)");

                if (poz > jobbSzel)
                {
                    Console.WriteLine("Folyamatosan mélyül");
                } else
                {
                    Console.WriteLine("Nem mélyül folyamatosan");
                }

                int legmelyebb = 0;

                for (int i = balSzel -1; i < jobbSzel; i++)
                {
                    if (melysegek[i] > legmelyebb)
                    {
                        legmelyebb = melysegek[i];
                    }
                }

                Console.WriteLine("c)");

                Console.WriteLine($"A legnagyobb mélysége: {legmelyebb} méter.");

                int terfogat = 0;

                for (int i = balSzel -1; i <= jobbSzel; i++)
                {
                    terfogat += (melysegek[i] * 10);
                }

                Console.WriteLine("d)");
                Console.WriteLine($"A térfogata {terfogat} m^3");

                for (int i = balSzel; i <= jobbSzel; i++)
                {
                    terfogat -= 10;
                }

                Console.WriteLine("e)");
                Console.WriteLine($"A vízmennyiség {terfogat} m^3");
            }

            Console.ReadKey();
        }
    }
}
