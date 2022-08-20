using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
public class thirdpersoncontroller : MonoBehaviour
{
    public FixedJoystick joy;
    public FixedTouchField camerajoy;
    public ThirdPersonUserControl controller;
    public FixedButton jumpbt;
    public Animator anim;
    public float x;
    public float camerangle;
    public float cameranglespeed=2f;
    public GameObject maincamera;
    private void Start()
    {
        controller = GetComponent<ThirdPersonUserControl>();
    }

    private void Update()
    {
        controller.m_Jump = jumpbt.Pressed;
        controller.horizontal = joy.Horizontal;
        controller.vertical = joy.Vertical;
        Vector2 y = new Vector2(joy.Horizontal, joy.Vertical);
        x = y.magnitude;

        camerangle += camerajoy.TouchDist.x * cameranglespeed;

        maincamera.transform.position = transform.position + Quaternion.AngleAxis(camerangle,Vector3.up) * new Vector3(0, 3, 4);
        maincamera.transform.rotation = Quaternion.LookRotation(transform.position+Vector3.up*2f-maincamera.transform.position, Vector3.up);

        if(x>.1)
        {
            anim.SetFloat("Blend", 1);
        }
        else
        {
            anim.SetFloat("Blend", 0);
        }
    }
}
