using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {


    public GameObject player;
    public PlayerScript playerController;

    public List<GameObject> enemyList;
    public List<GameObject> bulletList;
    public GameObject harpoon;

    public System.Random r;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject harpoonPrefab;

    public Text EnemyListText;

    public Text DebugText;

	// Use this for initialization
	void Start () {
        this.r = new System.Random();
        
        
        enemyList = new List<GameObject>();
        bulletList = new List<GameObject>();

        InitPrefabs();
        InitGameObjects();
       
        
    }

    private void InitGameObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerScript>();

        DebugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        EnemyListText = GameObject.FindGameObjectWithTag("EnemyList").GetComponent<Text>();
        LoadHarpoon(player.transform.position);
    }

    private void InitPrefabs()
    {
        enemyPrefab = Resources.Load<GameObject>("EnemyPrefab");
        bulletPrefab = Resources.Load<GameObject>("BulletPrefab");
        harpoonPrefab = Resources.Load<GameObject>("HarpoonPrefab");
    }

    private void LoadEnemy()
    {
        if (enemyList.Count < GameConfig.maxEnemies)
        {
            GameObject enemyObj = (GameObject)Instantiate(enemyPrefab);
            enemyObj.transform.position = GameHelper.getRandomPos(r);

            var enemyController = enemyObj.GetComponent<EnemyScript>();
            enemyController.gameController = this;


            enemyList.Add(enemyObj);
        }
    }

    public void LoadTurnedEnemy(Vector3 pos,  Vector3 dest, EnemyType enemyType)
    {
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab);
        enemyObj.transform.position = pos + dest;

        var enemySprite = enemyObj.GetComponent<SpriteRenderer>();
        enemySprite.color = Color.green;

        var enemyController = enemyObj.GetComponent<EnemyScript>();
        enemyController.gameController = this;
        enemyController.enemyState = EnemyState.Shot;
        enemyController.Velocity = dest;
        enemyController.enemyType = enemyType;

        //add to a game object list
 
    }

    private void LoadBullet(Vector3 pos, Vector3 dest)
    {
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab);
        var bulletScript = bulletObj.GetComponent<BulletScript>();
        bulletScript.Velocity = dest;
        bulletScript.gameController = this;
        bulletObj.transform.position = pos;

        bulletList.Add(bulletObj);
    }

    private void LoadHarpoon(Vector3 pos)
    {
        this.harpoon= (GameObject)Instantiate(harpoonPrefab);
        var harpoonScript = harpoon.GetComponent<HarpoonScript>();
        harpoonScript.Velocity = new Vector3(0,0,0);
        harpoonScript.maxDistance = GameConfig.HarpoonDist;
        harpoonScript.gameController = this;
        harpoonScript.harpoonState = HarpoonState.Waiting;

        harpoon.transform.position = pos;
    }

	
	// Update is called once per frame
	void Update () {
	    if(r.Next(1000) > 990)
        {
            //LoadEnemy();
        }

        SetEnemyListText();
	}

    public void SetDebugText(string text)
    {
        DebugText.text = text;
    }

    public void SetEnemyListText()
    {
        EnemyListText.text = playerController.printEnemyList();
    }

    public void Shoot(Vector3 pos, Vector3 dest)
    {
        LoadBullet(pos,dest);
    }

    public void ShootHarpoon(Vector3 pos, Vector3 dest)
    {
        dest.z = 0;
        dest = Vector3.Normalize(dest);
        SetDebugText(dest.ToString());
        harpoon.GetComponent<HarpoonScript>().Shoot(pos, dest, GameConfig.HarpoonDist);

    }

    //deprecated
    public void ShootEnemy(GameObject enemy, Vector3 dest)
    {
        dest.z = 0;
        dest = Vector3.Normalize(dest);
        var enemyScript = enemy.GetComponent<EnemyScript>();
        enemyScript.enemyState = EnemyState.Shot;
        enemyScript.Velocity = dest;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        enemyList.Remove(enemy);
    }

    public void DestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
        bulletList.Remove(bullet);
    }

    public void DestroyHarpoon()
    {
        Destroy(harpoon);
        this.harpoon = null;
    }

    public void GameOver()
    {
        Application.LoadLevel(2);
    }
}
