namespace banyato;

public class Program
{
    static List<string[]> list = new List<string[]>();
    static int Sor, Oszlop;
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        StreamReader sr = new StreamReader(@"melyseg.txt");

        Sor = int.Parse(sr.ReadLine());
        Oszlop = int.Parse(sr.ReadLine());

        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split();
            list.Add(line);
        }

        sr.Close();
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");

        Console.Write("A mérés sorának azonosítója: ");
        int sor = int.Parse(Console.ReadLine()) - 1;

        Console.Write("A mérés oszlopának azonosítója: ");
        int oszlop = int.Parse(Console.ReadLine()) - 1;

        string meres = list[sor].ElementAt(oszlop);
        Console.WriteLine($"A mért mélység az adott helyen {meres} dm.\n");
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        int count = 0;
        decimal ossz = 0;

        foreach (var item in list)
        {
            foreach (var x in item)
            {
                if (x != "0")
                {
                    count++;
                    ossz += int.Parse(x);
                }
            }
        }

        Console.WriteLine($"A tó felszíne: {count} m2, átlagos mélysége: {Math.Round(ossz / count / 10, 2)} m.\n");
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        var melysegek = new List<int[]>();
        int max = 0, sor = 0, oszlop = 0;

        foreach (var item in list)
        {
            sor++;

            foreach (var x in item)
            {
                oszlop++;

                if (int.Parse(x) >= max)
                {
                    max = int.Parse(x);
                    int[] line = { max, sor, oszlop };
                    melysegek.Add(line);
                }
            }

            oszlop = 0;
        }

        melysegek = melysegek.OrderByDescending(x => x.ElementAt(0)).ToList();
        max = 0;
        Console.WriteLine($"A tó legnagyobb mélysége: {melysegek[0].ElementAt(0)} dm.");
        Console.WriteLine("A legmélyebb helyek sor-oszlop koordinátái: ");

        foreach (var item in melysegek)
        {
            if (item[0] >= max)
            {
                max = item[0];
                Console.Write($"({item[1]}; {item[2]})\t");
            }
        }

        Console.WriteLine();
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        int count = 0;

        for (int i = 0; i < Sor; i++)
        {
            for (int j = 0; j < Oszlop; j++)
            {
                if (list[i].ElementAt(j) != "0")
                {
                    if (list[i - 1].ElementAt(j) == "0") count++;
                    if (list[i + 1].ElementAt(j) == "0") count++;
                    if (list[i].ElementAt(j - 1) == "0") count++;
                    if (list[i].ElementAt(j + 1) == "0") count++;
                }
            }
        }

        Console.WriteLine($"A tó partvonala {count} m hosszú.\n");
    }
    private static void Feladat6()
    {
        Console.WriteLine("6. feladat");

        Console.Write($"A vizsgált szelvény oszlopának azonosítója: ");
        int oszlop = int.Parse(Console.ReadLine()) - 1;

        StreamWriter sw = new StreamWriter(@"diagram.txt");
        int count = 0;
        var szures = new List<int>();

        foreach (var item in list)
        {
            szures.Add(int.Parse(item[oszlop]));
        }

        foreach (var item in szures)
        {
            count++;
            sw.Write($"{count:D2}");

            for (int i = 0; i < (int)(Math.Round(item / 10.0)); i++)
            {
                sw.Write("*");
            }

            sw.WriteLine();
        }

        sw.Close();
    }
}