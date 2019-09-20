using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class RobotManager : MonoBehaviour {

    Robot robot;
    public Button AddJointButton;

    MotionProfile RobMotionProfile;

    static public Vector3 robot_center;

    // Use this for initialization
    void Start () {
        Joint StarterJoint = new Joint("r", 0, 0, 0, 0, new Vector3(0,0,0));
        robot = new Robot(StarterJoint, gameObject);

        // robot = 


        RobMotionProfile = new MotionProfile();

        RobMotionProfile.LoadProfile();
	}
	
	// Update is called once per frame
	void Update () {
        robot.dhtf();

        foreach (Joint joint in robot.Joints)
        {
            joint.UpdateJointShape();
        }
        
        if (RobMotionProfile.Executing){
            UpdateProfile(RobMotionProfile, robot);
        }
	}

    public void AddRobotJoint()
    {
        robot.AddJoint();

        // Vector3 ButtonPosTemp = AddJointButton.GetComponent<RectTransform>().anchoredPosition;
        // ButtonPosTemp[1] = -(165 + 150 * (robot.Joints.Count-1));
        // AddJointButton.GetComponent<RectTransform>().anchoredPosition = ButtonPosTemp;

        // if (robot.Joints.Count > 6)
        // {
        //     AddJointButton.gameObject.SetActive(false);
        // }
    }

    public void AddRobotJoint(Joint joint_)
    {
        robot.AddJoint(joint_);

    //     Vector3 ButtonPosTemp = AddJointButton.GetComponent<RectTransform>().anchoredPosition;
    //     ButtonPosTemp[1] = -(165 + 150 * (robot.Joints.Count-1));
    //     AddJointButton.GetComponent<RectTransform>().anchoredPosition = ButtonPosTemp;

    //     if (robot.Joints.Count > 6)
    //     {
    //         AddJointButton.gameObject.SetActive(false);
    //     }
    }

    public void StartProfile()
    {
        RobMotionProfile.ExecuteProfile();
    }

    private void UpdateProfile(MotionProfile MotionProfile_, Robot Robot_)
    {
        float TargetTime = Time.time-MotionProfile_.StartTime;
        for (int i = 0; i < MotionProfile_.MotionProfileLines.Count; i++)
        {
            if (MotionProfile_.MotionProfileLines[i][0] >= TargetTime){
                for (int j = 0; j < MotionProfile_.MotionProfileLines[i].Length-1; j++)
                {
                    if (Robot_.Joints[j].jointtype == "r")
                    {
                        Robot_.Joints[j].pannel.InputFieldList[3].text = MotionProfile_.MotionProfileLines[i][j+1].ToString();
                    }
                    else
                    {
                        Robot_.Joints[j].pannel.InputFieldList[2].text = MotionProfile_.MotionProfileLines[i][j+1].ToString();
                    }
                }
                return;
            }
        }
        MotionProfile_.Executing = false;
    }

    // public void ReadRobotFile(){
    //     string dataAsJson = File.ReadAllText(@"Robots/Robot1.json");    
    //     // Pass the json to JsonUtility, and tell it to create a GameData object from it
    //     RobotJson LoadedRobot = JsonUtility.FromJson<RobotJson>(dataAsJson);
    //     List<Joint> JointList = new List<Joint>();
    //     Robot newRobot;
    //     foreach (var LoadedJoint in LoadedRobot.Joints)
    //     {
    //         AddRobotJoint(new Joint(LoadedJoint.JointType,LoadedJoint.alpha,LoadedJoint.a,LoadedJoint.d,LoadedJoint.theta,new Vector3(0,0,0)));
    //     }
    // }
}
