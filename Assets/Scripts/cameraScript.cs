using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    private Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
