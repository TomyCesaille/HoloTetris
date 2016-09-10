﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TuretShoot : MonoBehaviour {

	public GameObject headTurret;
	public GameObject projectile;
	public Transform canon;
	public float range; 

	public float intervalSeconds;
	private float time;

	private Vector3 savedfocuslocation;

	public Transform focusedDude;

	public List<GameObject> ennemies;

	// Use this for initialization
	void Start () {
		time = intervalSeconds;
		savedfocuslocation = this.transform.position;
		focusedDude = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (focusedDude != null) {
			savedfocuslocation = focusedDude.transform.position;
			RotateToward (focusedDude.transform.position);
		}


		time += Time.deltaTime;

		if (time < intervalSeconds)
			return;

		time = 0;
		ennemies = GameObject.FindGameObjectsWithTag ("Ennemy").ToList ();
		if (focusedDude == null) {
			
			foreach (var ennemy in ennemies) {
				if (Vector3.Distance (this.transform.position, ennemy.transform.position) < range) {
					focusedDude = ennemy.gameObject.transform;
					break;
				}
			}
		}
		if (focusedDude != null) {
			if (Vector3.Distance (this.transform.position, focusedDude.transform.position) > range) {
				focusedDude = null;
				return;
			}


			//Debug.Log ("shoot at " + focusedDude.gameObject.name);

			GameObject go = Instantiate (projectile);

			go.GetComponent<Projectile> ().target = focusedDude.gameObject.transform;
			go.transform.position = canon.transform.position;

			if (focusedDude.GetComponent<HealthSystem>().currentLife < 1) {
				
				focusedDude = null;
				return;
			}
		}


	}

	void RotateToward(Vector3 targetPos)
    {
        Vector3 directionX = transform.position - targetPos;
		headTurret.transform.rotation = Quaternion.Slerp(headTurret.transform.rotation, 
            Quaternion.LookRotation(directionX, transform.up), 30f);
    }
}