using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityStandardAssets.Characters.ThirdPerson;
public class thirdpersoncontroller : MonoBehaviour
{
    public FixedJoystick joy;
    public FixedTouchField camerajoy;
    public ThirdPersonUserControl controller;
    public  ThirdPersonCharacter forjump;
    public FixedButton jumpbt;
    public Animator anim;
    public float x;
    public float camerangle;
    public float cameranglespeed=2f;
    public GameObject maincamera;

    public GameObject  Canvas, SentButton, MsgCanvas, Player, camrotateobject, camparent;
    public Text MsgText, Msgreceiver, camtester;
    public GameObject[] PlayersObj;
    public InputField IF;
    PhotonView pv;
    GameObject ourplayer;
    public float jumspeed, jumptime;
    Animator an;
    public bool jumped, increasecamera, decreasecamer, changecamparent;


    private void Start()
    {
        pv = GetComponent<PhotonView>();
        
        if (pv.IsMine)
        {
            pv.RPC("selectedcharector", RpcTarget.AllBuffered, Menu.PlayerNum - 1);
            maincamera = Camera.main.gameObject;
            controller = GetComponent<ThirdPersonUserControl>();
        }
        else
        {
            Canvas.SetActive(false);
        }
    }
    [PunRPC]
    void selectedcharector(int x)
    {
        instantialtecharecter(x);
    }

    void instantialtecharecter(int x)
    {
        ourplayer = PlayersObj[x];
        ourplayer.SetActive(true);
        ourplayer.transform.parent = transform;
        an = ourplayer.GetComponent<Animator>();
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
    private void Update()
    {
        if (pv.IsMine)
        {
            controller.m_Jump = jumpbt.Pressed;
            if(jumpbt.Pressed&&forjump.m_IsGrounded)
            {
                an.SetTrigger("jump");
            }
            else
            {
                an.ResetTrigger("jump");
            }
            controller.horizontal = joy.Horizontal;
            controller.vertical = joy.Vertical;
            Vector2 y = new Vector2(joy.Horizontal, joy.Vertical);
            x = y.magnitude;

            camerangle += camerajoy.TouchDist.x * cameranglespeed;

            maincamera.transform.position = transform.position + Quaternion.AngleAxis(camerangle, Vector3.up) * new Vector3(0, 2, 3);
            maincamera.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 1.4f - maincamera.transform.position, Vector3.up);

            if (x > .1)
            {
                an.SetFloat("Blend", 1);
            }
            else
            {
                an.SetFloat("Blend", 0);
            }
        }
  
    }
}
