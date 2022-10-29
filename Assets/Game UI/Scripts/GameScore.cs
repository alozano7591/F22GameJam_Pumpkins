using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    private int score = 0;
    public TMP_Text txt;
    
    public void SetText()
    {
        score++;
        //TextMeshPro txt = transform.Find("GameScore1").GetComponent<TextMeshPro>();
        txt.text = score.ToString();
    }

    public void AddScore(int amt)
    {
        score++;
        txt.text = score.ToString();
    }

}
