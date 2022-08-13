using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class animationtransfer : MonoBehaviour
{
   public PhotonView pv;
   public   Animator anim;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }



    public void animationsync(float x)
    {
        pv.RPC("syncanim", RpcTarget.All, x);
    }
    [PunRPC]
    void syncanim(float x)
    {
        anim.SetFloat("Blend", x);
    }

}
