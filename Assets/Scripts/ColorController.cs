using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {
	
	private Material thisMaterial;
	
	void Awake () {
		thisMaterial = renderer.material;
	}
	
	public void SetMaterialColor ( Color rgbValue ) {
		thisMaterial.SetColor( "_Color" , rgbValue );
		//rgbValue /= 2;
		//thisMaterial.SetColor( "_ReflectColor" , rgbValue );
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	public Color materialColor;
	
	// Update is called once per frame
	void Update () {
	}
}
