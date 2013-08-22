using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public GameObject targetPart;
	public Transform parentTarget;
	
	private void SetCarPartColors ( Color rgbValue ) {
		targetPart.GetComponent< ColorController >().SetMaterialColor( rgbValue );
	}
	
	private Dictionary< string , float > partColorDictionary = new Dictionary< string , float >();
	
	public void GeneratePartColorDictionary ( GameObject[] carParts ) {
		for ( int i = 0 ; i < carParts.Length ; i++ ) {
			partColorDictionary.Add( ( string ) carParts[ i ].name , 0.0f );
		}

	}
	
	public float sliderBottomOffset;
	public float sliderSidePadding;
	
	private float hSliderValue = 0.0f;
	private float bufferedSliderValue = 0.0f;
	
	private void HorizontalSliderControl () {
		if ( targetPart.transform != parentTarget ) {
			hSliderValue = partColorDictionary[ targetPart.name ];
			bufferedSliderValue = hSliderValue;
			float sliderLength = Screen.width - ( sliderSidePadding * 2 );
			hSliderValue = GUI.HorizontalSlider( new Rect ( ( Screen.width / 2 ) - ( sliderLength / 2 ) , Screen.height - sliderBottomOffset , sliderLength , 50 ) , hSliderValue, 0.0f , 1.0f );
			Color newColor = Color.Lerp( Color.red , Color.green , hSliderValue );
			newColor.a = 155.0f;
			if ( hSliderValue != bufferedSliderValue ) { 
				SetCarPartColors( newColor );
				partColorDictionary[ targetPart.name ] = hSliderValue;
			}
		}
	}

	void OnGUI () {
		HorizontalSliderControl();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
