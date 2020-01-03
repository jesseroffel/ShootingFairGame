using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPlay : MonoBehaviour {

	public void LoadNewScene() {
		SceneManager.LoadScene("game");
	}
}
