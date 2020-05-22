using System.Collections.Generic;
using System.IO;

namespace LoCCount
{
    internal class DateiAnalysierer
    {
        public static IEnumerable<Locstat> AlleDateien(IEnumerable<string> dateinamen)
        {
            var ergebnis = new List<Locstat>();

            foreach (var dateiname in dateinamen)
            {
                var dateiInhalt = LeseDatei(dateiname);

                var analyse = AnalysiereDateiInhalt(dateiname, dateiInhalt);

                ergebnis.Add(analyse);
            }

            return ergebnis;
        }

        private static string[] LeseDatei(string dateiname)
        {
            return File.ReadAllLines(dateiname);
        }

        private static Locstat AnalysiereDateiInhalt(string dateiname, string[] dateiInhalt)
        {
            var zeilen = 0;
            var codeZeilen = 0;

            foreach (var zeile in dateiInhalt)
            {
                if (string.IsNullOrWhiteSpace(zeile))
                {
                    zeilen++;
                    continue;
                }

                if (!Kommentarzeile(zeile))
                    codeZeilen++;

                zeilen++;
            }

            return new Locstat
            {
                AnzahlCodeZeilen = codeZeilen,
                AnzahlZeilen = zeilen,
                Dateiname = dateiname
            };
        }

        private static bool Kommentarzeile(string zeile)
        {
            return zeile.TrimStart().StartsWith("//");
        }
    }
}