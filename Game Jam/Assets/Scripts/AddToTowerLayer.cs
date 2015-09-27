using UnityEngine;
using System.Collections;

public class AddToTowerLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
		if(c.gameObject.layer == 9 && gameObject.layer != 9)
		{
			gameObject.layer = 9;
			Transform[] temp = gameObject.GetComponentsInChildren<Transform>();
			foreach(Transform i in temp)
			{
				i.gameObject.layer = 9;
			}
			Vector3 newPos = gameObject.transform.position;
			newPos.z = 3.46917f;
			gameObject.transform.position = newPos;;
			SpringJoint j = gameObject.AddComponent<SpringJoint>();
			j.spring = 500;
			j.damper = 10;
			j.connectedBody = c.rigidbody;
		}
    }
}
