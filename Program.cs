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
            JObject jsonResp;
            if (args == null || args.Length == 0)
            {
                Usage();
                return;
            }
            switch (args.First()) {
                case "files":
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
                    jsonResp = JObject.Parse(orig);
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
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                    }

                    // Pass merged to file
                    string name = "merged-" + Path.GetFileName(args[1]);
                    string dir = Path.GetDirectoryName(args[1]);
                    File.WriteAllText(String.Format("{dir}/merged-{0}", dir, name), jsonResp.ToString());

                    break;

                case "strings":
                    // Parse first item
                    jsonResp = JObject.Parse(args[1]);
                    for (int i=0; i<args.Length;i++)
                    {
                        // Make sure we aren't using the command as a string somehow
                        if (args[0] == args.First())
                        {
                            continue;
                        }

                        // Parse string into return
                        jsonResp.Merge(JObject.Parse(args[i]), new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Union
                        });
                    }

                    // Return merged strings
                    Console.WriteLine(jsonResp.ToString());
                    break;

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
