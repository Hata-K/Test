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

            var fileLines = File.ReadAllLines(sourceDir.ToString());
            var lineSum = File.ReadAllLines(sourceDir.ToString()).Length;

            List<String> name = new List<String>();
            List<String> nameEdited = new List<String>();
            List<String> dob = new List<String>();
            List<String> start = new List<String>();
            List<String> end = new List<String>();

            for (int i = 1; i <= (lineSum - 1); i++)
            {
                string[] eachLine = fileLines[i].Split(',');
                name.Add(eachLine[1]);
                nameEdited.Add(eachLine[1]);
                dob.Add(eachLine[2]);
                start.Add(eachLine[3]);
                end.Add(eachLine[4]);
            }

            var nameDuplicates = nameEdited.GroupBy(i=>i).Where(g => g.Count() > 1).Select(g => g.Key);

            int count = 1;
            foreach (var d in nameDuplicates)
            {
                for (int i = 0; i < nameEdited.Count; i++)
                {
                
                    if (nameEdited[i] == (d))
                    {
                        nameEdited[i] = (nameEdited[i] +"-TERM-"+ count);
                        ++count;
                    }
                    else
                    {
                        continue;
                    }
                }
                count = 1;
            }
            //An int arry is initiated to store 3 random number from 1 to 77;
            int[] indexes = new int[3];
            
            //Random funtion is initiated.
            Random rnd = new Random();
            //Number is picked from 1 to 77
            int index = rnd.Next(1, 77);
            //0th place is placed with that random number
            indexes[0] = index;
            
            for (int i = 1; i < 3; i++)
            {
                int ind = rnd.Next(1, 77);
                
                if (name[indexes[(i-1)]] != name[ind])
                {
                    indexes[i] = ind;
                }
                else
                {
                    continue;
                }
            }

            //DISPLAYING THE SELCETED NAMES TO THE USER
            count = 1;
            foreach (var s in indexes)
            {
                Console.WriteLine(count+": "+nameEdited[s]);
                count += 1;
            }
            var kList = new List<KeyValuePair<string, DateTime>>();

            for (int i = 0; i < indexes.Length; i++)
            {
                DateTime oDate = Convert.ToDateTime(start[indexes[i]]);

                kList.Add(new KeyValuePair<string, DateTime>(nameEdited[indexes[i]],oDate));
            }
            kList.Sort((a, b) => (b.Value.CompareTo(a.Value)));
            kList.Reverse();
            Console.WriteLine("\n");

            foreach (var element in kList)
            {
                Console.WriteLine(element.Key + "\t" + element.Value.ToShortDateString());
            }
            Console.WriteLine("\n");

            Console.WriteLine("Who's the first president to run?...");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int userInputIndex = userInput - 1;
                        
            if (nameEdited[indexes[userInputIndex]] == kList[0].Key)
            {
                Console.WriteLine("Correct!");
                Console.WriteLine(kList[0].Value.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("Oops! Wrong...");
                Console.WriteLine("The correct answer is: {0} --> {1}", kList[0].Key, kList[0].Value.ToString("dd/MM/yyyy"));
            }

            //End of program.
            Console.ReadLine();
        }
    }
}