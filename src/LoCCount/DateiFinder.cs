using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoCCount
{
    internal class DateiFinder
    {
        private readonly DirectoryInfo _startVerzeichnis;

        public DateiFinder(DirectoryInfo startVerzeichnis)
        {
            _startVerzeichnis = startVerzeichnis;
        }

        public IEnumerable<string> AlleDateienImVerzeichnis()
        {
            var unterverzeichnisse = _startVerzeichnis.GetDirectories();
            var dateien = new List<FileInfo>();

            dateien.AddRange(_startVerzeichnis.GetFiles());

            foreach (var unterverzeichnis in unterverzeichnisse)
            {
                dateien.AddRange(unterverzeichnis.GetFiles());
            }

            var dateinamen = dateien.Where(datei => datei.FullName.EndsWith(".cs")).Select(datei => datei.FullName);

            return dateinamen;
        }
    }
}