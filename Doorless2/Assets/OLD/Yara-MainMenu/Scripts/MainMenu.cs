using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;
    public Button CreditsButton;
    public Button ExiteButton;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartOnClick);
        CreditsButton.onClick.AddListener(CreditsOnClick);
        ExiteButton.onClick.AddListener(ExiteOnClick);
    }

    void StartOnClick()
    {
        Debug.Log("The start Button Is Clicked");
    }
    void  CreditsOnClick()
    {
        
        SceneManager.LoadScene("Credits");
    }
    void ExiteOnClick()
    {
        // Quit the application
         Application.Quit();
         // Stop the Unity editor
          #if UNITY_EDITOR 
          UnityEditor.EditorApplication.isPlaying = false;
           #endif
    }
}
