using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotClasses : MonoBehaviour {

    public class Joint
    {
        public string jointtype;
        public double a;
        public double alpha;
        public double d;
        public double theta;
        public Vector3 origin;
        public GameObject gameobject;
        public GameObject panel;


        public Joint(string jointtype_, double alpha_, double a_, double d_, double theta_, Vector3 origin_)
        {
            this.jointtype = jointtype_;   
            this.a = a_;
            this.alpha = alpha_;
            this.d = d_;
            this.theta = theta_;
            this.origin = origin_;
        }
    }

    public class Robot
    {
        Joint DefaultJoint = new Joint("r", 0, 0, 0, 0, new Vector3(0,0,0));
        public List<Joint> Joints = new List<Joint>();
        public List<string> JointTypes = new List<string>();
        public List<double> Alpha = new List<double>();
        public List<double> A = new List<double>();
        public List<double> D = new List<double>();
        public List<double> Theta = new List<double>();
        public GameObject RobotObject;

        public Robot(GameObject RobotObject_)
        {
            RobotObject = RobotObject_;
            AddJoint();
        }

        public Robot(Joint joint_, GameObject RobotObject_)
        {
            RobotObject = RobotObject_;
            AddJoint(joint_);
        }

        public Robot(List<Joint> joints_, GameObject RobotObject_)
        {
            RobotObject = RobotObject_;
            foreach (Joint joint in Joints)
            {
                AddJoint(joint);
            }
        }

        // Adds a default joint to the Robot. Appends joint vector as well as all the parameters
        // In this case, the default joint is a rotational joint with all parameters (a, alpha, d, theta as 0)
        public void AddJoint()
        {
            this.AddJoint(new Joint("r", 0, 0, 0, 0, new Vector3(0,0,0)));
        }

        // Adds a joint to the Robot. Appends joint vector as well as all the parameters
        public void AddJoint(Joint joint_)
        {
            Joints.Add(joint_);
            this.JointTypes.Add(joint_.jointtype);
            this.Alpha.Add(joint_.alpha);
            this.A.Add(joint_.a);
            this.D.Add(joint_.d);
            this.Theta.Add(joint_.theta);

            GameObject NewJoint = Instantiate(Resources.Load("Joint", typeof(GameObject)), RobotObject.transform) as GameObject;
            NewJoint.transform.position = new Vector3(0, 0, 0);
            NewJoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            Joints[Joints.Count - 1].gameobject = NewJoint;
        }

        public void dhtf()
        {
            double[,] T_full = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < this.Joints.Count; i++)
            {
                double [,] T_hold = {{ Math.Cos(this.Joints[i].theta), -Math.Sin(this.Joints[i].theta), 0, this.Joints[i].a}, 
                                     { Math.Sin(this.Joints[i].theta) * Math.Cos(this.Joints[i].alpha), Math.Cos(this.Joints[i].theta) * Math.Cos(this.Joints[i].alpha), -Math.Sin(this.Joints[i].alpha), -Math.Sin(this.Joints[i].alpha) * this.Joints[i].d}, 
                                     { Math.Sin(this.Joints[i].theta) * Math.Sin(this.Joints[i].alpha), Math.Cos(this.Joints[i].theta) * Math.Sin(this.Joints[i].alpha), Math.Cos(this.Joints[i].alpha), Math.Cos(this.Joints[i].alpha) * this.Joints[i].d}, 
                                     { 0, 0, 0, 1} };

                T_full = MatrixFunctions.MultiplyMatrix(T_full, T_hold);
                this.Joints[i].origin = new Vector3((float)(T_full[0,0]), (float)(T_full[0, 1]), (float)(T_full[0, 2]));

                this.Joints[i].gameobject.transform.Rotate(new Vector3((float)this.Joints[i].alpha,0,(float)this.Joints[i].theta));
            }
        }
    }

    // Use this for initialization
    void Start () {
        Joint StarterJoint = new Joint("r", 0, 0, 10, 0, new Vector3(0,0,0));
        Robot robot = new Robot(StarterJoint, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
