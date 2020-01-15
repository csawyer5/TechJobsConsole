using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           

           
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else
                {
                    
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }

                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count > 0)
            {
                foreach (Dictionary<string, string> job in someJobs)
                {
                    Console.WriteLine("*****");
                    foreach (KeyValuePair<string, string> column in job)
                    {
                        Console.WriteLine("{0}: {1}", column.Key, column.Value);
                    }
                    Console.WriteLine("*****");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No jobs matching the search criteria were found. ");
            }
        }
    }
}
