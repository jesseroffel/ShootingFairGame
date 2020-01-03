using UnityEngine;
using System.Collections;

namespace Schiettent 
{
	public class SpawnObjects : MonoBehaviour {

		public string direction;
		public int rowNumber;

		public Transform prop_can;
		public Transform prop_duck;
		public Transform prop_balloon;
		public Transform prop_planktarget;
		public Transform prop_tinytarget;

		private HandleGame game;

		private float nextUpdate = 0;
		private float updateTick = 0.25f;

		private Transform myTransform;
		private float StartPos = 1;
		private float heightrow1 = 3.2f;
		private float heightrow2 = 2.2f;
		private float heightrow3 = 1.4f;

		private float speed = 0.01f;
		private float sugarspeed = 0.01f;
		private bool sugarspeedover = false;
        private bool active = false;
        private bool canspawn = false;

		// Use this for initialization
		void Start () {
			SetInitialReferences();
            FirstSpawn();
            active = true;
        }
		
		// Update is called once per frame
		void Update () {
            if (active)
            {
                if (game.m_RoundOver)
                {
                    CleanUpItems();
                    active = false;
                    canspawn = true;
                }
                if (Time.time > nextUpdate)
                {
                    if (myTransform.childCount == 0)
                    {
                        canspawn = true;
                        NewSpawn();
                        canspawn = false;
                    }
                    if (game.getSugarRushState() && sugarspeedover == false)
                    {
                        if (direction == "Stop")
                        {
                            int randdir = Random.Range(0, 2);
                            if (randdir == 1)
                                direction = "Left";
                            else
                                direction = "Right";
                        }
                        if (sugarspeed < 0.10f)
                        {
                            sugarspeed += 0.005f;
                        }
                        UpdateSugarSpeed();
                    }
                    if (sugarspeedover == false && game.getSugarRushState() == false)
                    {
                        sugarspeedover = true;
                    }
                    if (sugarspeedover)
                    {
                        EndSugarRush();
                        sugarspeedover = false;
                    }
                    nextUpdate = Time.time + updateTick;
                }
            }
            if (active == false && game.m_GameRunning)
            {
                if (canspawn)
                {
                    NewSpawn();
                    canspawn = false;
                    active = true;
                }
            }
                

        }

		void SetInitialReferences() {
			myTransform = transform;
			if (prop_can == null || prop_duck == null || prop_balloon == null || prop_planktarget == null || prop_tinytarget == null) { Debug.LogError("Missing references: props"); }
			game = (HandleGame)FindObjectOfType(typeof(HandleGame));
			if (game == null) { Debug.LogError("Missing reference: game"); }
		}

		void CleanUpItems() {
			foreach (Transform child in myTransform) 
			{
				Destroy(child.gameObject);
			}
		}

		void UpdateSugarSpeed() {
			foreach (Transform child in myTransform) 
			{
				ObjectHandler objh = child.GetComponent<ObjectHandler>();
				objh.SugarRush = true;
				objh.SugarSpeed = sugarspeed;
			}
		}

		void EndSugarRush() {
			foreach (Transform child in myTransform) 
			{
				ObjectHandler objh = child.GetComponent<ObjectHandler>();
				objh.SugarRush = false;
				objh.SugarSpeed = 0.01f;
			}
            sugarspeed = 0.01f;
        }

		public void setRowNumber(int number) 
		{
			rowNumber = number;
		}

		void FirstSpawn() {
			switch (rowNumber) {
			case 0:
				for (int x = 0; x < 8; x++) {
					Transform newProp = Instantiate(prop_balloon);
					newProp.transform.position = new Vector3((StartPos * (x + 9.0f)), heightrow1, 14.6f);
					newProp.transform.parent = myTransform.transform;
					if (game.getSugarRushState()) {newProp.GetComponent<ObjectHandler>().speed = sugarspeed; } 
					else { newProp.GetComponent<ObjectHandler>().speed = speed;}
					direction = "Left";
				}
				break;
			
			case 1:
				for (int x = 0; x < 8; x++) {
					Transform newProp = Instantiate(prop_duck);
					newProp.transform.position = new Vector3((StartPos * (x + 9.0f)), heightrow3, 13.05f);
					newProp.transform.parent = myTransform.transform;
					if (game.getSugarRushState()) {newProp.GetComponent<ObjectHandler>().speed = sugarspeed; } 
					else { newProp.GetComponent<ObjectHandler>().speed = speed;}
					direction = "Right";
				}
				break;
			case 2:
				for (int x = 0; x < 8; x++) {
					Transform newProp = Instantiate(prop_can);
					newProp.transform.position = new Vector3((StartPos * (x + 9.0f)), heightrow2, 13.8f);
					newProp.transform.parent = myTransform.transform;
					if (game.getSugarRushState()) {newProp.GetComponent<ObjectHandler>().speed = sugarspeed; } 
					else { newProp.GetComponent<ObjectHandler>().speed = speed;}
					direction = "Stop";
				}
				break;
			}
		}

        void NewSpawn()
        {
            int spawnmount = Random.Range(4, 9);
            int spawndirection = Random.Range(0, 4);
            int spawnobject = Random.Range(1, 6);
            int row = rowNumber;
            for (int x = 0; x < spawnmount; x++)
            {
                switch (spawnobject)
                {
                    case 1:
                        Transform newProp1 = Instantiate(prop_can);
                        if (row == 0) { newProp1.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow1, 14.6f); }
                        if (row == 1) { newProp1.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow3, 13.05f); }
                        if (row == 2) { newProp1.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow2, 13.8f); }
                        newProp1.transform.parent = myTransform.transform;
                        if (game.getSugarRushState()) { newProp1.GetComponent<ObjectHandler>().speed = sugarspeed; }
                        else { newProp1.GetComponent<ObjectHandler>().speed = speed; }
                        if (spawndirection == 1) { direction = "Left"; }
                        if (spawndirection == 2) { direction = "Right"; }
                        if (spawndirection == 3) { direction = "Stop"; }
                        break;
                    case 2:
                        Transform newProp2 = Instantiate(prop_duck);
                        if (row == 0) { newProp2.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow1, 14.6f); }
                        if (row == 1) { newProp2.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow3, 13.05f); }
                        if (row == 2) { newProp2.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow2, 13.8f); }
                        newProp2.transform.parent = myTransform.transform;
                        if (game.getSugarRushState()) { newProp2.GetComponent<ObjectHandler>().speed = sugarspeed; }
                        else { newProp2.GetComponent<ObjectHandler>().speed = speed; }
                        if (spawndirection == 1) { direction = "Left"; }
                        if (spawndirection == 2) { direction = "Right"; }
                        if (spawndirection == 3) { direction = "Stop"; }
                        break;
                    case 3:
                        Transform newProp3 = Instantiate(prop_balloon);
                        if (row == 0) { newProp3.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow1, 14.6f); }
                        if (row == 1) { newProp3.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow3, 13.05f); }
                        if (row == 2) { newProp3.transform.position = new Vector3((StartPos * (x + 9.0f) + (8.0f - spawnmount)), heightrow2, 13.8f); }
                        newProp3.transform.parent = myTransform.transform;
                        if (game.getSugarRushState()) { newProp3.GetComponent<ObjectHandler>().speed = sugarspeed; }
                        else { newProp3.GetComponent<ObjectHandler>().speed = speed; }
                        if (spawndirection == 1) { direction = "Left"; }
                        if (spawndirection == 2) { direction = "Right"; }
                        if (spawndirection == 3) { direction = "Stop"; }
                        break;
                    case 4:
                        Transform newProp4 = Instantiate(prop_planktarget);
                        if (row == 0) { newProp4.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow1, 14.6f); }
                        if (row == 1) { newProp4.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow3, 13.05f); }
                        if (row == 2) { newProp4.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow2, 13.8f); }
                        newProp4.transform.parent = myTransform.transform;
                        if (game.getSugarRushState()) { newProp4.GetComponent<ObjectHandler>().speed = sugarspeed; }
                        else { newProp4.GetComponent<ObjectHandler>().speed = speed; }
                        if (spawndirection == 1) { direction = "Left"; }
                        if (spawndirection == 2) { direction = "Right"; }
                        if (spawndirection == 3) { direction = "Stop"; }
                        break;
                    case 5:
                        Transform newProp5 = Instantiate(prop_tinytarget);
                        if (row == 0) { newProp5.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow1, 14.6f); }
                        if (row == 1) { newProp5.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow3, 13.05f); }
                        if (row == 2) { newProp5.transform.position = new Vector3((StartPos * (x + 9.0f) + (9.0f - spawnmount)), heightrow2, 13.8f); }
                        newProp5.transform.parent = myTransform.transform;
                        if (game.getSugarRushState()) { newProp5.GetComponent<ObjectHandler>().speed = sugarspeed; }
                        else { newProp5.GetComponent<ObjectHandler>().speed = speed; }
                        if (spawndirection == 1) { direction = "Left"; }
                        if (spawndirection == 2) { direction = "Right"; }
                        if (spawndirection == 3) { direction = "Stop"; }
                        break;

                }
            }
        }
	}
}


