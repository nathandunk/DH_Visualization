using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint
    {
        public string jointtype;
        public double a;
        public double alpha;
        public double d;
        public double theta;
        public Vector3 origin;
        public Vector3 EE;
        public GameObject gameobject;
        public JointPanel pannel;

        public Joint(string jointtype_, double alpha_, double a_, double d_, double theta_, Vector3 origin_)
        {
            this.jointtype = jointtype_;   
            this.a = a_;
            this.alpha = alpha_;
            this.d = d_;
            this.theta = theta_;
            this.origin = origin_;
        }

        public void UpdateJointShape()
        {
            ///////// UPDATING D /////////
            Vector3 TempScale = this.gameobject.transform.Find("Connections").Find("Cube").transform.localScale;
            TempScale[1] = (float)this.d;
            this.gameobject.transform.Find("Connections").Find("Cube").transform.localScale = TempScale;

            // Update the position to be d/2 to keep the connection in the correct place
            Vector3 TempPosit = this.gameobject.transform.Find("Connections").Find("Cube").transform.localPosition;
            TempPosit[1] = (float)this.d/2.0f;
            this.gameobject.transform.Find("Connections").Find("Cube").transform.localPosition = TempPosit;
        }
    }
