using UnityEngine;

public class SolutionButton : MonoBehaviour
{
    private SolutionDisplayManager solutionManager;                                             // Reference to the solution manager.
    [SerializeField] private Situation.SolutionType solution = Situation.SolutionType.Data;     // The solution type that this button represents.
    public Situation.SolutionType Solution                                                      // Getter for solution.
    {
        get
        {
            return solution;
        }
    }

    void Start()
    {
        solutionManager = FindObjectOfType<SolutionDisplayManager>();
    }

    /// <summary>
    /// Called whenever the related solution button is pressed.
    /// </summary>
    public void PlayerGuessedThis()
    {
        if (solutionManager.CheckSolution(solution))
        {
            Debug.Log("Is correct.");
        }
        else
        {
            Debug.Log("Isn't correct.");
        }
    }
}
