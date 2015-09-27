using UnityEngine;
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
		int ungrabHash = Animator.StringToHash("UnGrab");


		yield return new WaitForSeconds(1.0f);
		mator.Play(grabHash);
		yield return 0;
		while(mator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1)
		{
			yield return 0;
		}

		Transform returnPos = claw.GetComponent<ClawYPos>().Target;
		Vector3 target = claw.transform.position;
		target.y = clawTarget.position.y;
		yield return StartCoroutine(MoveClawDown(returnPos, target));

		spawner.Spawn();

		yield return StartCoroutine(MoveClawUp(target, returnPos));

		mator.Play(ungrabHash);
		yield return 0;
		while(mator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1)
		{
			yield return 0;
		}
		grabbing = false;
		yield return null;
	}

	IEnumerator MoveClawDown(Transform ini, Vector3 target)
	{
		float covered = 0;
		float frac = 0;
		while(frac < 1.0f)
		{
			covered += Time.deltaTime;
			frac = covered / dropSpeed;
			if(frac > 1.0f)
				frac = 1.0f;
			claw.transform.position = Vector3.Lerp(ini.position,target,frac);
			yield return 0;
		}
	}

	IEnumerator MoveClawUp(Vector3 ini, Transform target)
	{
		float covered = 0;
		float frac = 0;
		while(frac < 1.0f)
		{
			covered += Time.deltaTime;
			frac = covered / dropSpeed;
			if(frac > 1.0f)
				frac = 1.0f;
			claw.transform.position = Vector3.Lerp(ini,target.position,frac);
			yield return 0;
		}
	}


}
