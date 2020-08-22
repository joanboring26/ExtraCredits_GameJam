using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alertTextScript : MonoBehaviour
{
    [SerializeField] Text alertText;

    private float typeInterval;
    void Awake()
    {
        typeInterval = 0.05f;
    }
    // Start is called before the first frame update
    void Start()
    {
        alertText.text = " ";
    }

    public IEnumerator PrintText(string textLine)
    {
        for(int i = 0; i < textLine.Length; i++)
        {
            alertText.text = textLine.Substring(0, i) + "|";
            yield return new WaitForSeconds(typeInterval);
        }
        alertText.text = textLine;
    }

    public void ClearText()
    {
        alertText.text = " ";
    }
}
