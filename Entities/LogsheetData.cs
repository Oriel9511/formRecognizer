namespace Growth_Strategy_Form_Recognizer.Entities;

public class LogsheetData
{
    // public LogsheetData(string farmName, string treaterNumber, string legalLandLocation, string operatorName, string date, string temperature, string notes, TableLogsheet table, string productApplied1, string productApplied2, string productApplied3, string productApplied4, string samplesTaken, string productReceivedOnSite, string operatorSignature)
    // {
    //     FarmName = farmName;
    //     TreaterNumber = treaterNumber;
    //     LegalLandLocation = legalLandLocation;
    //     Operator = operatorName;
    //     Date = date;
    //     Temperature = temperature;
    //     Notes = notes;
    //     Table = table;
    //     ProductApplied1 = productApplied1;
    //     ProductApplied2 = productApplied2;
    //     ProductApplied3 = productApplied3;
    //     ProductApplied4 = productApplied4;
    //     SamplesTaken = samplesTaken;
    //     ProductReceivedOnSite = productReceivedOnSite;
    //     OperatorSignature = operatorSignature;
    // }
    
    public string FarmName { get; set; }
    public string TreaterNumber { get; set; }
    public string LegalLandLocation { get; set; }
    public string Operator { get; set; }
    public string Date { get; set; }
    public string Temperature { get; set; }
    public string Notes { get; set; }
    public IEnumerable<LogsheetTableRow> Table { get; set; } 
    public string ProductApplied1 { get; set; }
    public string ProductApplied2 { get; set; }
    public string ProductApplied3 { get; set; }
    public string ProductApplied4 { get; set; }
    public string SamplesTaken { get; set; }
    public string ProductReceivedOnSite { get; set; }
    public string OperatorSignature { get; set; } 
}

public class LogsheetTableRow
{
    // public TableLogsheet(string seedType, string bushelWeight, string kilograms, string bushels, string product1, string product2, string product3, string product4)
    // {
    //     SeedType = seedType;
    //     BushelWeight = bushelWeight;
    //     Kilograms = kilograms;
    //     Bushels = bushels;
    //     Product1 = product1;
    //     Product2 = product2;
    //     Product3 = product3;
    //     Product4 = product4;
    // }
    public string SeedType { get; set; }
    public string BushelWeight { get; set; }
    public string Kilograms { get; set; }
    public string Bushels { get; set; }
    public string Product1 { get; set; }
    public string Product2 { get; set; }
    public string Product3 { get; set; }
    public string Product4 { get; set; }
}
