using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GameControllerScript gameController;
    GameObject playerObject;
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

        var curPos = playerObject.transform.position;

        var newPos = new Vector3(curPos.x+x,curPos.y+y);

        playerObject.transform.position = newPos;

        if(Input.GetMouseButtonDown(0))
        {
            var dest = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            Shoot(dest);
        }

    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, mainCamera.transform.position.z);
    }

    public void Shoot(Vector3 dest)
    {
        dest = Vector3.Normalize(dest);
        gameController.Shoot(gameObject.transform.position, dest);
    }
}
