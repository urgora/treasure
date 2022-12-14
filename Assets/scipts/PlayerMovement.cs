using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerMovement  : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    // public CharacterController cc;
    public Rigidbody rb;
    public float movespeed,camspeed,turnspeed;
    public GameObject anim;
    public GameObject SelfCam, Canvas, SentButton,MsgCanvas,Player,camrotateobject,camparent;
    public Text MsgText, Msgreceiver,camtester;
    public GameObject[] PlayersObj;
    public InputField IF;
    PhotonView pv;
    GameObject ourplayer;
    public float jumspeed,jumptime;
    Animator an;
    public bool jumped,increasecamera,decreasecamer,changecamparent;
    public Toggle voicetoggle;
    GameManage gm;



    public FixedJoystick joy;
    public FixedTouchField camerajoy;
    public ThirdPersonUserControl controller;
    public FixedButton jumpbt;
    public float x;
    public float camerangle;
    public float cameranglespeed = 2f;
    public GameObject maincamera;


    private void Awake()
    {
       // PlayersObj[Menu.PlayerNum - 1].SetActive(true);
    }
    
    void Start()
    {
       // camerachange();
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        if(photonView.IsMine)
        {
            pv.RPC("selectedcharector", RpcTarget.AllBuffered, Menu.PlayerNum-1);
            maincamera = Camera.main.gameObject;
            controller = GetComponent<ThirdPersonUserControl>();
            gm = FindObjectOfType<GameManage>();
            playercanspeak();
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
        an = ourplayer.GetComponent<Animator>();
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
        Invoke("DisableMsg", 5);
    }

    void DisableMsg()
    {
        MsgCanvas.SetActive(false);
    }
    // Update is called once per frame
    float camangle;
    void Update()
    {
        if (pv.IsMine)
        {
            //  horizontal =  turnspeed * Time.deltaTime* buttonhorizontal;
            //  vertical =  movespeed*Time.deltaTime* buttonvertical;

            //  //   cc.Move(transform.forward * forBack);
            //  transform.Rotate(Vector3.up * horizontal*20);
            //  Vector3 positiontomove = transform.forward * vertical;
            // // rb.MovePosition(transform.position+positiontomove * Time.deltaTime*movespeed);

            //  transform.position += transform.forward * vertical;
            ////  cc.Move(transform.forward * vertical);
            //  //  PlayersObj[Menu.PlayerNum - 1].GetComponent<Animator>().SetFloat("Blend", buttonvertical);
            //an.SetFloat("Blend", buttonvertical);

            //  // if(buttonvertical!=0)
            //  // PlayersObj[Menu.PlayerNum - 1].GetComponent<animationtransfer>().animationsync(buttonvertical);



            //  if(increasecamera)
            //  {
            //      camangle += camspeed * Time.deltaTime;
            //      camangle = Mathf.Clamp(camangle, -30, 30);
            //      camrotateobject.transform.localRotation = Quaternion.Euler(camangle, camrotateobject.transform.rotation.y, camrotateobject.transform.rotation.z);
            //  }
            //  if (decreasecamer)
            //  {
            //      camangle -= camspeed * Time.deltaTime;
            //     camangle= Mathf.Clamp(camangle, -30, 30);
            //      camrotateobject.transform.localRotation = Quaternion.Euler(camangle, camrotateobject.transform.rotation.y, camrotateobject.transform.rotation.z);
            //  }

            movement();

        }
        else
        {
            Canvas.SetActive(false);
            SelfCam.SetActive(false);
        }
    }
    public void movement()
    {
        controller.m_Jump = jumpbt.Pressed;
        controller.horizontal = joy.Horizontal;
        controller.vertical = joy.Vertical;
        Vector2 y = new Vector2(joy.Horizontal, joy.Vertical);
        x = y.magnitude;

        camerangle += camerajoy.TouchDist.x * cameranglespeed;

        maincamera.transform.position = transform.position + Quaternion.AngleAxis(camerangle, Vector3.up) * new Vector3(0, 3, 4);
        maincamera.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - maincamera.transform.position, Vector3.up);

        if (x > .1)
        {
            an.SetFloat("Blend", 1);
        }
        else
        {
            an.SetFloat("Blend", 0);
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
    public void camerup(bool x)
    {
        increasecamera = x;
    }
    public void camerdown(bool x)
    {
        decreasecamer = x;
    }



    public void jump()
    {
        if(!jumped)
        {
            rb.AddForce(Vector3.up * jumspeed, ForceMode.VelocityChange);
            an.SetTrigger("jump");
            jumped = true;
            Invoke("resertjump", jumptime);
        }
      
    }
    void resertjump()
    {
        jumped = false;
    }

    public void camerachange()
    {
        changecamparent = !changecamparent;
        if(changecamparent)
        {
            camrotateobject = SelfCam;
            camtester.text = "camera";
            camparent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            SelfCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
            camangle = 0;
        }
        else
        {
            camrotateobject = camparent;
            camtester.text = "player";
            camparent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            SelfCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
            camangle = 0;
        }


    }
    public void sprinton()
    {
        buttonvertical = buttonvertical+2;
    }
    public void sprintoff()
    {
        buttonvertical = buttonvertical -2;
    }
    #endregion

    public void playercanspeak()
    {
        if (voicetoggle.isOn)
        {
            gm.cantalk();
        }
        else
        {
            gm.cannottalk();
        }
    }

}
