using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenerySpawner : MonoBehaviour {
    private List<GameObject> scenery;
    public List<GameObject> lowL, midL, hiL, lowR, midR, hiR;
    public float heightChange = 100;
    private int previous, current;
    public float lerpSpeed;
    public AudioSource[] sources;  
    public float spawnDelay = 5f;
    private float timer;
    public float depthRange = 5;
    public float heightRange = 10f;
    public float minHeight = 6f;
    public GameObject right, left;
	// Use this for initialization
	void Start () {
        sources[0].loop = true;
        sources[0].volume = 1.0f;
        sources[0].Play();
        sources[1].loop = true;
        sources[1].volume = 0;
        sources[1].Play();
        sources[2].loop = true;
        sources[2].volume = 0;
        sources[2].Play();
        previous = 0;
        current = 0;
    }
	
	// Update is called once per frame
	void Update () {
        AudioShit();
        int r = (int)Random.Range(0, 2);
        if (CameraMove.Height <= heightChange)
        {
            if (r == 0)
                scenery = lowL;
            else
                scenery = lowR;
        }
        else if (CameraMove.Height > heightChange && CameraMove.Height <= 2 * heightChange)
        {
            if (r == 0)
                scenery = midL;
            else
                scenery = midR;
        }
        else
        {
            if (r == 0)
                scenery = hiL;
            else
                scenery = hiR;
        }
        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            timer = 0;
            GameObject g = Instantiate(scenery[(int)Random.Range(0, scenery.Count - 1)]) as GameObject;
            int direction = (int)Random.Range(0, 2);
            if (r == 1)
            {
                g.tag = "right";
                g.transform.position = new Vector3(left.transform.position.x, left.transform.position.y + Random.Range(-heightRange, heightRange), left.transform.position.z + Random.Range(-depthRange, depthRange));
                SceneryMovement sm = g.GetComponent<SceneryMovement>() as SceneryMovement;
                sm.direction = SceneryMovement.Direction.Right;
            }
            else
            {
                g.tag = "left";
                g.transform.position = new Vector3(right.transform.position.x, right.transform.position.y + Random.Range(-heightRange, heightRange), right.transform.position.z + Random.Range(-depthRange, depthRange));
                SceneryMovement sm = g.GetComponent<SceneryMovement>() as SceneryMovement;
                sm.direction = SceneryMovement.Direction.Left;
            }
            if (g.transform.position.y < minHeight)
            {
                g.transform.position = new Vector3(g.transform.position.x, minHeight, g.transform.position.z);
            }
            //float offset = Random.Range(-depthRange, depthRange);
            //g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, right.transform.position.z + offset);
        }
	}

    void AudioShit()
    {
        if (CameraMove.Height < heightChange)
            current = 0;
        else if (CameraMove.Height >= heightChange && CameraMove.Height < 2 * heightChange)
            current = 1;
        else
            current = 2;
        if(current != previous)
        {
            FadeOut(previous);
            FadeIn(current);
        }
        if (sources[current].volume > 0.95f && sources[previous].volume < 0.05f)
            previous = current;
    }

    void FadeOut(int source)
    {
        sources[source].volume = Mathf.Lerp(sources[source].volume, 0, lerpSpeed * Time.deltaTime);
    }

    void FadeIn(int source)
    {
        sources[source].volume = Mathf.Lerp(sources[source].volume, 1, lerpSpeed * Time.deltaTime);
    }
}
