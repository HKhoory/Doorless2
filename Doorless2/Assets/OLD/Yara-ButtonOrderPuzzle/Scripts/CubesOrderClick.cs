using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesOrderClick : MonoBehaviour
{

    [SerializeField] private GameObject cam;
    [SerializeField] private Camera camerat;

    // Start is called before the first frame update
    void Start()
    {
        cam.GetComponent<Camera>();
        camerat.GetComponent<Camera>();
    }

    // Update is called once per frame
   

    public List<string> correctOrderTags = new List<string> { "Sound1", "Sound2", "Sound3","Sound4","Sound5" }; // Replace with your actual object names or tags
    private List<string> clickedOrderTags = new List<string>();

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
                clickedOrderTags.Add(clickedNameTag);

                // Check if the clicked order matches the correct order
                if (clickedOrderTags.Count == correctOrderTags.Count)
                {
                    bool correct = true;
                    for (int i = 0; i < correctOrderTags.Count; i++)
                    {
                        if (clickedOrderTags[i] != correctOrderTags[i])
                        {
                            correct = false;
                            break;
                        }
                    }

                    if (correct)
                    {
                        Debug.Log("Clicked in correct order!");
                    }
                    else
                    {
                        Debug.Log("Clicked in incorrect order.");
                    }

                    // Clear the list for the next try
                    clickedOrderTags.Clear();
                }
            }
        }
    }
}


