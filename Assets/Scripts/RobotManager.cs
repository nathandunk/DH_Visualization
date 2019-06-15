using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
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

        Vector3 ButtonPosTemp = AddJointButton.GetComponent<RectTransform>().anchoredPosition;
        ButtonPosTemp[1] = -(165 + 150 * (robot.Joints.Count-1));
        AddJointButton.GetComponent<RectTransform>().anchoredPosition = ButtonPosTemp;

        if (robot.Joints.Count > 6)
        {
            AddJointButton.gameObject.SetActive(false);
        }
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
                    Debug.Log(MotionProfile_.MotionProfileLines[i].Length);
                    Robot_.Joints[j].pannel.InputFieldList[3].text = MotionProfile_.MotionProfileLines[i][j+1].ToString();
                }
                return;
            }
        }
        MotionProfile_.Executing = false;
    }
}
