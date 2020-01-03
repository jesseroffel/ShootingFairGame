using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Schiettent
{
	public class HandleClock : MonoBehaviour {

		public HandleGame Game;
		public AudioSource AudioPlayer;
		public AudioClip tick;
		public AudioClip tock;
		public Text TextUI;

		public int seconds = 0;
		public bool active = false;
		public bool timereset = false;
        public bool timeup = false;

		private int startSeconds = 60;
		private bool ticktock = true;
		private float speedTimer = 1.0f;
		private float nextTick = 0;
		private int SugarStart = 99;
		private int SugarStartTime = 0;
		private int SugarEndTime = 99;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
			setClock ();
			if (AudioPlayer == null) {
				AudioPlayer = new AudioSource();
			}
		}
		
		// Update is called once per frame
		void Update () {
			UpdateClock ();
		}

		void SetInitialReferences() {
			if (Game == null) { Debug.LogError("Missing reference: Game"); }
			if (AudioPlayer == null) { Debug.LogError("Missing reference: AudioPlayer"); } 
			if (tick == null) { Debug.LogError("Missing reference: tick audio"); } 
			if (tock == null) { Debug.LogError("Missing reference: tock audio"); } 
			if (TextUI == null) { Debug.LogError("Missing reference: TextUI"); } 
		}

		void UpdateClock() {
			if (active) {
				if (Time.time > nextTick) {
					if (Game.m_SugarRushThisRound && SugarStart == 99 && SugarEndTime == 99) 
					{ 
						SugarStart = Game.GetSugarRushTime(); 
						//Debug.Log("Seconds: " + seconds +  ", sugarstart: " + SugarStart + " sugarend: " + SugarEndTime);
					}

					if (seconds == 0) {
						setNewTime();
                        timeup = true;
                        active = false;
					}
					if (seconds > 0) {
						setNewTime();
						playSound();
						nextTick = Time.time + speedTimer;
					}
					if (seconds == SugarStart) {
						Game.StartSugarRush();
						SugarStartTime = seconds;
						SugarEndTime = SugarStartTime - 21;
					}
					if (seconds == SugarEndTime) {
						Game.EndSugarRush();
					}
				}
			}

			if (timereset) {
				setClock();
				timereset = false;
			}
		}

        public void NewRound()
        {
            timereset = true;
        }

		void setNewTime() {
			int newtime = seconds--;
			TextUI.text = ""+newtime;
		}

		public void addTime(int amount) {
			int newtime = seconds + amount;
			seconds = newtime;
		}

        public void ResetClock()
        {
            seconds = startSeconds;
            timeup = false;
            active = false;
        }

		void setClock() {
			SugarStart = 99;
			SugarEndTime = 99;
			SugarStartTime = 0;
            timeup = false;

            if (seconds == 0 || seconds == -1) {
				seconds = startSeconds;
			}
			active = true;
		}

		void playSound() {
			if (ticktock) {
				AudioPlayer.PlayOneShot(tick, 1.0f);
				ticktock = false;
			} else {
				AudioPlayer.PlayOneShot(tock, 1.0f);
				ticktock = true;
			}
		}
	}
	
}