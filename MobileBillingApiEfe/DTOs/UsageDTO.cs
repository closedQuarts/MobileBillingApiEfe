public class UsageDTO
{
    public int SubscriberId { get; set; }
    public string UsageType { get; set; } = string.Empty;
    public int Month { get; set; }
    public int Year { get; set; }

    public int Amount { get; set; } 
}
