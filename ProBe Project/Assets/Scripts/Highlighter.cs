using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class Highlighter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const string contentName = "Content";

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
        

        int index = FindCharacterClicked(content, eventData.position, eventData.enterEventCamera);
        Debug.Log(index);
        if (index > -1)
        {
            Debug.Log(content.textInfo.characterInfo[index]);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Returns the index of the characterInfo of the content text.
    private int FindCharacterClicked(TextMeshProUGUI content, Vector2 position, Camera eventCamera)
    {
        for (int index = 0; index < content.textInfo.characterCount; index ++)
        {
            // Gets the character info for that character
            TMP_CharacterInfo characterInfo = content.textInfo.characterInfo[index];

            // Finds the location of that character.
            Vector3 topLeft = content.rectTransform.TransformPoint(characterInfo.topLeft);
            Vector3 topRight = content.rectTransform.TransformPoint(characterInfo.topRight);
            Vector3 bottomRight = content.rectTransform.TransformPoint(new Vector3(characterInfo.topRight.x, characterInfo.bottomLeft.y, 0f));
            Vector3 bottomLeft = content.rectTransform.TransformPoint(new Vector3(characterInfo.bottomLeft.x, characterInfo.topRight.y, 0f));

            Debug.Log(position);
            // Compares to clicked position to the character bounds.
            if (topLeft.x < position.x && position.x < topRight.x)
            {
                if (bottomLeft.y < position.y && position.y < topLeft.y)
                    return index;
            }
        }
        return -1;
    }
}