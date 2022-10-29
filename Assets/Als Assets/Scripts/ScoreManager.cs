using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public List<GameScore> gameScores;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Adds score to a specific player, updating the score UI.
    /// </summary>
    /// <param name="playerNum"></param>
    /// <param name="scoreAmt"></param>
    public void AddToPlayerScore(int playerNum, int scoreAmt)
    {
        //player numbers start at 1, so we deduct a 1 for the index
        gameScores[playerNum - 1].AddScore(scoreAmt);


    }
    
}
