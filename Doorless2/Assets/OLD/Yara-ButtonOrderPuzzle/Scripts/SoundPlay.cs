using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private Camera camerat;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The start function is called");
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        camerat.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray1 = camerat.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray1, out hit))
            {
                string clickedNameTag = hit.transform.gameObject.tag;

                switch (clickedNameTag)
                {
                    case "Sound1":
                    audioSource.pitch = 0.5f;
                    break;
                    case "Sound2":
                    audioSource.pitch = 1f;
                    break;
                    case "Sound3":
                    audioSource.pitch = 1.5f;
                    break;
                    case "Sound4":
                    audioSource.pitch = 2f;
                    break;
                    default:
                    audioSource.pitch = 2.5f;
                    break;
                }
                audioSource.Play();
                
            }
        }
    }
}
