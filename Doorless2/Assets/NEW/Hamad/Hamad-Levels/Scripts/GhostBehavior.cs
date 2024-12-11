using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{

    [SerializeField] private float timerTillStart;
    [SerializeField] private GameObject[] players;
    [SerializeField] private float speed;
    [SerializeField] private float timer;
    private float timerStore;

    private GameObject targetPlayer;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        timerStore = timer;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerTillStart > 0f)
        {

            timerTillStart -= Time.deltaTime;
        }
        else
        {

            if (targetPlayer == null) timer = 0f;

            if (timer <= 0)
            {
                i = Random.Range(0, players.Length);
                targetPlayer = players[i];

                
                //RandomChase();
                timer = timerStore;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, speed * Time.deltaTime);

            timer -= Time.deltaTime;

        }

    }

    //private IEnumerator RandomChase()
    //{
        
    //    yield return new WaitForSeconds(timerStore);
    //}

    
}
