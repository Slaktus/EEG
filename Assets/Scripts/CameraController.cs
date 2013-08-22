using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
	
	public Transform parentTarget;
	public float distanceFromTarget;
	
	private Transform thisTransform;
	private Vector3 initialPosition;
	private Transform targetTransform;
	private UIController uiControllerScript;

	void Awake() {
		thisTransform = transform;
		targetTransform = carParts[ 0 ].transform;
		thisTransform.rotation = Quaternion.LookRotation( Vector3.zero - thisTransform.position );
		initialPosition = thisTransform.position;
		GenerateCarPartDictionary();
		uiControllerScript = gameObject.GetComponent< UIController >();
		uiControllerScript.GeneratePartColorDictionary( carParts );
		uiControllerScript.parentTarget = parentTarget;
	}
	
	public GameObject[] carParts;
	public float[] distances;
		
	private Dictionary< string , Vector4 > carPartDictionary = new Dictionary< string , Vector4 >();
	
	private void GenerateCarPartDictionary () {
		for ( int i = 0 ; i < carParts.Length ; i++ ) {
			//Not a good idea. The Vector4 holds X, Y and Z plus camera distance -- but it turns out we need height and probably others. Better to do a linked list
			Vector4 cameraPosition = new Vector4( carParts[ i ].transform.position.x , carParts[ i ].transform.position.y , carParts[ i ].transform.position.z , distances[ i ] );
			carPartDictionary.Add( ( string ) carParts[ i ].name , cameraPosition );
		}
	}
		
	public float xRotationSpeed;
	public float yRotationSpeed;
	public float resetRotationSpeed;
	public float positioningRotationSpeed;
			
	private void RotateCamera ( Vector3 mouseDisplacement ) {
		//Look at target
		thisTransform.rotation = Quaternion.Slerp( thisTransform.rotation , Quaternion.LookRotation( ( targetTransform.position - thisTransform.position ) + ( thisTransform.InverseTransformPoint( Vector3.zero ) - thisTransform.position ) ) , Time.deltaTime * positioningRotationSpeed );
		if ( Input.GetMouseButton( 1 ) && targetTransform != parentTarget ) {
			//Fetch relative direction to target
			//Vector3 relativeDirection = targetTransform.TransformDirection( targetTransform.position - thisTransform.position );
			//Rotate along Y axis
			thisTransform.RotateAround( targetTransform.position , thisTransform.TransformDirection( Vector3.right ) , ( mouseDisplacement.y * yRotationSpeed ) * Time.deltaTime );
			//Rotate along X axis
			thisTransform.RotateAround( targetTransform.position , thisTransform.TransformDirection( Vector3.up ) , ( mouseDisplacement.x * yRotationSpeed ) * Time.deltaTime );
		} else thisTransform.position = Vector3.Slerp( thisTransform.position , initialPosition , resetRotationSpeed * Time.deltaTime );

		
	}
	
	public float cameraPositioningSpeed;
	
	private void PositionCamera () {
		if ( !Input.GetMouseButton( 1 ) && targetTransform != parentTarget ) {
			Vector3 newCameraPosition = Vector3.Slerp( thisTransform.position , new Vector3( carPartDictionary[ targetTransform.name ].x ,  carPartDictionary[ targetTransform.name ].y , carPartDictionary[ targetTransform.name ].z ) + ( parentTarget.TransformDirection( Vector3.back + ( Vector3.down * 2 ) ) * carPartDictionary[ targetTransform.name ].w ) + targetTransform.position , Time.deltaTime * cameraPositioningSpeed );
			thisTransform.position = newCameraPosition;
		}
	}
	
	private Vector3 bufferedMousePosition;
	private Vector3 mouseDisplacement;
	
	private Vector3 DetermineMouseDisplacement () {
		//Check mouse displacement since last update
		mouseDisplacement = bufferedMousePosition - Input.mousePosition;
		//Buffer mouse position
		bufferedMousePosition = Input.mousePosition;
		return mouseDisplacement;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if ( Input.GetKeyUp( KeyCode.Alpha1 ) ) targetTransform = carParts[ 0 ].transform; uiControllerScript.targetPart = targetTransform.gameObject;
		if ( Input.GetKeyUp( KeyCode.Alpha2 ) ) targetTransform = carParts[ 1 ].transform; uiControllerScript.targetPart = targetTransform.gameObject;
		if ( Input.GetKeyUp( KeyCode.Alpha3 ) ) targetTransform = carParts[ 2 ].transform; uiControllerScript.targetPart = targetTransform.gameObject;
		if ( Input.GetKeyUp( KeyCode.Alpha4 ) ) targetTransform = carParts[ 3 ].transform; uiControllerScript.targetPart = targetTransform.gameObject;
		if ( Input.GetKeyUp( KeyCode.Alpha5 ) ) targetTransform = parentTarget; uiControllerScript.targetPart = targetTransform.gameObject;
		RotateCamera( DetermineMouseDisplacement() );
		PositionCamera();
	}
}
