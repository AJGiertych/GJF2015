using UnityEngine;
using System.Collections;

public class MoveUpWithCamera : MonoBehaviour {

	bool lifted;
	public float offset;

	// Use this for initialization
	void Start () {
		lifted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main.transform.position.y - offset > transform.position.y)
		{
			if(!lifted)
			{
				GetComponentInParent<CraneManager>().Lift();
				lifted = true;
			}

			Vector3 temp = transform.position;
			temp.y = Camera.main.transform.position.y - offset;
			transform.position = Vector3.Lerp(transform.position,temp,Time.deltaTime);
		}
	}
}
