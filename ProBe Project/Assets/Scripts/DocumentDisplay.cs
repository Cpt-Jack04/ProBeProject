using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DocumentDisplay : MonoBehaviour
{
    [SerializeField] private DocumentDisplayManager displayManager = null;  // Reference to access the mananger so we know where the document is onscreen.

    [SerializeField] private TextMeshProUGUI nameText = null;               // Reference for the text that displays the name of the document.

    [SerializeField] private TextMeshProUGUI contentText = null;            // Reference for the text that displays the information in the document.

    [SerializeField] private List<string> hints = null;                     // Reference to the hints of the current doc.

    private string textBeforeHighlight = "";                            // Holds the text before the highlighter tag is ablility to the content.
    private const string startHighlight = "<mark=#ffff00aa>";           // Start tag for highlighter.
    private const string endHighlight = "</mark>";                      // End tag for highlighter.
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
            textBeforeHighlight = newDoc.content;

            hints = newDoc.hints;
            if (FindObjectOfType<TutorialManager>().TutorialIndex == 2)
                FindObjectOfType<TutorialManager>().NextTutorialObject();
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

        hints = null;
    }

    // Replaces the highlighted string with the original string.
    public void HideHints()
    {
        contentText.text = textBeforeHighlight;
    }

    // Finds the hints shown on seen
    public void DisplayHints()
    {
        if (hints != null)
        {
            char[] startHLChar = startHighlight.ToCharArray();
            char[] endHLChar = endHighlight.ToCharArray();

            char[] contentToChar = contentText.text.ToCharArray();
            foreach (string hint in hints)
            {
                char[] hintToChar = hint.ToCharArray();
                char[] replacementChar = new char[startHLChar.Length + hintToChar.Length + endHLChar.Length];

                // Add the chars together.
                for (int index = 0; index < startHLChar.Length; index++)
                {
                    replacementChar[index] = startHLChar[index];
                }
                for (int index = 0; index < hintToChar.Length; index++)
                {
                    replacementChar[index + startHLChar.Length] = hintToChar[index];
                }
                for (int index = 0; index < endHLChar.Length; index++)
                {
                    replacementChar[index + startHLChar.Length + hintToChar.Length] = endHLChar[index];
                }

                // Replaces hint char with hint plus highlighting.
                contentText.text = contentText.text.Replace(hint, replacementChar.ArrayToString());
            }
        }
    }
}