using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

public class Robot
    {
        Joint DefaultJoint = new Joint("r", 0, 0, 0, 0, new Vector3(0, 0, 0));
        public List<Joint> Joints = new List<Joint>();
        public List<string> JointTypes = new List<string>();
        public List<double> Alpha = new List<double>();
        public List<double> A = new List<double>();
        public List<double> D = new List<double>();
        public List<double> Theta = new List<double>();
        public List<Vector3> Origins = new List<Vector3> { new Vector3(0, 0, 0) };
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
            this.AddJoint(new Joint("r", 0, 0, 0, 0, new Vector3(0, 0, 0)));
        }

        // Adds a joint to the Robot. Appends joint vector as well as all the parameters
        public void AddJoint(Joint joint_)
        {
            joint_.pannel = new JointPanel(joint_, Joints.Count);
            Joints.Add(joint_);
            this.JointTypes.Add(joint_.jointtype);
            this.Alpha.Add(joint_.alpha);
            this.A.Add(joint_.a);
            this.D.Add(joint_.d);
            this.Theta.Add(joint_.theta);

            GameObject NewJoint = GameObject.Instantiate(Resources.Load("Joint", typeof(GameObject)), RobotObject.transform) as GameObject;
            NewJoint.transform.position = new Vector3(0, 0, 0);
            NewJoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            Joints[Joints.Count - 1].gameobject = NewJoint;
            Origins.Add(new Vector3(0, 0, 0));
        }

        public void dhtf()
        {
            double[,] T_full = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < this.Joints.Count; i++)
            {

                double AlphaRad = this.Joints[i].alpha * Math.PI / 180;
                double A = this.Joints[i].a;
                double ThetaRad = this.Joints[i].theta * Math.PI / 180;
                double D = this.Joints[i].d;

                double[,] T_hold = {{ Math.Cos(ThetaRad),                      -Math.Sin(ThetaRad),                       0,                   A},
                                     { Math.Sin(ThetaRad) * Math.Cos(AlphaRad), Math.Cos(ThetaRad) * Math.Cos(AlphaRad), -Math.Sin(AlphaRad), -Math.Sin(AlphaRad) * D},
                                     { Math.Sin(ThetaRad) * Math.Sin(AlphaRad), Math.Cos(ThetaRad) * Math.Sin(AlphaRad),  Math.Cos(AlphaRad),   Math.Cos(AlphaRad) * D},
                                     { 0, 0, 0, 1} };

                T_full = MatrixFunctions.MultiplyMatrix(T_full, T_hold);

                this.Origins[i + 1] = new Vector3((float)(T_full[1, 3]), (float)(T_full[0, 3]), (float)(T_full[2, 3]));

                // Transform position to -x, z, y
                this.Joints[i].gameobject.transform.position = new Vector3(-this.Origins[i][0], this.Origins[i][2], this.Origins[i][1]);

                // Perform rotation (see TtoXYZ for details)
                this.Joints[i].gameobject.transform.eulerAngles = TtoXYZ(T_full);
                if (i == this.Joints.Count - 1)
                {
                    RobotManager.robot_center = this.Joints[i].gameobject.transform.position/2f;
                }

            }

        }

        private Vector3 TtoXYZ(double[,] T)
        {
            float Y_angle = -(float)Math.Asin(T[2, 0]);
            float X_angle = (float)Math.Atan2(T[2, 1] / Math.Cos(Y_angle), T[2, 2] / Math.Cos(Y_angle));
            float Z_angle = (float)Math.Atan2(T[1, 0] / Math.Cos(Y_angle), T[0, 0] / Math.Cos(Y_angle));

            float Rad2Deg = 180f / (float)Math.PI;

            return new Vector3( Y_angle* Rad2Deg, -Z_angle * Rad2Deg, -X_angle * Rad2Deg);
        }
    }