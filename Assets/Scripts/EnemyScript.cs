using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public GameControllerScript gameController;


    Vector2 Velocity;
    Vector2 Acceleration;
    
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
	    //chase player
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameController.player.transform.position,GameConfig.EnemySpeed * Time.deltaTime);
	}

    public void Hit()
    {
        gameController.DestroyEnemy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
         
        Debug.Log("Enemy HIt: " + other.gameObject.name);
       
        switch(other.gameObject.name)
        {
            case "PlayerSprite":
                gameController.playerController.Hit(1);
                Hit();
                    break;
            default:
                    break;

        }
    }



}
