using UnityEngine;
using System.Collections;

namespace Schiettent 
{
	public class ObjectHandler : MonoBehaviour {
		public AudioSource AudioS;
		public AudioClip hitsound;
		public int points = 0;
		public int coins = 0;
		public bool isHit = false;
		public bool moving = false;
		public bool Special = false;
		public bool SugarRush = false;
		public float SugarSpeed = 0;
		public float speed;

		private bool m_Active = false;
		private int countdown = 2;
		private float timehit = 0;
		private Transform myTransform;
		[SerializeField]private string direction;

		private SpawnObjects r_SpawnObjects;


		void Start () {
			SetInitialReferences();
			InitObject();
		}

		void SetInitialReferences() {
			myTransform = transform;
			if (AudioS == null) { Debug.LogError("Missing reference: AudioSource"); }
			if (hitsound == null) { Debug.LogError("Missing reference: hitsound"); }
			r_SpawnObjects = myTransform.parent.GetComponent<SpawnObjects> ();
		}

		void Update () {
			if (m_Active) {
				if (isHit) {
					if (Time.time > timehit) {
						//changeActive(false);
						shrinkobject();
					}
				} else {
					getDirectionInformation();
					MoveObject();
				}
			}
		}

		void getDirectionInformation(){
			if (r_SpawnObjects) {
				direction = r_SpawnObjects.direction;
			}
		}

		public void SetSpeed(float newspeed) {
			if (SugarRush) {
				SugarSpeed = newspeed;
			} else {
				speed = newspeed;
			}
		}

		void MoveObject() {
			switch (direction) {
			case "Left":
				if (moving == false) {moving = true;}
				if (SugarRush) {
					if (SugarSpeed > 0) { myTransform.position -= new Vector3(SugarSpeed,0f,0f);}
				} else {
					if (speed > 0) { myTransform.position -= new Vector3(speed,0f,0f);}
				}
				break;
			case "Right":
				if (moving == false) {moving = true;}
				if (SugarRush) {
					if (SugarSpeed > 0) { myTransform.position += new Vector3(SugarSpeed,0f,0f);}
				} else {
					if (speed > 0) { myTransform.position += new Vector3(speed,0f,0f);}
				}
				break;
			default:
				if (moving == true) {moving = false;}
				break;
			}
		}

		void InitObject() {
			m_Active = true;
		}

		void resetStats() {
			isHit = false;
			Special = false;
			changeActive (false);
		}

		public void SetHit(bool state) {
			if (state) {
				isHit = true;
				timehit = Time.time + countdown;
				gameObject.GetComponent<BoxCollider>().size = new Vector3(0.0f, 0.0f, 0.0f);
				if (AudioS) {
					if (hitsound) {
						AudioS.PlayOneShot(hitsound, 0.5f);
					}
				}
			}
		}

		void shrinkobject() {
			if (myTransform.localScale.x > 0.1f && myTransform.localScale.y > 0.1f && myTransform.localScale.z > 0.1f) {
				myTransform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
			} else {
				Destroy (gameObject);
			}
		}


		
		void changeActive(bool state) {
			m_Active = state;
			if (m_Active) {
				myTransform.gameObject.SetActive(true);
			} else {
				myTransform.gameObject.SetActive(false);
			}
		}
	}
}
