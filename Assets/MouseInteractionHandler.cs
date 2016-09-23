﻿using UnityEngine;
using System.Collections;

public class MouseInteractionHandler : MonoBehaviour
{
    public Transform tower;

    HitHelper hitHelper;

    // Use this for initialization
    void Start ()
    {
        hitHelper = new HitHelper();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))  // left mouse button
        {
            var hitInfo = hitHelper.GetHitInfo();
            if (hitInfo != null)
            {
                var towerToPlace = Instantiate(tower);
                towerToPlace.transform.position = hitInfo.Value.point;
            }
        }

        if (Input.GetMouseButtonDown(1))  // right mouse button
        {
            var hitInfo = hitHelper.GetHitInfo();
            if (hitInfo != null)
            {
                if (hitInfo.Value.transform.tag == "Tower")
                {
                    Destroy(hitInfo.Value.transform.gameObject);
                    var exploder = hitInfo.Value.transform.gameObject.GetComponent<MeshExploder>();
                    exploder.Explode();
                }
            }
        }
	}
}