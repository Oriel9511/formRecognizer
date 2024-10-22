using System.Collections;
using System.Globalization;
using System.Reflection;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Growth_Strategy_Form_Recognizer.Data;
using Growth_Strategy_Form_Recognizer.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace Growth_Strategy_Form_Recognizer.Pages;

public partial class GeneralFormRecognition
{
    //private dynamic? _result;
    public string? Error; 
    private AnalyzeResult? _data;
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

            var formAnalysis = FormAnalyses.First(f => f.GetName() == "GeneralModel");
            _data = await formAnalysis.AnalysisPrebuilt(fileUrl) ?? throw new InvalidOperationException();
            
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