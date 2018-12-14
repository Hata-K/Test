using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace test2_assignment3
{
    internal class Program
    {
                
        public static void Main(string[] args)
        {
            var sourceDir = new DirectoryInfo("source.csv");

            var fileLines = File.ReadAllLines(sourceDir.ToString());
            var lineSum = File.ReadAllLines(sourceDir.ToString()).Length;

            var name = new List<string>();
            var nameEdited = new List<string>();
            var dob = new List<string>();
            var start = new List<string>();
            var end = new List<string>();

            for (var i = 1; i <= (lineSum - 1); i++)
            {
                var eachLine = fileLines[i].Split(',');
                name.Add(eachLine[1]);
                nameEdited.Add(eachLine[1]);
                dob.Add(eachLine[2]);
                start.Add(eachLine[3]);
                end.Add(eachLine[4]);
            }

            var nameDuplicates = nameEdited.GroupBy(i=>i).Where(g => g.Count() > 1).Select(g => g.Key);

            var count = 1;
            foreach (var d in nameDuplicates)
            {
                for (var i = 0; i < nameEdited.Count; i++)
                {
                    if (nameEdited[i] != (d)) continue;
                    nameEdited[i] = (nameEdited[i] +"-TERM-"+ count);
                    ++count;
                }
                count = 1;
            }
            //An int array is initiated to store 3 random number from 1 to 77;
            var indexes = new int[3];
            
            //Random function is initiated.
            var rnd = new Random();
            //Number is picked from 1 to 77
            var index = rnd.Next(1, 77);
            //0th place is placed with that random number
            indexes[0] = index;
            try
            {
                for (var i = 1; i < 3; i++)
                {
                    var ind = rnd.Next(1, 77);
                
                    if (name[indexes[(i-1)]] != name[ind])
                    {
                        indexes[i] = ind;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //DISPLAYING THE SELECTED NAMES TO THE USER
            count = 1;
            foreach (var s in indexes)
            {
                Console.WriteLine(count+": "+nameEdited[s]);
                count += 1;
            }
            var kList = (from t in indexes let oDate = Convert.ToDateTime(start[t]) select new KeyValuePair<string, DateTime>(nameEdited[t], oDate)).ToList();

            kList.Sort((a, b) => (b.Value.CompareTo(a.Value)));
            kList.Reverse();
            Console.WriteLine("\n");

            foreach (var element in kList)
            {
                Console.WriteLine(element.Key + "\t" + element.Value.ToShortDateString());
            }
            Console.WriteLine("\n");

            Console.WriteLine("Who's the first president to run?...");
            var userInput = Convert.ToInt32(Console.ReadLine());
            var userInputIndex = userInput - 1;
                        
            if (nameEdited[indexes[userInputIndex]] == kList[0].Key)
            {
                Console.WriteLine("Correct!");
                Console.WriteLine(kList[0].Value.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("Oops! Wrong...");
                Console.WriteLine("The correct answer is: {0} --> {1:dd/MM/yyyy}", kList[0].Key, kList[0].Value);
            }

            //End of program.
            Console.ReadLine();
        }
    }
}