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
        public class MultiDimDictList<K, T>: Dictionary<K, List<T>>  { }
        
        public static void Main(string[] args)
        {
            DirectoryInfo sourceDir = new DirectoryInfo("source.csv");
            
            //var myDicList = new MultiDimDictList<string, string> ();
            var myDicList = new MultiDimDictList<string, List<String>>();;

            var y = File.ReadAllLines(sourceDir.ToString());
            var x = File.ReadAllLines(sourceDir.ToString()).Length;
            //Console.WriteLine(x);
            
            List<String> No = new List<String>();
            List<String> Name = new List<String>();
            List<String> dob = new List<String>();
            List<String> start = new List<String>();
            List<String> end = new List<String>();

            for (int i = 1; i <= (x-1); i++)
            {
                string[] eachLine = y[i].Split(',');
                //Console.WriteLine(eachLine.Length);
                //Console.WriteLine(eachLine[1] + " " + eachLine[2] + " " + eachLine[3] + " "+ eachLine[4]);
                //myDicList.Add("No" , eachLine[0]);
                No.Add(eachLine[1]);
                Name.Add(eachLine[1]);
                dob.Add(eachLine[2]);
                start.Add(eachLine[3]);
                end.Add(eachLine[4]);
                for(int r = 1; r < eachLine.Length; r++)
                {
                    //Console.WriteLine(eachLine[r]);
                    
                }
            }
            /*
            foreach (var VARIABLE in Name)
            {
                
                if (VARIABLE == ("Thomas Pelham-Holles"))
                {
                    
                    VARIABLE = VARIABLE +"-TERM-"+ count);
                    ++count;
                }
                else
                {
                    Console.WriteLine(VARIABLE);
                }
                
            }*/
            var dup = Name.GroupBy(i=>i).Where(g => g.Count() > 1).Select(g => g.Key);

            int count = 1;
            foreach (var d in dup)
            {
                for (int i = 0; i < Name.Count; i++)
                {
                
                    if (Name[i] == (d))
                    {
                    
                        Name[i] = (Name[i] +"-TERM-"+ count);
                        ++count;
                        //Console.WriteLine(Name[i]);
                    }
                    else
                    {
                        //Console.WriteLine(Name[i]);
                        continue;
                    }
                }
                count = 1;
            }

            foreach (var VARIABLE in Name)
            {
                //Console.WriteLine(VARIABLE);
            }
            /*
            int[] listOfItems = new[] { 4, 2, 3, 1, 6, 4, 3 };
            var duplicates = listOfItems
                .GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            foreach (var d in duplicates)
                Console.WriteLine(d);  
            */
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
                //Console.WriteLine(s);
                Console.WriteLine(count+": "+Name[s]);
                count += 1;
            }

            //DateTime oDate = Convert.ToDateTime(start[indexes[0]]);
            List<DateTime> dateCol = new List<DateTime>();

            for (int i = 0; i < indexes.Length; i++)
            {
                DateTime oDate = Convert.ToDateTime(start[indexes[i]]);
                dateCol.Add(oDate);
            }

            Console.WriteLine(indexes.Length);
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

            int score = 0;
            
            if (Name[indexes[userInputIndex]] == kList[0].Key)
            {
                Console.WriteLine("xxxx");
                score += 1;
            }
            else
            {
                Console.WriteLine("Please try again...");
            }
            //Console.WriteLine(Name[indexes[userInputIndex]]);
            Console.WriteLine(score);
        }
    }
}