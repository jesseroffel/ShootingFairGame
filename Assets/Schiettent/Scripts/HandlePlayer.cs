using UnityEngine;
using System.Collections;

namespace Schiettent
{
	public class HandlePlayer : MonoBehaviour 
	{
		public HandleScore HandleScore;
		public AudioSource AudioPlayer;
		public AudioClip gunshot;
		public AudioClip reload;
		public AudioClip reloaddone;
		public Transform clipPrefab;

        public bool m_OutOfAmmo = false;

        public bool m_PlayerMovement = false;

		private int maxbullets = 5;
        private int maxmagazines = 5;

		private int m_Bullets = 6;
		private int m_Magazines = 3;

		private float fireRate = 1f;
		private float reloadRate = 1f;

        private float maxfirerate = 1f;
        private float maxreloadrate = 1f;

        private bool m_Reloading = false;

        private float nextFire = 0;
		private float nextReload = 0;
		private float propulsionForce = 3;
		private RaycastHit raycastHit;
		private float rayCastRange = 50;
		private Transform myTransform;


		void Start () {
			SetInitialReferences();
			if (HandleScore == null) { Debug.LogError("Missing reference: HandleScore"); } 
			if (AudioPlayer == null) { Debug.LogError("Missing reference: AudioPlayer"); } 
			if (gunshot == null) { Debug.LogError("Missing reference: gunshot"); } 
			if (reload == null) { Debug.LogError("Missing reference: reload"); } 
			if (reloaddone == null) { Debug.LogError("Missing reference: reloaddone"); } 
			if (clipPrefab == null) { Debug.LogError("Missing reference: clipPrefab"); }
            m_PlayerMovement = true;

        }

		void Update () {
			if (m_PlayerMovement) {
				CheckForInput();
                if (m_Magazines == 0 && m_Bullets == 0) 
				{ 
					m_OutOfAmmo = true; 
				}
				checkForReload();
                //checkGun();

            }
		}

		void SetInitialReferences() {
			myTransform = transform;
		}

		void CheckForInput() {
            if (Input.GetButton("Fire1") && Time.time > nextFire) {
				if (m_Reloading != true && m_OutOfAmmo != true) {
                    //Debug.DrawRay(myTransform.TransformPoint(0,0,1), myTransform.forward, Color.green, rayCastRange);
                    fireBullet();
					nextFire = Time.time + fireRate;
				}
			}
		}

        public void StartNewRound()
        {
            m_Bullets = maxbullets;
            m_Magazines = maxmagazines;
            fireRate = maxfirerate;
            reloadRate = maxreloadrate;
            m_Reloading = false;
            m_OutOfAmmo = false;
            m_PlayerMovement = true;
        }

		void checkGun() {
			if (m_Reloading) {
				//GameObject ThisModel = myTransform.transform.Find("M1").gameObject;
				//.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f, 0.0f);
				//ThisModel.transform.Rotate(new Vector3(60.0f, 180.0f, 90.0f));
			} else {
				//GameObject ThisModel = myTransform.transform.Find("M1").gameObject;
				//.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f, 1.0f);
			}
		}

		public enum WeaponState
		{
			RELOAD,
			FIRING,
            BULLETS,
            CLIPS
		}

		public void ChangePlayerRates(WeaponState type, float newvalue)
        {
            switch (type)
            {
                case WeaponState.RELOAD:
                    {
                        reloadRate = newvalue;
                        maxreloadrate = newvalue;
                        break;
                    }
                case WeaponState.FIRING:
                    {
                        fireRate = newvalue;
                        maxfirerate = newvalue;
                        break;
                    }
            }
        }

        public void ChangePlayerBulls(string type, int newvalue)
        {
            switch (type)
            {
                case "bullets":
                    m_Bullets = newvalue;
                    maxbullets = newvalue;
                    break;
                case "clips":
                    m_Magazines = newvalue;
                    maxmagazines = newvalue;
                    break;
            }
        }

        void checkForReload() {
			if (m_Reloading && m_OutOfAmmo == false) {
				if (m_Magazines > 0) {
					// reload clip;
					if (Time.time > nextReload) {
						m_Bullets++;
						AudioPlayer.PlayOneShot(reload, 1.0f);
						nextReload = Time.time + reloadRate;
					}
				} else {
					Debug.Log("Out of ammo!!");
					m_OutOfAmmo = true;
				}
				if (m_Bullets == maxbullets) {
					m_Magazines--;
					AudioPlayer.PlayOneShot(reloaddone, 1.0f);
					m_Reloading = false;
				}
			}

		}

		void spawnClip() {
			GameObject newClip = (GameObject)Instantiate(clipPrefab, myTransform.TransformPoint(0, 0, 0.5f), myTransform.rotation);
			Destroy(newClip, 1);
			//newClip.transform.position = 
		}

		void fireBullet() {
            // Check if you have enough bullets and change
            if (m_Bullets == 0) {
				m_Reloading = true;
			}
			if (m_Bullets > 0) {
				// Set new variables
				m_Bullets--;
				AudioPlayer.PlayOneShot(gunshot, 1.0f);
				//spawnClip();
				// Move object
				bool didhit = false;
				if (Physics.Raycast(myTransform.TransformPoint(0,0,1), myTransform.forward, out raycastHit, rayCastRange)) {
					if (raycastHit.transform.CompareTag("Prop")) {
						didhit = true;
						ObjectHandler targethandler = raycastHit.transform.GetComponent<ObjectHandler>();
						targethandler.SetHit(true);
						int points = targethandler.points;
						int coins = targethandler.coins;
						float speed = targethandler.speed;
						bool moving = targethandler.moving;
						bool special = targethandler.Special;
						bool sugarrush = targethandler.SugarRush;
						float sugarspeed = targethandler.SugarSpeed;
						//Debug.Log("Prop " + raycastHit.transform.name + " points: " + points + " coins: " + coins);
						HandleScore.CalculateScore(points, coins, speed, moving, special, sugarrush, sugarspeed, raycastHit.transform);
						raycastHit.rigidbody.AddForce(myTransform.forward * propulsionForce, ForceMode.Impulse);
						//Destroy (hit.transform.gameObject, 2f);
					}
				}
				if (didhit == false) {
					SetMiss();
				}
			}
		}

		void SetMiss() {
			HandleScore.HandleMiss();
		}

		public int getMagazines() {
			return m_Magazines;
		}

		public int getBullets() {
			return m_Bullets;
		}

        public bool getReloading()
        {
            return m_Reloading;
        }
	}
}