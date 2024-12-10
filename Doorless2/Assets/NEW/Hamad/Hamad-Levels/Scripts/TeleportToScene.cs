using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{

    [SerializeField] private int playerCount;
    [SerializeField] private int gameIndex;

    [SerializeField] private float timer;
    private float timerStore;

    // Start is called before the first frame update
    void Start()
    {
        playerCount = 0;
        timerStore = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount >= 1)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timerStore;
        }
        if (timer <= 0)
        {
            SceneManager.LoadScene(gameIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerCount++;
    }

    private void OnTriggerExit(Collider other)
    {

        playerCount--;

    }

}
