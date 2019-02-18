using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Text Document", menuName = "Document")]
public class Document : ScriptableObject
{
    public List<string> docTags;    // The tags that can be used to find the document.

    [TextArea(2, 2)]
    public new string name;         // Name of the document.
    [TextArea(5, 1)]
    public string destription;      // Description of the document.

    [TextArea(15, 20)]
    public string content;          // The text within the document.

    [TextArea(2, 4)]
    public List<string> hints;      // Hints for this document.     // You will need to copy the hint from the document.

    [HideInInspector]
    public float searchScore;       // Comparision score EXCLUSIVELY used for searches.
}