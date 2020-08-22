using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textFadeScript : MonoBehaviour
{
    Text thisText;

    private bool fadingIn = false;
    void Awake()
    {
        thisText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TextFadeIn(float t)
    {
        //StopCoroutine(cancelable);
        fadingIn = true;
        while(thisText.color.a < 1.0f)
        {
            thisText.color = new Color(thisText.color.r,thisText.color.g, thisText.color.b, thisText.color.a + (Time.deltaTime / t));
            yield return null;
        }
        fadingIn = false;
    }

    public IEnumerator TextFadeOut(float t)
    {
        while(thisText.color.a > 0.0f && !fadingIn)
        {
            thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, thisText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
