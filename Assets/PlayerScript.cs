using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GameControllerScript gameController;
 
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Debug.Log("Starting player script");
	}
	
	// Update is called once per frame
	void Update () {

        
        HandleInput();
        UpdateCamera();
	}

    private void HandleInput()
    {
        var x = Input.GetAxis("Horizontal") * GameConfig.PlayerSpeed;
        var y = Input.GetAxis("Vertical") * GameConfig.PlayerSpeed;

        var curPos = gameObject.transform.position;

        var newPos = new Vector3(curPos.x+x,curPos.y+y);

        gameObject.transform.position = newPos;

        if(Input.GetMouseButtonDown(0))
        {
            var dest = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            Shoot(dest);
        }

    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, mainCamera.transform.position.z);
    }

    public void Shoot(Vector3 dest)
    {
        dest = Vector3.Normalize(dest);
        gameController.Shoot(gameObject.transform.position, dest);
    }
}
