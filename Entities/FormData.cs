namespace Growth_Strategy_Form_Recognizer.Entities;

public class FormData
{
    public FormData(){}
    public FormData(
        decimal federalTd1Amount,
        decimal provincialTd1Amount,
        bool basicForBoth,
        decimal payRate,
        decimal salary,
        bool isSalary,
        bool isHourly,
        decimal vacationsPercentage,
        bool isAccrued,
        bool isPay,
        string date = "",
        string chequeNumber = "",
        string employer = "",
        string city = "", string name = "",
        string province = "",
        string postalCode = "",
        string socialInsuranceNumber = "", string position = "",
        string payFrequency = "",
        string department = "",
        string email = "",
        string birthdate = "", string startDate = "",
        string mailingAddress = ""
        )
    {
        Employer = employer;
        Name = name;
        Position = position;
        City = city;
        Province = province;
        PostalCode = postalCode;
        Birthdate = birthdate;
        SocialInsuranceNumber = socialInsuranceNumber;
        StartDate = startDate;
        FederalTD1Amount = federalTd1Amount;
        ProvincialTD1Amount = provincialTd1Amount;
        BasicForBoth = basicForBoth;
        PayRate = payRate;
        Salary = salary;
        IsSalary = isSalary;
        IsHourly = isHourly;
        Department = department;
        PayFrequency = payFrequency;
        VacationsPercentage = vacationsPercentage;
        IsAccrued = isAccrued;
        IsPay = isPay;
        Email = email;
        Date = date;
        ChequeNumber = chequeNumber;
        MailingAddress = mailingAddress;
    }

    public string Employer { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string MailingAddress { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string Birthdate { get; set; }
    public string SocialInsuranceNumber { get; set; }
    public string StartDate { get; set; }
    public decimal FederalTD1Amount { get; set; }
    public decimal ProvincialTD1Amount { get; set; }
    public bool BasicForBoth { get; set; }
    public decimal PayRate { get; set; }
    public decimal Salary { get; set; }
    public bool IsSalary { get; set; }
    public bool IsHourly { get; set; }
    public string Department { get; set; }
    public string PayFrequency { get; set; }
    public decimal VacationsPercentage { get; set; }
    public bool IsAccrued { get; set; }
    public bool IsPay { get; set; }
    public string Email { get; set; }
    public string Date { get; set; }
    public string ChequeNumber { get; set; }
}