using System;
using System.Collections.Generic;
using System.Text;

namespace cegesauto
{
    class Program
    {
        struct Autok
        {
            public int nap { get; set; }
            public string ora { get; set; }
            public string perc { get; set; }
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
                autok[i].ora =file[i].Split(" ")[1].Substring(0, 2);
                autok[i].perc = file[i].Split(" ")[1].Substring(3, 2);
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

            Console.WriteLine("4. feladat");
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

                Console.WriteLine("5. feladat");
                Console.WriteLine($"{rendszamok[i]} {max-min}");
            }

            Console.WriteLine("6. feladat");

            List<int> szemelyik = new List<int>();

            for (int i = 0; i < autok.Length; i++)
            {
                if (!szemelyik.Contains(autok[i].szemelyi))
                {
                    szemelyik.Add(autok[i].szemelyi);
                }
            }

            int ossz = 0;
            int ki = 0;
            int be = 0;
            int szemely = 0;

            for (int i = 0; i < szemelyik.Count; i++)
            {
                for (int j = 0; j < autok.Length; j++)
                {
                    if (szemelyik[i] == autok[j].szemelyi)
                    {
                        if (autok[j].kibe == 0)
                        {
                            ki = autok[j].km;
                        }

                        if (autok[j].kibe == 1)
                        {
                            be = autok[j].km;

                            if ((be - ki) > ossz)
                            {
                                ossz = be - ki;
                                szemely = szemelyik[i];
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Leghosszabb út: {ossz} km, személy: {szemely}");

            Console.WriteLine("7. feladat");

            Console.Write("Rendszám: ");
            string rendszam = Console.ReadLine();

            StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < autok.Length; i++)
                {
                    if (autok[i].rendszam == rendszam)
                    {
                        if (autok[i].kibe == 0)
                        {
                            stringBuilder.Append($"{autok[i].szemelyi}\t{autok[i].nap}. {autok[i].ora}:{autok[i].perc}\t{autok[i].km} km \t");
                       
                        } else
                        {
                            stringBuilder.Append($"{autok[i].nap}. {autok[i].ora}:{autok[i].perc}\t{autok[i].km} km\n");
                        }
                    }
                }

            Console.WriteLine(stringBuilder);
        }
    }
}
