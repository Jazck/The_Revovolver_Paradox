using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem.SequencerCommands;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool firstEncounterTrigger;
    [SerializeField] private bool firstEncounterTriggerOn;
    [SerializeField] private bool secondEncounterTriggerOn;
    [SerializeField] private bool exitTrigger;
    [SerializeField] private bool secondEncounterTrigger;
    [SerializeField] private bool towerTriggerOn;
    [SerializeField] private bool towerTrigger;
    [SerializeField] private bool beginnings;


    // Update is called once per frame
    void Start()
    {
        if (beginnings)
        {
            AudioList.Instance.StartAmbiance(AudioList.Ambiance.Rain, true);
        }

 

    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(delayer(2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exitTrigger)
        {
            Debug.Log("stop everything");
            AudioList.Instance.StopAmbiance();
            AudioList.Instance.StopMusic();
            StartCoroutine(delayer(3));
        }
    }

    IEnumerator delayer(float delay)
    {
        if (firstEncounterTriggerOn)
        {
            if (firstEncounterTrigger)
            {
                AudioList.Instance.StartMusic(AudioList.Music.FirstEncounter, true);
                yield return new WaitForSeconds(delay);
                firstEncounterTrigger = false;


            }

            else
            {
                AudioList.Instance.StopMusic();
                yield return new WaitForSeconds(delay);
                firstEncounterTrigger = true;
            }
        }

        if (secondEncounterTriggerOn)
        {
            if (secondEncounterTrigger)
            {
                AudioList.Instance.StartMusic(AudioList.Music.SecondEncounter, true);
                yield return new WaitForSeconds(delay);
                secondEncounterTrigger = false;

            }
            
            
        }
        if (towerTriggerOn)
        {
            if (towerTrigger)
            {
                AudioList.Instance.StartAmbiance(AudioList.Ambiance.Tower, true);
                yield return new WaitForSeconds(delay);
                towerTrigger = false;


            }

            else
            {
                AudioList.Instance.StopAmbiance();
                yield return new WaitForSeconds(delay);
                towerTrigger = true;
            }
        }
        if (exitTrigger)
        {
            yield return new WaitForSeconds(delay);
           AudioList.Instance.StartAmbiance(AudioList.Ambiance.Tower, true);

        }
    }
}

