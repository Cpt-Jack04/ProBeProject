using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject notice = null;                      // Reference to the page to be moved.
    [SerializeField] private GameObject searchButtons = null;               // Reference to the search buttons in the scene.
    [SerializeField] private DocumentDisplayManager documentManager = null; // Reference to the document manager.
    [SerializeField] private SolutionDisplayManager solutionManager = null; // Reference to the solution manager.
    [TextArea(15, 20)]
    [SerializeField] private string noticeText = "";                        // Text to be displayed on the notice.
    private Vector3 hiddenPos = Vector3.zero;                               // Vector3 reference for documents while off screen.
    [SerializeField] private Vector3 shownPos = Vector3.zero;               // Vector3 reference for documents while on screen.


    private bool isShowing;                                                 // Is true if the doc is to move on screen or while stationary on screen.
    private bool isMoving;                                                  // Is true if the doc is moving on/off screen.


    [Range(1f, 20f)] [SerializeField] private float speed = 5f;             // Multiplier for the speed of the document moving on/off.
    private float startTime;                                                // Variable for the when the document starts moving. 
    private float journeyLength;                                            // Distance the document needs to travel.


    public Button showNoticeButton;                                         // Reference to the button the shows/hides the document.
    private TextMeshProUGUI showNoticeText;                                 // Reference to the TMPro component on the button above.
    [HideInInspector] public string openNoticeText = "Open Notice";         // Switches to this string once the document is closed.
    private const string openingNoticeText = "Opening Notice";              // Switches to this string while the document is opening.
    private const string closeNoticeText = "Close Notice";                  // Switches to this string once the document is opened.
    private const string closingNoticeText = "Closing Notice";              // Switches to this string while the document is closing.                                           // Timer used to display hint's if player is taking too long


    void Awake()
    {
        notice.GetComponentInChildren<TextMeshProUGUI>().text = noticeText;

        hiddenPos = notice.transform.position;
        journeyLength = Vector3.Distance(hiddenPos, shownPos);

        isMoving = false;

        showNoticeText = showNoticeButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        isShowing = true;

        documentManager.showDocumentButton.interactable = false;
        solutionManager.showSolutionsButton.interactable = false;
        searchButtons.SetActive(false);
        SwitchOpenButtonText(closeNoticeText);

        notice.transform.position = shownPos;
    }

    void Update()
    {
        // Controls the document while moving.
        if (isMoving)
        {
            if (FindObjectOfType<TutorialManager>().TutorialIndex == 0)
                FindObjectOfType<TutorialManager>().NextTutorialObject();
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            if (isShowing)
                notice.transform.position = Vector3.Lerp(hiddenPos, shownPos, fracJourney);
            else
                notice.transform.position = Vector3.Lerp(shownPos, hiddenPos, fracJourney);

            if (distCovered > 0.1f)
            {
                if (notice.transform.position == hiddenPos || notice.transform.position == shownPos)
                {
                    isMoving = false;
                    if (isShowing)
                    {
                        SwitchOpenButtonText(closeNoticeText);
                    }
                    else
                    {
                        if (!documentManager.IsDisplayBlank())
                            documentManager.showDocumentButton.interactable = true;
                        solutionManager.showSolutionsButton.interactable = true;
                        searchButtons.SetActive(true);
                        SwitchOpenButtonText(openNoticeText);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets the IsShowing variable relating to the solutions page.
    /// </summary>
    /// <returns>Returns true if the solutions page is out or is moving out on screen.</returns>
    public bool GetIsShowing()
    {
        return isShowing;
    }

    /// <summary>
    /// Sets varibeles to allow movement.
    /// </summary>
    public void StartMovingNotice()
    {
        if (!isMoving && !documentManager.GetIsShowing() && !solutionManager.GetIsShowing())
        {
            isShowing = !isShowing;
            startTime = Time.time;

            if (isShowing)
            {
                documentManager.showDocumentButton.interactable = false;
                solutionManager.showSolutionsButton.interactable = false;
                searchButtons.SetActive(false);
                SwitchOpenButtonText(openingNoticeText);
            }
            else
                SwitchOpenButtonText(closingNoticeText);

            isMoving = true;
        }
    }

    /// <summary>
    /// Switches the OpenButtonText to a given string.
    /// </summary>
    /// <param name="toShow">The string that will replace the button text</param>
    public void SwitchOpenButtonText(string toShow)
    {
        if (showNoticeText.text != toShow)
            showNoticeText.text = toShow;
    }
}