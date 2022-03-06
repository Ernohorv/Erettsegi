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
        var file = System.IO.File.ReadAllLines("tavok.txt");

        Futar[] futarok = new Futar[file.Length];

        for (int i = 0; i < futarok.Length; i++)
        {
            futarok[i].nap = Convert.ToInt32(file[i].Split(" ")[0]);
            futarok[i].fuvarszam = Convert.ToInt32(file[i].Split(" ")[1]);
            futarok[i].km = Convert.ToInt32(file[i].Split(" ")[2]);
        }

        Console.WriteLine("2. feladat");

        int index = 0;

        while (index != futarok.Length - 1)
        {

            if (futarok[index].nap == 1 && futarok[index].fuvarszam == 1)
            {
                Console.WriteLine($"Első út: {futarok[index].km} km");
                break;
            }

            index++;
        }

        if (index == futarok.Length)
        {
            Console.WriteLine("Nem dolgozott az 1. napon.");
        }

        int maximum = 0;
        index = 0;

        for (int i = 0; i < futarok.Length; i++)
        {
            if (futarok[i].nap == 7)
            {
                if (futarok[i].fuvarszam > maximum)
                {
                    maximum = futarok[i].fuvarszam;
                    index = i;
                }
            }
        }

        Console.WriteLine("3. feladat");

        Console.WriteLine($"Az utolsó napon az utolsó fuvar: {futarok[index].km} km");

        List<int> napok = new List<int> { 0, 0, 0, 0, 0, 0, 0};

        for (int i = 0; i < futarok.Length; i++)
        {
            if (!napok.Contains(futarok[i].nap))
            {
                napok[futarok[i].nap -1] = futarok[i].nap;
            }
        }

        Console.WriteLine("4. feladat");

        Console.Write("Ezeken a napokon nem dolgozott: ");

        for (int i = 1; i <= 7; i++)
        {
            if (i != napok[i - 1])
            {
                Console.Write($"{i}. nap ");
            }
        }

        Console.WriteLine();
    }
}
