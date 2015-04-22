using UnityEngine;
using System.Collections;

public class HarpoonScript : MonoBehaviour {

    public GameControllerScript gameController { get; set; }

    public Vector3 Velocity { get; set; }
    public float maxDistance { get; set; }
    public float distance { get; set; }
    public bool grabbed { get; set; }

    public HarpoonState harpoonState;
   

    public GameObject grabbedGameObject { get; set; }

	// Use this for initialization
	void Start () {
        this.distance = 0;
       
        this.harpoonState = HarpoonState.Waiting;
	}

    public void Shoot(Vector3 pos, Vector3 velocity, float maxDist)
    {
        if (harpoonState == HarpoonState.Waiting)
        {

            gameObject.transform.position = pos;
            this.harpoonState = HarpoonState.Shooting;
            this.maxDistance = maxDist;
            this.Velocity = velocity;
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (harpoonState)
        {
            case HarpoonState.Waiting:
                gameObject.transform.position = gameController.player.transform.position;
                break;
            case HarpoonState.Shooting:
                UpdateShooting(Time.deltaTime);
                break;
            case HarpoonState.Retrieving:
                UpdateRetrieving(Time.deltaTime);
                break;
            default:
                break;
        }
     
	}

    private void UpdateShooting(float delta)
    {
        var curPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(curPos.x + Velocity.x * delta * GameConfig.HarpoonSpeed, curPos.y + Velocity.y * delta * GameConfig.HarpoonSpeed);

       distance += Mathf.Abs( Velocity.x * Time.deltaTime * GameConfig.HarpoonSpeed) + Mathf.Abs(Velocity.y * Time.deltaTime * GameConfig.HarpoonSpeed);

        if (distance >= maxDistance)
        {
            distance = 0;
            harpoonState = HarpoonState.Retrieving;
        }

    }

    private void UpdateRetrieving(float delta)
    {
        var curPos = gameObject.transform.position;
        var dest = gameController.player.transform.position;

        gameObject.transform.position = Vector3.MoveTowards(curPos, dest, GameConfig.HarpoonSpeed / GameConfig.HarpoonDist * delta);

        if(Vector3.Distance(curPos,dest) < .1)
        {
            if(grabbedGameObject != null)
            {
                gameController.playerController.AddEnemy(grabbedGameObject);
                //gameController.playerController.playerState = PlayerState.Captive;
            }

            harpoonState = HarpoonState.Waiting;
          
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (harpoonState == HarpoonState.Shooting)
        {

            Debug.Log("Harpoon HIt: " + other.gameObject.name);

            switch (other.gameObject.name)
            {
                case "EnemyPrefab(Clone)":
                    Grab(other.gameObject);
                    break;
                default:
                    break;

            }
        }
    }

    private void Grab(GameObject enemy)
    {
        this.grabbedGameObject = enemy;
        enemy.transform.position = gameObject.transform.position;
        var enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemySprite.sortingOrder = -1;
        enemy.transform.SetParent(gameObject.transform);

        var enemyScript = enemy.GetComponent<EnemyScript>();
        enemyScript.Capture();

        harpoonState = HarpoonState.Retrieving;
    }
}
