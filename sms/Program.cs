using System;

namespace sms
{
    class Program
    {
        struct Sms
        {
            public int ora;
            public int perc;
            public string telefonszam;
            public string uzenet;
        }
        static void Main(string[] args)
        {
            var file = System.IO.File.ReadAllLines("sms.txt");
            // Az első sor az egész napi smsek számát adja meg
            int darab = Convert.ToInt32(file[0].Split(" ")[0]);
            // Elég akkora tömb, ahány sms van
            Sms[] smsek = new Sms[(darab)];

            int szamlalo = 1;
            // Mivel i-1. elemet nézünk, 1-el tovább kell menni, mint a tömb hossza
            for (int i = 1; i < smsek.Length + 1; i++)
            {
                smsek[i - 1].ora = Convert.ToInt32(file[szamlalo].Split(" ")[0]);
                smsek[i - 1].perc = Convert.ToInt32(file[szamlalo].Split(" ")[1]);
                smsek[i - 1].telefonszam = file[szamlalo].Split(" ")[2];
                // +2 sor van, így kettesével növeljük mindig a számlálót. 
                smsek[i - 1].uzenet = file[szamlalo + 1];
                szamlalo+= 2;
            }

            Console.WriteLine("2. feladat");
            // Utolsó előtti elem volt a kérdés
            Console.WriteLine($"{smsek[smsek.Length - 2].uzenet}");

            Console.WriteLine("3. feladat");
            // Maximum és minimum keresés
            Sms minSms = new Sms();
            Sms maxSms = new Sms();
            minSms.uzenet = smsek[0].uzenet;
            maxSms.uzenet = smsek[0].uzenet;

            for (int i = 0; i < smsek.Length; i++)
            {
                if (smsek[i].uzenet.Length > maxSms.uzenet.Length)
                {
                    maxSms.uzenet = smsek[i].uzenet;
                    maxSms.ora = smsek[i].ora;
                    maxSms.perc = smsek[i].perc;
                    maxSms.telefonszam = smsek[i].telefonszam;
                    maxSms.uzenet = smsek[i].uzenet;
                    Console.WriteLine($"{smsek[i].uzenet.Length}");
                }

                if (smsek[i].uzenet.Length < minSms.uzenet.Length)
                {
                    minSms.uzenet = smsek[i].uzenet;
                    minSms.ora = smsek[i].ora;
                    minSms.perc = smsek[i].perc;
                    minSms.telefonszam = smsek[i].telefonszam;
                    minSms.uzenet = smsek[i].uzenet;
                }
            }

            Console.WriteLine($"{maxSms.ora}:{maxSms.perc} {maxSms.telefonszam} {maxSms.uzenet}");
            Console.WriteLine($"{minSms.ora}:{minSms.perc} {minSms.telefonszam} {minSms.uzenet}");

            Console.WriteLine("4. feladat");
            // 5 intervallum van, 5 változó kell hozzá, szeirintem ehhez nem kell magyarázat
            int a, b, c, d, e;
            a = b = c = d = e = 0;

            for (int i = 0; i < smsek.Length; i++)
            {
                if (smsek[i].uzenet.Length >= 1 && smsek[i].uzenet.Length <= 20)
                {
                    a++;
                }

                if (smsek[i].uzenet.Length >= 21 && smsek[i].uzenet.Length <= 40)
                {
                    b++;
                }

                if (smsek[i].uzenet.Length >= 41 && smsek[i].uzenet.Length <= 60)
                {
                    c++;
                }

                if (smsek[i].uzenet.Length >= 61 && smsek[i].uzenet.Length <= 80)
                {
                    d++;
                }

                if (smsek[i].uzenet.Length >= 81 && smsek[i].uzenet.Length <= 100)
                {
                    e++;
                }
            }

            Console.WriteLine($"1-20: {a}db");
            Console.WriteLine($"21-40: {b}db");
            Console.WriteLine($"41-60: {c}db");
            Console.WriteLine($"61-80: {d}db");
            Console.WriteLine($"81-100: {e}db");


        }
    }
}
