using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour {
    public List<GameObject> roads;
    public float offset = 150; 
	// Use this for initialization
	void Start ()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MoveRoad()
    {
        GameObject Moveroad = roads[0];
        roads.Remove(Moveroad);
        float newZ = roads[roads.Count -1].transform.position.z+offset;
        Moveroad.transform.position = new Vector3(0, 0, newZ);
        roads.Add(Moveroad);
    }
}
