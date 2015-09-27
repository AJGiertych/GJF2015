using UnityEngine;
using System.Collections;

public class Tripping : MonoBehaviour {

	public Camera endGameCam;
	bool collapsed;

	// Use this for initialization
	void Start () {
		collapsed = false;
		endGameCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.eulerAngles.z > 40 && transform.eulerAngles.z < 320)
		{
			CameraMove.towerCollapse = true;
			if(collapsed == false)
			{
				Collapse();
				collapsed = true;
				endGameCam.enabled = true;
			}
		}
	}

	void Collapse()
	{
		Transform [] children = GetComponentsInChildren<Transform>();
		foreach(Transform t in children)
		{
			if(t != this.transform)
				t.gameObject.AddComponent<Rigidbody>();
		}
		Destroy(gameObject.GetComponent<Rigidbody>());
	}
}
