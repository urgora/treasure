using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Menu  : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject WPanel,PlayerSelectionPanel,PlayerObjects,player;
    public static int sceneNum;

    public static int PlayerNum;
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
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
    public void CreateRoomFun()
    {
        roomname = Random.Range(10000, 99999);

            PhotonNetwork.CreateRoom(roomname.ToString());
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("room joined " + PhotonNetwork.CurrentRoom.Name);
        WPanel.SetActive(true);
     //   PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
     if(PhotonNetwork.IsMasterClient)
        SceneManager.LoadScene(sceneNum);
     else
            WPanel.SetActive(true);
        //PhotonNetwork.LoadLevel()
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {


        Debug.Log("   OnJoinRandomFailed         " + returnCode + "  " + message);

        if (returnCode == 32760)
        {
            CreateRoomFun();
        }
    }

    public void JoinRoomFun(int x)
    {
        PhotonNetwork.JoinRandomRoom();
        sceneNum = x;
    }


    // void up

    public void PlayerSelectionFun(int x)
    {
        PlayerNum = x;
        PlayerSelectionPanel.SetActive(false);
        PlayerObjects.SetActive(false);
    }

}
