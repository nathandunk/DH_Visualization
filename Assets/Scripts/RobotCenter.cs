using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = RobotManager.robot_center;
	}
}
