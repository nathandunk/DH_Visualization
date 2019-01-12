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


        public Joint(string jointtype_, double alpha_, double a_, double d_, double theta_)
        {
            this.jointtype = jointtype_;   
            this.a = a_;
            this.alpha = a_;
            this.d = a_;
            this.theta = a_;
        }
    }

    public class Robot
    {
        Joint DefaultJoint = new Joint("r", 0, 0, 0, 0);
        public List<Joint> Joints = new List<Joint>();
        public List<string> JointTypes = new List<string>();
        public List<double> Alpha = new List<double>();
        public List<double> A = new List<double>();
        public List<double> D = new List<double>();
        public List<double> Theta = new List<double>();

        public Robot()
        {
            AddJoint();
        }

        public Robot(Joint joint_)
        {
            AddJoint(joint_);
        }

        public Robot(List<Joint> joints_)
        {
            foreach (Joint joint in Joints)
            {
                AddJoint(joint);
            }
        }

        // Adds a default joint to the Robot. Appends joint vector as well as all the parameters
        // In this case, the default joint is a rotational joint with all parameters (a, alpha, d, theta as 0)
        public void AddJoint()
        {
            this.AddJoint(new Joint("r", 0, 0, 0, 0));
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
        }

        private void dhtf()
        {
            double[,] T_full = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < this.Joints.Count; i++)
            {
                T_hold = {{ Math.Cos(this.Joints[i].theta), np.cos(self.joint_values[i]), -np.sin(self.joint_values[i]), 0, self.a[i]},
        //        [np.sin(self.joint_values[i]) * np.cos(self.alpha[i]), np.cos(self.joint_values[i]) * np.cos(self.alpha[i]), -np.sin(self.alpha[i]), -np.sin(self.alpha[i]) * self.d[i]],
        //        [np.sin(self.joint_values[i]) * np.sin(self.alpha[i]), np.cos(self.joint_values[i]) * np.sin(self.alpha[i]), np.cos(self.alpha[i]), np.cos(self.alpha[i]) * self.d[i]],
        //        [0, 0, 0, 1]])
            }
        }
        //for i in range(0, self.size+1):
        //    
        //    # print(T_hold.shape)
        //    # print(T_hold)
        //    self.T_full = np.matmul(self.T_full, T_hold)

        //    self.O[:, i] = self.T_full[0:3, 3]

        //    self.X[:, i] = self.O[:, i] + self.T_full[0:3, 0] * 10
        //    self.Y[:, i] = self.O[:, i] + self.T_full[0:3, 1] * 10
        //    self.Z[:, i] = self.O[:, i] + self.T_full[0:3, 2] * 10
        //    # print(self.T_full)
    }

    // Use this for initialization
    void Start () {
        Joint StarterJoint = new Joint("r", 0, 0, 10, 0);
        Robot robot = new Robot(StarterJoint);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
