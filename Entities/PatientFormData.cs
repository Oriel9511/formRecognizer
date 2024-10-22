namespace Growth_Strategy_Form_Recognizer.Entities;

public class PatientFormData
{
    public PatientFormData()
    {
        DataTable = new List<PatientDataTable>();
    }
    
    public string? Name { get; set; }
    public string? HearingInstrumentInformation {get; set;}
    public string? MakeStyle { get; set; }
    public string? HomeTelephoneNumber { get; set; }
    public string? Other { get; set; }
    public string? BatterySize { get; set; }
    public string? Address { get; set; }
    public string? ReceiverSize { get; set; }
    public string? DomeSize { get; set; }
    public string? L_Serial { get; set; }
    public string? Cost { get; set; }
    public string? DOB { get; set; }
    public string? Deposit { get; set; }
    public string? Balance { get; set; }
    public string? R_Serial { get; set; }
    public string? AccesoriesSerialNo { get; set; }
    public List<PatientDataTable> DataTable { get; set; }
}