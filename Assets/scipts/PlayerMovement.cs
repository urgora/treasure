using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement  : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    // public CharacterController cc;
    public Rigidbody rb;
    public float speed,camspeed;
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
    private void Awake()
    {
       // PlayersObj[Menu.PlayerNum - 1].SetActive(true);
    }
    
    void Start()
    {
        camerachange();
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
            horizontal =  speed * Time.deltaTime* buttonhorizontal;
            vertical =  speed * Time.deltaTime* buttonvertical;

            //   cc.Move(transform.forward * forBack);
            transform.Rotate(Vector3.up * horizontal*20);
            transform.position += transform.forward * vertical;
          //  cc.Move(transform.forward * vertical);
            //  PlayersObj[Menu.PlayerNum - 1].GetComponent<Animator>().SetFloat("Blend", buttonvertical);
          an.SetFloat("Blend", buttonvertical);
            // if(buttonvertical!=0)
            // PlayersObj[Menu.PlayerNum - 1].GetComponent<animationtransfer>().animationsync(buttonvertical);


            
            if(increasecamera)
            {
                camangle += camspeed * Time.deltaTime;
                camangle = Mathf.Clamp(camangle, -30, 30);
                camrotateobject.transform.localRotation = Quaternion.Euler(camangle, camrotateobject.transform.rotation.y, camrotateobject.transform.rotation.z);
            }
            if (decreasecamer)
            {
                camangle -= camspeed * Time.deltaTime;
               camangle= Mathf.Clamp(camangle, -30, 30);
                camrotateobject.transform.localRotation = Quaternion.Euler(camangle, camrotateobject.transform.rotation.y, camrotateobject.transform.rotation.z);
            }
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
        buttonvertical = 2;
    }
    public void sprintoff()
    {
        buttonvertical = 0;
    }
    #endregion



}
