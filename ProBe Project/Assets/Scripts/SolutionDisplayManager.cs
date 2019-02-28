using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolutionDisplayManager : MonoBehaviour
{
    [SerializeField] private Situation situation = null;                    // Reference to the situation of this scene.
    [SerializeField] private GameObject solutions = null;                   // Reference to the page to be moved.
    [SerializeField] private DocumentDisplayManager documentManager = null; // Reference to the document manager.
    [SerializeField] private NoticeDisplayManager noticeManager = null;     // Reference to the notice manager.
    private SolutionDisplay display = null;                                 // Reference to the display component on the document.
    private Vector3 hiddenPos = Vector3.zero;                               // Vector3 reference for documents while off screen.
    [SerializeField] private Vector3 shownPos = Vector3.zero;               // Vector3 reference for documents while on screen.


    private bool isShowing;                                         // Is true if the doc is to move on screen or while stationary on screen.
    private bool isMoving;                                          // Is true if the doc is moving on/off screen.


    [Range(1f, 20f)] [SerializeField] private float speed = 5f;     // Multiplier for the speed of the document moving on/off.
    private float startTime;                                        // Variable for the when the document starts moving. 
    private float journeyLength;                                    // Distance the document needs to travel.


    public Button showSolutionsButton;                              // Reference to the button the shows/hides the document.
    private TextMeshProUGUI showSolutionsText;                      // Reference to the TMPro component on the button above.
    [HideInInspector] public string openSolText = "Open Solutions"; // Switches to this string once the document is closed.
    private const string openingSolText = "Opening Solution";       // Switches to this string while the document is opening.
    private const string closeSolText = "Close Solution";           // Switches to this string once the document is opened.
    private const string closingSolText = "Closing Solution";       // Switches to this string while the document is closing.


    void Awake()
    {
        display = solutions.GetComponent<SolutionDisplay>();

        hiddenPos = solutions.transform.position;
        journeyLength = Vector3.Distance(hiddenPos, shownPos);

        isShowing = false;
        isMoving = false;

        showSolutionsText = showSolutionsButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Controls the document while moving.
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            if (isShowing)
                solutions.transform.position = Vector3.Lerp(hiddenPos, shownPos, fracJourney);
            else
                solutions.transform.position = Vector3.Lerp(shownPos, hiddenPos, fracJourney);

            if (distCovered > 0.1f)
            {
                if (solutions.transform.position == hiddenPos || solutions.transform.position == shownPos)
                {
                    isMoving = false;
                    if (isShowing)
                        SwitchOpenButtonText(closeSolText);
                    else
                    {
                        if (!documentManager.IsDisplayBlank())
                            documentManager.showDocumentButton.interactable = true;
                        noticeManager.showNoticeButton.interactable = true;
                        SwitchOpenButtonText(openSolText);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns true if the document is showing.
    /// </summary>
    /// <returns>Returns true if the document is the document is or is moving out on screen.</returns>
    public bool GetIsShowing()
    {
        return isShowing;
    }

    /// <summary>
    /// Sets varibeles to allow movement.
    /// </summary>
    public void StartMovingDoc()
    {
        if (!isMoving && !documentManager.GetIsShowing() && !noticeManager.GetIsShowing())
        {
            isShowing = !isShowing;
            startTime = Time.time;

            if (isShowing)
            {
                documentManager.showDocumentButton.interactable = false;
                noticeManager.showNoticeButton.interactable = false;
                SwitchOpenButtonText(openingSolText);
            }
            else
                SwitchOpenButtonText(closingSolText);

            isMoving = true;
        }
    }

    /// <summary>
    /// Switches the OpenButtonText to a given string.
    /// </summary>
    /// <param name="toShow">The string that will replace the button text</param>
    public void SwitchOpenButtonText(string toShow)
    {
        if (showSolutionsText.text != toShow)
            showSolutionsText.text = toShow;
    }

    /// <summary>
    /// Checks to see if the player's guess is for a solution is correct.
    /// </summary>
    /// <param name="guess">The solution the player tapped on.</param>
    /// <returns>Returns true if the guess the player made is the correct solution.</returns>
    public bool CheckGuess(Situation.SolutionType guess)
    {
        return guess == situation.correctSolution;
    }
}