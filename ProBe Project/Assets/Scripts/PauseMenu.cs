using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float quitTimer = 5f;
    private float currentTimerValue = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            currentTimerValue -= Time.fixedDeltaTime;
            Debug.Log(currentTimerValue);
            if (quitTimer <= 0f)
                Application.Quit();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
            currentTimerValue = quitTimer;
    }
}
