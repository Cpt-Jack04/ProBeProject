using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class Highlighter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const string contentName = "Content";
    private const string startHighlight = "<mark=#ffff00aa>";
    private const string endHighlight = "</mark>";

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Checks for to make sure that the click on object is the content text.
        RaycastResult downClick = eventData.pointerCurrentRaycast;
        TextMeshProUGUI content = downClick.gameObject.GetComponent<TextMeshProUGUI>();
        if (content == null || !content.gameObject.name.Equals(contentName))
            return;
        
        // Check to see if you clicked on a letter.
        int index = FindCharacterClicked(content, eventData.position, eventData.enterEventCamera);
        if (index > -1)
            content.text.Insert(index, startHighlight);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Checks for to make sure that the click on object is the content text.
        RaycastResult upClick = eventData.pointerCurrentRaycast;
        TextMeshProUGUI content = upClick.gameObject.GetComponent<TextMeshProUGUI>();
        if (content == null || !content.gameObject.name.Equals(contentName))
            return;

        // Check to see if you left go on a letter.
        int index = FindCharacterClicked(content, eventData.position, eventData.enterEventCamera);
        if (index > -1)
            content.text.Insert(index, endHighlight);
    }

    // Returns the index of the characterInfo of the content text.
    private int FindCharacterClicked(TextMeshProUGUI content, Vector2 position, Camera eventCamera)
    {
        Vector3 wordlPos = eventCamera.ScreenToWorldPoint(position);
        float buffer = .001f;

        for (int index = 0; index < content.textInfo.characterCount; index ++)
        {
            // Gets the character info for that character
            TMP_CharacterInfo characterInfo = content.textInfo.characterInfo[index];

            // Finds the location of that character.
            Vector3 topLeft = content.rectTransform.TransformPoint(characterInfo.topLeft);
            Vector3 topRight = content.rectTransform.TransformPoint(characterInfo.topRight);
            Vector3 bottomRight = content.rectTransform.TransformPoint(new Vector3(characterInfo.topRight.x, characterInfo.bottomLeft.y, 0f));
            Vector3 bottomLeft = content.rectTransform.TransformPoint(new Vector3(characterInfo.bottomLeft.x, characterInfo.topRight.y, 0f));

            // Compares to clicked position to the character bounds.
            if (((topLeft.x - buffer) < wordlPos.x) && (wordlPos.x < (topRight.x + buffer)))
            {
                if (((bottomRight.y - buffer) < wordlPos.y) && (wordlPos.y < (topRight.y + buffer)))
                    return index;
            }
        }
        return -1;
    }
}