using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Credits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void BackToMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    public void ToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
