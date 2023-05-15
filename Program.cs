using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the number of cities in the model:");
        int numCities = int.Parse(Console.ReadLine());
        string[] cityNames = new string[numCities];
        int[] contactNumbers = new int[numCities];
        int[] outbreakLevels = new int[numCities];

        for (int i = 0; i < numCities; i++)
        {
            Console.WriteLine($"Enter the details for City {i}:");
            cityNames[i] = ReadString("City Name: ");
            contactNumbers[i] = ReadInt("Number of cities in contact: ");
            Console.WriteLine("Enter the city numbers in contact:");
            for (int j = 0; j < contactNumbers[i]; j++)
            {
                int contactCity = ReadInt($"City {j + 1} in contact: ");
                while (contactCity < 0 || contactCity >= numCities)
                {
                    Console.WriteLine("Invalid city number. Please enter again.");
                    contactCity = ReadInt($"City {j + 1} in contact: ");
                }
            }
            outbreakLevels[i] = 0;
        }

        PrintCityOutbreakLevels(numCities, cityNames, outbreakLevels);

        while (true)
        {
            Console.WriteLine("\nEnter an event that occurred during the COVID-19 outbreak:");
            string userEvent = Console.ReadLine();

            if (userEvent == "Outbreak" || userEvent == "Vaccinate" || userEvent == "Lockdown")
            {
                int cityNumber = ReadInt("Enter the city number where the incident took place: ");

                if (cityNumber >= 0 && cityNumber < numCities)
                {
                    if (userEvent == "Outbreak")
                    {
                        UpdateOutbreakLevels(outbreakLevels, contactNumbers, cityNumber);
                    }
                    else if (userEvent == "Vaccinate")
                    {
                        outbreakLevels[cityNumber] = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid city number. Please try again.");
                }
            }
            else if (userEvent == "Spread")
            {
                SpreadOutbreak(outbreakLevels, contactNumbers);
            }
            else if (userEvent == "Exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid event. Please try again.");
            }
        }

        PrintCityOutbreakLevels(numCities, cityNames, outbreakLevels);
    }

    static string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    static int ReadInt(string message)
    {
        Console.Write(message);
        return int.Parse(Console.ReadLine());
    }

    static void UpdateOutbreakLevels(int[] outbreakLevels, int[] contactNumbers, int cityNumber)
    {
        outbreakLevels[cityNumber] += 2;
        for (int j = 0; j < outbreakLevels.Length; j++)
        {
            if (contactNumbers[j] > 0 && Array.IndexOf(contactNumbers, cityNumber) >= 0)
            {
                outbreakLevels[j] = Math.Min(outbreakLevels[j] + 1, 3);
            }
        }
    }

    static void SpreadOutbreak(int[] outbreakLevels, int[] contactNumbers)
    {
        for (int i = 0; i < outbreakLevels.Length; i++)
        {
            if (outbreakLevels[i] > 0)
            {
                for (int j = 0; j < outbreakLevels.Length; j++)
                {
                    if (contactNumbers[j] > 0 && outbreakLevels[j] > outbreakLevels[i])
                    {
                        outbreakLevels[i] += 1;
                        break;
                    }
                }
            }
        }
    }

    static void PrintCityOutbreakLevels(int numCities, string[] cityNames, int[] outbreakLevels)
    {
        Console.WriteLine("City number\tCity name\tCOVID-19 Outbreak Level");
        for (int i = 0; i < numCities; i++)
        {
            Console.WriteLine($"{i}\t\t{cityNames[i]}\t\t{outbreakLevels[i]}");
        }
    }
}
