using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {


    public GameObject player;
    public List<GameObject> enemyList;
    public System.Random r;

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
        this.r = new System.Random();
        
        
        enemyList = new List<GameObject>();

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
    }

    private void LoadEnemy()
    {
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab);
        enemyObj.transform.position = GameHelper.getRandomPos(r);

        var enemyController = enemyObj.GetComponent<EnemyScript>();
        enemyController.gameController = this;


        enemyList.Add(enemyObj);
    }

	
	// Update is called once per frame
	void Update () {
	    if(r.Next(100) > 90)
        {
            LoadEnemy();
        }
	}
}
