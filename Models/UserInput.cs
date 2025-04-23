namespace SmartPhoneAdvisor.Models;

public class UserInput
{
    public string Gender { get; set; } = string.Empty;
    public string AgeGroup { get; set; } = string.Empty;
    public string Budget { get; set; } = string.Empty;
    public List<string> Needs { get; set; } = new();
    public string Color { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string InferenceMethod { get; set; } = "Rule";
}