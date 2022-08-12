using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement  : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public CharacterController cc;

    public float speed;
    public GameObject anim;
    public GameObject SelfCam, Canvas, SentButton,MsgCanvas,Player;
        public Text MsgText, Msgreceiver;
    public GameObject[] PlayersObj;
    public InputField IF;
    PhotonView pv;
    GameObject ourplayer;
    private void Awake()
    {
       // PlayersObj[Menu.PlayerNum - 1].SetActive(true);
    }
    
    void Start()
    {

        pv = GetComponent<PhotonView>();

        if(photonView.IsMine)
        {
            pv.RPC("selectedcharector", RpcTarget.AllBuffered, Menu.PlayerNum-1);
        }
    }
    [PunRPC]
    void selectedcharector(int x)
    {
        instantialtecharecter(x);
    }

    void instantialtecharecter(int x)
    {
        //  ourplayer = Instantiate(PlayersObj[x], transform.position, transform.rotation);
        ourplayer = PlayersObj[x];
        ourplayer.SetActive(true);
        ourplayer.transform.parent = transform;
    }


    public void SentMsgFun()
    {
        pv.RPC("SentMsg", RpcTarget.All,MsgText.text);
        MsgText.text = "";
    }

    [PunRPC] 
    void SentMsg(string s)
    {
        MsgCanvas.SetActive(true);
        Msgreceiver.text = s;
        IF.text = "";
        Invoke("DisableMsg", 3);
    }

    void DisableMsg()
    {
        MsgCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            horizontal =  speed * Time.deltaTime* buttonhorizontal;
            vertical =  speed * Time.deltaTime* buttonvertical;

            //   cc.Move(transform.forward * forBack);
            transform.Rotate(Vector3.up * horizontal*20);
            cc.Move(transform.forward * vertical);
            //  PlayersObj[Menu.PlayerNum - 1].GetComponent<Animator>().SetFloat("Blend", buttonvertical);
            ourplayer.GetComponent<Animator>().SetFloat("Blend", buttonvertical);
            // if(buttonvertical!=0)
            // PlayersObj[Menu.PlayerNum - 1].GetComponent<animationtransfer>().animationsync(buttonvertical);

        }
        else
        {
            Canvas.SetActive(false);
            SelfCam.SetActive(false);
        }
    }





    #region mobileinput
    public float horizontal;
    public float vertical;
    public float buttonhorizontal, buttonvertical;
    public void buttonhorizontaloon()
    {
        buttonhorizontal = -1;
      //  Player.GetComponent<Animator>().blen
    }
    public void buttonhorizontaloff()
    {
        buttonhorizontal = 0;
    }
    public void buttonhorizontalnegativeon()
    {
        buttonhorizontal = 1;
    }
    public void buttonhorizontalnegativeoff()
    {
        buttonhorizontal = 0;
    }
    public void buttonvertivaloon()
    {
        buttonvertical = 1;
    }
    public void buttonverticaloff()
    {
        buttonvertical = 0;
    }
    public void buttonverticalnegativeon()
    {
        buttonvertical = -1;
    }
    public void buttonverticalnegativeoff()
    {
        buttonvertical = 0;
    }
    #endregion



}
