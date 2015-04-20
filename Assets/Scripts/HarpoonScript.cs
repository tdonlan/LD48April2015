using UnityEngine;
using System.Collections;

public class HarpoonScript : MonoBehaviour {

    public GameControllerScript gameController { get; set; }

    public Vector3 Velocity { get; set; }
    public float maxDistance { get; set; }
    public float distance { get; set; }
    public bool grabbed { get; set; }

    public bool isShooting { get; set; }

    public GameObject grabbedGameObject { get; set; }

	// Use this for initialization
	void Start () {
        this.distance = 0;
        isShooting = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShooting)
        {
            UpdateShooting(Time.deltaTime);
        }
        else
        {
           // gameController.DestroyHarpoon();
            UpdateRetrieving(Time.deltaTime);
        }
	}

    private void UpdateShooting(float delta)
    {
        var curPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(curPos.x + Velocity.x * delta * GameConfig.HarpoonSpeed, curPos.y + Velocity.y * delta * GameConfig.HarpoonSpeed);

       distance += Mathf.Abs( Velocity.x * Time.deltaTime * GameConfig.HarpoonSpeed) + Mathf.Abs(Velocity.y * Time.deltaTime * GameConfig.HarpoonSpeed);

       gameController.SetDebugText(distance.ToString());

        if (distance >= maxDistance)
        {
            isShooting = false;
        }

    }

    private void UpdateRetrieving(float delta)
    {
        var curPos = gameObject.transform.position;
        var dest = gameController.player.transform.position;

        gameObject.transform.position = Vector3.MoveTowards(curPos, dest, GameConfig.HarpoonSpeed / GameConfig.HarpoonDist * delta);

        if(Vector3.Distance(curPos,dest) < .1)
        {
            gameController.DestroyHarpoon();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isShooting)
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

        isShooting = false;
    }
}
