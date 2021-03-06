﻿using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class CameraPositioner : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (VRDevice.isPresent)
        {
            // when running on hololens, we ARE the camera and we are always on position 0, 0, 0
            Camera.main.transform.position = new Vector3(0, 0, 0);
            Camera.main.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            Camera.main.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
        }
        else
        {
            Camera.main.transform.position = new Vector3(0.3f, 1.0f, 1.4f);
            Camera.main.transform.localEulerAngles = new Vector3(70.0f, -30.0f, -30.0f);
            Camera.main.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }
}
