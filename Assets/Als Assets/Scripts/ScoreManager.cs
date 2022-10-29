using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable;
using Unity.VisualScripting;
using System.Linq;

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
            //Debug.Log("master trying to add " + scoreAmt + " score to player" + playerNum);
            photonView.RPC("AddToPlayerScoreRPC", RpcTarget.All, playerNum, scoreAmt);

            //since player number was created from PlayerList[index + 1], we need to subtract 1 
            //this is also not accurate since players leaving the game causes the newst joining player to inherit a new higher number than the rest
            if (PhotonNetwork.CountOfPlayers < playerNum)
            {
                Debug.Log("Player number out of range. There are " + PhotonNetwork.CountOfPlayers + " players, and player number is " + playerNum);
            }
            else if (PhotonNetwork.PlayerList[playerNum - 1].CustomProperties.ContainsKey("score"))
            {

                Debug.Log("attempting hash change for player score. player " + playerNum);

                Hashtable hash = new Hashtable();
                //hash.Add("SpawnPoint", spawnArray[i]);

                int oldScore = (int)PhotonNetwork.PlayerList[playerNum].CustomProperties["score"];

                hash.Add("Score", oldScore + 1);
                PhotonNetwork.PlayerList[playerNum].SetCustomProperties(hash);
            }
            
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
