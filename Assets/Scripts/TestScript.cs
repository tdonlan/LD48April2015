using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    GameObject parentSprite;
    GameObject childSprite;

	// Use this for initialization
	void Start () {
        parentSprite = GameObject.FindGameObjectWithTag("Player");
        childSprite = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        var curPos = parentSprite.transform.position;
        parentSprite.transform.position = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime + curPos.x, Input.GetAxis("Vertical") * Time.deltaTime + curPos.y);
	}


    public void AddSpriteChild()
    {



        childSprite.transform.position = parentSprite.transform.position;
        var childSpriteRender = childSprite.GetComponent<SpriteRenderer>();



        childSpriteRender.sortingOrder = -1;

        childSprite.transform.SetParent(parentSprite.transform);

        
    }

}
