using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Schiettent
{
	public class HandleHUD : MonoBehaviour {

		public HandlePlayer Player;
		public HandleGame Game;
		public bool p_OutOfAmmo = false;
		private float nextUpdate = 0;
		private float updateTick = 0.25f;

		private int Points = 0;
		private int Coins = 0;
		private int Bullets = 0;
		private int Magazines = 0;
		private bool Reloading = false;
		private bool sugarmode = false;

		[SerializeField] private Text t_Points;
		[SerializeField] private Text t_Coins;
		[SerializeField] private Text t_Bullets;
		[SerializeField] private Text t_Magazines;
		[SerializeField] private GameObject hud_reloading;
		[SerializeField] private GameObject hud_sugarrush;

		//void Start () {

		//}

		void checkReferences() {
			if (Player == null) { Debug.LogError("Missing reference: Player"); } 
			if (Game == null) { Debug.LogError("Missing reference: Game"); } 
			if (t_Points == null) { Debug.LogError("Missing reference: t_Points"); } 
			if (t_Coins == null) { Debug.LogError("Missing reference: t_Coins"); } 
			if (t_Bullets == null) { Debug.LogError("Missing reference: t_Bullets"); } 
			if (t_Magazines == null) { Debug.LogError("Missing reference: t_Magazines"); } 
			if (hud_reloading == null) { Debug.LogError("Missing reference: hud_reloading"); } 
		}
		
		// Update is called once per frame
		void Update () {
			if (Time.time > nextUpdate) {
				getInformation();
				changeHUD();
				nextUpdate = Time.time + updateTick;
			}
		}

		void getInformation() {
			if (Game) {
				if (Player) {
					Points = Game.m_currentPoints;
					Coins = Game.m_currentCoins;
					Bullets = Player.getBullets();
					Magazines = Player.getMagazines();
					Reloading = Player.getReloading();
				}
				sugarmode = Game.getSugarRushState();
			}
		}

		void changeHUD() {
			setReloading();
			setAmmo();
			setMagazines();
			setPoints();
			setCoins();
			//HandleOutOfAmmo();
			setSugarRush();
		}

		void setReloading() {
			if (Reloading) {
				hud_reloading.SetActive(true);

			} else {
				hud_reloading.SetActive(false);
			}
		}

		void setAmmo() {
			t_Bullets.text = "" + Bullets;
		}

		void setMagazines() {
			t_Magazines.text = "" + Magazines;
		}

		void setPoints() {
			t_Points.text = "" + Points;
		}

		void setCoins() {
			t_Coins.text = "" + Coins;
		}

		void HandleOutOfAmmo() {

		}

		void setSugarRush() {
			if (sugarmode) {
				hud_sugarrush.SetActive(true);
			} else {
				hud_sugarrush.SetActive(false);
			}
		}

	}

}
