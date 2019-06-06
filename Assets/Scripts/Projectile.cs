using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;
    // Use this for initialization

    void Start()
    {

        if (speed == 0)
        {
            speed = 7.0f;
            Debug.Log("speed was not set. Defaulting to " + speed);
        }

        if (lifeTime <= 0)
        {
            lifeTime = 2.0f;
            Debug.Log("lifeTime was not set. Defaulting to " + lifeTime);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

         Destroy(gameObject, lifeTime); 

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

}
