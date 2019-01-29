using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float velocidad;
    // Update is called once per frame
    void Update ()
	{
		transform.Rotate (new Vector3 (0, -velocidad, 0 ) * Time.deltaTime);
	}
}
