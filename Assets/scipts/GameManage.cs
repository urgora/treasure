using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class GameManage :  MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject Pname;
    public GameObject Camera;

    public List<Transform> spawnposition;
    void Start()
    {
        Invoke("SpawnPlayer", 2);
    }
    void SpawnPlayer()
    {
      //  Camera.SetActive(false);

        int x = Random.Range(0, spawnposition.Count);
        PhotonNetwork.Instantiate(Pname.name, spawnposition[x].position, spawnposition[x].rotation);
        //if (Menu.sceneNum==1)
        //PhotonNetwork.Instantiate(Pname.name,new Vector3(0,1,Random.Range(-30,30)),Quaternion.identity);
        //else if (Menu.sceneNum == 2)
        //    PhotonNetwork.Instantiate(Pname.name, new Vector3(0, 1, Random.Range(20, 50)), Quaternion.Euler(0,180,0));
        //else if (Menu.sceneNum == 3)
        //    PhotonNetwork.Instantiate(Pname.name, new Vector3(-18, 1, Random.Range(10.5f, 16)), Quaternion.identity);
        //else if (Menu.sceneNum == 4)
        //    PhotonNetwork.Instantiate(Pname.name, new Vector3(Random.Range(20, 50),1,-10), Quaternion.Euler(0,90,0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void home()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
