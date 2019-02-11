using UnityEngine;

public class DocumentSearchManager : MonoBehaviour
{
    // Reference to the displaymanagers manager.
    DocumentDisplayManager documentManager;
    SolutionDisplayManager searchManager;

    // References to the result button scripts in the scene.
    [SerializeField] private ResultsManager result1;
    [SerializeField] private ResultsManager result2;
    [SerializeField] private ResultsManager result3;

    void Awake()
    {
        documentManager = GetComponent<DocumentDisplayManager>();
        searchManager = GetComponent<SolutionDisplayManager>();
    }

    // Performs a search based in the document database based on the provided string.
    public void ConductSearch(string searchFor)
    {
        // Returns if search is blank.
        if (searchFor == "" || documentManager.GetIsShowing() || searchManager.GetIsShowing())
            return;

        // Calls the static search method...
        DocumentDatabase.PerformSearch(searchFor);

        // ... then updates the results on screen.
        result1.ChangeResult(PullFirst());
        result2.ChangeResult(PullSecond());
        result3.ChangeResult(PullThird());
    }

    // Pulls the first result of the search.
    public Document PullFirst()
    {
        if (DocumentDatabase.documentDatabase.Length < 1 || DocumentDatabase.documentDatabase[0] == null || DocumentDatabase.documentDatabase[0].searchScore < 1f)
            return null;

        Debug.Log(DocumentDatabase.documentDatabase[0].searchScore);
        return DocumentDatabase.documentDatabase[0];
    }

    // Pulls the second result of the search.
    public Document PullSecond()
    {
        if (DocumentDatabase.documentDatabase.Length < 2 || DocumentDatabase.documentDatabase[1] == null || DocumentDatabase.documentDatabase[1].searchScore < 1f)
            return null;

        Debug.Log(DocumentDatabase.documentDatabase[1].searchScore);
        return DocumentDatabase.documentDatabase[1];
    }

    // Pulls the third result of the search.
    public Document PullThird()
    {
        if (DocumentDatabase.documentDatabase.Length < 3 || DocumentDatabase.documentDatabase[2] == null || DocumentDatabase.documentDatabase[2].searchScore < 1f)
            return null;

        Debug.Log(DocumentDatabase.documentDatabase[2].searchScore);
        return DocumentDatabase.documentDatabase[2];
    }

    // Pulls a result a the provided index.
    public Document PullResult(int index)
    {
        if (DocumentDatabase.documentDatabase.Length < index || DocumentDatabase.documentDatabase[index] == null || DocumentDatabase.documentDatabase[index].searchScore < 1f)
            return null;

        Debug.Log(DocumentDatabase.documentDatabase[index].searchScore);
        return DocumentDatabase.documentDatabase[index];
    }
}