using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolutionDisplay : MonoBehaviour
{
    private SolutionDisplayManager solutionManager;                         // Reference to access the mananger so we know where the document is onscreen.
    [SerializeField] private Button[] solutionButtons = new Button[6];      // Array of the buttons used by the solutions.

    [SerializeField] private TextMeshProUGUI answerResponseText = null;     // TMPro object that will display the reason why an answer is correct or incorrect.
    private const string blank = "";                                        // Use if there's no guess.

    void Start()
    {
        solutionManager = FindObjectOfType<SolutionDisplayManager>();

        foreach (Button button in solutionButtons)
        {
            TextMeshProUGUI descriptionText = button.GetComponentInChildren<TextMeshProUGUI>();
            descriptionText.text = solutionManager.GetSolutionDescription(button.GetComponent<SolutionButton>().Solution);
        }
        SetResponseText();
    }

    /// <summary>
    /// Makes the answerResponseText.text blank.
    /// </summary>
    public void SetResponseText()
    {
        answerResponseText.SetText(blank);
    }

    /// <summary>
    /// Sets the answerResponseText.text to the given string.
    /// </summary>
    /// <param name="response">Was the answer correct? Why or Why not?</param>
    public void SetResponseText(string response)
    {
        answerResponseText.SetText(response);
    }
}