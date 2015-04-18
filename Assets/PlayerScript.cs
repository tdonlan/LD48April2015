using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GameObject playerObject;
    Camera mainCamera;

	// Use this for initialization
	void Start () {
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
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var curPos = playerObject.transform.position;

        var newPos = new Vector3(curPos.x+x,curPos.y+y);

        playerObject.transform.position = newPos;

    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, mainCamera.transform.position.z);
    }
}
