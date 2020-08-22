using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textBoxScript : MonoBehaviour
{
    //TextMesh thisText;
    TextMeshPro thisText;
    GameObject background;
    void Awake()
    {
        background = transform.GetChild(0).gameObject;
        thisText = transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        thisText.text = "";
        SetTextBox(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void Setup(string text)
    {
        thisText.SetText(text);
        thisText.ForceMeshUpdate();
        Vector2 textSize = thisText.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f, 2f);
        background.size = textSize + padding;

    }*/

    public void WriteText(string textUsed, float timeUp) //to make calling it easier
    {
        StartCoroutine(SetText(textUsed,timeUp));
    }
    public IEnumerator SetText(string textUsed, float timeUp)
    {
        SetTextBox(true);
        float textWidth = textUsed.Length * 0.06f;
        Vector3 backgroundSize = background.transform.localScale;
        backgroundSize.x = textWidth;
        background.transform.localScale = backgroundSize;
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for(int i = 0; i < textUsed.Length; i++)
        {
            thisText.text = textUsed.Substring(0, i);
            yield return wait;
        }
        thisText.text = textUsed;
        yield return new WaitForSeconds(timeUp);
        thisText.text = "";
        SetTextBox(false);
    }

    public void SetTextBox(bool set)
    {
        background.SetActive(set);
        thisText.gameObject.SetActive(set);
    }
}
