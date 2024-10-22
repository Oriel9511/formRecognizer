using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Growth_Strategy_Form_Recognizer.Data
{
    public class FormAnalysis : IFormAnalysis
    {

        private readonly string _name;  
        private readonly DocumentAnalysisClient _client;
        private readonly string _modelName;
        public FormAnalysis(string name, string key, string endpoint, string modelName)
        {
            _name = name;
            _modelName = modelName;
            var credential = new AzureKeyCredential(key);
            _client =  new DocumentAnalysisClient(new Uri(endpoint), credential);
        }

        public string GetName()
        {
            return _name;
        }

        public async Task<AnalyzeResult?> Analysis(string? fileUrl)
        {
            try
            {
                var fileUri = new Uri(fileUrl);
                var operation = await _client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, _modelName, fileUri);
                var result = operation.Value;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public async Task<AnalyzeResult?> AnalysisPrebuilt(string? fileUrl)
        {
            try
            {
                var fileUri = new Uri(fileUrl);
                AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-document", fileUri);

                AnalyzeResult result = operation.Value;

                Console.WriteLine("******* Detected key-value pairs: *******");

                foreach (DocumentKeyValuePair kvp in result.KeyValuePairs)
                {
                    if (kvp.Value == null)
                    {
                        Console.WriteLine($"  Found key with no value: '{kvp.Key.Content}'");
                    }
                    else
                    {
                        Console.WriteLine($"  Found key-value pair: '{kvp.Key.Content}' and '{kvp.Value.Content}'");
                    }
                }
                
                Console.WriteLine("******* The following tables were extracted: *******");

                for (int i = 0; i < result.Tables.Count; i++)
                {
                    DocumentTable table = result.Tables[i];
                    Console.WriteLine($"  Table {i} has {table.RowCount} rows and {table.ColumnCount} columns.");

                    foreach (DocumentTableCell cell in table.Cells)
                    {
                        Console.WriteLine($"    Cell ({cell.RowIndex}, {cell.ColumnIndex}) has kind '{cell.Kind}' and content: '{cell.Content}'.");
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
