using UnityEngine;
using System.Collections;

public class BuildTower : MonoBehaviour {

	Transform tower;
	Transform floor;

	// Use this for initialization
	void Start () {
		tower = GameObject.Find("Tower").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.layer == 8)
		{
			coll.gameObject.layer = 9;
			Destroy(coll.rigidbody);
			Destroy(coll.gameObject.GetComponent<ObjectManager>());
			coll.transform.parent = tower;
		}
	}


}
