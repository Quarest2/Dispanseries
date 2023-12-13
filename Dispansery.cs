namespace Dispensery_CSVProcessing;

/// <summary>
///  Class that contains iformation about the dispansery (fields: num, full name, short name, admArea, district, chief name,
///  chief position, chief gender, chief phone, email, close flag, close reason, close date, reopen date, working hours,
///  clarification of working hours, specialization, beneficial drug prescriptions, extra info, point X, point Y, global ID, adress).
/// </summary>
public class Dispensery 
{
    // All fields from the csv file.

    public int? _num { get; }
	public string? _fullName { get; }
    public string? _shortName { get; }
    public string? _admArea { get; }
    public string? _district { get; }
    public string? _chiefName { get; }
    public string? _chiefPosition { get; }
    public string? _chiefGender { get; }
    public string? _chiefPhone { get; }
    public string? _email { get; }
    public string? _closeFlag { get; }
    public string? _closeReason { get; }
    public string? _closeDate { get; }
    public string? _reopenDate { get; }
    public string? _workingHours { get; }
    public string? _clarificationOfWorkingHours { get; }
    public string? _specialization { get; }
    public string? _beneficialDrugPrescriptions { get; }
    public string? _extraInfo { get; }
    public string? _pointX { get; }
    public string? _pointY { get; }
    public string? _globalID { get; }
    public Adress? _adress { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Dispensery()
    {
    }

    /// <summary>
    /// Constructor based on a string from a csv file.
    /// </summary>
    /// <param name="splittedHeadings">Array of headings from csv file.</param>
    /// <param name="splittedData">Array from string(splitted by ";").</param>
    /// <exception cref="ArgumentException">Exception if parameters are invalid.</exception>
	public Dispensery(string[] splittedHeadings, string[] splittedData)
	{
        int flag_num = Array.IndexOf(splittedHeadings, "ROWNUM");
        int flag_fullName = Array.IndexOf(splittedHeadings, "FullName");
        int flag_shortName = Array.IndexOf(splittedHeadings, "ShortName");
        int flag_admArea = Array.IndexOf(splittedHeadings, "AdmArea");
        int flag_district = Array.IndexOf(splittedHeadings, "District");
        int flag_chiefName = Array.IndexOf(splittedHeadings, "ChiefName");
        int flag_chiefPosition = Array.IndexOf(splittedHeadings, "ChiefPosition");
        int flag_chiefGender = Array.IndexOf(splittedHeadings, "ChiefGender");
        int flag_chiefPhone = Array.IndexOf(splittedHeadings, "ChiefPhone");
        int flag_email = Array.IndexOf(splittedHeadings, "Email");
        int flag_closeFlag = Array.IndexOf(splittedHeadings, "CloseFlag");
        int flag_closeReason = Array.IndexOf(splittedHeadings, "CloseReason");
        int flag_closeDate = Array.IndexOf(splittedHeadings, "CloseDate");
        int flag_reopenDate = Array.IndexOf(splittedHeadings, "ReopenDate");
        int flag_workingHours = Array.IndexOf(splittedHeadings, "WorkingHours");
        int flag_clarificationOfWorkingHours = Array.IndexOf(splittedHeadings, "ClarificationOfWorkingHours");
        int flag_specialization = Array.IndexOf(splittedHeadings, "Specialization");
        int flag_beneficialDrugPrescriptions = Array.IndexOf(splittedHeadings, "BeneficialDrugPrescriptions");
        int flag_extraInfo = Array.IndexOf(splittedHeadings, "ExtraInfo");
        int flag_pointX = Array.IndexOf(splittedHeadings, "POINT_X");
        int flag_pointY = Array.IndexOf(splittedHeadings, "POINT_Y");
        int flag_globalID = Array.IndexOf(splittedHeadings, "GLOBALID");

        if (flag_num == -1 || flag_fullName == -1 || flag_shortName == -1 || flag_admArea == -1 || flag_district == -1 ||
            flag_chiefName == -1 || flag_chiefPosition == -1 || flag_chiefGender == -1 || flag_chiefPhone == -1 || flag_email == -1 ||
            flag_closeFlag == -1 || flag_closeReason == -1 || flag_closeDate == -1 || flag_reopenDate == -1 || flag_workingHours == -1 ||
            flag_clarificationOfWorkingHours == -1 || flag_specialization == -1 || flag_beneficialDrugPrescriptions == -1 ||
            flag_extraInfo == -1 || flag_pointX == -1 || flag_pointY == -1 || flag_globalID == -1)
        {
            throw new ArgumentException("There is no necessary fields in \"headings\".");
        }

        try
        {
            _num = Convert.ToInt32(splittedData[flag_num]);
            _fullName = splittedData[flag_fullName];
            _shortName = splittedData[flag_shortName];
            _admArea = splittedData[flag_admArea];
            _district = splittedData[flag_district];
            _chiefName = splittedData[flag_chiefName];
            _chiefPosition = splittedData[flag_chiefPosition];
            _chiefGender = splittedData[flag_chiefGender];
            _chiefPhone = splittedData[flag_chiefPhone];
            _email = splittedData[flag_email];
            _closeFlag = splittedData[flag_closeFlag];
            _closeReason = splittedData[flag_closeReason];
            _closeDate = splittedData[flag_closeDate];
            _reopenDate = splittedData[flag_reopenDate];
            _workingHours = splittedData[flag_workingHours];
            _clarificationOfWorkingHours = splittedData[flag_clarificationOfWorkingHours];
            _specialization = splittedData[flag_specialization];
            _beneficialDrugPrescriptions = splittedData[flag_beneficialDrugPrescriptions];
            _extraInfo = splittedData[flag_extraInfo];
            _pointX = splittedData[flag_pointX];
            _pointY = splittedData[flag_pointY];
            _globalID = splittedData[flag_globalID];
            _adress = new Adress(splittedHeadings, splittedData);
        }
        catch
        {
            throw new ArgumentException("Something wrong with neessary fields in \"data\".");
        }

    }

    // I decided to output only a short name so as not to clog up the console.

    /// <summary>
    /// Method that prints short name of dispansery in console.
    /// </summary>
    /// <param name="dispensery">The dispensary that we want to display information about.</param>
    public static void PrintDispancery(Dispensery dispensery)
    {
        Console.WriteLine(dispensery._shortName);
    }
}

