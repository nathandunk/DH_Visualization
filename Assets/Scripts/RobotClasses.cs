using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotClasses : MonoBehaviour {

    public class Joint
    {
        public string JointType;

        public Joint(string JointType_)
        {
            JointType = JointType_;
        }
    }

    public class Robot
    {
        public string[] Joints;
        public string[] JointTypes;
        public float[]  alpha;
        public float[]  a;
        public float[]  d;
        public float[]  theta;

        public Robot(Joint[] joints_)
        {
            Joints = joints_;
        }

        public void AddJoint(string type, float alpha, float a, float d, float theta)
        {

        }
    }

    // Use this for initialization
    void Start () {
		Robot robot(Joint)
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
