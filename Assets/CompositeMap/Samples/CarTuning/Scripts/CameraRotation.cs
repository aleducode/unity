using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public float X;
	public float Y;
	public float Speed = 0.2f;
	public float MaxY = 60f;
	public float MinY = -10f;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, X, 0) * Quaternion.Euler (Y, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (Event.current.type == EventType.MouseDrag) {
			X = (X+Speed*Event.current.delta.x+360f)%360f;
			Y = Mathf.Clamp(Y+Speed*Event.current.delta.y,MinY,MaxY);
		}


		if (Event.current.type == EventType.ScrollWheel) {
			transform.localScale =  transform.localScale*(Mathf.Pow(0.99f,-Event.current.delta.y)); 
		}

		transform.rotation = Quaternion.Euler (0, X, 0) * Quaternion.Euler (Y, 0, 0);

	}

}
