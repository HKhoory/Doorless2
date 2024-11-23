using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PS4ControllerTester : MonoBehaviour
{

    [SerializeField] private TMP_Text outputs;

    //how about have them as static ints and another script prints the ints instead
    
    void Start()
    {
        //for (int i = 0; i < texts.Length; i++)
        //{
            //outputs[i] = GetComponent<TMP_Text>(texts[i]);
        //}
    }


    void Update()
    {

        outputs.text = "X = " + Input.GetKey(KeyCode.Joystick1Button0) + "<br>"
        + "Square = " + Input.GetKey(KeyCode.Joystick1Button1) + "<br>"
        + "O = " + Input.GetKey(KeyCode.Joystick1Button2) + "<br>"
        + "Triangle = " + Input.GetKey(KeyCode.Joystick1Button3) + "<br>"
        + "L1 = " + Input.GetKey(KeyCode.Joystick1Button4) + "<br>"
        + "L2 = " + Input.GetKey(KeyCode.Joystick1Button5) + "<br>"
        + "R1 = " + Input.GetKey(KeyCode.Joystick1Button6) + "<br>"
        + "R2 = " + Input.GetKey(KeyCode.Joystick1Button7) + "<br>"
        + "D-Up = " + Input.GetKey(KeyCode.Joystick1Button8) + "<br>"
        + "D-Down = " + Input.GetKey(KeyCode.Joystick1Button9) + "<br>"
        + "D-Left = " + Input.GetKey(KeyCode.Joystick1Button10) + "<br>"
        + "D-Right = " + Input.GetKey(KeyCode.Joystick1Button11) + "<br>"
        + " = " + Input.GetKey(KeyCode.Joystick1Button12) + "<br>";
        Input.GetAxisRaw("Horizontal");
    }
}
