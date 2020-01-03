using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Schiettent
{
    public class EndGame : MonoBehaviour {

        public void GoToMenu()
        {
            SceneManager.LoadScene("menu");
            // HIGHSCORE SHIZZLE
        }
    }
}