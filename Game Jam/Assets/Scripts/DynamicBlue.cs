using UnityEngine;
using System.Collections;

public class DynamicBlue : MonoBehaviour {

	public float blackHeight = 20.0f;


	Color initial;
	Color black;
	float t;
	float iniHeight;

	// Use this for initialization
	void Start () {
		initial = GetComponent<MeshRenderer>().material.color;
		black = Color.black;
		t = 0;
		iniHeight = Camera.main.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		T = (Camera.main.transform.position.y - iniHeight) / blackHeight;
		Color temp = Color.Lerp(initial,black,t);
		//print(t);
		temp.b *= T + 1;
		GetComponent<MeshRenderer>().material.color = temp;
	}

	public float T
	{
		get{return t;}
		set
		{
			t = value;
			if(t < 0.0f)
				t = 0.0f;
			else if(t > 1.0f)
				t = 1.0f;
		}
	}
}
