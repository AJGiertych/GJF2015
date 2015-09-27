using UnityEngine;
using System.Collections;

public class ClawYPos : MonoBehaviour {

	public Transform original;
	public Transform yTrans;
	Transform target;

	// Use this for initialization
	void Start () {
		target = new GameObject().transform;
		target.position = original.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(original.position.y > yTrans.position.y)
		{
			Vector3 temp = original.position;
			temp.y = yTrans.position.y;
			target.position = temp;
		}
		else
			target.position = original.position;

		if(transform.position.y > target.position.y)
		{
			Vector3 temp = transform.position;
			temp.y = target.position.y;
			transform.position = temp;
		}
	}

	public Transform Target
	{
		get{return target;}
	}
}
