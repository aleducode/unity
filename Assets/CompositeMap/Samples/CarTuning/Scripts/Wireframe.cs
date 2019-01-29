using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour {



	void Start () {
		if (SystemInfo.graphicsShaderLevel < 40)
			gameObject.SetActive(false);
	}
	
	void OnGUI () {
		Vector2 Size = new Vector2 (180, 16);
		Vector2 Space = new Vector2 (10, 10);
		Rect GUIRect = new Rect ();
		GUIRect.size = Size;
		GUIRect.position = new Vector2 (Space.x, Screen.height-Size.y - Space.y);

		MeshRenderer mr = GetComponent<MeshRenderer> ();

		bool v = mr.enabled;

		v = GUI.Toggle (GUIRect, v, "Wireframe");
		if (v != mr.enabled) {
			mr.enabled = v;
			foreach(MeshRenderer cmr in GetComponentsInChildren<MeshRenderer>()){
				cmr.enabled = v;
			}
		}


	}
}
