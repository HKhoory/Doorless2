using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerInput : MonoBehaviour
{

    public static MainPlayerInput instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        //do the thing with instance so it doesn't copy itself
        DontDestroyOnLoad(this);

    }

    
    void Update()
    {
        
    }
}
