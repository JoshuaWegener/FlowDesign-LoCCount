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

            void AusgabeAlle(IEnumerable<Locstat> locstats) => KommandozeilenAusgabe(locstats);

            var analyse = AnalysiereDateien(dateinamen, AusgabeAlle);

            void AusgabeGesamt(int zeilen, int codeZeilen)
            {
                Console.WriteLine($"Gesamt: Zeilen:{zeilen} | Code Zeilen:{codeZeilen}");

                Console.WriteLine("Beliebige Taste drücken...");
                Console.ReadKey();
            }

            SummiereLocstats(analyse, AusgabeGesamt);
        }

        private static void SummiereLocstats(IEnumerable<Locstat> analyse, Action<int, int> ausgabeGesamt)
        {
            var zeilen = analyse.Sum(datei => datei.AnzahlZeilen);
            var codeZeilen = analyse.Sum(datei => datei.AnzahlCodeZeilen);

            ausgabeGesamt(zeilen, codeZeilen);
        }

        private static void KommandozeilenAusgabe(IEnumerable<Locstat> analyse)
        {
            foreach (var locstat in analyse)
            {
                Console.WriteLine(
                    $"{locstat.Dateiname}: Zeilen:{locstat.AnzahlZeilen} | Code Zeilen:{locstat.AnzahlCodeZeilen}");
            }
        }

        private static IEnumerable<Locstat> AnalysiereDateien(IEnumerable<string> dateinamen,
            Action<IEnumerable<Locstat>> ausgabe)
        {
            return DateiAnalysierer.AlleDateien(dateinamen, ausgabe).ToList();
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