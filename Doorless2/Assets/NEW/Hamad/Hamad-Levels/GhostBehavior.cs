using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{

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
        


        if (timer <= 0)
        {
            i = Random.Range(0, players.Length);
            //RandomChase();
            timer = timerStore;
        }
        transform.position = Vector3.MoveTowards(transform.position, players[i].transform.position, speed * Time.deltaTime);

        timer -= Time.deltaTime;


    }

    //private IEnumerator RandomChase()
    //{
        
    //    yield return new WaitForSeconds(timerStore);
    //}

    
}
