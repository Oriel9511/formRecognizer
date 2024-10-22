using System.Globalization;
using System.Reflection;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Growth_Strategy_Form_Recognizer.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Growth_Strategy_Form_Recognizer.Pages;

public partial class BusinessCard
{
    public string? Error;

    private List<BusinessCardData>? _data;
    private List<List<BusinessCardData>> _doc = new List<List<BusinessCardData>>();
    private int _totalDocs = 1;
    private float _progress = 0;
    private int _docsProcessed = 0;
    private Type? _dataType;
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

            var formAnalysis = FormAnalyses.First(f => f.GetName() == "PrebuiltCardModel");
            AnalyzeResult result = await formAnalysis.Analysis(fileUrl) ?? throw new InvalidOperationException();

            IReadOnlyList<AnalyzedDocument> documents = result.Documents;
            // IReadOnlyDictionary<string, DocumentField> fieldStore = result.Documents[0].Fields;

            _tableCount = 0;
            _data = new List<BusinessCardData>();
            _dataType = typeof(BusinessCardData);
            foreach (var document in documents)
            {
                IReadOnlyDictionary<string, DocumentField> fieldStore = document.Fields;
                var card = new BusinessCardData();
                foreach (var item in fieldStore)
                {
                    PropertyInfo? property = _dataType?.GetProperty(item.Key);
                    if (property != null)
                    {
                        var list = new List<string>();
                        foreach (var documentField in item.Value.Value.AsList())
                        {
                            list.Add(documentField.Content);
                        }
                        property.SetValue(card, list);
                    }
                }
                _data.Add(card);
            }
            _loading = "";
            _disableLoadBtn = "";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    
    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        try
        {
            _disableLoadBtn = "disabled";
            _loading = "Loading...";
            _data = null;
            _fileUrl = null;
            _totalDocs = e.GetMultipleFiles().Count;
            foreach (var file in e.GetMultipleFiles())
            {
                Stream stream = file.OpenReadStream(10000000);
                string? fileName = file.Name;
                string contentType = file.ContentType;
                var fileUrl = await DataStorage.UploadFile(stream, fileName, contentType);
                //_fileUrl = fileUrl;

                var formAnalysis = FormAnalyses.First(f => f.GetName() == "PrebuiltCardModel");
                AnalyzeResult result = await formAnalysis.Analysis(fileUrl) ?? throw new InvalidOperationException();

                IReadOnlyList<AnalyzedDocument> documents = result.Documents;

                _tableCount = 0;
                _data = new List<BusinessCardData>();
                _dataType = typeof(BusinessCardData);
                foreach (var document in documents)
                {
                    IReadOnlyDictionary<string, DocumentField> fieldStore = document.Fields;
                    var card = new BusinessCardData();
                    foreach (var item in fieldStore)
                    {
                        PropertyInfo? property = _dataType?.GetProperty(item.Key);
                        if (property != null)
                        {
                            var list = new List<string>();
                            foreach (var documentField in item.Value.Value.AsList())
                            {
                                list.Add(documentField.Content);
                            }
                            property.SetValue(card, list);
                        }
                    }
                    _data.Add(card);
                }
                _doc.Add(_data ?? new List<BusinessCardData>());
                _docsProcessed++;
                _progress = (int)(((float)_docsProcessed) / ((float)_totalDocs) * 100);
                StateHasChanged();
            }
            _loading = "";
            _disableLoadBtn = "";
            
            string json =  JsonConvert.SerializeObject(_doc, Formatting.Indented);
            await JSRuntime.InvokeVoidAsync("downloadJsonFile", "BusinessCards.json", json);
            _progress = 0;
            _docsProcessed = 0;
            _totalDocs = 1;
        }catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    
}