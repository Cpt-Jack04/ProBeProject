using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentDisplayManager : MonoBehaviour
{
    [SerializeField] private Document[] documents;                          // Reference to all of the documents in the scene.
    [SerializeField] private GameObject document;                           // Reference to the document to be moved.
    [SerializeField] private SolutionDisplayManager solutionManager;        // Reference to the solution manager.
    private DocumentDisplay display;                                        // Reference to the display component on the document.
    private Vector3 hiddenPos;                                              // Vector3 reference for documents while off screen.
    [SerializeField] private Vector3 shownPos;                              // Vector3 reference for documents while on screen.


    private bool isShowing;                                                 // Is true if the doc is to move on screen or while stationary on screen.
    private bool isMoving;                                                  // Is true if the doc is moving on/off screen.

    
    [Range(1f, 20f)] [SerializeField] private float speed = 5f;             // Multiplier for the speed of the document moving on/off.
    private float startTime;                                                // Variable for the when the document starts moving. 
    private float journeyLength;                                            // Distance the document needs to travel.


    public Button showDocumentButton;                                       // Reference to the button the shows/hides the document.
    private TextMeshProUGUI showDocumentText;                               // Reference to the TMPro component on the button above.
    [HideInInspector] public string openDocText = "Open Document";          // Switches to this string once the document is closed.
    private const string openingDocText = "Opening Document";               // Switches to this string while the document is opening.
    private const string closeDocText = "Close Document";                   // Switches to this string once the document is opened.
    private const string closingDocText = "Closing Documnet";               // Switches to this string while the document is closing.

    
    [Range(60f,120f)] [SerializeField] private float hintTimerLength = 60f; // Length of time before hint's are displayed.
    private float hintTimer = 0f;                                           // Timer used to display hint's if player is taking too long


    void Awake()
    {
        // Sets the static list of documents for the database to the list of documents for this scene.
        DocumentDatabase.AddDocumentsAtStart(documents);

        display = document.GetComponent<DocumentDisplay>();
        
        hiddenPos = document.transform.position;
        journeyLength = Vector3.Distance(hiddenPos, shownPos);

        isShowing = false;
        isMoving = false;

        showDocumentText = showDocumentButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Controls the document while moving.
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            if (isShowing)
                document.transform.position = Vector3.Lerp(hiddenPos, shownPos, fracJourney);
            else
                document.transform.position = Vector3.Lerp(shownPos, hiddenPos, fracJourney);

            if (distCovered > 0.1f)
            {
                if (document.transform.position == hiddenPos || document.transform.position == shownPos)
                {
                    isMoving = false;
                    if (isShowing)
                    {
                        StartHintTimer();
                        SwitchOpenButtonText(closeDocText);
                    }
                    else
                    {
                        StopHintTimer();
                        solutionManager.showSolutionsButton.interactable = true;
                        SwitchOpenButtonText(openDocText);
                    }
                }
            }
        }

        // Hint timer.
        if (hintTimer != -5f)
        {
            if (hintTimer > 0)
            {
                hintTimer -= Time.fixedDeltaTime;
                Debug.Log(hintTimer);
            }
            else
            {
                display.DisplayHints();
                StopHintTimer();
            }
        }
    }

    // Returns true if the document is showing.
    public bool GetIsShowing()
    {
        return isShowing;
    }

    // Sets varibeles to allow movement.
    public void StartMovingDoc()
    {
        if (!isMoving && !display.CurrentlyBlank() && !solutionManager.GetIsShowing())
        {
            isShowing = !isShowing;
            startTime = Time.time;

            if (isShowing)
            {
                solutionManager.showSolutionsButton.interactable = false;
                SwitchOpenButtonText(openingDocText);
            }
            else
                SwitchOpenButtonText(closingDocText);

            isMoving = true;
        }
    }

    // Switches the OpenButtonText to a given string.
    public void SwitchOpenButtonText(string toShow)
    {
        if (showDocumentText.text != toShow)
            showDocumentText.text = toShow;
    }

    // Makes the display blank.
    public bool IsDisplayBlank()
    {
        return display.CurrentlyBlank();
    }

    // Sets the timer value to it's start value.
    public void StartHintTimer()
    {
        hintTimer = hintTimerLength;
    }

    // Sets the timer value to -1.
    public void StopHintTimer()
    {
        hintTimer = -5f;
    }
}