using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;


namespace Com.ThePoopCrew.SmashingPumpkins
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Public Fields

        public static GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        #endregion

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                //Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
                //// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                //PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

                if (PlayerMovement.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

            if (PhotonNetwork.IsMasterClient)
            {
                //TellPlayersToSpawn();
                AssignPlayerSpawnHash(PhotonNetwork.LocalPlayer.ActorNumber);
            }

        }

        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        //assigns random spawn value to hash so that it will be stored over network
        //also assigned Color numbers
        public void AssignPlayerSpawnHash()
        {

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                Hashtable hash = new Hashtable();
                //hash.Add("SpawnPoint", spawnArray[i]);
                hash.Add("ColorNum", i);
                hash.Add("Score", i);
                PhotonNetwork.PlayerList[i].SetCustomProperties(hash);          //this line sets a unique spawn number for each player 
            }

        }

        /// <summary>
        /// Uses given actor number to assign hash to player on PlayerList
        /// </summary>
        /// <param name="playerID"></param>
        public void AssignPlayerSpawnHash(int playerID)
        {

            int photonIndex = 0;        //the index of the the player in the PhotonNetwork.PlayerList

            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                if (PhotonNetwork.PlayerList[i].ActorNumber == playerID)
                {
                    photonIndex = i;
                    break;
                }

            }

            Hashtable hash = new Hashtable();
            //hash.Add("SpawnPoint", spawnArray[i]);
            hash.Add("ColorNum", photonIndex);
            hash.Add("Score", 0);
            PhotonNetwork.PlayerList[photonIndex].SetCustomProperties(hash);          //this line sets a unique spawn number for each player 

        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }


        #endregion

        #region Photon Callbacks


        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting



            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                //LoadArena();

                AssignPlayerSpawnHash(other.ActorNumber);       //set up player hash (used for storying player values over the network)
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            //if (PhotonNetwork.IsMasterClient)
            //{
            //    Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            //    LoadArena();
            //}
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

            if(changedProps.ContainsKey("Score"))
            {
                Debug.Log("Player " + targetPlayer.ActorNumber + " now has a new score of " + targetPlayer.CustomProperties["Score"]);
            }

        }


        #endregion
    }
}