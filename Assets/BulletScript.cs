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

        CheckOffscreen();
            
           
	}

    public void CheckOffscreen()
    {
        var curPos = gameObject.transform.position;
        if(curPos.x > 1000 || curPos.x < -1000 || curPos.y > 1000 || curPos.y < -1000)
        {
            gameController.DestroyBullet(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet HIt: " + other.gameObject.name);
       

        switch(other.gameObject.name)
        {
            case "EnemySprite":
                var enemyScript = other.gameObject.GetComponent<EnemyScript>();
                enemyScript.Hit();
                gameController.DestroyBullet(gameObject);
                break;
            case "PlayerSprite":
                    break;
            default:
                    
                    break;

        }


    }

 
}
