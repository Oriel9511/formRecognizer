namespace Growth_Strategy_Form_Recognizer.Entities;

public class BusinessCardData
{
    public IEnumerable<string> Addresses { get; set; }
    public IEnumerable<string> CompanyNames { get; set; }
    public IEnumerable<string> ContactNames { get; set; }
    public IEnumerable<string> Emails { get; set; }
    public IEnumerable<string> Faxes { get; set; }
    public IEnumerable<string> MobilePhones { get; set; }
    public IEnumerable<string> Websites { get; set; }
    public IEnumerable<string> WorkPhones { get; set; }
    public IEnumerable<string> JobTitles { get; set; }
}