using UnityEngine;

[CreateAssetMenu(fileName = "New Situation", menuName = "Situation")]
public class Situation : ScriptableObject
{
    /// <summary>
    /// Documents for this situations.
    /// </summary>
    public Document[] documents;

    /// <summary>
    /// Different kinds of solutions for situations.
    /// </summary>
    public enum SolutionType
    {
        Data,
        ToolsAndResources,
        Incentives,
        KnowledgeAndSkills,
        Capacity,
        Motivation
    }

    /// <summary>
    /// The correct solution for this siutation.
    /// </summary>
    public SolutionType correctSolution;

    /// <summary>
    /// A description to for the data solution.
    /// </summary>
    [TextArea(2, 2)]
    public string dataDescription;

    /// <summary>
    /// A reasoning of why the Data is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string dataResponse;

    /// <summary>
    /// A description to for the ToolsAndResources solution.
    /// </summary>
    [TextArea(2, 2)]
    public string toolsandresourcesDescription;

    /// <summary>
    /// A reasoning of why the Tools and Resources is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string toolsandresourcesResponse;

    /// <summary>
    /// A description to for the Incentives solution.
    /// </summary>
    [TextArea(2, 2)]
    public string incentivesDescription;

    /// <summary>
    /// A reasoning of why the Incentives is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string incentivesResponse;

    /// <summary>
    /// A description to for the Knowledge and Skills solution.
    /// </summary>
    [TextArea(2, 2)]
    public string knowledgeandskillsDescription;

    /// <summary>
    /// A reasoning of why the Knowledge and Skills is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string knowledgeandskillsResponse;

    /// <summary>
    /// A description to for the Capacity solution.
    /// </summary>
    [TextArea(2, 2)]
    public string capacityDescription;

    /// <summary>
    /// A reasoning of why the Capacity is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string capacityResponse;

    /// <summary>
    /// A description to for the Motivation solution.
    /// </summary>
    [TextArea(2, 2)]
    public string motivationDescription;

    /// <summary>
    /// A reasoning of why the Motivation is the correct/incorrect answer.
    /// </summary>
    [TextArea(2, 2)]
    public string motivationResponse;
}