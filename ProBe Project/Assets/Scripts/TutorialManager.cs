using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    /// <summary> Array of all tutorial objects in a scene. </summary>
    [SerializeField] private GameObject[] tutorialObjects = new GameObject[0];
    /// <summary> Current index of the tutorial objects. </summary>
    private int tutorialIndex = 0;
    /// <summary> Current index of the tutorial objects. </summary>
    public int TutorialIndex
    {
        get
        {
            return tutorialIndex;
        }
    }
    /// <summary> Timer value for specific tutorial objects.</summary>
    [Range(10f, 60f)] [SerializeField] private float tutorialTimer = 5;
    /// <summary> Active timer for the tutorialTimer.</summary>
    private float timer;

    void Start()
    {
        for (int index = 0; index < tutorialObjects.Length; index++)
        {
            tutorialObjects[index].SetActive(false);
        }
        StartTimer();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            if (tutorialIndex < tutorialObjects.Length && !tutorialObjects[tutorialIndex].activeSelf)
            {
                SetTutorialObjectActive();
            }
        }
    }

    /// <summary>
    /// Resets the timer value to the tutorialTimer value.
    /// </summary>
    private void StartTimer()
    {
        timer = tutorialTimer;
    }

    /// <summary>
    /// Activates the current tutoral object.
    /// </summary>
    public void SetTutorialObjectActive()
    {
        tutorialObjects[tutorialIndex].SetActive(true);
    }

    /// <summary>
    /// Deactives the current tutorial object.
    /// </summary>
    public void SetTutorialObjectDeactive()
    {
        tutorialObjects[tutorialIndex].SetActive(false);
    }

    /// <summary>
    /// Deactives the current tutorial object before activating the next one.
    /// </summary>
    public void NextTutorialObject()
    {
        if (tutorialObjects[tutorialIndex].activeSelf)
            tutorialObjects[tutorialIndex].SetActive(false);
        tutorialIndex++;
        if (tutorialIndex < tutorialObjects.Length)
            tutorialObjects[tutorialIndex].SetActive(true);
    }
}