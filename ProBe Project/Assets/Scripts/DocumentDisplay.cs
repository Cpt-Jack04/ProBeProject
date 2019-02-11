using UnityEngine;
using TMPro;

public class DocumentDisplay : MonoBehaviour
{
    [SerializeField] private DocumentDisplayManager displayManager;     // Reference to access the mananger so we know where the document is onscreen.

    [SerializeField] private TextMeshProUGUI nameText;                  // Reference for the text that displays the name of the document.

    [SerializeField] private TextMeshProUGUI contentText;               // Reference for the text that displays the information in the document.
    private const string blank = "";                                    // Use if the document is null.

    void Start()
    {
        // Makes the display blank to start.
        MakeBlank();
    }

    // Set name and description text.
    public void UpdateInfo(Document newDoc)
    {
        if (!displayManager.GetIsShowing() && newDoc != null)
        {
            if (CurrentlyBlank())
            {
                displayManager.SwitchOpenButtonText(displayManager.openDocText);
                displayManager.showDocumentButton.interactable = true;
            }

            nameText.text = newDoc.name;

            contentText.text = newDoc.content;
        }
    }

    // Returns true if the information of the current document is noTextResult.
    public bool CurrentlyBlank()
    {
        return nameText.text == blank;
    }

    // Makes the display document blank.
    public void MakeBlank()
    {
        displayManager.SwitchOpenButtonText(blank);
        displayManager.showDocumentButton.interactable = false;

        nameText.text = blank;

        contentText.text = blank;
    }
}