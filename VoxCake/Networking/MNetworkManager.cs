using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace VoxCake.Networking
{
    public class MNetworkManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            ConnectToMaster();
            //JoinRandomRoom();
        }

        void ConnectToMaster()
        {
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting to master");
        }

        void JoinRandomRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.LogError("You are not connected to master!");
            }
            Debug.Log("Joining a random room");
        }

        void LeftRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Debug.LogError(cause);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            SceneManager.LoadScene(0);
            Debug.Log("You are left the room");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.LogError(message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.LogError(message);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("You are connected to master");
            JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("You are joined the room");
            SceneManager.LoadScene(1);
        }
    }
}

