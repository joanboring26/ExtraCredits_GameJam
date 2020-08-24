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
        toDoListScript = FindObjectOfType<toDoListScript>();
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
        toDoListScript.CrossOut((toDoListScript.ToDoTasks)currentObjective);
        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            currentObjective = objectives.Count - 1;
            // TODO: signal end of game
        }
    }



}
