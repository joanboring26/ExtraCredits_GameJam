using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingAIScript : Character
{
    private CharacterType thisType = CharacterType.KING;

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
        Character hitCharacter = new Character();
        Mover.MoveCharacter(this, out hitCharacter, true, MovDir.FORWARD, Physics.DefaultRaycastLayers);
    }
}
