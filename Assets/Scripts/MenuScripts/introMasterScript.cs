using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class introMasterScript : MonoBehaviour
{
    [SerializeField] textFadeScript [] textFades = null;
    [SerializeField] AudioSource sound = null;
    [SerializeField] AudioClip music = null;
    [SerializeField] AudioClip recordScratch = null;
    [SerializeField] GameObject introText = null;
    [SerializeField] GameObject instructions = null;
    //float fadeTime = 1;
    //float waitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCutscene());
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            StopCoroutine(IntroCutscene());
            if(!instructions.activeSelf){
                introText.SetActive(false);
                instructions.SetActive(true);
            }
            else
                SceneManager.LoadScene("kingEscortScene");
        }
    }

    private IEnumerator IntroCutscene()
    {
        sound.clip = music;
        sound.Play();
        yield return StartCoroutine(textFades[0].TextFadeIn(1.0f)); //you are the highest ranking
        yield return new WaitForSeconds(1.5f);
        //StartCoroutine(textFades[0].TextFadeOut(2.0f));
        yield return StartCoroutine(textFades[1].TextFadeIn(1.0f)); //as the highest ranking
        yield return new WaitForSeconds(1.5f);
        //StartCoroutine(textFades[1].TextFadeOut(2.0f));
        yield return StartCoroutine(textFades[2].TextFadeIn(0.5f)); //there's
        //StartCoroutine(textFades[2].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[3].TextFadeIn(0.5f)); //just
        //StartCoroutine(textFades[3].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[4].TextFadeIn(0.5f)); //one
        //StartCoroutine(textFades[4].TextFadeOut(1.0f));
        yield return StartCoroutine(textFades[5].TextFadeIn(0.5f)); //problem
        yield return new WaitForSeconds(1.0f);
        //StartCoroutine(textFades[5].TextFadeOut(1.5f));
        yield return StartCoroutine(textFades[6].TextFadeIn(1.0f)); //he's the kinda guy
        sound.clip = recordScratch;
        sound.volume = 0.1f;
        sound.Play();
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(textFades[7].TextFadeIn(1.0f));
    }
}
