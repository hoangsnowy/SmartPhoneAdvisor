using SmartPhoneAdvisor.Models;

namespace SmartPhoneAdvisor.Services;

public class BFSRuleEngine : IInferenceEngine
{
    private readonly Dictionary<string, List<string>> _graph;

    public BFSRuleEngine(Dictionary<string, List<string>> graph)
    {
        _graph = graph;
    }

    public (string recommendation, double score, List<string> explanation) Evaluate(UserInput input)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<(string node, List<string> path)>();
        var explanation = new List<string>();

        foreach (var start in input.Needs.Select(n => n.ToLower().Trim()))
        {
            queue.Enqueue((start, new List<string> { start }));
        }

        while (queue.Count > 0)
        {
            var (current, path) = queue.Dequeue();
            if (visited.Contains(current)) continue;
            visited.Add(current);

            if (!_graph.ContainsKey(current))
            {
                explanation.Add($"✅ Dừng tại: {current}");
                return (current, 1.0, explanation);
            }

            foreach (var next in _graph[current])
            {
                var newPath = new List<string>(path) { next };
                queue.Enqueue((next, newPath));
                explanation = newPath.Select(p => "→ " + p).ToList();
            }
        }

        return ("Không tìm thấy kết luận từ BFS", 0.0, new List<string> { "Không có đường suy diễn phù hợp." });
    }
}