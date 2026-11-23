using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Interfaces;

internal interface IAiService
{
    ServiceCategory? SuggestCategory(string problem);
    string? NextQuestion(List<string> answers);
    string? Summarize(List<string> answers);
}