using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class HandleRows : MonoBehaviour {

		public Transform spawnRow;
		
		private Transform myTransform;
		private int RowCount = 3;

		// Use this for initialization


		void Start () {
			SetInitialReferences();
			SpawnFirstRows ();
		}

		void SetInitialReferences() {
			myTransform = transform;
			if (spawnRow == null) { Debug.LogError("Missing reference: spawnRow"); }
		}

		void SpawnFirstRows() {
			for (int i = 0; i < RowCount; ++i) {
				Transform newRow = Instantiate(spawnRow);
				newRow.transform.parent = myTransform.transform;
				newRow.GetComponent<SpawnObjects>().setRowNumber(i);
			}
		}

		//void spaw
	}

}


