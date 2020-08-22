using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSpaceScript : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int column;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return column;
    }

    public void LightUpValid()
    {
        //light up green because the space is valid
    }

    public void LightUpInvalid()
    {
        //light up red because the space is invalid
    }
}
