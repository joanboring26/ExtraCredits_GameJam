using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightScript : MonoBehaviour
{
    private int row = 0;
    private int column = 0;
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

    public Vector2 GetPosition()
    {
        return new Vector2(row, column);
    }
}
