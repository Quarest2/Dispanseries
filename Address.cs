namespace Dispensery_CSVProcessing;

/// <summary>
/// Class with fields: index, city, phone number, fax, street, house number.
/// </summary>
public class Adress
{
    // All fields from the task condition.

    private string? _postalCode;
    // There is no "city" field in the csv-file, but according to the task condition, it should be in the class, so I'll just fill it in with my hands.
    private string? _city = "Moscow";
    private string? _publicPhone;
    private string? _fax;
    private string? _houseNumber;
    private string? _street;

    // Method that returns one of the field.
    public string GetPostalCode()
    {
        if (!string.IsNullOrEmpty(_postalCode))
        {
            return _postalCode;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_postalCode\" is null or empty.");
        }
    }

    // Method that returns one of the field.
    public string GetCity()
    {
        if (!string.IsNullOrEmpty(_city))
        {
            return _city;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_city\" is null or empty.");
        }
    }

    // Method that returns one of the field.
    public string GetPublicPhone()
    {
        if (!string.IsNullOrEmpty(_publicPhone))
        {
            return _publicPhone;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_publicPhone\" is null or empty.");
        }
    }

    // Method that returns one of the field.
    public string GetFax()
    {
        if (!string.IsNullOrEmpty(_fax))
        {
            return _fax;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_fax\" is null or empty.");
        }
    }

    // Method that returns one of the field.
    public string GetStreet()
    {
        if (!string.IsNullOrEmpty(_street))
        {
            return _street;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_street\" is null or empty.");
        }
    }

    // Method that returns one of the field.
    public string GetHouseNumber()
    {
        if (!string.IsNullOrEmpty(_houseNumber))
        {
            return _houseNumber;
        }
        else
        {
            throw new ArgumentException("Sorry, field \"_houseNumber\" is null or empty.");
        }
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Adress(){}

    /// <summary>
    /// Constructor based on a string from a csv file.
    /// </summary>
    /// <param name="splittedHeadings">Array of headings from csv file.</param>
    /// <param name="splittedData">Array from string(splitted by ";").</param>
    /// <exception cref="ArgumentException">Exception if parameters are invalid.</exception>
    public Adress(string[] splittedHeadings,string[] splittedData)
    {
        int flag_postalCode = Array.IndexOf(splittedHeadings, "PostalCode");
        int flag_publicPhone = Array.IndexOf(splittedHeadings, "PublicPhone");
        int flag_fax = Array.IndexOf(splittedHeadings, "Fax");
        int flag_adress = Array.IndexOf(splittedHeadings, "Address");

        if(flag_postalCode == -1 || flag_publicPhone == -1 || flag_fax == -1 || flag_adress == -1){
            throw new ArgumentException("There is no necessary fields in \"headings\".");
        }

        try
        {
            _postalCode = splittedData[flag_postalCode];
            _publicPhone = splittedData[flag_publicPhone];
            _fax = splittedData[flag_fax];
            string adress = splittedData[flag_adress];
            AdressToStreetAndNumber(adress);
        } catch
        {
            throw new ArgumentException("Something wrong with neessary fields in \"data\".");
        }
    }

    // In the file, the street and the house number are given together, but I decided to separate them by the word "house".

    /// <summary>
    /// Method that separates the address and the house number.
    /// </summary>
    /// <param name="adress">Field "adress" from a csv file.</param>
    /// <exception cref="ArgumentException">Exception if parameters are invalid.</exception>
    private void AdressToStreetAndNumber(string adress)
    {
        string[] splittedAdress = adress.Split(" ");
        int flag_house = Array.IndexOf(splittedAdress, "дом");

        if (flag_house == -1)
        {
            flag_house = Array.IndexOf(splittedAdress, "корпус");
            if(flag_house == -1)
            {
                throw new ArgumentException("There is no word \"дом\" in field \"Adress\".");
            }
        }

        try
        {
            _street = String.Join(" ", splittedAdress, 0, flag_house);
            _houseNumber = String.Join(" ", splittedAdress, flag_house, splittedAdress.Length - flag_house);
        }
        catch
        {
            throw new ArgumentException("Something wrong with crating fields in \"_street\" and \"_houseNumber\".");
        }
    }
}

