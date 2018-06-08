using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 50f;
    public GameObject laserprefab;
    float Xmin ;
    public float Health = 250f;
    float Xmax;
    public float firingRate = 0.2f;
    float padding = 0.8f;
    public float projectSpeed = 5f;
    public AudioClip firesound;
	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));//returns a worldpoint see video lecture 4 from laser defender 
        Vector3 rightMost= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Xmin = leftMost.x+padding;
        Xmax = rightMost.x-padding;
    }
	void Fire()
    {
        Vector3 offset = transform.position + new Vector3(0, 1, 0);
        GameObject Laser = Instantiate(laserprefab, offset, Quaternion.identity) as GameObject;
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectSpeed, 0);
        AudioSource.PlayClipAtPoint(firesound, transform.position);

    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, Xmin, Xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        enemyProjectile missile = collision.gameObject.GetComponent<enemyProjectile>();
        if (missile)
        {
            Health -= missile.GetDamage();
            missile.Hit();
            if (Health <= 0)
            {
                die();
            }
        }
    }
    void die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("win");
        Destroy(gameObject);
    }

}
