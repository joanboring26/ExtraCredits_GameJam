using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAI : Character
{
    [SerializeField] GameObject waypoint = null;

    [SerializeField] Vector2 destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CharacterUpdate()
    {
        if (waypoint == null)
            return;
        Vector3 waypointPos = waypoint.transform.position;
        Character hitCharacter = null;
        Mover.MoveCharacterToWayPoint(
            this,
            out hitCharacter,
            true,
            waypointPos,
            Physics.DefaultRaycastLayers);
    }
}
