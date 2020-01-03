using UnityEngine;
using System.Collections;

namespace Schiettent
{
    public class NextRound : MonoBehaviour
    {
        public HandleGame Game;

        public void GoToNextRound()
        {
            Game.NextRound();
        }
    }

}
