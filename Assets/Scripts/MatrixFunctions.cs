﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public double[,] MultiplyMatrix(double[,] A, double[,] B)
    {
        int rA = A.GetLength(0);
        int cA = A.GetLength(1);
        int rB = B.GetLength(0);
        int cB = B.GetLength(1);
        double temp = 0;
        double[,] kHasil = new double[rA, cB];
        if (cA != rB)
        {
            Debug.Log("matrix can't be multiplied !!");
            double[,] badmat = { { 1, 0 }, { 0, 1 } };
            return badmat;
        }
        else
        {
            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cB; j++)
                {
                    temp = 0;
                    for (int k = 0; k < cA; k++)
                    {
                        temp += A[i, k] * B[k, j];
                    }
                    kHasil[i, j] = temp;
                }
            }
            return kHasil;
        }
    }
}
