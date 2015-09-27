﻿using UnityEngine;
using System.Collections;

public class CraneManager : MonoBehaviour {

	public float dropSpeed = 1.0f;
	public Transform clawTarget;
	Animator mator;
	Spawner spawner;
	GameObject claw;
	bool lifted;
	bool grabbing;

	// Use this for initialization
	void Start () {
		mator = GetComponent<Animator>();
		spawner = GetComponentInChildren<Spawner>();
		claw = gameObject.GetComponentInChildren<ClawYPos>().gameObject;
		lifted = false;
		grabbing = false;
		StartCoroutine(GrabItem());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GrabNewItem()
	{
		if(!grabbing)
		{
			StartCoroutine(GrabItem());
			return true;
		}
		return false;
	}

	public bool Lift()
	{
		if(lifted == false)
		{
			mator.Play("Lift");
			return true;
		}
		return false;
	}

	IEnumerator GrabItem()
	{
		grabbing = true;
		int grabHash = Animator.StringToHash("Grab");
		int clawOpenHash = Animator.StringToHash("Claw Open");
		int clawCloseHash = Animator.StringToHash("Claw Close");
		int ungrabHash = Animator.StringToHash("UnGrab");


		yield return new WaitForSeconds(1.0f);
		mator.Play(grabHash);
		if(mator.GetNextAnimatorStateInfo(2).shortNameHash != clawOpenHash)
			mator.Play(clawOpenHash);
		yield return 0;
		while(mator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1)
		{
			yield return 0;
		}

		Vector3 returnPos = claw.transform.position;
		Vector3 target = claw.transform.position;
		target.y = clawTarget.position.y;
		yield return StartCoroutine(MoveClaw(returnPos, target));

		spawner.Spawn();

		mator.Play(clawCloseHash);
		yield return 0;
		while(mator.GetCurrentAnimatorStateInfo(2).normalizedTime < 1)
		{
			yield return 0;
		}

		yield return StartCoroutine(MoveClaw(target, returnPos));

		mator.Play(ungrabHash);
		yield return 0;
		while(mator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1)
		{
			yield return 0;
		}
		grabbing = false;
		yield return null;
	}

	IEnumerator MoveClaw(Vector3 ini, Vector3 target)
	{
		Vector3 initial = ini;
		Vector3 end = ini;
		end.y = target.y;
		float start = Time.time;
		float covered = 0;
		float frac = 0;
		while(frac < 1.0f)
		{

			covered = (Time.time - start) * dropSpeed;
			covered += Time.deltaTime;
			frac = covered / dropSpeed;
			if(frac > 1.0f)
				frac = 1.0f;
			claw.transform.position = Vector3.Lerp(initial,end,frac);
			yield return 0;
		}
	}

	public void OpenClaw()
	{
		int clawOpenHash = Animator.StringToHash("Claw Open");
		if(mator.GetNextAnimatorStateInfo(2).shortNameHash != clawOpenHash)
			mator.Play(clawOpenHash);

	}


}
