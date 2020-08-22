using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiMasterScript : MonoBehaviour
{
    [SerializeField] textFadeScript alertText;
    [SerializeField] healthbarScript kingHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowAlertText()
    {
        
        yield return StartCoroutine(alertText.TextFadeIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(alertText.TextFadeOut(0.5f));
    }

    public void DamageKing(float damage)
    {
        kingHealthBar.ChangeHealth(-damage);
    }
}
