namespace StatusReport.API.Models;

public class User
{
    public string id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ReportingTo { get; set; }
}
