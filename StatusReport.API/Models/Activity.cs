namespace StatusReport.API.Models;
public class Activity
{
    public string id { get; set; }
    public string ITPR { get; set; }
    public string TaskName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CurrentActiiviyDetails { get; set; }
    public string PlannedActiiviyDetails { get; set; }
    public Clarity ClarityDetails { get; set; }
    public string MeetingHours { get; set; }
}

public class Clarity
{
    private int _numberOfLeavesTaken;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AllocatedHours { get; set; }
    public int SubmittedHours { get; set; }
    public List<DateTime>? Leaves { get; set; }
    public int NumberOfLeavesTaken {
        get { return _numberOfLeavesTaken; }
        set { _numberOfLeavesTaken = this.Leaves!=null? this.Leaves.Count:0; } }
}

