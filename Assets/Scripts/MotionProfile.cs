using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

	public class MotionProfile{
        // int num_joints;
        // double sample_rate;
        // double max_time;
        public float StartTime = 0;

        public bool Executing = false;

        private List<string[]> MotionProfileLinesString = new List<string[]>();
        public List<float[]> MotionProfileLines = new List<float[]>();

        public void LoadProfile(){
            string[] MotionProfileCSV = System.IO.File.ReadAllLines(@"MotionProfiles\Profile1.csv");

            foreach (string line in MotionProfileCSV){
                MotionProfileLinesString.Add(line.Trim().Split(","[0]));

                float[] hold = {0,0,0,0};
                int i = 0;

                foreach (var item in MotionProfileLinesString[MotionProfileLinesString.Count-1])
                {
                    try
                    {
                        hold[i] = float.Parse(item);
                        i++;
                    }
                    catch (SystemException)
                    {
                        
                    }
                }
                MotionProfileLines.Add(hold);
            }
        }

        public void ExecuteProfile(){
            StartTime = Time.time;
            Executing = true;
        }
    }
