using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {


    public GameObject player;
    public List<GameObject> enemyList;
    public List<GameObject> bulletList;
    public System.Random r;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
        this.r = new System.Random();
        
        
        enemyList = new List<GameObject>();
        bulletList = new List<GameObject>();

        InitGameObjects();
        InitPrefabs();
        
    }

    private void InitGameObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void InitPrefabs()
    {
        enemyPrefab = Resources.Load<GameObject>("EnemyPrefab");
        bulletPrefab = Resources.Load<GameObject>("BulletPrefab");
    }

    private void LoadEnemy()
    {
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab);
        enemyObj.transform.position = GameHelper.getRandomPos(r);

        var enemyController = enemyObj.GetComponent<EnemyScript>();
        enemyController.gameController = this;


        enemyList.Add(enemyObj);
    }

    private void LoadBullet(Vector3 pos, Vector3 dest)
    {
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab);
        var bulletScript = bulletObj.GetComponent<BulletScript>();
        bulletScript.Velocity = dest;
        bulletObj.transform.position = pos;
            


        bulletList.Add(bulletObj);
    }

	
	// Update is called once per frame
	void Update () {
	    if(r.Next(100) > 90)
        {
            LoadEnemy();
        }
	}

    public void Shoot(Vector3 pos, Vector3 dest)
    {
        LoadBullet(pos,dest);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void DestroyBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
    }
}
