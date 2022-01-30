using System;
using System.Collections.Generic;
using System.Text;

namespace metjelentes
{
    class Program
    {
        struct Jelentes
        {
            public string kod;
            public string ora;
            public string perc;
            public string irany;
            public int homerseklet;
        }

        static void Main(string[] args)
        {
            var tavirat = System.IO.File.ReadAllLines("tavirathu13.txt");

            Jelentes[] jelentes = new Jelentes[tavirat.Length];

            Jelentes jelentesTemp = new Jelentes();

            for (int i = 0; i < jelentes.Length; i++)
            {
                jelentesTemp.kod = tavirat[i].Split(" ")[0];
                jelentesTemp.ora = tavirat[i].Split(" ")[1].Substring(0,2);
                jelentesTemp.perc = tavirat[i].Split(" ")[1].Substring(2, 2);
                jelentesTemp.irany = tavirat[i].Split(" ")[2];           
                jelentesTemp.homerseklet = Convert.ToInt32(tavirat[i].Split(" ")[3]);

                jelentes[i] = jelentesTemp;
            }

            Console.WriteLine("2. feladat");
            Console.Write("Adja meg egy település kódját! Település: ");
            string telepules = Console.ReadLine();

            int index = jelentes.Length -1;

            while (jelentes[index].kod != telepules) 
            {
                index--;
            }

            Console.WriteLine($"Az utolsó mérési adat a megadott településről {jelentes[index].ora}:{jelentes[index].perc}-kor érkezett. ");

            Console.WriteLine("3. feladat");

            int max = 0;
            int min = jelentes[0].homerseklet;
            string maxTelepules = "";
            string minTelepules= "";
            string maxOra = "";
            string maxPerc = "";
            string minOra = "";
            string minPerc = "";

            for (int i = 0; i < jelentes.Length; i++)
            {
                if (jelentes[i].homerseklet > max)
                {
                    max = jelentes[i].homerseklet;
                    maxTelepules = jelentes[i].kod;
                    maxOra = jelentes[i].ora;
                    maxPerc = jelentes[i].perc;
                }

                if (jelentes[i].homerseklet < min)
                {
                    min = jelentes[i].homerseklet;
                    minTelepules = jelentes[i].kod;
                    minOra = jelentes[i].ora;
                    minPerc = jelentes[i].perc;
                }
            }

            Console.WriteLine($"A legalacsonyabb hőmérséklet: {minTelepules} {minOra} {minPerc} {min} fok.");
            Console.WriteLine($"A legmagasabb hőmérséklet: {maxTelepules} {maxOra} {maxPerc} {max} fok.");

            Console.WriteLine("4. feladat");

            Jelentes[] szelcsend = new Jelentes[jelentes.Length];

            int darab = 0;

            for (int i = 0; i < jelentes.Length; i++)
            {
                if (jelentes[i].irany == "00000")
                {
                    Console.WriteLine($"{jelentes[i].kod} {jelentes[i].ora}:{jelentes[i].perc}");
                    darab++;
                }
            }

            if (darab == 0)
            {
                Console.WriteLine("Nem volt szélcsend a mérések idején.");
            }

            Console.WriteLine("5. feladat");

            List<string> varosok = new List<string>();

            for (int i = 0; i < jelentes.Length; i++)
            {
                if (!varosok.Contains(jelentes[i].kod))
                {
                    varosok.Add(jelentes[i].kod);
                }
            }

            for (int i = 0; i < varosok.Count; i++)
            {
                List<Jelentes> kozepadatok = new List<Jelentes>();
                bool egyOra = false;
                bool hetOra = false;
                bool tizenharomOra = false;
                bool tizenkilencOra = false;

                int kozepÖssz = 0;
                int maxHom = 0;
                int minHom = jelentes[0].homerseklet;

                for (int j = 0; j < jelentes.Length; j++)
                {
                    if (jelentes[j].ora == "01" && jelentes[j].kod == varosok[i])
                    {
                        Jelentes temp = new Jelentes();
                        temp.kod = jelentes[j].kod;
                        temp.ora = jelentes[j].ora;
                        temp.perc = jelentes[j].perc;
                        temp.homerseklet = jelentes[j].homerseklet;
                        kozepadatok.Add(temp);
                        egyOra = true;
                    }

                    if (jelentes[j].ora == "07" && jelentes[j].kod == varosok[i])
                    {
                        Jelentes temp = new Jelentes();
                        temp.kod = jelentes[j].kod;
                        temp.ora = jelentes[j].ora;
                        temp.perc = jelentes[j].perc;
                        temp.homerseklet = jelentes[j].homerseklet;
                        kozepadatok.Add(temp);
                        hetOra = true;
                    }

                    if (jelentes[j].ora == "13" && jelentes[j].kod == varosok[i])
                    {
                        Jelentes temp = new Jelentes();
                        temp.kod = jelentes[j].kod;
                        temp.ora = jelentes[j].ora;
                        temp.perc = jelentes[j].perc;
                        temp.homerseklet = jelentes[j].homerseklet;
                        kozepadatok.Add(temp);
                        tizenharomOra = true;
                    }

                    if (jelentes[j].ora == "19" && jelentes[j].kod == varosok[i])
                    {
                        Jelentes temp = new Jelentes();
                        temp.kod = jelentes[j].kod;
                        temp.ora = jelentes[j].ora;
                        temp.perc = jelentes[j].perc;
                        temp.homerseklet = jelentes[j].homerseklet;
                        kozepadatok.Add(temp);
                        tizenkilencOra = true;
                    }

                    if ((jelentes[j].homerseklet > maxHom) && (jelentes[j].kod == varosok[i]))
                    {
                        maxHom = jelentes[j].homerseklet;
                    }

                    if (jelentes[j].homerseklet < minHom && (jelentes[j].kod == varosok[i]))
                    {
                        minHom = jelentes[j].homerseklet;
                    }
                }

                for (int k = 0; k < kozepadatok.Count; k++)
                {
                    kozepÖssz += kozepadatok[k].homerseklet;
                }

                if (egyOra == false || hetOra == false || tizenkilencOra == false || tizenharomOra == false)
                {
                    Console.WriteLine($"{varosok[i]} NA");
                } else
                {
                    Console.WriteLine($"{varosok[i]} Középhőmérséklet: {(Math.Round((double)kozepÖssz / kozepadatok.Count))}; " +
                        $"Hőmérséklet-ingadozás: {maxHom - minHom}");
                }
            }

            Console.WriteLine("6. feladat");

            for (int i = 0; i < varosok.Count; i++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"{varosok[i]}\n");

                for (int j = 0; j < jelentes.Length; j++)
                {
                    if (jelentes[j].kod == varosok[i])
                    {
                        stringBuilder.Append($"{jelentes[j].ora}:{jelentes[j].perc} ");

                        string szelero = jelentes[j].irany.Substring(3, 2);

                        if (szelero.StartsWith('0'))
                        {
                            for (int k = 0; k < Convert.ToInt32(szelero.Substring(1,1)); k++)
                            {
                                stringBuilder.Append('#');
                            }
                        } else
                        {
                            for (int k = 0; k < Convert.ToInt32(szelero); k++)
                            {
                                stringBuilder.Append('#');
                            }
                        }

                        stringBuilder.Append('\n');
                    }
                }

                System.IO.File.WriteAllText($"{varosok[i]}.txt",stringBuilder.ToString());

            }

            

            Console.ReadKey();
        }
    }
}
