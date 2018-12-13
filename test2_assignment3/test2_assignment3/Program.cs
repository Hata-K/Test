using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

namespace test2_assignment3
{
    internal class Program
    {
                
        public static void Main(string[] args)
        {
            DirectoryInfo sourceDir = new DirectoryInfo("source.csv");

            var y = File.ReadAllLines(sourceDir.ToString());
            var x = File.ReadAllLines(sourceDir.ToString()).Length;
            
            List<String> No = new List<String>();
            List<String> Name = new List<String>();
            List<String> dob = new List<String>();
            List<String> start = new List<String>();
            List<String> end = new List<String>();

            for (int i = 1; i <= (x-1); i++)
            {
                string[] eachLine = y[i].Split(',');
                No.Add(eachLine[1]);
                Name.Add(eachLine[1]);
                dob.Add(eachLine[2]);
                start.Add(eachLine[3]);
                end.Add(eachLine[4]);
            }

            var dup = Name.GroupBy(i=>i).Where(g => g.Count() > 1).Select(g => g.Key);

            foreach (var item in dup)
            {
                Console.WriteLine(item);
            }

            int count = 1;
            foreach (var d in dup)
            {
                for (int i = 0; i < Name.Count; i++)
                {
                
                    if (Name[i] == (d))
                    {
                        Name[i] = (Name[i] +"-TERM-"+ count);
                        ++count;
                    }
                    else
                    {
                        continue;
                    }
                }
                count = 1;
            }

            int[] indexes = new int[3];
            
            Random rnd = new Random();
            int index = rnd.Next(1, 77);
            
            indexes[0] = index;
            
            for (int i = 1; i < 3; i++)
            {
                int ind = rnd.Next(1, 77);
                
                if (No[indexes[(i-1)]] != No[ind])
                {
                    indexes[i] = ind;
                }
                else
                {
                    continue;
                }
            }

            count = 1;
            foreach (var s in indexes)
            {
                Console.WriteLine(count+": "+Name[s]);
                count += 1;
            }

            List<DateTime> dateCol = new List<DateTime>();

            for (int i = 0; i < indexes.Length; i++)
            {
                DateTime oDate = Convert.ToDateTime(start[indexes[i]]);
                dateCol.Add(oDate);
            }

            dateCol.Sort();
            dateCol.Reverse();

            for (int i = 0; i < dateCol.Count; i++)
            {
                string da = dateCol[i].ToString("dd/MM/yyyy");
                Console.WriteLine(da);
            }

            Console.WriteLine("\n");
            var kList = new List<KeyValuePair<string, DateTime>>();

            for (int i = 0; i < indexes.Length; i++)
            {
                DateTime oDate = Convert.ToDateTime(start[indexes[i]]);
                kList.Add(new KeyValuePair<string, DateTime>(Name[indexes[i]],oDate));
            }
            kList.Sort((a, b) => (b.Value.CompareTo(a.Value)));
            kList.Reverse();
            foreach (var element in kList)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("Who's the first president to run?...");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int userInputIndex = userInput - 1;
                        
            if (Name[indexes[userInputIndex]] == kList[0].Key)
            {
                Console.WriteLine("Correct!");
                Console.WriteLine(kList[0].Value.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("Oops! Wrong...");
                Console.WriteLine("The correct answer is: {0} --> {1}", kList[0].Key, kList[0].Value.ToString("dd/MM/yyyy"));
            }

            Console.ReadLine();

        }
    }
}