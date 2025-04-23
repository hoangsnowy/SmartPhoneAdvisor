namespace SmartPhoneAdvisor.Models;

public class Rule
{
    public string Id { get; set; } = string.Empty;
    public Dictionary<string, object> Conditions { get; set; } = new();
    public string Recommendation { get; set; } = string.Empty;
    public double Certainty { get; set; }
}