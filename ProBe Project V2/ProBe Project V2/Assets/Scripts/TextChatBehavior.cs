using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextChatBehavior : MonoBehaviour
{
    private Canvas textChatCanvas = null;
    [SerializeField] private Image chatBoxImage = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void SpawnTextBubble(string textMessage)
    {
        Instantiate(chatBoxImage, Vector3.zero, Quaternion.identity, textChatCanvas.transform);
    }
}