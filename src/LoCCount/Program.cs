using System.IO;

namespace LoCCount
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var arg = @"D:\FlowDesign\LoCCount\";

            var startVerzeichnis = new DirectoryInfo(arg);

            var dateiFinder = new DateiFinder(startVerzeichnis);

            var dateien = dateiFinder.AlleDateienImVerzeichnis();
        }
    }
}