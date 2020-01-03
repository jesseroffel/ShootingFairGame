using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class BulletHit : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionEnter (Collision col)
		{
			if (col.gameObject.tag == "Prop") 
			{
				Destroy (col.gameObject);
			}
		}
	}
}


