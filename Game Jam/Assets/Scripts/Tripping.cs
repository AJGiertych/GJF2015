using UnityEngine;
using System.Collections;

public class Tripping : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(transform.eulerAngles.z < 40 || transform.eulerAngles.z > 320)
		{
			CameraMove.towerCollapse = true;
			print("Fell");
		}
	}
}
