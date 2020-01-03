using UnityEngine;
using System.Collections;

namespace Schiettent {
	public class LookAtMouse : MonoBehaviour {
		private Transform myTransform;
		private float fireRate = 0.3f;
		private float nextFire = 0;
		public float dir_updown;
		public float dir_leftright;
		// Update is called once per frame
		void Update () 
		{
			if (Time.time > nextFire) {
				//Get the Screen positions of the object
				//Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
				
				//Get the Screen position of the mouse
				Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);
				dir_updown = mouseOnScreen[1];
				dir_leftright = mouseOnScreen[0];
				//print (mouseOnScreen);
				changeModel();
				//Get the angle between the points
				//float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);
				//print (" " + positionOnScreen + " " + mouseOnScreen + " " + angle);
				//Ta Daaa
				//transform.rotation =  Quaternion.Euler (new Vector3(angle,0f,0f));
				nextFire = Time.time + fireRate;
			}

		}

		void changeModel() {
			float posX = 0;
			float posY = 0;

			if (dir_leftright < 0.5) {
				posY = 90 - (dir_leftright * 10);
			}
			if (dir_leftright > 0.5) {
				posY = 90 + (dir_leftright * 10);

			}
			posX = 90;

			myTransform.Rotate(new Vector3(posX, posY, 0.0f));
		}

		void SetInitialReferences() {
			myTransform = transform;
		}
		
		float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
			return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
		}
	}
}



	