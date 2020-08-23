using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class mouseControlScript : MonoBehaviour
{
    [SerializeField] knightScript knight = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                if(hit.collider.gameObject.tag == "MoveSpace")
                    CheckMove(hit.collider.gameObject.GetComponent<moveSpaceScript>());
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.gameObject.tag == "MoveSpace")
                Debug.Log("space");
                //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
    private bool SpaceIsValid(int row, int column)
    {
        int rowDifference = Mathf.Abs(knight.GetRow() - row);
        int columnDifference = Mathf.Abs(knight.GetColumn() - column);
        return((rowDifference == 1 && columnDifference == 2) || (rowDifference == 2 && columnDifference == 1));
    }

    void MoveSpaceLightUp(moveSpaceScript space)
    {
        if(SpaceIsValid(space.GetRow(), space.GetColumn()))
            space.LightUpValid();
        else
            space.LightUpInvalid();
    }

    void CheckMove(moveSpaceScript space)
    {
        if(SpaceIsValid(space.GetRow(), space.GetColumn()))
            knight.MoveToSpace(space);
    }
}
*/