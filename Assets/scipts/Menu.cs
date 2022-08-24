using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject WPanel, PlayerSelectionPanel, PlayerObjects, player;
    public static int sceneNum;
    public GameObject[] players;
    public static int PlayerNum;
    public int playerselectiontemp;
    public string[] roomnames;
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        selectcharector();

    }

    // Update is called once per frame
    //public override void OnPlayerEnteredRoom()
    //{
    //    if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
    //    {
    //        this.GetComponent<PhotonView>().RPC("GameScene", RpcTarget.All);
    //    }
    //}
    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    //if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
    //    //{
    //    //    this.GetComponent<PhotonView>().RPC("GameScene", RpcTarget.All);
    //    //}
    //    SceneManager.LoadScene(sceneNum);
    //}



    [PunRPC]
    void GameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void connecttomaster()
    {
        Debug.Log(" Not connected to master so try to connect to master ...... wait for connected debug line ");
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    public override void OnCreatedRoom()
    {
        Debug.Log("room created " + PhotonNetwork.CurrentRoom.Name);
    }
    public int roomname;
    public void CreateRoomFun(int x)
    {
      //  roomname = Random.Range(10000, 99999);
    

        PhotonNetwork.CreateRoom(roomnames[x]);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("room joined " + PhotonNetwork.CurrentRoom.Name);
        WPanel.SetActive(true);
        //   PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
        if (PhotonNetwork.IsMasterClient)
            SceneManager.LoadSceneAsync(sceneNum);
        else
            WPanel.SetActive(true);
        //PhotonNetwork.LoadLevel()
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {


        Debug.Log("   OnJoinRandomFailed         " + returnCode + "  " + message);

        if (returnCode == 32760)
        {
            CreateRoomFun(sceneNum-1);
        }
    }

    public void JoinRoomFun(int x)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
      
        PhotonNetwork.JoinOrCreateRoom(roomnames[x-1], roomOptions, TypedLobby.Default);

        sceneNum = x;
       
    }


    // void up

    public void PlayerSelectionFun()
    {
        PlayerNum = playerselectiontemp+1;
        PlayerSelectionPanel.SetActive(false);
        PlayerObjects.SetActive(false);
    }

    public void selectcharector()
    {
        for(int i=0;i<6;i++)
        {
            if(i==playerselectiontemp)
            {
                players[i].SetActive(true);
            }
            else
            {
                players[i].SetActive(false);
            }
        }
    }

    public void increase()
    {
        playerselectiontemp++;
        if(playerselectiontemp>5)
        {
            playerselectiontemp = 0;
        }
        selectcharector();
    }
    public void decrese()
    {
        playerselectiontemp--;
        if (playerselectiontemp <0)
        {
            playerselectiontemp = 5;
        }
        selectcharector();
    }
    public void select()
    {
    }
 
}

