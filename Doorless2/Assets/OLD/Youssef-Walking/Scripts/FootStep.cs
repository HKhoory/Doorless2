using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public GameObject footStepPrefab;

    void Start()
    {
        footStepPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            ActiveSFX();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActiveSFX();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ActiveSFX();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ActiveSFX();
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            DeActiveSFX();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            DeActiveSFX();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            DeActiveSFX();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            DeActiveSFX();
        }
    }

    void ActiveSFX()
    {
        footStepPrefab.SetActive(true);
    }

    void DeActiveSFX()
    {
        footStepPrefab.SetActive(false);
    }

}
