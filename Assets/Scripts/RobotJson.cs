using System;

[Serializable]
public class RobotJson
{
    public JointJson[] Joints;
}

[Serializable]
public class JointJson
{
    public string JointType;
    public float alpha;
    public float a;
    public float d;
    public float theta;
}
