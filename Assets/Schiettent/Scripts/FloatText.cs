using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

namespace Schiettent
{

	public class FloatText : MonoBehaviour {
		public TextMesh textObject;
		public string m_Text = "";

		private bool m_Active = false;
		private float m_StartYPos = 0;
		private Color m_Color = Color.clear;
		private Transform myTransform;
 


		void SetInitialReferences() {
			if (textObject == null) { Debug.LogError("Missing reference: textObject"); } 
		}

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
			myTransform = transform;
		}
		
		// Update is called once per frame
		void Update () {
			if (m_Active) {
				MovePosition();
			}
		}

		public void setProperties(string text, string color, Transform startpos) {
			SetText (text);
			SetColor (color);
			transform.position = new Vector3(startpos.position.x, startpos.position.y, startpos.position.z);
			m_Active = true;
			m_StartYPos = startpos.position.y;
		}

		void MovePosition() {
			if (myTransform.position.y > (m_StartYPos + 2)) {
				FadeOut();
			} else {
				myTransform.position += new Vector3(0f, 0.025f, 0f);
			}
		}

		void SetText(string entrytext) {
			m_Text = entrytext;
			textObject.text = entrytext;
		}

		void SetColor(string setcolor) {
			switch (setcolor) {
			case "red":
			{
				m_Color = Color.red;
				break;
			}
			case "yellow": {
				m_Color = Color.yellow;
				break;
			}
			case "green": {
				m_Color = Color.green;
				break;
			}
			case "blue": {
				m_Color = Color.blue;
				break;
			}
			case "purple": {
				m_Color = Color.magenta;
				break;
			}
			case "cyan": {
				m_Color = Color.cyan;
				break;
			}
			case "black": {
				m_Color = Color.black;
				break;
			}
			case "white": {
				m_Color = Color.white;
				break;
			}
			default:
			{
				m_Color = Color.white;
				break;
			}
			}

			textObject.color = m_Color;
		}

		void FadeOut() {
			if (myTransform.localScale.x > 0.1f && myTransform.localScale.y > 0.1f && myTransform.localScale.z > 0.1f) {
				myTransform.localScale -= new Vector3(0.05f, 0.01f, 0.05f);
			} else {
				Destroy (gameObject);
			}
		}

		void RainbowEffect() {

		}
	}

}