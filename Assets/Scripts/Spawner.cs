using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Transform[] spawnpoints;
    public float random;
    public GameObject Collectible1;
    public GameObject Collectible2;


    // Use this for initialization
    void Start () {

        foreach (var point in spawnpoints)
        {
            random = Mathf.Round(Random.Range(1, 3));
            if (random == 1)
            {
                GameObject temp = Instantiate(Collectible1, point.position, point.rotation);
            }
            else
            {
                GameObject temp = Instantiate(Collectible2, point.position, point.rotation);
            }
        }

    }

    void Update()
    {

    }

}





       

