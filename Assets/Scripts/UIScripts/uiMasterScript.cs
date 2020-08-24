using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiMasterScript : MonoBehaviour
{
    [SerializeField] textFadeScript alertText = null;
    [SerializeField] healthbarScript kingHealthBar = null;
    [SerializeField] Camera cam = null;

    [SerializeField] FailureScreenScript failure;
    [SerializeField] EndingScreenScript success;
    // Start is called before the first frame update
    void Start()
    {
        if(cam.aspect < 1.5)
            SetSmallScreen();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void SetSmallScreen()
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

    public void FailureState(){
        StartCoroutine(FailureCoroutine());
    }

    private IEnumerator FailureCoroutine(){
        yield return new WaitForSeconds(0.5f);
        failure.EnableScreen();
    }

    public void SuccessState()
    {
        
    }
}
