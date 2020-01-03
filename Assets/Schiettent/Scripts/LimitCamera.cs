using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class LimitCamera : MonoBehaviour {
		Transform myTransform;
		float yPos;

		void Start() {
			myTransform = transform;
            Cursor.lockState = CursorLockMode.Locked;
        }

		// Update is called once per frame
		void LateUpdate () {
			yPos = myTransform.eulerAngles.y;
			if (yPos > 60 && yPos < 180) {
				yPos = 60;
			}
			if (yPos > 180.1f && yPos < 300) {
				yPos = 300;
			}
			myTransform.eulerAngles  = new Vector3 (0, yPos, 0);
		}
	}

}