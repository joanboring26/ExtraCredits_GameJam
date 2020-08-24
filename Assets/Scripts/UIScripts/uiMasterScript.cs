using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class uiMasterScript : MonoBehaviour
{
    [SerializeField] textFadeScript alertText = null;
    [SerializeField] healthbarScript kingHealthBar = null;
    [SerializeField] Camera cam = null;
    [SerializeField] toDoListScript toDoList = null;
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] FailureScreenScript failure;
    [SerializeField] EndingScreenScript success;
    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        dialogue.SetText("");
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
        if(kingHealthBar.GetHealthValue() <= 0)
            FailureState();
    }

    public float GetHealthValue()
    {
        return kingHealthBar.GetHealthValue();
    }

    private void FailureState(){
        StartCoroutine(FailureCoroutine());
    }

    private IEnumerator FailureCoroutine(){
        yield return new WaitForSeconds(0.5f);
        failure.EnableScreen();
    }

    public void SuccessState()
    {
        success.EnableMenu();
    }

    public void ShowText(string text)
    {
        StartCoroutine(TypeText(text));
    }

    public IEnumerator TypeText(string text)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for(int i = 0; i < text.Length; i++){
            dialogue.SetText(text.Substring(0,i));
            yield return wait;
        }
        dialogue.SetText(text);
        yield return new WaitForSeconds(2.0f);
        dialogue.text = "";
    }

    public void CompleteTask(toDoListScript.ToDoTasks tasks)
    {
        toDoList.CrossOut(tasks);
    }
}
