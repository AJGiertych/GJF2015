using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnables;
    public static bool hasObject;
	CraneManager crane;

	// Use this for initialization
	void Start () {
		crane = GetComponentInParent<CraneManager>();
    }

    public void Spawn()
    {
		int temp = Random.Range(0,spawnables.Length);
        GameObject go = (GameObject)Instantiate((GameObject)spawnables[temp], transform.position, transform.rotation);
        go.transform.parent = transform;
		//go.GetComponent<Rigidbody>().isKinematic = true;
        hasObject = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("floating");
        if (gos.Length == 0)
        {
			crane.GrabNewItem();
        }
    }
}
