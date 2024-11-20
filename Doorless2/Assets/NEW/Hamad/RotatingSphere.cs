using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSphere : MonoBehaviour
{

    [SerializeField] private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);

    }
}
