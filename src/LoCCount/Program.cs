using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoCCount
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startVerzeichnis = LeseArgumente(args);

            var dateinamen = LeseDateinamen(startVerzeichnis);

            var analyse = AnalysiereDateien(dateinamen);

            KommandozeilenAusgabe(analyse);
        }

        private static void KommandozeilenAusgabe(IEnumerable<Locstat> analyse)
        {
            foreach (var locstat in analyse)
            {
                Console.WriteLine($"{locstat.Dateiname}: Zeilen:{locstat.AnzahlZeilen} | Code Zeilen:{locstat.AnzahlCodeZeilen}");
            }

            Console.WriteLine("Beliebige Taste drücken...");
            Console.ReadKey();
        }

        private static IEnumerable<Locstat> AnalysiereDateien(IEnumerable<string> dateinamen)
        {
            var analyse = DateiAnalysierer.AlleDateien(dateinamen).ToList();

            analyse.Add(new Locstat
            {
                Dateiname = "Gesamt",
                AnzahlCodeZeilen = analyse.Sum(datei => datei.AnzahlCodeZeilen),
                AnzahlZeilen = analyse.Sum(datei => datei.AnzahlZeilen)
            });

            return analyse;
        }

        private static IEnumerable<string> LeseDateinamen(DirectoryInfo startVerzeichnis)
        {
            var dateiFinder = new DateiFinder(startVerzeichnis);

            var dateinamen = dateiFinder.AlleDateienImVerzeichnis();

            return dateinamen;
        }

        private static DirectoryInfo LeseArgumente(string[] args)
        {
            var startVerzeichnis = new DirectoryInfo(args[0]);
            return startVerzeichnis;
        }
    }
}