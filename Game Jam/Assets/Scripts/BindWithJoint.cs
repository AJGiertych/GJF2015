using UnityEngine;
using System.Collections;

public class BindWithJoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.layer == 9 && gameObject.layer != 9)
		{
			FixedJoint j = gameObject.AddComponent<FixedJoint>();
			j.connectedBody = coll.rigidbody;
		}
	}
}
