using System.IO;
using FluentAssertions;
using Xunit;

namespace LoCCount.Tests
{
    public class DateiFinderSpecs
    {
        [Fact]
        public void Anzahl_Dateien_In_Verzeichnis_Ist_8()
        {
            var startVerzeichnisPfad = @"D:\FlowDesign\LoCCount\";

            var startVerzeichnis = new DirectoryInfo(startVerzeichnisPfad);
            var dateiFinder = new DateiFinder(startVerzeichnis);

            dateiFinder.AlleDateienImVerzeichnis().Should().BeEquivalentTo(8);
        }
    }
}