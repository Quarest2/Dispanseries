using Dispensery_CSVProcessing;
using CHW2_3;

string? input;
do
{
    string fileName = MethodsForUserIteration.GetFileNameToRead();
    try
    {
        List<string> lines = MethodsForUserIteration.Read(fileName);
        if (lines != null && lines.Any())
        {
            string[] splittedHeadings = lines[0].Split(';');
            var Dispanseries = new List<Dispensery>();
            lines.RemoveAt(0);
            foreach (string i in lines)
            {
                Dispanseries.Add(new Dispensery(splittedHeadings, MethodsForUserIteration.CorrectSplit(i.Split(';'))));
            }
            MethodsForUserIteration.PrintMenu();
            input = MethodsForUserIteration.PointOfMenu();
            var result = new List<Dispensery>();
            int n;
            switch (input)
            {
                // Show data about the first "n" dispensaries.
                case "1":
                    do
                    {
                        Console.WriteLine(@"How many items do you want to select? (a natural number not greater than {0})", Dispanseries.Count);

                    } while (!int.TryParse(Console.ReadLine(), out n) || n > Dispanseries.Count || n < 1);
                    result = MethodsForUserIteration.ExtremeDispanseries(Dispanseries, MethodsForUserIteration.TakeFrom.Top, n);
                    foreach (Dispensery i in result)
                    {
                        Dispensery.PrintDispancery(i);
                    }
                    MethodsForUserIteration.SaveResultInFile(splittedHeadings, result);
                    break;
                // Show data about the last "n" dispansaries.
                case "2":
                    do
                    {
                        Console.WriteLine(@"How many items do you want to select? (a natural number not greater than {0})", Dispanseries.Count);

                    } while (!int.TryParse(Console.ReadLine(), out n) || n > Dispanseries.Count || n < 1);
                    result = MethodsForUserIteration.ExtremeDispanseries(Dispanseries, MethodsForUserIteration.TakeFrom.Bottom, n);
                    foreach (Dispensery i in result)
                    {
                        Dispensery.PrintDispancery(i);
                    }
                    MethodsForUserIteration.SaveResultInFile(splittedHeadings, result);
                    break;
                // Sort dispensaries by the "District".
                case "3":
                    result = Dispanseries;
                    result.Sort(new DispanseryComparer());
                    do
                    {
                        Console.WriteLine("Alphabetical order (1) or reverse alphabetical order (2)? (Write \"1\" or \"2\")");
                    } while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 2);
                    if (n == 1)
                    {
                        result.Reverse();
                    }
                    foreach (Dispensery i in result)
                    {
                        Dispensery.PrintDispancery(i);
                    }
                    MethodsForUserIteration.SaveResultInFile(splittedHeadings, result);
                    break;
                // Make a selection of dispensaries by the "Specialization".
                case "4":
                    do
                    {
                        Console.WriteLine("By what value do you want to create a selection? (write without quotation marks)");
                        input = Console.ReadLine();
                    } while (string.IsNullOrEmpty(input));
                    foreach (Dispensery i in Dispanseries)
                    {
                        if (i._specialization == ("\"" + input + "\""))
                        {
                            result.Add(i);
                            Dispensery.PrintDispancery(i);
                        }
                    }
                    if (result.Count == 0)
                    {
                        Console.WriteLine("There are no dispensaries with this value of the \"Specialization\" field.");
                    }
                    else
                    {
                        MethodsForUserIteration.SaveResultInFile(splittedHeadings, result);
                    }
                    break;
                // Make a selection of dispensaries by the "ChiefPosition".
                case "5":
                    do
                    {
                        Console.WriteLine("By what value do you want to create a selection? (write without quotation marks)");
                        input = Console.ReadLine();
                    } while (string.IsNullOrEmpty(input));
                    foreach (Dispensery i in Dispanseries)
                    {
                        if (i._chiefPosition == ("\"" + input + "\""))
                        {
                            result.Add(i);
                            Dispensery.PrintDispancery(i);
                        }
                    }
                    if (result.Count == 0)
                    {
                        Console.WriteLine("There are no dispensaries with this value of the \"ChiefPosition\" field.");
                    }
                    else
                    {
                        MethodsForUserIteration.SaveResultInFile(splittedHeadings, result);
                    }
                    break;
            }
        }
    } catch (Exception ex)
    {
        Console.WriteLine(@"Sorry, exception: {0}", ex);
    }
    Console.WriteLine("If you want to leave the program, write \"out\"");
    input = Console.ReadLine();
} while (string.IsNullOrEmpty(input) || input != "out");


