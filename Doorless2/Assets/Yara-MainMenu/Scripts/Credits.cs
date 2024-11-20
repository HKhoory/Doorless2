using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Button BackButton;
    // Start is called before the first frame update
    void Start()
    {
        BackButton.onClick.AddListener(BackOnClick);
    }

    void BackOnClick() {
        Debug.Log("BackButton is pressed");
        SceneManager.LoadScene("MainMenu");
    }
}
