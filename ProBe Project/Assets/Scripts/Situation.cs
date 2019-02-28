using System.Collections.Generic;
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
    /// A description to for the ToolsAndResources solution.
    /// </summary>
    [TextArea(2, 2)]
    public string toolsandresourcesDescription;

    /// <summary>
    /// A description to for the Incentives solution.
    /// </summary>
    [TextArea(2, 2)]
    public string incentivesDescription;

    /// <summary>
    /// A description to for the KnowledgeAndSkills solution.
    /// </summary>
    [TextArea(2, 2)]
    public string knowledgeandskillsDescription;

    /// <summary>
    /// A description to for the Capacity solution.
    /// </summary>
    [TextArea(2, 2)]
    public string capacityDescription;

    /// <summary>
    /// A description to for the Motivation solution.
    /// </summary>
    [TextArea(2, 2)]
    public string motivationDescription;
}