using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotClasses : MonoBehaviour {

    public class Joint
    {
        public string JointType;

        public Joint(string JointType_, double alpha, double a, double d, double theta)
        {
            this.JointType = JointType_;
        }
    }

    public class Robot
    {
        public List<Joint> Joints = new List<Joint>();
        public List<string> JointTypes = new List<string>();
        public string[] JointTypes;
        public double[] alpha;
        public double[] a;
        public double[] d;
        public double[] theta;

        public Robot(Joint[] joints_)
        {
            this.Joints = joints_;
        }

        public void AddJoint(string type, double alpha, double a, double d, double theta)
        {

        }

        //private void dhtf(self):

        //Matrix self.T_full = Matrix.Identity
        //for i in range(0, self.size+1):
        //    T_hold = np.array([[np.cos(self.joint_values[i]), -np.sin(self.joint_values[i]), 0, self.a[i]],
        //        [np.sin(self.joint_values[i]) * np.cos(self.alpha[i]), np.cos(self.joint_values[i]) * np.cos(self.alpha[i]), -np.sin(self.alpha[i]), -np.sin(self.alpha[i]) * self.d[i]],
        //        [np.sin(self.joint_values[i]) * np.sin(self.alpha[i]), np.cos(self.joint_values[i]) * np.sin(self.alpha[i]), np.cos(self.alpha[i]), np.cos(self.alpha[i]) * self.d[i]],
        //        [0, 0, 0, 1]])
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
		Robot robot(Joint)
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
