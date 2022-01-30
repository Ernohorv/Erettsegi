using System;
using System.Collections.Generic;

namespace cegesauto
{
    class Program
    {
        struct Autok
        {
            public int nap { get; set; }
            public int ora { get; set; }
            public int perc { get; set; }
            public string rendszam { get; set; }
            public int szemelyi { get; set; }
            public int km { get; set; }
            public int kibe { get; set; }
        }

        static void Main(string[] args)
        {
            var file = System.IO.File.ReadAllLines("autok.txt");
            Autok[] autok = new Autok[file.Length];

            for (int i = 0; i < autok.Length; i++)
            {
                autok[i].nap = Convert.ToInt32(file[i].Split(" ")[0]);
                autok[i].ora = Convert.ToInt32(file[i].Split(" ")[1].Substring(0, 2));
                autok[i].perc = Convert.ToInt32(file[i].Split(" ")[1].Substring(3, 2));
                autok[i].rendszam = file[i].Split(" ")[2];
                autok[i].szemelyi = Convert.ToInt32(file[i].Split(" ")[3]);
                autok[i].km = Convert.ToInt32(file[i].Split(" ")[4]);
                autok[i].kibe = Convert.ToInt32(file[i].Split(" ")[5]);
            }

            Console.WriteLine("2. feladat");

            int index = autok.Length - 1;

            while (autok[index].kibe != 0)
            {
                index--;
            }

            Console.WriteLine($"{autok[index].nap}. nap rendszám: {autok[index].rendszam}");

            Console.WriteLine("3. feladat");
            Console.Write("Nap: ");
            int nap = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Forgalom a(z) {nap}. napon:");
            for (int i = 0; i < autok.Length; i++)
            {
                if (autok[i].nap == nap)
                {
                    if (autok[i].kibe == 0)
                    {
                        Console.WriteLine($"{autok[i].ora}:{autok[i].perc} {autok[i].rendszam} {autok[i].szemelyi} ki");
                    }
                    else
                    {
                        Console.WriteLine($"{autok[i].ora}:{autok[i].perc} {autok[i].rendszam} {autok[i].szemelyi} be");
                    }
                }
            }

            int osszeg = 0;

            for (int i = 0; i < autok.Length; i++)
            {
                if (autok[i].kibe == 1)
                {
                    osszeg++;
                }
            }

            Console.WriteLine($"A hónap végén {(autok.Length - (2 * osszeg))}  autót nem hoztak vissza. ");

            List<string> rendszamok = new List<string>();

            for (int i = 0; i < autok.Length; i++)
            {
                if (!rendszamok.Contains(autok[i].rendszam))
                {
                    rendszamok.Add(autok[i].rendszam);
                }
            }

            rendszamok.Sort();

            for (int i = 0; i < rendszamok.Count; i++)
            {
                int max = 0;
                int szamlalo = 0;
                int min = 0;

                for (int j = 0; j < autok.Length; j++)
                {
                    if (autok[j].rendszam == rendszamok[i])
                    {
                        if (szamlalo == 0)
                        {
                            min = autok[j].km;
                            szamlalo++;
                        }

                        if (autok[j].km > max)
                        {
                            max = autok[j].km;
                        }
                    }
                }

                Console.WriteLine($"{rendszamok[i]} {max-min}");
            }
        }
    }
}
