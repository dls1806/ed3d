using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct InputData
{
    public float hMovement;
    public float vMovement;
    
    public float verticalMouse;
    public float horizontalMouse;
    
    public bool dash;
    public bool jump;

    public void getInput()
    {
        hMovement = Input.GetAxis("Horizontal");
        vMovement = Input.GetAxis("Vertical");

        verticalMouse = Input.GetAxis("Mouse Y");
        horizontalMouse = Input.GetAxis("Mouse X");

        dash = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetButtonDown("Jump");
    }
}
