using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

public class JointPanel
    {
        Button TranslationalButton;
        Button RotationalButton;
		GameObject MasterGameObject;
        Joint CorrespondingJoint;
        GameObject gameobject;
        int JointNumber;
        public List<Slider>     SliderList     = new List<Slider>(); // order alpha, a, d, theta
        public List<InputField> InputFieldList = new List<InputField>(); // order alpha, a, d, theta
        string JointName = "<b>Joint 1:</b>";

        public JointPanel(Joint joint_, int JointNumber_)
        {
            this.JointNumber = JointNumber_;
            CorrespondingJoint = joint_;
            GameObject NewPanel = GameObject.Instantiate(Resources.Load("JointPanel", typeof(GameObject)), GameObject.Find("Panel").transform) as GameObject;
            this.gameobject = NewPanel;
            this.JointName = "<b>Joint " + (this.JointNumber+1).ToString() + ":</b>";

            this.gameobject.transform.Find("JointName").GetComponent<Text>().text = this.JointName;

            // Get the slider and input field and sync them
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

            Button rot_button_temp   = this.gameobject.transform.Find("JointType").Find("Rotational").gameObject.GetComponent<Button>();
            Button trans_button_temp = this.gameobject.transform.Find("JointType").Find("Translational").gameObject.GetComponent<Button>();

            rot_button_temp.onClick.AddListener(delegate{ChangeJointType(CorrespondingJoint, "r", rot_button_temp, trans_button_temp); });
            trans_button_temp.onClick.AddListener(delegate{ChangeJointType(CorrespondingJoint, "t", trans_button_temp, rot_button_temp); });
        }

        public void ChangeJointType(Joint joint_, string joint_type, Button new_button, Button old_button){
            joint_.jointtype = joint_type;
            new_button.GetComponent<Image>().color = Color.green;
            old_button.GetComponent<Image>().color = Color.white;
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
