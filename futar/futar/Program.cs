class Program
{
    struct Futar
    {
        public int nap;
        public int fuvarszam;
        public int km;
    }
    public static void Main()
    {
        /// Létrehozunk egy var típusú változót, amibe beolvassuk a fájlt soronként.
        /// Ez string tömb típusú lesz, aminek minden eleme a fájl egy-egy sora.
        var file = System.IO.File.ReadAllLines("tavok.txt");
        /// Létrehozunk egy tömböt, amiben Futar típusú adatokat fogunk tárolni.
        /// A tömb mérete egyenlő a fájl sorainak a számával.
        Futar[] futarok = new Futar[file.Length];
        /// Végigmegyünk a teljes tömbön, amiben már alapból létrejönnek a Futar elemek, 
        /// amik kezdetben még üresek.
        for (int i = 0; i < futarok.Length; i++)
        {
            /// A tömb minden elemének értéket adunk.
            /// A string tömb adott elemét (a file egy sorát) Split(" ")-el felbontjuk egy újabb tömbre,
            /// aminek az adott eleme lesz az adott változó értéke.
            /// file[i] (string) -> file[i].Split(" ") (string tömb) -> file[i].Split(" ")[0] (string)
            futarok[i].nap = Convert.ToInt32(file[i].Split(" ")[0]);
            futarok[i].fuvarszam = Convert.ToInt32(file[i].Split(" ")[1]);
            futarok[i].km = Convert.ToInt32(file[i].Split(" ")[2]);
        }

        Console.WriteLine("2. feladat");

        int index = 0;
        // A tömb végéig megyünk
        while (index != futarok.Length - 1)
        {
            // Ha elértünk az 1. nap 1. fuvaráig, akkor kiírjuk a km számát és kilépünk a cilkusból.
            if (futarok[index].nap == 1 && futarok[index].fuvarszam == 1)
            {
                Console.WriteLine($"Első út: {futarok[index].km} km");
                break;
            }
            // Fontos, hogy csak azután növeljük az indexet, hogy megvizsgáltunk egy elemet.
            index++;
        }

        // Ha a tömb végére értünk, akkor nem dolgozott az 1. napon.
        if (index == futarok.Length)
        {
            Console.WriteLine("Nem dolgozott az 1. napon.");
        }

        index = 0;
        int maximum = 0;
        int maxnap = 0;
        // A tömb végéig megyünk.
        for (int i = 0; i < futarok.Length; i++)
        {
            /// Ha elértünk az utolsó napig, akkor megkeressük, hogy melyik volt az utolsó fuvarszám.
            /// Azért kell még 1 maximum keresés, mert lehet, hogy a 7. napon nem dolgozott.
            if (futarok[i].nap >= maxnap)
            {
                maxnap = futarok[i].nap;

                if (futarok[i].fuvarszam > maximum)
                {
                    maximum = futarok[i].fuvarszam;
                    index = i;
                }
            }
        }

        Console.WriteLine($"Az utolsó napon az utolsó fuvar: {futarok[index].km} km");

        Console.WriteLine("3. feladat");
        // Létrehozunk 7 db 0-ával feltöltött listát.
        List<int> napok = new List<int> { 0, 0, 0, 0, 0, 0, 0};
        // A tömb végéig megyünk.
        for (int i = 0; i < futarok.Length; i++)
        {
            /// Ha nincs még benne a listában az adott nap, akkor hozzáadjuk,
            /// méghozzá a listában arra a helyre, ahanyadik napot vizsgáljuk.
            /// nap - 1, mivel nullától hatig indexeljük a listát
            if (!napok.Contains(futarok[i].nap))
            {
                napok[futarok[i].nap -1] = futarok[i].nap;
            }
        }

        Console.WriteLine("4. feladat");
        // Azért nincs szükség rendezésre, mert alapból sorrendben szúrtuk be az elemeket.
        Console.Write("Ezeken a napokon nem dolgozott: ");
        // 1-7-ig, mivel ezekre a napokra vagyunk kíváncsiak.
        for (int i = 1; i <= 7; i++)
        {
            /// Ha minden napon dolgozna, akkor 1-7-ig lenne feltöltve a lista,
            /// és minden elem megegyezne az adott index értékével. 
            if (i != napok[i - 1])
            {
                Console.Write($"{i}. nap ");
            }
        }

        Console.WriteLine();
    }
}
