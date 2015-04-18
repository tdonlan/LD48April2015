using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public GameControllerScript gameController;
    public bool isDead;
    public Vector2 Velocity;

    private BoxCollider2D bulletCollider;

	// Use this for initialization
	void Start () {
	
	}

    private void Init()
    {
        this.bulletCollider = gameObject.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        
        var curPos = gameObject.transform.position;



        gameObject.transform.position = new Vector3(curPos.x + (Velocity.x * Time.deltaTime * GameConfig.BulletSpeed), curPos.y + (Velocity.y * Time.deltaTime * GameConfig.BulletSpeed));

        //CheckEnemyCollision();

           
	}

    private void CheckEnemyCollision()
    {
        foreach(var enemy in gameController.enemyList)
        {
            var enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if(enemyCollider.IsTouching(bulletCollider))
            {
                var enemyScript = enemy.GetComponent<EnemyScript>();
                enemyScript.Hit();
                gameController.DestroyBullet(gameObject);
            }
        }
    }
}
