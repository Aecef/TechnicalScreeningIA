namespace TechnicalScreeningIA
{
    public interface ILetterService
    {
        string CombinedLettersPath { get; }
        string[] AdmissionSubFolders();
        string[] ScholarshipSubFolders();

        string[] GetTodaysAdmissions(string dateFormatted);
        string[] GetTodaysScholarships(string dateFormatted);
        string GetStudentId(string path);


        bool WasCombined(string[] combinedArray, string letter);

        void CreateReport(string[] combinedLetters, string dateFormatted);
        void UncombinedLetters(string inputFile, string dateFormatted);


        void CombineTwoLetters(string inputFile1, string inputFile2, string dateFormatted);
        void ArchiveFiles(string dateFormatted);
    }
}
