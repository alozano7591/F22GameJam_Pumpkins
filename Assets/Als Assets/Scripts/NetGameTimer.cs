using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetGameTimer : MonoBehaviour
{
    public static NetGameTimer _instance;

    ExitGames.Client.Photon.Hashtable CustomValue;

    [Header("Networked Game Timer")]
    public bool timerOn = false;
    public double startTime = -1;
    public bool gameEndTriggered = false;

    [SerializeField]
    private bool timeLimitOn = true;

    [Tooltip("Total Game time in seconds")]
    public double gameDuration = 120;

    [Tooltip("Amount of seconds left in game")]
    public double timeLeft;

    [Header("UI Variables")]
    public TextMeshProUGUI timertxt;
    public Image clockImage;
    public Color32 lowTimeColor;



    // Start is called before the first frame update
    void Start()
    {
        timeLeft = gameDuration;

        if(PhotonNetwork.IsMasterClient)
        {
            StartTimer(PhotonNetwork.Time);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!timerOn && !gameEndTriggered)
        {
            
            if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {

                if (PhotonNetwork.IsMasterClient)
                {
                    //StartTimer(PhotonNetwork.Time);
                }
                else
                {
                    Debug.Log("Client starting timer");
                    startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
                    timeLeft = gameDuration - (PhotonNetwork.Time - startTime);
                    timerOn = true;
                }
            }

        }

        if (timerOn && startTime > 0)
        {
            //float quarterTime = gameDuration / 4;
            //if (timeLeft == quarterTime)
            //{
            //    timertxt.color = Color.red;
            //}
            if (timeLeft > 0)
            {
                
                //timeLeft -= Time.deltaTime;
                timeLeft = gameDuration - (PhotonNetwork.Time - startTime);
                UpdateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                timerOn = false;
                gameEndTriggered = true;
            }
        }
    }

    /// <summary>
    /// Updates the in-game timer
    /// </summary>
    /// <param name="currentTime">How much time is left</param>
    void UpdateTimer(double currentTime)
    {
        double timePercentage = 1;
        timePercentage = timeLeft / gameDuration;
        clockImage.fillAmount = (float)timePercentage;

        if (currentTime <= (gameDuration / 4))
        {
            if (clockImage.color != Color.red)
            {
                clockImage.color = Color.red;
                timertxt.color = Color.red;
            }
        }
        currentTime += 1;

        double minutes = Mathf.FloorToInt((float)currentTime / 60);
        double seconds = Mathf.FloorToInt((float)currentTime % 60);

        timertxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


    public void StartTimer(double startingTime)
    {

        Debug.Log("Start Timer NetworkTimer called");

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            Debug.Log("NetworkTimer: Player Is Master");

            CustomValue = new ExitGames.Client.Photon.Hashtable();
            //startTime = PhotonNetwork.Time;

            startTime = startingTime;

            timerOn = true;
            CustomValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
        }
        else
        {
            startTime = startingTime;
            timerOn = true;
        }

    }


    public void StartTimer(double startingTime, float timeLimit)
    {

        //Debug.Log("Start Timer NetworkTimer called");

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            //Debug.Log("NetworkTimer: Player Is Master");

            CustomValue = new ExitGames.Client.Photon.Hashtable();
            //startTime = PhotonNetwork.Time;

            startTime = startingTime;
            gameDuration = timeLimit;

            timerOn = true;
            CustomValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
        }
        else
        {
            startTime = startingTime;
            timerOn = true;
        }

    }


}
