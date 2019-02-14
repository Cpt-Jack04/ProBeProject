using UnityEngine;

[CreateAssetMenu(fileName = "New Hint ", menuName = "Hint")]
public class Hints : ScriptableObject
{
    public Document document;       // Document the hint is for.

    [TextArea(2, 2)]
    public string hint;             // The hint itself.     // You will need to copy the hint from the document.
}