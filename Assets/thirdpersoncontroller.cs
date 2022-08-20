using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersoncontroller : MonoBehaviour
{
    public Rigidbody rb;
    public FixedJoystick joy;


    public float horizontal, vertical;
    public float movespeed;

    private void Update()
    {
        horizontal = joy.Horizontal * Time.deltaTime * movespeed;
        vertical = joy.Vertical * Time.deltaTime * movespeed;

        Vector3 position = vertical * Vector3.forward + horizontal * Vector3.right;

        transform.position +=position;
    }
}
