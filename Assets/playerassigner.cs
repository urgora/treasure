using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using StarterAssets;
public class playerassigner : MonoBehaviour
{

    public GameObject Canvas, MsgCanvas;
    public Text MsgText, Msgreceiver;
  
    public InputField IF;
    PhotonView pv;
    GameObject ourplayer;
    public Toggle voicetoggle;
    GameManage gm;
    public Image mic;
    public Sprite micon, micoff;
    ThirdPersonController tr;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        tr = GetComponent<ThirdPersonController>();
        if (pv.IsMine)
        {
           // pv.RPC("selectedcharector", RpcTarget.AllBuffered, Menu.PlayerNum - 1);
            gm = FindObjectOfType<GameManage>();
            playercanspeak();
        }
        else
        {
            Canvas.SetActive(false);
            tr.enabled = false;
        }
    }
    [PunRPC]
    void selectedcharector(int x)
    {
        instantialtecharecter(x);
    }

    void instantialtecharecter(int x)
    {
      //  ourplayer = PlayersObj[x];
        ourplayer.SetActive(true);
        ourplayer.transform.parent = transform;
      //  an = ourplayer.GetComponent<Animator>();
    }


    public void SentMsgFun()
    {
        pv.RPC("SentMsg", RpcTarget.All, MsgText.text);
        MsgText.text = "";
    }

    [PunRPC]
    void SentMsg(string s)
    {
        MsgCanvas.SetActive(true);
        Msgreceiver.text = s;
        IF.text = "";
        Invoke("DisableMsg", 5);
    }
    void DisableMsg()
    {
        MsgCanvas.SetActive(false);
    }
    public void playercanspeak()
    {
        if (voicetoggle.isOn)
        {
            gm.cantalk();
            mic.sprite = micon;
        }
        else
        {
            gm.cannottalk();
            mic.sprite = micoff;
        }
    }
}
