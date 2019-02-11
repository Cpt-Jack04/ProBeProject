using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    // Reference to the document display.
    [SerializeField] private DocumentDisplay display;

    // Reference to the results of the previous search.
    public Document result;

    // The the button as well as textmeshpro component from the child of the button.
    private Button button;
    private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI description;
    private const string noResult = "";

    void Start()
    {
        button = GetComponent<Button>();

        // Gets the textmeshpro component and assigns it the no result default.
        resultText = GetComponentInChildren<TextMeshProUGUI>();
        ChangeResult(result);
    }

    // Changes the reference and text for result of this button. 
    public void ChangeResult(Document newResult)
    {
        result = newResult;
        if (result != null)
        {
            resultText.text = result.name;
            description.text = result.destription;

            button.interactable = true;
        }
        else
        {
            resultText.text = noResult;
            description.text = noResult;

            button.interactable = false;
        }
    }

    // Sets the display to show the info of the referenced document.
    public void SetDisplayToResult()
    {
        display.UpdateInfo(result);
    }
}