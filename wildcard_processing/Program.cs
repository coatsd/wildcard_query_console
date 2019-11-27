using System;
using System.Collections.Generic;

namespace wildcard_processing
{
    class Program
    {
        const char Wildcard = '*';

        // this is the dummy data
        static string[] DataArray = new string[3]
        {
            "Hello World",
            "Dataset 1",
            "Try this out"
        };

        // This counts the wildcard characters in a string.
        static int CountWildcards(string string_in)
        {
            int wildcards = 0;

            foreach (char c in string_in)
            {
                if (c == Wildcard)
                {
                    wildcards++;
                }
            }

            return wildcards;
        }
        
        // This divides the input string into substrings if the substring
        // contains wildcards. This is so we can process the input to see
        // if the substrings are in a piece of data.
        static string[] CreateSubstrings(string string_in)
        {
            int wildcard_count = CountWildcards(string_in);
            string[] substrings = new string[wildcard_count + 1];

            if (wildcard_count > 0)
            {
                int last_pos = 0;

                for (int i = 0; i < substrings.Length; i++)
                {
                    int beginning_pos = last_pos;

                    while (last_pos < string_in.Length)
                    {
                        if (string_in[last_pos] == Wildcard ||
                            last_pos == string_in.Length - 1)
                        {
                            substrings[i] =
                                    string_in.Substring(beginning_pos, last_pos - (beginning_pos - 1))
                                        .Trim('*');
                            beginning_pos = last_pos;

                            Console.WriteLine("Placing item into substring list: "
                                + substrings[i] + 
                                    (substrings[i].Contains(' ') ? "":" (contains whitespace)"));
                        }
                        last_pos++;
                    }
                }
            }
            else
            {
                substrings[0] = string_in;
            }
            return substrings;
        }

        static List<string> SearchData(string[] substrings)
        {
            List<string> found_data = new List<string>();

            // iterate through the dataset
            for (int i = 0; i < DataArray.Length; i++)
            {
                string working_data = DataArray[i];
                if (IsInData(substrings, working_data) == true)
                {
                    found_data.Add(working_data);
                }
            }

            return found_data;
        }

        // this iterates through the list of substrings, made from the input,
        // and matches the substrings to a portion of the data piece 
        // (working_data) in question.
        static bool IsInData(string[] substrings, string working_data)
        {
            if (substrings.Length > 1)
            {
                // test for the existence of substrings in each data piece
                foreach (string substring in substrings)
                {
                    if (substring != null && substring != "")
                    {
                        string comparison = "";
                        bool contains = false;

                        for (int i = 0; i < working_data.Length; i++)
                        {
                            comparison += working_data[i];

                            for (int j = 0; j < comparison.Length; j++)
                            {
                                if (comparison.Substring(j, comparison.Length - j) == substring)
                                {
                                    contains = true;
                                }
                            }
                        }

                        if (contains == false)
                        {
                            return contains;
                        }
                        working_data.Remove(0, comparison.Length);
                    }
                }
            }
            else
            {
                if (substrings[0] != working_data)
                {
                    return false;
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Wildcard Processing app!");
            Console.WriteLine("Please enter a term to be queried:");

            string user_search = Console.ReadLine();

            List<string> search_results = SearchData(CreateSubstrings(user_search));

            Console.WriteLine("Data Found:");
            foreach (string search_result in search_results)
            {
                Console.WriteLine(search_result);
            }

            Console.WriteLine("Continue query? (y/n, n default)");
            string user_response = Console.ReadLine();

            while (user_response == "y")
            {
                Console.WriteLine("Please enter a term to be queried:");
                user_search = Console.ReadLine();

                search_results = SearchData(CreateSubstrings(user_search));

                Console.WriteLine("Data Found:");
                foreach (string search_result in search_results)
                {
                    Console.WriteLine(search_result);
                }

                Console.WriteLine("Continue query? (y/n, n default)");
                user_response = Console.ReadLine();
            }
        }
    }
}
