using Dispensery_CSVProcessing;
namespace CHW2_3;

/// <summary>
/// A static class that contains methods for running a console application.
/// </summary>
public static class MethodsForUserIteration
{
    /// <summary>
    /// Methods that prints menu.
    /// </summary>
    public static void PrintMenu()
    {
        Console.WriteLine("What can I do for you? (Choose an item from menu)");
        Console.WriteLine("1 - Show data about the first \"n\" dispensaries");
        Console.WriteLine("2 - Show data about the last \"n\" dispansaries");
        Console.WriteLine("3 - Sort dispensaries by the \"District\"");
        Console.WriteLine("4 - Make a selection of dispensaries by the \"Specialization\"");
        Console.WriteLine("5 - Make a selection of dispensaries by the \"ChiefPosition\"");
    }

    /// <summary>
    /// Method that processes the selection of a menu item.
    /// </summary>
    /// <returns>Point of menu.</returns>
    public static string PointOfMenu()
    {
        string? input;
        do
        {
            Console.WriteLine("Write an integer from 1 to 5");
            input = Console.ReadLine();
        } while (string.IsNullOrEmpty(input) || (input != "1" && input != "2" && input != "3" && input != "4" && input != "5"));
        return input;
    }

    // The fields in the csv file are written in quotation marks. At the same time, semicolons may appear inside the quotes.
    // Therefore, we had to modify the "split" method by gluing accidentally separated strings.

    /// <summary>
    /// Method that connects strings that are accidentally split.
    /// </summary>
    /// <param name="strings">Array of strings after "split" method.</param>
    /// <returns>Array of correctly splitted strings.</returns>
    public static string[] CorrectSplit(string[] strings)
    {
        List<string> result = strings.ToList();
        for (int i = 1; i < result.Count; i++)
        {
            if (i != result.Count - 1 && result[i][result[i].Length - 1] != '"')
            {
                result[i] = result[i] + ";" + result[i + 1];
                result.RemoveAt(i + 1);
                i--;
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Method that gets file name to read from user.
    /// </summary>
    /// <returns>Name of the csv file.</returns>
    public static string GetFileNameToRead()
    {
        string FileName;
        do
        {
            Console.WriteLine("Please, write the name of file to read a sentences" +
                "(without specifying extension)" +
                "(the file must be located next to the executable file of the console application):");
            FileName = Console.ReadLine() + ".csv";
        } while (!File.Exists(FileName));
        return FileName;
    }

    /// <summary>
    /// Method that reads the csv file and writes the result to a list of dispensaries.
    /// </summary>
    /// <param name="fileName">Name of the csv file.</param>
    /// <returns>List of dispanseries.</returns>
    /// <exception cref="ArgumentNullException">Exception if parameters are invalid.</exception>
    public static List<string> Read(string fileName)
    {
        var lines = new List<string>();
        try
        {
            using StreamReader sr = new StreamReader(fileName);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }
        catch
        {
            throw new ArgumentNullException("File is invalid.");
        }
    }

    /// <summary>
    /// Method that selects the "n" first or "n" last dispensaries from the list.
    /// </summary>
    /// <param name="data">List of dispanseries.</param>
    /// <param name="takeFrom">Enum that indicates whether a selection is being made from the top or the bottom.</param>
    /// <param name="count">Number of items in selection.</param>
    /// <returns>Selection of dispanseries.</returns>
    public static List<Dispensery> ExtremeDispanseries(List<Dispensery> data, TakeFrom takeFrom, int count)
    {
        var result = new List<Dispensery>();
        if (takeFrom == TakeFrom.Top)
        {
            for (int i = 0; i < count; i++)
            {
                result.Add(data[i]);
            }
        }
        else
        {
            for (int i = 1; i < count + 1; i++)
            {
                result.Add(data[data.Count - i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Enum that indicates whether a selection is being made from the top or the bottom.
    /// </summary>
    public enum TakeFrom
    {
        Top = 1,
        Bottom = 2
    }

    /// <summary>
    /// Method that saves list of dispanseries in file.
    /// </summary>
    /// <param name="splittedHeadings">Array of headings from csv file.</param>
    /// <param name="result">List of dispanseries.</param>
    public static void SaveResultInFile(string[] splittedHeadings, List<Dispensery> result)
    {
        Console.WriteLine("Write \"save\" if you want to save the result.");
        string? input = Console.ReadLine();
        if (input == "save")
        {
            Console.WriteLine("Write the name of a file, where you want to save the result.");
            string fileName = Console.ReadLine() + ".csv";
            if (!File.Exists(fileName))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        foreach (string i in splittedHeadings)
                        {
                            sw.Write(i + ";");
                        }
                        sw.WriteLine();
                        int num = 1;
                        foreach (Dispensery i in result)
                        {
                            sw.Write(num + ";" + i._fullName + ";" + i._shortName + ";"
                                + i._admArea + ";" + i._district + ";" + i._adress.GetPostalCode() + ";"
                                + i._adress.GetStreet() + " " + i._adress.GetHouseNumber() + ";" + i._chiefName + ";"
                                + i._chiefPosition + ";" + i._chiefGender + ";" + i._chiefPhone + ";"
                                + i._adress.GetPublicPhone() + ";" + i._adress.GetFax() + ";" + i._email + ";"
                                + i._closeFlag + ";" + i._closeReason + ";" + i._closeDate + ";"
                                + i._reopenDate + ";" + i._workingHours + ";" + i._clarificationOfWorkingHours + ";"
                                + i._specialization + ";" + i._beneficialDrugPrescriptions + ";" + i._extraInfo + ";"
                                + i._pointX + ";" + i._pointY + ";" + i._globalID + ";");
                            sw.WriteLine();
                            num++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Sorry, exception: {0}", ex);
                }
            }
            else
            {
                int n;
                do
                {
                    Console.WriteLine("Such a file already exists. Do you prefer to add data (1) to a file or overwrite it(2)? (Write \"1\" or \"2\")");
                } while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 2);
                if (n == 2)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            foreach (string i in splittedHeadings)
                            {
                                sw.Write(i + ";");
                            }
                            sw.WriteLine();
                            int num = 1;
                            foreach (Dispensery i in result)
                            {
                                sw.Write(num + ";" + i._fullName + ";" + i._shortName + ";"
                                    + i._admArea + ";" + i._district + ";" + i._adress.GetPostalCode() + ";"
                                    + i._adress.GetStreet() + " " + i._adress.GetHouseNumber() + ";" + i._chiefName + ";"
                                    + i._chiefPosition + ";" + i._chiefGender + ";" + i._chiefPhone + ";"
                                    + i._adress.GetPublicPhone() + ";" + i._adress.GetFax() + ";" + i._email + ";"
                                    + i._closeFlag + ";" + i._closeReason + ";" + i._closeDate + ";"
                                    + i._reopenDate + ";" + i._workingHours + ";" + i._clarificationOfWorkingHours + ";"
                                    + i._specialization + ";" + i._beneficialDrugPrescriptions + ";" + i._extraInfo + ";"
                                    + i._pointX + ";" + i._pointY + ";" + i._globalID + ";");
                                sw.WriteLine();
                                num++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Sorry, exception: {0}", ex);
                    }
                }
                else
                {
                    try
                    {
                        int num;
                        using (StreamWriter sw = new StreamWriter(fileName, true))
                        {
                            var lastLine = File.ReadAllLines(fileName).Last();
                            var LastDispansery = new Dispensery(splittedHeadings, CorrectSplit(lastLine.Split(';')));
                            if (!(LastDispansery._num is null))
                            {
                                num = (int)LastDispansery._num + 1;
                            }
                            else
                            {
                                Console.WriteLine("File is invalid.");
                                throw new Exception("File is invalid.");
                            }
                            foreach (Dispensery i in result)
                            {
                                sw.Write(num + ";" + i._fullName + ";" + i._shortName + ";"
                                    + i._admArea + ";" + i._district + ";" + i._adress.GetPostalCode() + ";"
                                    + i._adress.GetStreet() + " " + i._adress.GetHouseNumber() + ";" + i._chiefName + ";"
                                    + i._chiefPosition + ";" + i._chiefGender + ";" + i._chiefPhone + ";"
                                    + i._adress.GetPublicPhone() + ";" + i._adress.GetFax() + ";" + i._email + ";"
                                    + i._closeFlag + ";" + i._closeReason + ";" + i._closeDate + ";"
                                    + i._reopenDate + ";" + i._workingHours + ";" + i._clarificationOfWorkingHours + ";"
                                    + i._specialization + ";" + i._beneficialDrugPrescriptions + ";" + i._extraInfo + ";"
                                    + i._pointX + ";" + i._pointY + ";" + i._globalID + ";");
                                sw.WriteLine();
                                num++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Sorry, exception: {0}", ex);
                    }
                }
            }
        }
    }
}
