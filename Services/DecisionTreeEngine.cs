using SmartPhoneAdvisor.Models;

namespace SmartPhoneAdvisor.Services;

public class DecisionTreeEngine : IInferenceEngine
{
    private readonly List<Dictionary<string, object>> _trainingData;

    public DecisionTreeEngine(List<Dictionary<string, object>> trainingData)
    {
        _trainingData = trainingData;
    }

    public (string recommendation, double score, List<string> explanation) Evaluate(UserInput input)
    {
        var inputKeys = new Dictionary<string, string>
        {
            { "Gender", input.Gender },
            { "AgeGroup", input.AgeGroup },
            { "Budget", input.Budget },
            { "Color", input.Color },
            { "Brand", input.Brand }
        };

        string bestMatch = "Không tìm thấy kết quả phù hợp";
        double bestScore = 0;
        List<string> explain = new();

        foreach (var record in _trainingData)
        {
            int match = 0;
            int total = inputKeys.Count;

            foreach (var key in inputKeys.Keys)
            {
                if (record.ContainsKey(key) &&
                    string.Equals(record[key]?.ToString()?.Trim(), inputKeys[key], StringComparison.OrdinalIgnoreCase))
                {
                    match++;
                }
            }

            if (record.ContainsKey("Needs") && input.Needs.Count > 0)
            {
                var needs = ((System.Text.Json.JsonElement)record["Needs"])
                    .EnumerateArray()
                    .Select(n => n.GetString()?.ToLower()?.Trim())
                    .ToList();
                if (needs.Any(n => input.Needs.Select(x => x.ToLower().Trim()).Contains(n)))
                    match++;
                total++;
            }

            double score = (double)match / total;
            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = record.ContainsKey("Label") ? record["Label"]?.ToString() ?? "" : "Không rõ";
            }
        }

        explain.Add($"→ So sánh đầu vào với dữ liệu huấn luyện, mức độ khớp: {Math.Round(bestScore * 100)}%");
        return (bestMatch, bestScore, explain);
    }
}