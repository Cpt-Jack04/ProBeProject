using UnityEngine;

public class SolutionDisplay : MonoBehaviour
{
    [SerializeField] private SolutionDisplayManager solutionManager;        // Reference to access the mananger so we know where the document is onscreen.
    [SerializeField] private Solutions solution;                            // Solution type for this problem.
    public Solutions _solution { get; private set; }                        // Getter and Setter for solution.

    void Start()
    {
        _solution = solution;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Different kinds of solutions for situation.
    /// </summary>
    public enum Solutions
    {
        Data,
        ToolsAndResources,
        Incentives,
        KnowledgeAndSkills,
        Capacity,
        Motivation
    }

    /// <summary>
    /// Checks to see if the player's guess is for a solution is correct.
    /// </summary>
    /// <param name="guess">Guess the player made.</param>
    /// <returns>Returns true if the guess the player made is the correct solution.</returns>
    public bool CheckGuess(Solutions guess)
    {
        if (guess == solution)
            return true;
        return false;
    }
}