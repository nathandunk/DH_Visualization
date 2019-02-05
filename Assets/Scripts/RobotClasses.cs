using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

public class RobotClasses : MonoBehaviour {

    Robot robot;
    public Button AddJointButton;

    public class JointPanel
    {
        Joint CorrespondingJoint;
        GameObject gameobject;
        int JointNumber;
        public List<Slider>     SliderList     = new List<Slider>(); // order alpha, a, d, theta
        public List<InputField> InputFieldList = new List<InputField>(); // order alpha, a, d, theta
        string JointName = "<b>Joint 0:</b>";

        public JointPanel(Joint joint_, int JointNumber_)
        {
            this.JointNumber = JointNumber_;
            CorrespondingJoint = joint_;
            GameObject NewPanel = Instantiate(Resources.Load("JointPanel", typeof(GameObject)), GameObject.Find("Panel").transform) as GameObject;
            this.gameobject = NewPanel;
            this.JointName = "<b>Joint " + this.JointNumber.ToString() + ":</b>";

            this.gameobject.transform.Find("JointName").GetComponent<Text>().text = this.JointName;

            foreach (string term in new string[] {"alpha", "a", "d", "theta"})
            {
                Slider slider_temp = this.gameobject.transform.Find(term).Find("Slider").gameObject.GetComponent<Slider>();
                InputField inputfield_temp = this.gameobject.transform.Find(term).Find("Value").gameObject.GetComponent<InputField>();

                SliderList.Add(slider_temp);
                InputFieldList.Add(inputfield_temp);

                // Have the two objects, slider and input field, listen to eachother and stay in sync
                slider_temp.onValueChanged.AddListener(delegate { SyncSliderTextBox(slider_temp, inputfield_temp); });
                inputfield_temp.onValueChanged.AddListener(delegate { SyncTextBoxSlider(slider_temp, inputfield_temp); });

                inputfield_temp.onValueChanged.AddListener(delegate { SyncSliderJointValue(inputfield_temp, term); });
            }
            NewPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -75 - this.JointNumber*150, 0);
        }

        public void SyncSliderJointValue(InputField inputfield_, string term_)
        {
            switch (term_)
            {
                case "alpha":
                    try
                    {
                        this.CorrespondingJoint.alpha = float.Parse(inputfield_.text, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (FormatException)
                    {
                        this.CorrespondingJoint.alpha = 0;
                    }
                    break;
                case "a":
                    try
                    {
                        this.CorrespondingJoint.a = float.Parse(inputfield_.text, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (FormatException)
                    {
                        this.CorrespondingJoint.a = 0;
                    }
                    break;
                case "d":
                    try
                    {
                        this.CorrespondingJoint.d = float.Parse(inputfield_.text, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (FormatException)
                    {
                        this.CorrespondingJoint.d = 0;
                    }
                    break;
                case "theta":
                    try
                    {
                        this.CorrespondingJoint.theta = float.Parse(inputfield_.text, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (FormatException)
                    {
                        this.CorrespondingJoint.theta = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        public void SyncSliderTextBox(Slider slider_, InputField inputfield_)
        {
                        float Min;
            float Max;
            switch (slider_.transform.parent.name)
            {
                case "alpha":
                    Min = -180;
                    Max = 180;
                    break;
                case "a":
                    Min = 0;
                    Max = 200;
                    break;
                case "theta":
                    Min = -180;
                    Max = 180;
                    break;
                case "d":
                    Min = 0;
                    Max = 200;
                    break;
                default:
                    Min = 0;
                    Max = 1;
                    break;
            }
            inputfield_.text = ((Max-Min)*slider_.value+Min).ToString();
        }

        public void SyncTextBoxSlider(Slider slider_, InputField inputfield_)
        {
            float Min;
            float Max;
            switch (slider_.transform.parent.name)
            {
                case "alpha":
                    Min = -180;
                    Max = 180;
                    break;
                case "a":
                    Min = 0;
                    Max = 200;
                    break;
                case "theta":
                    Min = -180;
                    Max = 180;
                    break;
                case "d":
                    Min = 0;
                    Max = 200;
                    break;
                default:
                    Min = 0;
                    Max = 1;
                    break;
            }
            try
            {
                float InputNumber = float.Parse(inputfield_.text, CultureInfo.InvariantCulture.NumberFormat);
                slider_.value = (InputNumber - Min)/(Max-Min);
            }
            catch (FormatException)
            {
                slider_.value = 0;
            }
        }
    }

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

            ///////// UPDATING THETA ///////// 
            //Vector3 TempRot = this.gameobject.transform.localEulerAngles;
            //TempRot[1] = (float)this.theta;
            //this.gameobject.transform.localEulerAngles = TempRot;
        }
    }

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

            GameObject NewJoint = Instantiate(Resources.Load("Joint", typeof(GameObject)), RobotObject.transform) as GameObject;
            NewJoint.transform.position = new Vector3(0, 0, 0);
            NewJoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            Joints[Joints.Count - 1].gameobject = NewJoint;
            Origins.Add(new Vector3(0, 0, 0));
        }

        public void dhtf()
        {
            double[,] T_full = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            for (int i = 1; i < this.Joints.Count; i++)
            {
                double AlphaRad = this.Joints[i-1].alpha * Math.PI / 180;
                double ThetaRad = this.Joints[i].theta * Math.PI / 180;

                double[,] T_hold = {{ Math.Cos(ThetaRad),                      -Math.Sin(ThetaRad),                       0,                   this.Joints[i-1].a},
                                     { Math.Sin(ThetaRad) * Math.Cos(AlphaRad), Math.Cos(ThetaRad) * Math.Cos(AlphaRad), -Math.Sin(AlphaRad), -Math.Sin(AlphaRad) * this.Joints[i].d},
                                     { Math.Sin(ThetaRad) * Math.Sin(AlphaRad), Math.Cos(ThetaRad) * Math.Sin(AlphaRad),  Math.Cos(AlphaRad),   Math.Cos(AlphaRad) * this.Joints[i].d},
                                     { 0, 0, 0, 1} };
                //double[,] T_hold = {{  Math.Cos(ThetaRad),-Math.Sin(ThetaRad)*Math.Cos(AlphaRad),    Math.Sin(ThetaRad)*Math.Sin(AlphaRad), this.Joints[i].a*Math.Cos(ThetaRad)},
                //                     { Math.Sin(ThetaRad), Math.Cos(ThetaRad)*Math.Cos(AlphaRad),   -Math.Cos(ThetaRad)*Math.Sin(AlphaRad), this.Joints[i].a*Math.Sin(ThetaRad)},
                //                     { 0,                  Math.Sin(AlphaRad),                       Math.Cos(AlphaRad),                    this.Joints[i].d},
                //                     { 0,                  0,                                        0,                                     1} };

                T_full = MatrixFunctions.MultiplyMatrix(T_full, T_hold);

                this.Origins[i + 1] = new Vector3((float)(-T_full[1, 3]), (float)(T_full[0, 3]), (float)(T_full[2, 3]));

                this.Joints[i].gameobject.transform.localPosition = new Vector3(-this.Origins[i][0], this.Origins[i][2], this.Origins[i][1]);//new Vector3((float)(T_full[0,0]), (float)(T_full[0, 1]), (float)(T_full[0, 2]));
                //this.Joints[i].gameobject.transform.localRotation = QuaternionFromMatrix(T_full);
                this.Joints[i].gameobject.transform.localRotation = Quaternion.Euler(TtoXYZ(T_full));
                if (i == this.Joints.Count - 1)
                {
                    Debug.Log(new Vector3((float)T_hold[0, 0], (float)T_hold[1, 1], (float)T_hold[2, 2]));
                }

                //this.Joints[i].gameobject.transform.Rotate(new Vector3((float)this.Joints[i].alpha,0,(float)this.Joints[i].theta));
            }

        }

        private Vector3 TtoXYZ(double[,] T)
        {
            //float X_angle = (float)Math.Atan2(T[2, 1], T[2, 2]);
            //float Y_angle = (float)Math.Atan2(-T[2, 0], Math.Sqrt(T[2, 1] * T[2, 1] + T[2, 2] * T[2, 2]));
            //float Z_angle = (float)Math.Atan2(T[1, 0], T[0, 0]);

            float Y_angle = -(float)Math.Asin(T[2, 0]);
            float X_angle = (float)Math.Atan2(T[2, 1] / Math.Cos(Y_angle), T[2, 2] / Math.Cos(Y_angle));
            float Z_angle = (float)Math.Atan2(T[1, 0] / Math.Cos(Y_angle), T[0, 0] / Math.Cos(Y_angle));

            float Rad2Deg = 180f / (float)Math.PI;

            return new Vector3(-Y_angle * Rad2Deg, Z_angle * Rad2Deg, X_angle * Rad2Deg);
        }
    }

    // Use this for initialization
    void Start () {
        Joint StarterJoint = new Joint("r", 0, 0, 0, 0, new Vector3(0,0,0));
        robot = new Robot(StarterJoint, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        robot.dhtf();
        foreach (Joint joint in robot.Joints)
        {
            joint.UpdateJointShape();
        }
	}

    public void AddRobotJoint()
    {
        robot.AddJoint();

        Vector3 ButtonPosTemp = AddJointButton.GetComponent<RectTransform>().anchoredPosition;
        ButtonPosTemp[1] = -(165 + 150 * (robot.Joints.Count-1));
        AddJointButton.GetComponent<RectTransform>().anchoredPosition = ButtonPosTemp;

        if (robot.Joints.Count > 6)
        {
            AddJointButton.gameObject.SetActive(false);
        }
    }
}
