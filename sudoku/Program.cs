class Program
{
    public static void Main()
    {
        Console.WriteLine("1. feladat");
        Console.Write("Adja meg a bemeneti fájl nevét! ");
        string fajlnev = Console.ReadLine();
        Console.Write("Adja meg egy sor számát! ");
        int sor = Convert.ToInt32(Console.ReadLine());
        Console.Write("Adja meg egy oszlop számát! ");
        int oszlop = Convert.ToInt32(Console.ReadLine());

        var file = System.IO.File.ReadAllLines(fajlnev);

        int[,] sudoku = new int[9, 9];
        int[,] megoldasok = new int[4, 3];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sudoku[i, j] = Convert.ToInt32(file[i].Split(" ")[j]);
            }
        }

        int index = 0;

        for (int i = 9;i < file.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                megoldasok[index, j] = Convert.ToInt32(file[i].Split(" ")[j]);
            }
            index++;
        }

        Console.WriteLine("3. feladat");

        if (sudoku[sor - 1, oszlop -1] == 0)
        {
            Console.WriteLine("Az adott helyet még nem töltötték ki.");
        } else
        {
            Console.WriteLine($"{sudoku[sor - 1, oszlop - 1]}");

            Console.WriteLine($"{3*((sor -1)/3) + ((oszlop - 1)/3 + 1)}");
        }

        Console.WriteLine("4. feladat");

        int nullak = 0;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudoku[i,j] == 0)
                {
                    nullak++;
                }
            }
        }

        Console.WriteLine($"Az üres helyek aránya: {Math.Round(Convert.ToDouble(nullak*100)/81)}");

        for (int i = 0; i < 4; i++)
        {
            int szam = megoldasok[i, 0];
            int sorszam = megoldasok[i, 1];
            int oszlopszam = megoldasok[i, 2];

            if (sudoku[sorszam - 1, oszlopszam - 1] != 0)
            {
                Console.WriteLine("A helyet már kitöltötték");
            }

            for (int j = 0; j < 9; j++)
            {
                if (sudoku[sorszam -1,j] == szam)
                {
                    Console.WriteLine("Az adott sorban már szerepel a szám");
                }
            }

            for (int k = 0;k < 9; k++)
            {
                if (sudoku[k, oszlopszam - 1] == szam)
                {
                    Console.WriteLine("Az adott oszlopban már szerepel a szám");
                }
            }

            if (i >=0 && j >=0 && i <= 4 && j <= 4)
            {
                Console.WriteLine("1. resztabla");
            }
        }
    }
}
