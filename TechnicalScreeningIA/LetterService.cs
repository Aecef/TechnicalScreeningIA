using TechnicalScreeningIA;
using System.Globalization;

public class LetterService : ILetterService
{
    // Path for the fixed 'CombinedLetters' folder
    public string CombinedLettersPath { get; private set; }

    // Constructor
    public LetterService(string path) { CombinedLettersPath = path; }

    // Returns the path to \Input\Admission
    public string[] AdmissionSubFolders()
    {
        try
        {
            string InputPath = CombinedLettersPath + "\\Input\\Admission";
            return Directory.GetDirectories(InputPath);
        }
        catch (DirectoryNotFoundException dirEx)
        {
            //Console.WriteLine("There is no Admission ")
            Console.WriteLine("Admission Directory not found: " + dirEx.Message);
            return Array.Empty<string>();
        }

    }

    // Returns the path to \Input\Scholarship
    public string[] ScholarshipSubFolders()
    {
        try
        {
            string InputPath = CombinedLettersPath + "\\Input\\Scholarship";
            return Directory.GetDirectories(InputPath);
        }
        catch (DirectoryNotFoundException dirEx)
        {
            Console.WriteLine("Scholarship Directory not found: " + dirEx.Message);
            return Array.Empty<string>();
        }
    }

    // Returns the filepath to the 'Admission' folder for the current day
    public string[] GetTodaysAdmissions(string dateFormatted)
    {
        string AdmissionPath = CombinedLettersPath + "\\Input\\Admission" + "\\" + dateFormatted;
        if (!Directory.Exists(AdmissionPath))
        {
            Console.WriteLine("There is no 'Admission' dir for " + dateFormatted);
            return Array.Empty<string>();
        }
        return Directory.GetFiles(AdmissionPath);
    }


    // Returns the filepath to the 'Scholarship' folder for the current day
    public string[] GetTodaysScholarships(string dateFormatted)
    {
        string ScholarshipPath = CombinedLettersPath + "\\Input\\Scholarship" + "\\" + dateFormatted;
        if (!Directory.Exists(ScholarshipPath))
        {
            Console.WriteLine("There is no 'Scholarship' dir for " + dateFormatted);
            return Array.Empty<string>();
        }
        return Directory.GetFiles(ScholarshipPath);
    }

    // Returns the 8-digit Student Id as a String
    public string GetStudentId(string path)
    {
        char[] delimiters = { '\\', '-' };
        return path.Split(delimiters).Last().Split('.').First();
    }

    // Returns false if the letter was not in the combined Array
    public bool WasCombined(string[] combinedArray, string letter)
    {
        List<string> ids = new List<string>();

        foreach (string id in combinedArray)
        {
            ids.Add(GetStudentId(id));
        }

        return ids.ToArray().Contains(GetStudentId(letter));

    }


    // Generates the final report containing the IDs of the combined students and how many 
    // files were altered
    public void CreateReport(string[] combinedLetters, string dateFormatted)
    {
        // Creates the report file and updates every time there is a match
        string path = CombinedLettersPath + "\\Output\\" + dateFormatted + "\\Combined";
        string reportPath = path + "\\combination-report-" + dateFormatted + ".txt";
        if (!File.Exists(reportPath))
        {
            string localDate = DateTime.Today.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
            string initialText = localDate + " Report"
                + Environment.NewLine
                + "--------------------------------------"
                + Environment.NewLine
                + Environment.NewLine
                + "Number of Combined Letters: " + combinedLetters.Length
                + Environment.NewLine;
            foreach (string letter in combinedLetters)
            {
                initialText += "\t" + GetStudentId(letter) + Environment.NewLine;
            }
            File.WriteAllText(reportPath, initialText);
            Console.WriteLine(File.ReadAllText(reportPath));
        }
    }

    // Creates a copy of the original file to be placed in the 'Uncombined' directory
    public void UncombinedLetters(string inputFile, string dateFormatted)
    {
        string studentId = GetStudentId(inputFile);
        string fileText = File.ReadAllText(inputFile);


        string path = CombinedLettersPath + "\\Output\\" + dateFormatted + "\\Uncombined";
        Directory.CreateDirectory(path);

        string studentPath = "";
        if (inputFile.Contains("admission"))
        {
            studentPath = path + "\\admission-" + studentId + ".txt";
        }
        else
        {
            studentPath = path + "\\scholarship-" + studentId + ".txt";
        }



        if (!File.Exists(studentPath))
        {
            File.WriteAllText(studentPath, fileText);
        }
    }

    // Creates a file to combine two text files
    public void CombineTwoLetters(string inputFile1, string inputFile2, string dateFormatted)
    {
        string studentId = GetStudentId(inputFile1);

        string combinedText = File.ReadAllText(inputFile1)
            + Environment.NewLine
            + File.ReadAllText(inputFile2);

        string path = CombinedLettersPath + "\\Output\\" + dateFormatted + "\\Combined";
        Directory.CreateDirectory(path);


        string studentPath = path + "\\admission-scholarship-" + studentId + ".txt";
        if (!File.Exists(studentPath))
        {
            File.WriteAllText(studentPath, combinedText);
        }

    }

    // Moves the all of the input files into the 'Archive' dir
    public void ArchiveFiles(string dateFormatted)
    {
        string admissionDirPath = CombinedLettersPath + "\\Input\\Admission\\" + dateFormatted;
        string scholarshipDirPath = CombinedLettersPath + "\\Input\\Scholarship\\" + dateFormatted;

        string newAdmissionPath = CombinedLettersPath + "\\Archive\\Admission\\";
        string newScholarshipPath = CombinedLettersPath + "\\Archive\\Scholarship\\";

        Directory.Move(admissionDirPath, newAdmissionPath);
        Directory.Move(scholarshipDirPath, newScholarshipPath);
    }
}
