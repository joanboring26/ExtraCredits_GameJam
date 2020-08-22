using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introMasterScript : MonoBehaviour
{
    [SerializeField] textFadeScript [] textFades;
    float fadeTime = 1;
    float waitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            Debug.Log("continue here");
    }

    private IEnumerator IntroCutscene()
    {
        yield return null; //next line won't work without this for some reason
        yield return StartCoroutine(textFades[0].TextFadeIn(1.0f)); //you are the highest ranking
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(textFades[0].TextFadeOut(2.0f));
        yield return StartCoroutine(textFades[1].TextFadeIn(1.0f)); //as the highest ranking
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(textFades[1].TextFadeOut(2.0f));
        yield return StartCoroutine(textFades[2].TextFadeIn(0.5f)); //there's
        StartCoroutine(textFades[2].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[3].TextFadeIn(0.5f)); //just
        StartCoroutine(textFades[3].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[4].TextFadeIn(0.5f)); //one
        StartCoroutine(textFades[4].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[5].TextFadeIn(0.5f)); //problem
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(textFades[5].TextFadeOut(1.5f));
        yield return StartCoroutine(textFades[6].TextFadeIn(1.0f)); //he's the kinda guy
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(textFades[7].TextFadeIn(1.0f));
    }
}
