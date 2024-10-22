using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Growth_Strategy_Form_Recognizer.Data
{
    public interface IFormAnalysis
    {
        public string GetName();
        public Task<AnalyzeResult?> Analysis(string? fileUrl);
        public Task<AnalyzeResult?> AnalysisPrebuilt(string? fileUrl);
    }
}
