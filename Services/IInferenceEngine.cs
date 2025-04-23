using SmartPhoneAdvisor.Models;

namespace SmartPhoneAdvisor.Services;

public interface IInferenceEngine
{
    (string recommendation, double score, List<string> explanation) Evaluate(UserInput input);
}