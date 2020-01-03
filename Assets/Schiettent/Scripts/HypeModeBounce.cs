using UnityEngine;
using System.Collections;

public class HypeModeBounce : MonoBehaviour {
	Transform myTransform;
	bool shrink = true;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Bounce();
	}

	void Bounce() {
		if (shrink) {	
			if (myTransform.localScale.x > 0.8f && myTransform.localScale.y > 0.8f && myTransform.localScale.z > 0.8f) {
				myTransform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
			} else {
				shrink = false;
			}
		} else {
			if (myTransform.localScale.x < 1.0f && myTransform.localScale.y < 1.0f && myTransform.localScale.z < 1.0f) {
				myTransform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
			} else {
				shrink = true;
			}
		}
	}
}
