﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentDisplayManager : MonoBehaviour
{
    [SerializeField] private Situation situation = null;                    // Reference to the situation of this scene.
    [SerializeField] private GameObject document = null;                    // Reference to the document to be moved.
    [SerializeField] private SolutionDisplayManager solutionManager = null; // Reference to the solution manager.
    [SerializeField] private NoticeDisplayManager noticeManager = null;     // Reference to the notice manager.
    private DocumentDisplay display = null;                                 // Reference to the display component on the document.
    private Vector3 hiddenPos = Vector3.zero;                               // Vector3 reference for documents while off screen.
    [SerializeField] private Vector3 shownPos = Vector3.zero;               // Vector3 reference for documents while on screen.


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
        DocumentDatabase.AddDocumentsAtStart(situation.documents);

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
            if (FindObjectOfType<TutorialManager>().TutorialIndex == 3)
                FindObjectOfType<TutorialManager>().NextTutorialObject();
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
                        display.HideHints();
                        solutionManager.showSolutionsButton.interactable = true;
                        noticeManager.showNoticeButton.interactable = true;
                        SwitchOpenButtonText(openDocText);
                    }
                }
            }
        }

        // Hint timer.
        if (hintTimer != -5f)
        {
            if (hintTimer > 0)
                hintTimer -= Time.fixedDeltaTime;
            else
            {
                display.DisplayHints();
                StopHintTimer();
            }
        }
    }

    /// <summary>
    /// Gets the IsShowing variable relating to the document page.
    /// </summary>
    /// <returns>Returns true if the document page is out or is moving out on screen.</returns>
    public bool GetIsShowing()
    {
        return isShowing;
    }

    /// <summary>
    /// Sets varibeles to allow movement.
    /// </summary>
    public void StartMovingDoc()
    {
        if (!isMoving && !display.CurrentlyBlank() && !solutionManager.GetIsShowing() && !noticeManager.GetIsShowing())
        {
            isShowing = !isShowing;
            startTime = Time.time;

            if (isShowing)
            {
                solutionManager.showSolutionsButton.interactable = false;
                noticeManager.showNoticeButton.interactable = false;
                SwitchOpenButtonText(openingDocText);
            }
            else
                SwitchOpenButtonText(closingDocText);

            isMoving = true;
        }
    }

    /// <summary>
    /// Switches the OpenButtonText to a given string.
    /// </summary>
    /// <param name="toShow">The string that will replace the button text</param>
    public void SwitchOpenButtonText(string toShow)
    {
        if (showDocumentText.text != toShow)
            showDocumentText.text = toShow;
    }

    /// <summary>
    /// Checks to see if there's any information on the document at the moment.
    /// </summary>
    /// <returns>Returns true if there is no information being displayed.</returns>
    public bool IsDisplayBlank()
    {
        return display.CurrentlyBlank();
    }

    /// <summary>
    /// Starts the timer related to displaying the hints on a document.
    /// </summary>
    public void StartHintTimer()
    {
        hintTimer = hintTimerLength;
    }

    /// <summary>
    /// Stops the timer related to displaying the hints on a document.
    /// </summary>
    public void StopHintTimer()
    {
        hintTimer = -5f;
    }
}