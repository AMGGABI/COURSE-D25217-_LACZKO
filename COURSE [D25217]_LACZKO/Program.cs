using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    public class Hallgato
    {
        public string Nev { get; set; }
        public string Nem { get; set; }
        public double FizetettOsszeg { get; set; }
        public List<int> Pontszamok { get; set; }
        public double Atlag => Pontszamok.Average();
    }

    static void Main(string[] args)
    {
        
        string fajlElhelyezes = "..\\..\\..\\src\\course.txt"; 
        var hallgatok = HallgatokBeolvasasa(fajlElhelyezes);

        Console.WriteLine("\n1.feladat");
        Console.WriteLine($"A fájlban {hallgatok.Count} hallgató adatai szerepelnek.");

        Console.WriteLine("\n2.feladat");
        double backendAtlag = hallgatok.Average(h => h.Pontszamok[0]);
        Console.WriteLine($"A backend fejlesztés modul átlaga: {backendAtlag}");

        Console.WriteLine("\n3.feladat");
        var legjobbHallgato = hallgatok.OrderByDescending(h => h.Pontszamok.Sum()).First();
        Console.WriteLine($"Az osztályelső: {legjobbHallgato.Nev}");

        Console.WriteLine("\n4.feladat");
        double ferfiakAranya = hallgatok.Count(h => h.Nem == "m") / (double)hallgatok.Count();
        Console.WriteLine($"A férfiak aránya: {ferfiakAranya * 100}%");

        Console.WriteLine("\n5.feladat");
        var legjobbNoiWebFejlesztesben = hallgatok
            .Where(h => h.Nem == "f")
            .OrderByDescending(h => h.Pontszamok[1] + h.Pontszamok[2])
            .First();
        Console.WriteLine($"A legjobb női hallgató webfejlesztésből: {legjobbNoiWebFejlesztesben.Nev}");

        Console.WriteLine("\n6.feladat");
        double tanfolyamDija = 2600;
        var fizetettHallgatok = hallgatok.Where(h => h.FizetettOsszeg == tanfolyamDija).ToList();
        Console.WriteLine($"Előfinanszírozott teljes díjat: {fizetettHallgatok.Count} hallgató.");

        Console.WriteLine("\n7.feladat");
        Console.Write("Kérem, adja meg a hallgató nevét: ");
        string hallgatoNev = Console.ReadLine();
        var hallgatoAkiMegfelel = hallgatok.FirstOrDefault(h => h.Nev.Equals(hallgatoNev, StringComparison.OrdinalIgnoreCase));
        if (hallgatoAkiMegfelel != null)
        {
            var javitovizsgak = hallgatoAkiMegfelel.Pontszamok.Select((pont, index) => new { Index = index, Pont = pont })
                                              .Where(x => x.Pont < 51)
                                              .Select(x => GetModulNeve(x.Index))
                                              .ToList();
            if (javitovizsgak.Any())
            {
                Console.WriteLine($"A hallgatónak javítóvizsgát kell tennie: {string.Join(", ", javitovizsgak)}");
            }
            else
            {
                Console.WriteLine("A hallgatónak nincs szüksége javítóvizsgára.");
            }
        }
        else
        {
            Console.WriteLine("A hallgató nem található.");
        }

    }

    static string GetModulNeve(int index)
    {
        return index switch
        {
            0 => "Backend",
            1 => "Frontend",
            2 => "Webfejlesztés",
            3 => "Webdesign",
            _ => "Ismeretlen"
        };
    }

    static List<Hallgato> HallgatokBeolvasasa(string fajlElhelyezes)
    {
        var hallgatok = new List<Hallgato>();
        using (StreamReader sr = new(fajlElhelyezes, Encoding.UTF8))
        {
            sr.ReadLine(); 
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var reszek = sor.Split(';').Select(p => p.Trim()).ToArray();
                var nev = reszek[0];
                var nem = reszek[1];
                double fizetettOsszeg = double.Parse(reszek[2]);
                var pontszamok = reszek.Skip(3).Take(4).Select(int.Parse).ToList();
                hallgatok.Add(new Hallgato { Nev = nev, Nem = nem, FizetettOsszeg = fizetettOsszeg, Pontszamok = pontszamok });
            }
        }
        return hallgatok;
    }
}