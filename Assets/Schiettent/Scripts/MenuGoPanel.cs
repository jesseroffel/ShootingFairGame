using UnityEngine;
using System.Collections;

public class MenuGoPanel : MonoBehaviour {

	public GameObject NewPanel;
	private Transform ThisPanel;

	void Start() {
		ThisPanel = gameObject.transform.parent;
	}

	public void GoToNewPanel() {
		if (NewPanel) {
			NewPanel.SetActive(true);
			ThisPanel.gameObject.SetActive(false);
		} else {
			Debug.LogError("No reference found: NewPanel");
		}
	}
}
