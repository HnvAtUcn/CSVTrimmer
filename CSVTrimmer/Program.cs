using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTrimmer
{
    class Program
    {
        static void Main(string[] args)
        {
            String folderpath = "C:\\Users\\HNV\\Desktop\\LocalMaterial\\F2020\\Hildur F2020 - Kopi";
            String tail = ";;;;;;;;;;;;;;\n";
            if (args.Length > 0)
            {
                //Console.WriteLine(args[0]);
                //Console.WriteLine(args[0].Replace(@"\", @"\\"));
                folderpath = @args[0].Replace(@"\", @"\\"); 
                //Console.WriteLine(args[0].Replace(@"\", @"\\"));
                //folderpath = @"C:\\Users\\HNV\\Desktop\\LocalMaterial\\F2020\\Hildur F2020\\";
                //folderpath = folderpath.Replace(@"\", @"\\");
            }
            foreach (string filepath in Directory.EnumerateFiles(folderpath,"*.csv"))
            {
                //Console.WriteLine("IN " + folderpath + ": " + filepath);
                string output = "";
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader r = new StreamReader(fs))
                    {
                        String allText = r.ReadToEnd();
                        string[] rows = allText.Split('\n');
                        bool first = true;
                        foreach (string row in rows)
                        {
                            if (first)
                            {
                                output += row;
                                first = false;
                            }
                            else
                            {
                                string[] items = row.Split(';');
                                if (items.Length > 5)
                                {
                                    int count = 0;
                                    while (count < 5)
                                    {
                                        output += items[count];
                                        if (count < 4)
                                        {
                                            output += ";";
                                        }
                                        else
                                        {
                                            output += tail;
                                        }
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }

                using (FileStream fs = new FileStream(filepath, FileMode.Truncate))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        w.Write(output);
                    }
                }
            }
        }
    }
}
