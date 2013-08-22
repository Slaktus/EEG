using UnityEngine;
using System.Collections;

public class MouseCursorController : MonoBehaviour {
	
	private Transform thisTransform;
	private Transform cameraTransform;
	private Camera mainCamera;
		
	void Awake () {
		thisTransform = transform;
		cameraTransform = GameObject.FindGameObjectWithTag( "MainCamera" ).transform;
		mainCamera = cameraTransform.camera;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.position = mainCamera.ScreenToWorldPoint( Input.mousePosition );
	}
}
