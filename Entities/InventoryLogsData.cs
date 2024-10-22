namespace Growth_Strategy_Form_Recognizer.Entities;

public class InventoryLogsData
{
    public string Date { get; set; }
    public string Operator { get; set; }
    public  string CT { get; set; }
    public List<Dictionary<string, string>> Table { get; set; }
    public  string Note { get; set; }
}

// public class InventoryLogsTableRow
// {
//     public string  Product { get; set; }
//     public string  Y1 { get; set; }
//     public string  Y2 { get; set; }
//     public string  Y3 { get; set; }
//     public string  Y4 { get; set; }
//     public string  Y5 { get; set; }
//     public string  Y6 { get; set; }
//     public string  Y7 { get; set; }
//     public string  Required { get; set; }
//     public string  InTank { get; set; }
//     public string  Loaded { get; set; }
//     public string  OnDeck { get; set; }
//     public string  Total { get; set; }
// }