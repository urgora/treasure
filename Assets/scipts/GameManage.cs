using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
public class GameManage :  MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject Pname;
    public GameObject canvas;
    public GameObject voice;
    public List<Transform> spawnposition;
    void Start()
    {
        Invoke("SpawnPlayer", .5f);
    
    }
    void SpawnPlayer()
    {
        Invoke("disableloading", 1);

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
    public void disableloading()
    {
        canvas.SetActive(false);
     //   Camera.main.gameObject.SetActive(false);
    }
    public void home()
    {
       // Destroy(PhotonVoiceNetwork.Instance);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    public Recorder voicerecorder;



    public void cantalk()
    {
       // voicerecorder.TransmitEnabled = true;
        PhotonVoiceNetwork.Instance.PrimaryRecorder.TransmitEnabled = true;
    }
    public void cannottalk()
    {
        //voicerecorder.TransmitEnabled = false;
        PhotonVoiceNetwork.Instance.PrimaryRecorder.TransmitEnabled = false;
    }


}
