using SmartPhoneAdvisor.Models;

namespace SmartPhoneAdvisor.Services;

public class RuleEngine : IInferenceEngine
{
    private readonly List<Rule> _rules;

    public RuleEngine(List<Rule> rules)
    {
        _rules = rules;
    }

    public (string recommendation, double score, List<string> explanation) Evaluate(UserInput input)
    {
        var matched = _rules
            .Where(rule => Matches(rule.Conditions, input))
            .Select(rule => (rule, rule.Certainty))
            .ToList();

        var best = matched.OrderByDescending(r => r.Certainty).FirstOrDefault();
        var reasons = matched
            .Select(r => $"✔ {r.rule.Id}: {FormatConditions(r.rule.Conditions)} → {r.rule.Recommendation} (CF={r.rule.Certainty})")
            .ToList();

        return (best.rule?.Recommendation ?? "Không tìm thấy gợi ý phù hợp", best.Certainty, reasons);
    }

    private bool Matches(Dictionary<string, object> conds, UserInput input)
    {
        if (conds == null || conds.Count == 0) return false;

        foreach (var kv in conds)
        {
            var key = kv.Key.ToLower().Trim();
            var value = kv.Value;

            switch (key)
            {
                case "gender":
                    if (!string.Equals(input.Gender?.Trim(), value?.ToString()?.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                    break;
                case "agegroup":
                    if (!string.Equals(input.AgeGroup?.Trim(), value?.ToString()?.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                    break;
                case "budget":
                    if (!string.Equals(input.Budget?.Trim(), value?.ToString()?.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                    break;
                case "color":
                    if (!string.Equals(input.Color?.Trim(), value?.ToString()?.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                    break;
                case "brand":
                    if (!string.Equals(input.Brand?.Trim(), value?.ToString()?.Trim(), StringComparison.OrdinalIgnoreCase))
                        return false;
                    break;
                case "needs":
                    var needs = value as System.Text.Json.JsonElement?;
                    if (needs.HasValue && needs.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                    {
                        var ruleNeeds = needs.Value.EnumerateArray().Select(n => n.GetString()?.ToLower()?.Trim()).ToList();
                        var inputNeeds = input.Needs.Select(n => n.ToLower().Trim()).ToList();
                        if (!ruleNeeds.Any(n => inputNeeds.Contains(n)))
                            return false;
                    }
                    break;
            }
        }

        return true;
    }

    private string FormatConditions(Dictionary<string, object> conds)
    {
        return string.Join(", ", conds.Select(kv => $"{kv.Key}: {kv.Value}"));
    }
}