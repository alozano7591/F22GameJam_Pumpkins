using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    Timer gameTimer = new Timer();
    public float gameDuration;
    private float timeLeft;
    public bool timerOn = false;
    public TextMeshProUGUI timertxt;
    public Image clockImage;
    public Color32 lowTimeColor;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = gameDuration;
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            //float quarterTime = gameDuration / 4;
            //if (timeLeft == quarterTime)
            //{
            //    timertxt.color = Color.red;
            //}
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    /// <summary>
    /// Updates the in-game timer
    /// </summary>
    /// <param name="currentTime">How much time is left</param>
    void UpdateTimer(float currentTime)
    {
        float timePercentage = 1;
        timePercentage = timeLeft / gameDuration;
        clockImage.fillAmount = timePercentage;

        if (currentTime <= (gameDuration / 4))
        {
            if (clockImage.color != Color.red)
            {
                clockImage.color = Color.red;
                timertxt.color = Color.red; 
            }
        }
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timertxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
