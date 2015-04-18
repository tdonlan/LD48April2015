using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public GameControllerScript gameController;
    public bool isDead;
    public Vector2 Velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        var curPos = gameObject.transform.position;



        gameObject.transform.position = new Vector3(curPos.x + (Velocity.x * Time.deltaTime * GameConfig.BulletSpeed), curPos.y + (Velocity.y * Time.deltaTime * GameConfig.BulletSpeed));


           
	}
}
