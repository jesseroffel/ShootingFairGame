using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class MoveObject : MonoBehaviour {
		public string m_Direction;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void SetInitialReferences() {
			if (m_Direction == null) { Debug.LogWarning("Mission Direction"); } 
		}


		void OnTriggerEnter(Collider other) {
			if (m_Direction != null) {
				switch (m_Direction) {
				case "left":
					other.gameObject.transform.position -= new Vector3(13f,0f,0f);
					break;
				case "right":
					other.gameObject.transform.position += new Vector3(13f,0f,0f);
					break;
				default:
					break;
					
				}
			}
		}
	}

}