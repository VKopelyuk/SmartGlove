using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ControlHandByPaxPowerGloveAndMPU9150InUnity : MonoBehaviour {
	
	private static bool _continue;
	private static Quaternion _handQuaternion = new Quaternion();
	float accel_x,accel_y,accel_z;
	private static float[] _fingerVal = new float[5];
	private GameObject[,] _fingerObject = new GameObject[5,3];
	private GameObject hand;
	public OSC osc;


	void Start() {
		_fingerObject[0,2] = GameObject.Find ("Index0");
		_fingerObject[0,1] = GameObject.Find ("Index0/Index1");
		_fingerObject[0,0] = GameObject.Find ("Index0/Index1/Index2");
		_fingerObject[1,2] = GameObject.Find ("Middle0");
		_fingerObject[1,1] = GameObject.Find ("Middle0/Middle1");
		_fingerObject[1,0] = GameObject.Find ("Middle0/Middle1/Middle2");
		//_fingerObject[2,2] = GameObject.Find ("Ring0");
		//_fingerObject[2,1] = GameObject.Find ("Ring0/Ring1");
		//_fingerObject[2,0] = GameObject.Find ("Ring0/Ring1/Ring2");
		//_fingerObject[3,2] = GameObject.Find ("Pinky0");
		//_fingerObject[3,1] = GameObject.Find ("Pinky0/Pinky1");
		//_fingerObject[3,0] = GameObject.Find ("Pinky0/Pinky1/Pinky2");
		//_fingerObject[4,2] = GameObject.Find ("Thumb0");
		//_fingerObject[4,1] = GameObject.Find ("Thumb0/Thumb1");
		//_fingerObject[4,0] = GameObject.Find ("Thumb0/Thumb1/Thumb2");

		hand = GameObject.Find ("Hand");
		_continue = true;
		osc.SetAddressHandler("/mpu6050", OnReceiveXYZ );

	}

	void Update() {
		//hand.transform.localRotation = Quaternion.Euler (_handQuaternion.x, _handQuaternion.y, _handQuaternion.z);
		hand.transform.rotation=Quaternion.Euler(accel_z,0,0);
		//_fingerObject[0,2].transform.localRotation = Quaternion.Euler (_handQuaternion.x, -40, 60);
		//_fingerObject[0,1].transform.localRotation = Quaternion.Euler (_handQuaternion.x, 0, 0);
		//_fingerObject[0,].transform.localRotation = Quaternion.Euler (0, -_fingerVal [0], 0);
		_fingerObject[0,2].transform.localRotation = Quaternion.Euler (0, -_fingerVal [0], 0);
		_fingerObject[0,1].transform.localRotation = Quaternion.Euler (0, -_fingerVal [0], 0);
		_fingerObject[0,0].transform.localRotation = Quaternion.Euler (0, -_fingerVal [0], 0);
		//_fingerObject[1,2].transform.localRotation = Quaternion.Euler (0, -_fingerVal [2], 0);
		//_fingerObject[1,1].transform.localRotation = Quaternion.Euler (0, -_fingerVal [2], 0);
		//_fingerObject[1,0].transform.localRotation = Quaternion.Euler (0, -_fingerVal [2], 0);
		//_fingerObject[3,2].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
		//_fingerObject[3,1].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
		//_fingerObject[3,0].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
		//_fingerObject[4,2].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
		//_fingerObject[4,1].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
		//_fingerObject[4,0].transform.localRotation = Quaternion.Euler (_fingerVal [3], 0, 0);
	}

	void OnReceiveXYZ(OscMessage message){
		accel_x = message.GetFloat(0);
		accel_y = message.GetFloat(1);
		accel_z = message.GetFloat(2);
		_fingerVal [0] = message.GetFloat (3);
		//_fingerVal [1] = message.GetFloat (5);
		//_fingerVal [2] = message.GetFloat (6);
		//_fingerVal [3] = message.GetFloat (7);
		//_fingerVal [4] = message.GetFloat (8);
		//_handQuaternion.Set (x, y, z, 0);
		//UnityEngine.Debug.Log ("x: " + _fingerVal[0]+"y: "+z);


	}

	void OnApplicationQuit() {
		_continue = false;
	}


}
