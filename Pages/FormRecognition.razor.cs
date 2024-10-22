using System.Globalization;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Growth_Strategy_Form_Recognizer.Data;
using Growth_Strategy_Form_Recognizer.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace Growth_Strategy_Form_Recognizer.Pages
{
    public partial class FormRecognition
    {
        public string? Error;

        private FormData? _data;
        //private dynamic? _result;
        private string? _fileUrl;
        private string _loading = "";
        private string _disableLoadBtn = "";
        private async Task LoadFile(InputFileChangeEventArgs e)
        {
            try
            {
                _disableLoadBtn = "disabled";
                _loading = "Loading...";
                _data = null;
                _fileUrl = null;
                var file = e.File;
            
                if (file == null)
                {
                    Error = "Some error just happened";
                    return;
                }
            
                Stream stream = file.OpenReadStream();
                string? fileName = file.Name;
                string contentType = file.ContentType;
                var fileUrl = await DataStorage.UploadFile(stream, fileName,contentType);
                _fileUrl = fileUrl;

                var formAnalysis = FormAnalyses.First(f => f.GetName() == "EmployerModel");
                AnalyzeResult result = await formAnalysis.Analysis(fileUrl) ?? throw new InvalidOperationException();

                IReadOnlyDictionary<string, DocumentField> fieldStore = result.Documents[0].Fields;
                
                // foreach (AnalyzedDocument document in result.Documents)
                // {
                //     foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                //     {
                //         string fieldName = fieldKvp.Key;
                //         DocumentField field = fieldKvp.Value;
                //
                //         Console.WriteLine($"Field '{fieldName}': ");
                //
                //         Console.WriteLine($"  Content: '{field.Content}'");
                //         Console.WriteLine($"  Confidence: '{field.Confidence}'");
                //     }
                // }

                _data = new FormData
                {
                    Employer = (fieldStore["Employer"]).Content,
                    Name = (fieldStore["Name"]).Content,
                    Position = (fieldStore["Position"]).Content,
                    MailingAddress = (fieldStore["Mailing Address"]).Content,
                    City = (fieldStore["City"]).Content,
                    Province = (fieldStore["Province"]).Content,
                    PostalCode = (fieldStore["Postal Code"]).Content,
                    Birthdate = (fieldStore["Birthdate"]).Content,
                    SocialInsuranceNumber = (fieldStore["Social Insurance Number"]).Content,
                    StartDate = (fieldStore["Start Date"]).Content,
                    FederalTD1Amount = decimal.Parse((fieldStore["Federal TD1 Amount"]).Content ?? "0"),
                    ProvincialTD1Amount = decimal.Parse((fieldStore["Provincial TD1 Amount"]).Content ?? "0"),
                    BasicForBoth = (fieldStore["Basic For Both"]).Content == ":selected:",
                    PayRate = decimal.Parse((fieldStore["Pay Rate"]).Content ?? "0"),
                    Salary = decimal.Parse((fieldStore["Salary"]).Content ?? "0"),
                    IsSalary = (fieldStore["Is Salary"]).Content == ":selected:",
                    IsHourly = (fieldStore["Is Hourly"]).Content == ":selected:",
                    Department = (fieldStore["Department"]).Content,
                    PayFrequency = (fieldStore["Pay Frequency"]).Content,
                    VacationsPercentage = decimal.Parse(((fieldStore["Vacations Percentage"]).Content ?? "0").Replace("%","")),
                    IsAccrued = (fieldStore["Is Accrued"]).Content == ":selected:",
                    IsPay = (fieldStore["Is Pay"]).Content == ":selected:",
                    Email = (fieldStore["Email"]).Content,
                    Date = (fieldStore["Date"]).Content,
                    ChequeNumber = new string((fieldStore["Cheque Number"]).Content.Where(char.IsDigit).ToArray())
                };
                _loading = "";
                _disableLoadBtn = "";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
