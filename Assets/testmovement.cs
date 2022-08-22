using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class testmovement : MonoBehaviour
{
    public FixedJoystick joy;
    public ThirdPersonUserControl controller;
    private void Update()
    {
        controller.horizontal = joy.Horizontal;
        controller.vertical = joy.Vertical;
    }
}
