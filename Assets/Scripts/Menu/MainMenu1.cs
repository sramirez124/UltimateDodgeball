using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponent = null;


    private bool isConnecting = false;

    private const string GameVersion = "0.1";
    private const int maxPlayers = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindOpponent()
    {
        isConnecting = true;

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();

        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        if(isConnecting)
        {

            PhotonNetwork.JoinRandomRoom();
        }

        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected due to: {cause}");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No one is wating...creating new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != maxPlayers)
        {
            Debug.Log("Waiting on players...");

        }
        else
        {
            Debug.Log("Game ready.");

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("TestScene");

        }
    }


}
