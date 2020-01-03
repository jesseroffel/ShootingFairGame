using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class HandleScore : MonoBehaviour {
		public HandleGame Game;
		public Transform floattext;
		private int hitstreak = 0;
		private int hittype = 0;
		private int hittypestreak = 0;


		void Start() {

			if (Game == null) { Debug.LogError("Missing reference: Game"); } 
			if (floattext == null) { Debug.LogError("Missing reference: floattext"); } 
		}

		public void CalculateScore(int points, int coins, float speed, bool moving, bool special, bool sugarrush, float sugarspeed, Transform position) {
			float totalpoints = points / 10;
			float totalcoins = coins / 10;

			if (sugarrush) 
			{
				totalpoints += points * speed;
				if (moving) { totalpoints += points + coins * sugarspeed; } else { totalpoints += points + coins * sugarspeed * 0.2f; } 

				totalpoints += hitstreak * 1.5f;
				totalcoins += coins * speed;
				if (moving) { totalcoins += points + coins * sugarspeed;} else { totalcoins += points + coins * sugarspeed * 0.2f;  }
				totalcoins += points + coins * sugarspeed;
				totalcoins += hitstreak * 1.5f;

			} else 
			{
				totalpoints += points * speed;
				totalpoints += hitstreak * 1.5f;
				totalcoins += coins * speed;
				totalcoins += hitstreak * 1.5f;
			}
			if (special) {
				float tp = totalpoints;
				float tc = totalcoins;
				totalpoints += tp * 5f;
				totalcoins += tc * 5f;
			}

			Game.AddHitPC((int)totalpoints, (int)totalcoins);
			SpawnPointsText ((int)totalpoints, position);
			SpawnCoinsText ((int)totalcoins, position);
			hitstreak++;
		}

		public void HandleMiss() {
			hitstreak = 0;
		}

		public void SetHitType(int type) {
			if (hittype == type) 
			{
				hittypestreak ++;
			} else 	
			{
				hittypestreak = 0;
			}
		}

		public void SetRowBonus(int type) {
			int points = 0;
			int coins = 0;

			points += 25;
			coins += 25;
			if (type == hittype) {
				coins += 75;
				points += 75;
			} 
			if (hittypestreak >= 6) {
				coins += 100;
				points += 100;
			}

			
			hittypestreak = 0;
			Game.AddHitPC(points,coins);
		}

		void SpawnPointsText(int points, Transform textposition ) {
			Transform text = Instantiate(floattext);
			textposition.position = new Vector3 (textposition.position.x, textposition.position.y + 0.5f, textposition.position.z);
			text.GetComponent<FloatText>().setProperties("points: " + points, "red", textposition);

			//Destroy (text.gameObject, 2f);
		}

		void SpawnCoinsText(int coins, Transform textposition ) {
			Transform text = Instantiate(floattext);
			textposition.position = new Vector3 (textposition.position.x, textposition.position.y - 0.5f, textposition.position.z);
			text.GetComponent<FloatText>().setProperties("coins: " + coins, "green", textposition);
		}
	}
}
