namespace Dispensery_CSVProcessing;

/// <summary>
/// Comparer for dispanseries (by the "_district" field).
/// </summary>
public class DispanseryComparer : IComparer<Dispensery>
{
    /// <summary>
    /// Method for comparing two dispensaries. By the "_district" field is in reverse alphabetical order.
    /// </summary>
    /// <param name="dispensery1">First dispansery.</param>
    /// <param name="dispensery2">Second dispansery.</param>
    /// <returns>"1" if first is greater, "0" if they are equal, "-1" if the second is greater.</returns>
    /// <exception cref="ArgumentException">Exception if parameters are invalid.</exception>
    public int Compare(Dispensery? dispensery1, Dispensery? dispensery2)
    {
        if (dispensery1 is null || dispensery2 is null || dispensery1._district is null || dispensery2._district is null)
            throw new ArgumentException("One of parameters is invalid.");
        string[] sorted = new string[2] { dispensery1._district, dispensery2._district };
        Array.Sort(sorted);
        if (sorted[0] == sorted[1])
        {
            return 0;
        }
        else if (sorted[0] == dispensery1._district)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}

