using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{
    //This Scripit is to be Attached to the player

    public int totalCoins = 5;
    private int collectedCoins = 0;

    public int mainMenuIndex;

    private void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            collectedCoins++;

            if(collectedCoins >= totalCoins) 
            {
                YouWon();
            }

            Destroy(other.gameObject);
        }
    }
    private void YouWon()
    {
        Debug.Log("You WOn");

        SceneManager.LoadScene(mainMenuIndex);

    }
}

