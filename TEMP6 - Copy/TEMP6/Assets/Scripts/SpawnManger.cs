﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour {

    // Use this for initialization
    RoadSpawner Roadspawner;
	void Start () {
        Roadspawner = GetComponent<RoadSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpawonTriggerEnter()
    {
        Roadspawner.MoveRoad();
    }
      
}
