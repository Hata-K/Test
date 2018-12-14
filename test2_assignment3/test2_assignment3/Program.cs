using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace test2_assignment3
{
    public class Player
    {
        public int score = 0;
    }

    //class for game logics.
    public class Game
    {
        //DISPLAYING THE SELECTED NAMES
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sourceDir = new DirectoryInfo("source.csv");

            var fileLines = File.ReadAllLines(sourceDir.ToString());
            var lineSum = File.ReadAllLines(sourceDir.ToString()).Length;

            var name = new List<string>();
            var nameEdited = new List<string>();
            //var dob = new List<string>();
            var start = new List<string>();
            //var end = new List<string>();
            
            //ASSIGNING VALUES TO THE DECLARED LISTS
            for (var i = 1; i <= (lineSum - 1); i++)
            {
                var eachLine = fileLines[i].Split(',');
                name.Add(eachLine[1]);
                nameEdited.Add(eachLine[1]);
                //dob.Add(eachLine[2]);
                start.Add(eachLine[3]);
                //end.Add(eachLine[4]);
            }
            
            //EDITING DUPLICATES NAMES AND ASSIGNING THEM TERMS
            EditingDuplicates(nameEdited);
            
            
            
            Player player = new Player();

            for (int i = 0; i < 5; i++)
            {
                //FUNCTION CALLED TO GET A ARRAY WITH THREE RANDOM NUMBERS
                int[] rndNum = RandNumbers(name);
                
                //GETTING CORRECT ANSWER
                var kList = (from t in rndNum let oDate = Convert.ToDateTime(start[t]) select new KeyValuePair<string, DateTime>(nameEdited[t], oDate)).ToList();
                kList.Sort((a, b) => (b.Value.CompareTo(a.Value)));
                kList.Reverse();
                Console.WriteLine("\n");
                
                //CORRECT ANSWER
                Console.WriteLine("Correct answer is: {0}",kList[0].Key);
                
                Console.WriteLine("\nQuestion {0} \n", (i+1));
                DisplayPickedName(rndNum,nameEdited);

                GameLog(nameEdited,rndNum,kList,player);
            }

            Console.WriteLine();
            Console.WriteLine("Your score is: {0}", player.score);

            //End of program.
            Console.ReadLine();
        }

        //GAME LOGICS
        private static void GameLog(List<string> nameEdited, int[] rndNum, List<KeyValuePair<string, DateTime>> kList,
            Player player)
        {
            Console.WriteLine("Who's the first president to run?...");
            var userInput = Convert.ToInt32(Console.ReadLine());
            var userInputIndex = userInput - 1;
                        
            if (nameEdited[rndNum[userInputIndex]] == kList[0].Key)
            {
                Console.WriteLine("Correct!");
                Console.WriteLine(kList[0].Value.ToString("dd/MM/yyyy"));
                player.score += 1;
            }
            else
            {
                Console.WriteLine("Oops! Wrong...");
                Console.WriteLine("The correct answer is: {0} --> {1:dd/MM/yyyy}", kList[0].Key, kList[0].Value);
            }
        }

        //ADDING TERMS TO DUPLICATE NAMES
        private static void EditingDuplicates(List<string> nameEdited)
        {
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
        }

        //DISPLAYING THE USER WITH THE PICKED NAMES
        private static void DisplayPickedName(int[] rndNum, List<string> nameEdited)
        {
            int count = 1;
            foreach (var s in rndNum)
            {
                Console.WriteLine(count+": "+nameEdited[s]);
                count += 1;
            }
        }

        //GETTING 3 RANDOM NUMBERS, THAT ARE NOT THE SAME NAME...
        private static int[] RandNumbers(List<string> name)
        {
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
            return indexes;
        }
    }
}