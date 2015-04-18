using UnityEngine;
using System.Collections;

public class TestObjectScript : MonoBehaviour {

    int count=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided" + count++);
        

    }
}
