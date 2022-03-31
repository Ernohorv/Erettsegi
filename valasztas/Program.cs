using System.Text;

class Program
{
    struct Szavazatok
    {
        public int sorszam;
        public int szavazat;
        public string vezeteknev;
        public string keresztnev;
        public string rovidites;
    }
    public static void Main()
    {
        var file = System.IO.File.ReadAllLines("szavazatok.txt");
        Szavazatok[] szavazatok = new Szavazatok[file.Length];

        for (int i = 0; i < file.Length; i++)
        {
            szavazatok[i].sorszam = Convert.ToInt32(file[i].Split(" ")[0]);
            szavazatok[i].szavazat = Convert.ToInt32(file[i].Split(" ")[1]);
            szavazatok[i].vezeteknev = file[i].Split(" ")[2];
            szavazatok[i].keresztnev = file[i].Split(" ")[3];
            szavazatok[i].rovidites = file[i].Split(" ")[4];
        }

        Console.WriteLine("2. feladat");

        Console.WriteLine($"A helyhatósági választáson {szavazatok.Length} képviselőjelölt indult. ");

        Console.WriteLine("3. feladat");

        Console.Write("Vezeteknev: ");
        string vezeteknev = Console.ReadLine();
        Console.Write("Keresztnev: ");
        string keresztnev = Console.ReadLine();

        for (int i = 0; i < szavazatok.Length; i++)
        {
            if (szavazatok[i].vezeteknev == vezeteknev && szavazatok[i].keresztnev == keresztnev)
            {
                Console.WriteLine($"{vezeteknev} {keresztnev} {szavazatok[i].szavazat}");
                break;
            }

            if (i == szavazatok.Length - 1)
            {
                Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            }
        }

        int osszeg = 0;

        for (int i = 0; i < szavazatok.Length; i++)
        {
            osszeg += szavazatok[i].szavazat;
        }

        Console.WriteLine($"A választáson {osszeg} állampolgár, a jogosultak {Math.Round(Convert.ToDouble(osszeg) / 12345, 2) * 100}%-a vett részt.");

        List<string> roviditesek = new List<string>();

        for (int i = 0; i < szavazatok.Length; i++)
        {
            if (!roviditesek.Contains(szavazatok[i].rovidites))
            {
                roviditesek.Append(szavazatok[i].rovidites);
            }
        }

        int szOsszeg = 0;

        for (int i = 0; i < roviditesek.Count; i++)
        {
            for (int j = 0; j < szavazatok.Length; j++)
            {
                if (szavazatok[j].rovidites == roviditesek[i])
                {
                    szOsszeg += szavazatok[j].szavazat;
                }
            }

            if (roviditesek[i] == "GYEP")
            {
                Console.WriteLine($"Gyümölcsevők Pártja= {Math.Round(Convert.ToDouble(szOsszeg) / osszeg, 2) * 100}");
            } else if (roviditesek[i] == "ZEP")
            {
                Console.WriteLine($"Zöldségevők Pártja= {Math.Round(Convert.ToDouble(szOsszeg) / osszeg, 2) * 100}");
            } else if (roviditesek[i] == "TISZ")
            {
                Console.WriteLine($"Tejivók Szövetsége= {Math.Round(Convert.ToDouble(szOsszeg) / osszeg, 2) * 100}");
            } else if (roviditesek[i] == "HEP")
            {
                Console.WriteLine($"Húsevők Pártja= {Math.Round(Convert.ToDouble(szOsszeg) / osszeg, 2) * 100}");
            } else if (roviditesek[i] == "-")
            {
                Console.WriteLine($"Független jelöltek= {Math.Round(Convert.ToDouble(szOsszeg) / osszeg, 2) * 100}");
            }

            szOsszeg = 0;
        }

        int max = 0;

        for (int i = 0; i < szavazatok.Length; i++)
        {
            if (szavazatok[i].szavazat > max)
            {
                max = szavazatok[i].szavazat;
            }
        }

        for (int i = 0; i < szavazatok.Length; i++)
        {
            if (szavazatok[i].szavazat == max)
            {
                if (szavazatok[i].rovidites == "-")
                {
                    Console.WriteLine($"{szavazatok[i].vezeteknev} {szavazatok[i].keresztnev} független");
                } else
                {
                    Console.WriteLine($"{szavazatok[i].vezeteknev} {szavazatok[i].keresztnev} {szavazatok[i].rovidites}");
                }
            }
        }

        // Rendezzük a szavazatokat sorszám szerint.
        Szavazatok[] rendezettSzavazatok = szavazatok.OrderBy(s=>s.sorszam).ToArray();
        // 1-ről indulnak a sorszámok.
        int korzet = 1;
        StringBuilder sb = new StringBuilder();
        /** Létrehozunk egy új Szavazat objektumot mivel nem lenne túk szép
         * külön változóban tárolni minden adatot amire szükség van.
         **/
        Szavazatok sz = new Szavazatok();
        // A szavazatok száma viszont 0-ról undul.
        sz.szavazat = 0;
        // Itt már a rendezett tömbön megyünk végig.
        for (int i = 0; i < rendezettSzavazatok.Length; i++)
        {
            /** Sorban végigmegyünk a körzeteken.
             *  Mivel sorba vannak rendezve, így simán csak növeljük mindig 1-el a sorszámot.
            **/
            if (korzet == rendezettSzavazatok[i].sorszam)
            {
                // Maximum keresés :)
                if (rendezettSzavazatok[i].szavazat >= sz.szavazat)
                {
                    sz.sorszam = rendezettSzavazatok[i].sorszam;
                    sz.szavazat = rendezettSzavazatok[i].szavazat;
                    sz.rovidites = rendezettSzavazatok[i].rovidites;
                    sz.vezeteknev = rendezettSzavazatok[i].vezeteknev;
                    sz.keresztnev = rendezettSzavazatok[i].keresztnev;
                }
            } 
            /** Ha nem egyenlő a körzet az adott sorszámmal, az azt jelenti, hogy elértünk a következő körzethez.
             *  Hozzáadjuk a string builderhez, növeljük a körzet számát és lenullázzuk a szavazatok számát.
            * */
            else
            {
                // Kezeljük a függetleneket is.
                if (sz.rovidites == "-")
                {
                    sz.rovidites = "független";
                    sb.Append($"{sz.sorszam} {sz.vezeteknev} {sz.keresztnev} {sz.rovidites}\n");
                } else
                {
                    sb.Append($"{sz.sorszam} {sz.vezeteknev} {sz.keresztnev} {sz.rovidites}\n");
                }
                korzet++;
                sz.szavazat = 0;
                if (rendezettSzavazatok[i].szavazat >= sz.szavazat)
                {
                    sz.sorszam = rendezettSzavazatok[i].sorszam;
                    sz.szavazat = rendezettSzavazatok[i].szavazat;
                    sz.rovidites = rendezettSzavazatok[i].rovidites;
                    sz.vezeteknev = rendezettSzavazatok[i].vezeteknev;
                    sz.keresztnev = rendezettSzavazatok[i].keresztnev;
                }
            }
            // Ha elértünk a tömb végére, akkor csak hozzáadjuk a string builderhez az aktuális adatokat.
            if (i == rendezettSzavazatok.Length - 1)
            {
                if (sz.rovidites == "-")
                {
                    sz.rovidites = "független";
                    sb.Append($"{sz.sorszam} {sz.vezeteknev} {sz.keresztnev} {sz.rovidites}\n");
                }
                else
                {
                    sb.Append($"{sz.sorszam} {sz.vezeteknev} {sz.keresztnev} {sz.rovidites}\n");
                }
            }
        }
        // És persze a végén fájlba írjuk.
        File.WriteAllText("kepviselok.txt", sb.ToString());
    }
}
