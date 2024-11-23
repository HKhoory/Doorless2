using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PS4ControllerTester : MonoBehaviour
{

    [SerializeField] private GameObject[] texts;
    [SerializeField] private TMP_Text[] outputs;

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
        
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            //outputs[0] = texts[0].GetComponent<TMP_Text>();
            outputs[0].text = "X = 1";
        }
        else
        {
            outputs[0].text = "X = 0";
        }

    }
}
