using System.Globalization;
using System.Reflection;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Growth_Strategy_Form_Recognizer.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace Growth_Strategy_Form_Recognizer.Pages;

public partial class InventoryLogs
{
    public string? Error;

    private InventoryLogsData? _data;
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

            var formAnalysis = FormAnalyses.First(f => f.GetName() == "InventoryLogsModel");
            AnalyzeResult result = await formAnalysis.Analysis(fileUrl) ?? throw new InvalidOperationException();

            IReadOnlyDictionary<string, DocumentField> fieldStore = result.Documents[0].Fields;


            _data = new InventoryLogsData();
            _dataType = _data?.GetType();
            foreach (var item in fieldStore)
            {
                PropertyInfo? property = _dataType?.GetProperty(item.Key);
                if (property != null)
                {
                    if (property.Name == "Table")
                    {
                        var table = new List<Dictionary<string,string>>();
                        foreach (var doc in (fieldStore["Table"]).Value.AsList())
                        {
                            var tableRow = new Dictionary<string, string>(); 
                            foreach (var dict in doc.Value.AsDictionary())
                            {
                                //var pair = new KeyValuePair<string, string>(dict.Key, dict.Value.Content);
                                tableRow.Add(dict.Key, dict.Value.Content);
                            }
                            table.Add(tableRow);
                        }

                        if (_data != null)
                        {
                            _data.Table = table;
                        }
                    }
                    else
                    {
                        property.SetValue(_data, item.Value.Content);
                    }
                }
            }
            _loading = "";
            _disableLoadBtn = "";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}