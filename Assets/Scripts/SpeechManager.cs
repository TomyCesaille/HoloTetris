﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    readonly Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public Transform tower;  // inspector object to hold our tower

    private HitHelper _hitHelper;
    
    // Use this for initialization
    void Start()
    {
        _hitHelper = new HitHelper();

        keywords.Add("Restart game", () =>
        {
            SceneManager.LoadScene(0);
            // Call the OnReset method on every descendant object.
            //this.BroadcastMessage("OnReset");
        });

        keywords.Add("Pause game", () => { Time.timeScale = 0.01f; });

        keywords.Add("Resume game", () => { Time.timeScale = 1.0f; });

        // for placing towers
        keywords.Add("Tower", PlaceTower);
        keywords.Add("Build", PlaceTower);
        keywords.Add("Place", PlaceTower);

        keywords.Add("Destroy", DestroyTower);

        keywords.Add("Upgrade", UpgradeTower);

        keywords.Add("Menue", OpenMenue);

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void PlaceTower()
    {
        var hitInfo = _hitHelper.GetHitInfo();
        if (hitInfo != null)
        {
            var towerToPlace = Instantiate(tower);
            BroadcastMessage(Constants.LooseMoney, 40.0f, SendMessageOptions.DontRequireReceiver);

            towerToPlace.transform.position = hitInfo.Value.point;
			// Parent it to the stage transform so it does move with it !
			towerToPlace.transform.parent = this.transform;
        }
    }

    private void OpenMenue()
    {
        var hitInfo = _hitHelper.GetHitInfo();
        /* missing function to open Main Menue */
    }

    private void DestroyTower()
    {
        var hitInfo = _hitHelper.GetHitInfo();
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

    private void UpgradeTower()
    {
        var hitInfo = _hitHelper.GetHitInfo();
        if (hitInfo != null)
        {
            if (hitInfo.Value.transform.tag == "Tower")
            {
                /* missing launch of menue on tower for upgrade */
            }
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}