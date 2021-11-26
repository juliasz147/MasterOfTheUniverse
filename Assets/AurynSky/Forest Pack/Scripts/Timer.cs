using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float seconds;
    private int secondsForText;
    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (seconds > 0)
        {
            seconds -= Time.fixedDeltaTime;
            secondsForText = (int)seconds;
            timerText.text = "Time: " + secondsForText.ToString() + " Seconds";
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
