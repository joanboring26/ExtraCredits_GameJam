using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [System.Serializable]
    class Objective
    {
        [SerializeField] internal GameObject gameObj = null;
        [SerializeField] internal float radius = 2.5f;
    }
    [SerializeField] List<Objective> objectives = new List<Objective>();
    [SerializeField] AudioClip kingTalking;
    [SerializeField] AudioClip grocerTalking;
    [SerializeField] AudioClip blacksmithTalking;
    [SerializeField] AudioClip priestTalking;
    [SerializeField] AudioClip taxDodgerTalking;
    [SerializeField] AudioClip executedTalking;
    private AudioSource voice;
    uiMasterScript uiScript;
    toDoListScript toDoListScript;
    int currentObjective = 0;
    public Vector3 Waypoint
    {
        get {
            return objectives[currentObjective].gameObj.transform.position;
        }
    }
    float ObjectiveRadius
    {
        get {
            return objectives[currentObjective].radius;
        }
    }

    void Start()
    {
        uiScript = FindObjectOfType<uiMasterScript>();
        toDoListScript = FindObjectOfType<toDoListScript>();
        voice = GetComponent<AudioSource>();
    }

    public void AttemptToCompleteObjective()
    {
        // If king is outside objective radius then return
        float sqrDistanceFromWaypoint = 
            Vector3.SqrMagnitude(Waypoint - gameObject.transform.position);
        if (sqrDistanceFromWaypoint > ObjectiveRadius)
        {
            return;
        }
        StartCoroutine(Dialogue(currentObjective));
        toDoListScript.CrossOut((toDoListScript.ToDoTasks)currentObjective);
        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            currentObjective = objectives.Count - 1;
            // TODO: signal end of game
        }
    }

    private IEnumerator Dialogue(int objectiveCount)
    {
        string dialogue;
        WaitForSeconds timeBetweenLines = new WaitForSeconds(1.0f);
        switch(objectiveCount){
            case 1:
                voice.clip = kingTalking;
                voice.Play();
                dialogue = "Give me two cabbages, and make it quick!";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                //yield return timeBetweenLines;
                voice.clip = grocerTalking;
                voice.Play();
                dialogue = "Whatever you say, your highness, *grumble grumble*";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                break;
            case 2:
                voice.clip = kingTalking;
                voice.Play();
                dialogue = "Are my bangles ready yet? I've waited all night!";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                //yield return timeBetweenLines;
                voice.clip = blacksmithTalking;
                voice.Play();
                dialogue = "*sigh* Yes, your highness.";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                break;
            case 3:
                voice.clip = kingTalking;
                voice.Play();
                dialogue = "Your god is subservient to me. Bless me.";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                //yield return timeBetweenLines;
                voice.clip = priestTalking;
                voice.Play();
                dialogue = "I'm contractually obligated to do so.";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                break;
            case 4:
                voice.clip = kingTalking;
                voice.Play();
                dialogue = "There the tax dodger is, after him!";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                //yield return timeBetweenLines;
                voice.clip = taxDodgerTalking;
                voice.Play();
                dialogue = "Ahhhhh!";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                break;
            case 5:
                voice.clip = kingTalking;
                voice.Play();
                dialogue = "Ah, a false believer. This ought to be good.";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                //yield return timeBetweenLines;
                voice.clip = executedTalking;
                voice.Play();
                dialogue = "I'm not the only one. You won't make it out of the city alive!";
                yield return StartCoroutine(uiScript.TypeText(dialogue));
                break;
        }

    }



}
