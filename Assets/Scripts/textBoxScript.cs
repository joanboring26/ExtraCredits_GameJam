using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBoxScript : MonoBehaviour
{
    TextMesh thisText;
    GameObject background;
    void Awake()
    {
        background = transform.GetChild(0).gameObject;
        thisText = transform.GetChild(1).gameObject.GetComponent<TextMesh>();
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

    public IEnumerator SetText(string textUsed, float timeUp)
    {
        SetTextBox(true);
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
