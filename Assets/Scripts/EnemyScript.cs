using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {

    public GameControllerScript gameController;
    public EnemyState enemyState;

    public Vector2 Velocity;
    public Vector2 Acceleration;

    public float Timer;


	// Use this for initialization
	void Start () {
        enemyState = EnemyState.Default;
	}
	
	// Update is called once per frame
	void Update () {
	   
        switch(enemyState)
        {
            case EnemyState.Default:
                UpdateDefault();
                break; 
            case EnemyState.Shot:
                UpdateShot();
                break; 
            case EnemyState.Turned:
                UpdateTurned();
                break;
            default:
                break;
        }
       
	}

    private void UpdateDefault()
    {
        //chase player
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameController.player.transform.position, GameConfig.EnemySpeed * Time.deltaTime);
    }

    private void UpdateShot()
    {
        float d = Time.deltaTime;
        var curPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(curPos.x + Velocity.x * d, curPos.y + Velocity.y * d);

        Timer -= Time.deltaTime;
        if(Timer <=0)
        {
            Velocity = new Vector3(0,0,0);
            enemyState = EnemyState.Turned;
        }
    }

    private void UpdateTurned()
    {

        var nearEnemy = gameController.enemyList.OrderBy(x=>Vector3.Distance(x.transform.position, gameObject.transform.position)).ToList().FirstOrDefault();

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nearEnemy.transform.position, GameConfig.EnemySpeed * Time.deltaTime);

    }

    public void Hit()
    {
        gameController.DestroyEnemy(gameObject);
    }

    public void Capture()
    {
        enemyState = EnemyState.Captured;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        switch(enemyState)
        {
            case EnemyState.Default:
                CollideDefault(other);
                break;
            case EnemyState.Turned:
                CollideTurned(other);
                break;
            default:
                break;

        }
    }

    private void CollideDefault(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "PlayerSprite":
                gameController.playerController.Hit(1);
                Hit();
                break;
            default:
                break;

        }
    }

    private void CollideTurned(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "EnemyPrefab(Clone)":
                var otherEnemyScript = other.gameObject.GetComponent<EnemyScript>();
                otherEnemyScript.Hit();
                Hit();
                break;
            default:
                break;

        }
    }



}
