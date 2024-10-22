using System.Collections;
using System.Globalization;
using System.Reflection;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Growth_Strategy_Form_Recognizer.Data;
using Growth_Strategy_Form_Recognizer.Entities;
using Microsoft.AspNetCore.Components.Forms;


namespace Growth_Strategy_Form_Recognizer.Pages;

public partial class PatientFormRecognition
{
        public string? Error;

        private PatientFormData? _data;
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

                Stream stream = file.OpenReadStream(10000000);
                string? fileName = file.Name;
                string contentType = file.ContentType;
                var fileUrl = await DataStorage.UploadFile(stream, fileName, contentType);
                _fileUrl = fileUrl;

                var formAnalysis = FormAnalyses.First(f => f.GetName() == "PatientModel");
                AnalyzeResult result = await formAnalysis.Analysis(fileUrl) ?? throw new InvalidOperationException();

                IReadOnlyDictionary<string, DocumentField> fieldStore = result.Documents[0].Fields;
                _data = new PatientFormData();
                foreach (AnalyzedDocument document in result.Documents)
                {
                    foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                    {
                        string fieldName = fieldKvp.Key.Replace(" ", "");
                        DocumentField field = fieldKvp.Value;

                        PropertyInfo propertyInfo = _data.GetType().GetProperty(fieldName);
                        if (propertyInfo != null)
                        {
                            if (fieldName == "DataTable")
                            {
                                
                                Console.WriteLine(field.Value.GetType());
                                DocumentFieldValue values = field.Value;
                                foreach (var value in values.AsDictionary())
                                {
                                    var patientDataTable = new PatientDataTable();
                                    var row = value.Value.Value.AsDictionary();
                                    foreach (var cell in row)
                                    {
                                        if (cell.Key == "Date")
                                        {
                                            patientDataTable.Date = cell.Value.Content;
                                        }
                                        else
                                        {
                                            patientDataTable.Details = cell.Value.Content;
                                        }
                                        
                                    }
                                    _data.DataTable.Add(patientDataTable);
                                }
                                //patientDataTable.Date = field.Value;
                                //field.Value;
                            }
                            else
                            {
                                propertyInfo.SetValue(_data, field.Content);
                            }
                        }
                    }
                }
                
                _loading = "";
                _disableLoadBtn = "";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                _loading = "";
                _disableLoadBtn = "";
            }
        }
}