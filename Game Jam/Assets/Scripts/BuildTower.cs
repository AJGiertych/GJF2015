﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			Transform [] tempTrans = coll.gameObject.GetComponentsInChildren<Transform>();
			foreach(Transform i in tempTrans)
			{
				i.gameObject.layer = 9;
			}

			tempTrans = coll.gameObject.GetComponentsInParent<Transform>();
			foreach(Transform i in tempTrans)
			{
				i.gameObject.layer = 9;
			}

//			Destroy(coll.rigidbody);
			Destroy(coll.gameObject.GetComponent<ObjectManager>());
//			coll.transform.parent = tower;
		}
	}

	void OnCollisionStay(Collision coll)
	{
		if(coll.gameObject.layer == 8)
		{
			coll.gameObject.layer = 9;
			Transform [] tempTrans = coll.gameObject.GetComponentsInChildren<Transform>();
			foreach(Transform i in tempTrans)
			{
				i.gameObject.layer = 9;
			}
			Destroy(coll.gameObject.GetComponent<ObjectManager>());
		}
		if(coll.gameObject.layer == 9)
		{
			if(coll.rigidbody.IsSleeping())
			{
				Destroy(coll.rigidbody);
				//Destroy(coll.gameObject.GetComponent<ObjectManager>());
				coll.transform.parent = tower;
			}
			else
			{
				int tempInt = Random.Range(0,100);
				if(tempInt == 0)
					coll.rigidbody.Sleep();
			}
		}
	}
}
