using Newtonsoft.Json;
using System;
using System.Linq;

namespace Maji
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Usage();
                return;
            }
            switch (args.First()) {
                case "files":

                    break;
                case "strings":

                    break;
                default:

                default:
                    Usage();
                    break;
            }
        }

        static void Usage()
        {
            Console.WriteLine("");
            Console.WriteLine("マージ (Māji) — Dirt-simple JSON merging.");
            Console.WriteLine("Licensed under the 3-Clause BSD License.");
            Console.WriteLine("Developed while jamming by Matt Ronchetto (doamatto)");
            Console.WriteLine("Source: https://github.com/doamatto/maji");
            Console.WriteLine("");
            Console.WriteLine("=== === ===");
            Console.WriteLine("");
            Console.WriteLine("maji files <original> <patch file(s)> | You can specify as many patch files as your heart desires.");
            Console.WriteLine("maji strings <original> <patch string(s)> | You can specify as many patch files as your heart desires.");
            Console.WriteLine("");
            return;
        }
    }
}
