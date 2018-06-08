using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject enemyLaser;
    public float health = 150f;
    public float shotsPerSecond = 0.5f;
    public float Lspeed = 5f;
    public int scoreValue = 150;
    public AudioClip eFiresound;
    public AudioClip deathsound;


    public ScoreKeeper scKeeper;
    private void Start()
    {
       scKeeper= GameObject.Find("scoreKeeper").GetComponent<ScoreKeeper>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health<=0)
            {
                Die();

            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
        scKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathsound, transform.position);

    }

    private void Update()
    {
        float probabilty = Time.deltaTime * shotsPerSecond;
        if (Random.value < probabilty)
        {
            Fire();
        }
       

    }
   
    public void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject Elaser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        Elaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -Lspeed, 0);
        AudioSource.PlayClipAtPoint(eFiresound, transform.position);
    }

}
