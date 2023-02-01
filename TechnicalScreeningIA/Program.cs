using System.Globalization;

/* 
    Assumptions:
        Output File does not need to keep track of the date when the letters come in. 
            - Upon further reading, it may be helpful to put the files in a dated folder 
              to help keep reports near the changed files.
        There will be more admission letters than scholarship letters.
        Will assume the user can use the console.
        All of the files will be in correct format 
        This program will always be used in Iowa (CST)
 */
internal static class Program
{
    static void Main(string[] args)
    {
        
        // Processing date in the format of yyyyMMdd 
        DateTime localDate = DateTime.Today;
        string dateFormatted = localDate.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")).Replace("/", "");
        string combinedLettersPath;
        // Path to the CombinedLetters root folder
        if (args.Any())
        {
            combinedLettersPath = args[0];
        }
        else
        {
            combinedLettersPath = Directory.GetCurrentDirectory() + "\\CombinedLetters";
        }

        LetterService ls = new LetterService(combinedLettersPath);

        List<string> combinedLetters = new List<string>();

        
        string[] todaysAdmissions = ls.GetTodaysAdmissions(dateFormatted);
        string[] todaysScholarships = ls.GetTodaysScholarships(dateFormatted);

        // Loops Over Todays Admissions
        foreach (string admission in todaysAdmissions)
        {
            // Loops Over Todays Scholarships
            foreach (string scholarship in todaysScholarships)
            {
                if (ls.GetStudentId(admission) == ls.GetStudentId(scholarship))
                {
                    combinedLetters.Add(admission);
                    ls.CombineTwoLetters(admission, scholarship, dateFormatted);
                }
            }
        }

        string[] combinedLettersArray = combinedLetters.ToArray();

        // Places the uncombined admissions into the 'Uncombined' dir of that day
        foreach (string a in todaysAdmissions)
        {
            if (!ls.WasCombined(combinedLettersArray, a))
            {
                ls.UncombinedLetters(a, dateFormatted);
            }
        }

        // Places the uncombined scholarships into the 'Uncombined' dir of that day
        foreach (string s in todaysScholarships)
        {
            if (!ls.WasCombined(combinedLettersArray, s))
            {
                ls.UncombinedLetters(s, dateFormatted);
            }
        }
        
        if(combinedLetters.Count > 0)
        {
            ls.CreateReport(combinedLettersArray, dateFormatted);
        }

        ls.ArchiveFiles(dateFormatted);

    }
}
