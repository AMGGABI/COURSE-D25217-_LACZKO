using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COURSE__D25217__LACZKO
{
    internal class Hallgatok
    {
        public class Hallgato
        {
            public string Nev { get; set; }
            public string Nem { get; set; }
            public double FizetettOsszeg { get; set; }
            public List<int> Pontszamok { get; set; }
            public double Atlag => Pontszamok.Average();
        }
        static List<Hallgato> HallgatokBeolvasasa(string fajlElhelyezes)
        {
            var hallgatok = new List<Hallgato>();
            var sorok = File.ReadAllLines(fajlElhelyezes);
            foreach (var sor in sorok)
            {
                var reszek = sor.Split(';').Select(p => p.Trim()).ToArray();
                var nev = reszek[0];
                var nem = reszek[1];
                double fizetettOsszeg = double.Parse(reszek[2]);
                var pontszamok = reszek.Skip(3).Take(4).Select(int.Parse).ToList();
                hallgatok.Add(new Hallgato { Nev = nev, Nem = nem, FizetettOsszeg = fizetettOsszeg, Pontszamok = pontszamok });
            }
            return hallgatok;
        }
    }
}
