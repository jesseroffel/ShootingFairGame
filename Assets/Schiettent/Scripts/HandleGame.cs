using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Schiettent
{
	public class HandleGame : MonoBehaviour {
        public HandleClock clock;
        public HandlePlayer Player;
		public AudioSource audios;
		public AudioClip bgm;
		public AudioClip sugar;
		public GameObject EndScreen;
		public GameObject RoundScreen;
		public GameObject HUD;
        public Text ShopText;
        public Text RoundText;
        public Text EndPointsText;
        public Text EndCoinsText;
        public Text EndScoreText;

        public int m_totalScore = 0;
		public int m_totalPoints = 0;
		public int m_totalCoins = 0;

		public int m_currentScore = 0;
		public int m_currentPoints = 0;
		public int m_currentCoins = 0;

		public bool m_GameRunning = true;


		private bool m_SugarRush = false;
		public bool m_SugarRushThisRound = false;

		private int m_SugarRushTime = 0;

		private int m_Round = 0;
		public bool m_RoundOver = false;

        private float nextTick = 0;
        private float speedTimer = 0.25f;

		void Start() {
			SetInitialReferences();
			audios.PlayOneShot(bgm);
			StartNewRound();
		}

        void Update()
        {
            if (Time.time > nextTick)
            {
                nextTick = Time.time + speedTimer;
                if (m_RoundOver == false&& m_GameRunning)
                {
                    if (Player.m_OutOfAmmo)
                    {
                        Debug.Log("Round ended because of Player Out Of Ammo");
                        EndRound();
                    }
                    if (clock.timeup)
                    {
                        Debug.Log("Round ended because of Time ran out");
                        EndRound();
                    }
                }
                    
               
                        
            }
        }

		void SetInitialReferences() {
			if (Player == null) { Debug.LogError("Missing reference: Player"); }
			if (audios == null) { Debug.LogError("Missing reference: audios"); }
			if (bgm == null) { Debug.LogError("Missing reference: bgm"); }
			if (sugar == null) { Debug.LogError("Missing reference: sugar"); }
			if (EndScreen == null) { Debug.LogError("Missing reference: EndScreen"); }
			if (RoundScreen == null) { Debug.LogError("Missing reference: RoundScreen"); }
			if (HUD == null) { Debug.LogError("Missing reference: HUD"); }
            if (ShopText == null) { Debug.LogError("Missing reference: ShopText"); }
            if (RoundText == null) { Debug.LogError("Missing reference: RoundText"); }
            if (clock == null) { Debug.LogError("Missing reference: clock"); }
            if (EndPointsText == null) { Debug.LogError("Missing reference: EndPointsText"); }
            if (EndCoinsText == null) { Debug.LogError("Missing reference: EndCoinsText"); }
            if (EndScoreText == null) { Debug.LogError("Missing reference: EndScoreText"); }
        }

		void SetupGame() {
            Player.StartNewRound();
            SetClock();
		}

        void SetClock()
        {
            clock.NewRound();
        }

		public void StartNewRound() {
			m_Round++;
			CalcSugarRush();
            Player.m_PlayerMovement = false;
            Debug.Log("Round " + m_Round + " started!");
        }

		void CalcSugarRush() {
			float rand = Random.value;
			if (rand >= 0.5) {
				m_SugarRushThisRound = true;
				m_SugarRushTime = Random.Range(25,40);
				Debug.Log("Hype mode this round at: " + m_SugarRushTime + " seconds.");
			}
			else 
				m_SugarRushThisRound = false;
		}

		public int GetSugarRushTime() {
			return m_SugarRushTime;
		}

		public void EndRound() {
            m_SugarRush = false;
            clock.ResetClock();
            Debug.Log("Round " + m_Round + " ended!");
            m_GameRunning = false;
            Player.m_PlayerMovement = false;
            m_RoundOver = true;
            AddTotalScores();
            if (Player.m_OutOfAmmo) { Player.m_OutOfAmmo = false; }
            if (m_Round == 5)
            {
                AddTotalScores();
                CalculateEndScore();
                EndPointsText.text = m_totalPoints + "";
                EndCoinsText.text = m_totalCoins + "";
                EndScoreText.text = m_totalScore + "";
                EndScreen.SetActive(true);
            }
            else
            {
                ShopText.text = m_totalCoins + "";
                RoundText.text = "ROUND " + m_Round + " OVER";
                RoundScreen.SetActive(true);
            }
                
            HUD.SetActive(false);
		}

        public void NextRound()
        {
            if (audios.isPlaying == false)
            {
                audios.PlayOneShot(bgm);
            }
            m_RoundOver = false;
            RoundScreen.SetActive(false);
            HUD.SetActive(true);
            StartNewRound();
            SetupGame();
            m_GameRunning = true;
        }

        private void CalculateEndScore()
        {
            int coinsscore = m_totalCoins * 3;
            int pointsscore = m_totalPoints * 2;
            m_totalScore = coinsscore + pointsscore / 5;
        }

		void AddTotalScores() {
			m_totalScore += m_currentScore;
			m_totalPoints += m_currentPoints;
			m_totalCoins += m_currentCoins;
            m_currentScore = 0;
            m_currentPoints = 0;
            m_currentCoins = 0;
        }


		public bool getSugarRushState() {
			return m_SugarRush;
		}

		public void SetSugarRush(bool state) {
			m_SugarRush = state;
		}

		public void StartSugarRush() {
			SetSugarRush(true);
			audios.Stop();
			audios.PlayOneShot(sugar, 0.75f);
			Debug.Log("Started sugarmode");
		}

		public void EndSugarRush() {
			SetSugarRush(false);
			audios.Stop();
			audios.PlayOneShot(bgm);
			Debug.Log("Stopped sugar mode");
		}

		public void AddHitPC(int points, int coins) {
			m_currentPoints += points;
			m_currentCoins += coins;
		}

	}
}
