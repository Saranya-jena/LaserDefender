using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyPrefab;
    public float width = 10f;
    public float height = 5f;
    private bool moveRight = true;
    public float speed =0.5f;
    float xmin;
    float xmax;
    public float spawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
        float CameraToObjectDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, CameraToObjectDistance));
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, CameraToObjectDistance));
        xmax = rightMost.x;
        xmin = leftMost.x;
        SpawnUntilFull();
    }
    public void EnemySpawn()
    {
        
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;// enemy's transform is set to the transform of whatever this script is attached to i.e EnemyFormation
                                           // to create the enemy clone inside the EnemyForm gameObject
        }
    }


    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(EnemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;// enemy's transform is set to the transform of whatever this script is attached to i.e EnemyFormation
                                                  // to create the enemy clone inside the EnemyForm gameObject
        }
        if (NextFreePosition())
        Invoke("SpawnUntilFull", spawnDelay);//invokes a particular function after a time delay
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
    // Update is called once per frame
    void Update() {
        if (moveRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        { transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
         }
        float leftEdge = transform.position.x - (0.5f * width);
        float rightEdge = transform.position.x + (0.5f * width);
        if (leftEdge < xmin)
        {
            moveRight =true;
        }
        else if (rightEdge > xmax)
            moveRight = false;
        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }
    
bool AllMembersDead()
{
    foreach (Transform childPositionGameObject in transform)
    {
        if (childPositionGameObject.childCount > 0)
            return false;
    }
    return true;
}

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount ==0)
                return childPositionGameObject;
        }
        return null;
    }






}

