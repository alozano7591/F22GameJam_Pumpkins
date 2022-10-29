using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
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
        //gameScores[playerNum - 1].AddScore(scoreAmt);

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log("master trying to add " + scoreAmt + " score to player" + playerNum);
            photonView.RPC("AddToPlayerScoreRPC", RpcTarget.All, playerNum, scoreAmt);
        }

    }

    [PunRPC]
    public void AddToPlayerScoreRPC(int playerNum, int scoreAmt)
    {
        Debug.Log("RPC AddToPlayerScoreRPC called");

        //player numbers start at 1, so we deduct a 1 for the index
        gameScores[playerNum - 1].AddScore(scoreAmt);


    }

}
