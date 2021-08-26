using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace Maji
{
    class Program
    {
        static void Main(string[] args)
        {
            // Handle no files with help information
            if (args == null || args.Length == 0 || args.Length <1)
            {
                Usage();
                return;
            }

            // Check if first file exists
            if (!File.Exists(args[1]))
            {
                throw new ArgumentException(String.Format(@"The file, {0}, does not exist.
                Please ensure the file is there, that Maji has permissions to read and
                write to that directory and/or file, and that no other issues (such as
                anti-virus) could be blocking it. Otherwise, submit an issue at
                https://github.com/doamatto/maji/issues", args[1]));
            }
            // Parse first file to variable
            string orig = File.ReadAllText(args[1]);
            JObject jsonResp = JObject.Parse(orig);
            foreach (string file in args)
            {
                // Make sure we aren't using the command as a file somehow
                if (file == args.First())
                {
                    continue;
                }
                // Make sure the file exists; if not throw an error
                if (!File.Exists(file))
                {
                    throw new ArgumentException(String.Format(@"The file, {0}, does not exist.
                    Please ensure the file is there, that Maji has permissions to read and
                    write to that directory and/or file, and that no other issues (such as
                    anti-virus) could be blocking it. Otherwise, submit an issue at
                    https://github.com/doamatto/maji/issues", file));
                }

                // Read file into temp and merge
                string t = File.ReadAllText(file);
                jsonResp.Merge(JObject.Parse(t), new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Replace
                });
            }

            // Pass merged to file
            string name = "merged-" + Path.GetFileName(args[1]);
            string dir = Path.GetFullPath(args[1]);
            dir = Path.GetDirectoryName(dir);
            string newpath = Path.Combine(dir, name);
            File.WriteAllText(newpath, jsonResp.ToString());
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
