using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeEffect : MonoBehaviour
{

    SpriteRenderer sr; // 소세지맨 렌더러
    public GameObject go; // 소세지맨 오브젝트 

    [SerializeField]
    private float fadeSpeed; // 값이 10이면 1초 (값이 클수록 빠름)
    [SerializeField]
    private AnimationCurve fadeCurve;
    bool unBeatState; // true 일경우 무적인 상태


    

    
    public void startUnBeatTime(float unBeatTime)
    {
        sr = go.GetComponent<SpriteRenderer>();
        unBeatState = true;
        Invoke("BeatState", unBeatTime);

        StartCoroutine(FadeInOut());        
    }

    private IEnumerator FadeInOut()
    {
        Debug.Log("FadeInOut");
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));
            yield return StartCoroutine(Fade(0, 1));

            if(!unBeatState)
            {
                break;
            }
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent <1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeSpeed;

            Color color = sr.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            sr.color = color;

            yield return null;
        }
    }
    IEnumerator FadeIn()    
    {
        for (int i=0; i<10; i++)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut()
    {
        for(int i=10; i>= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void BeatState()
    {
        unBeatState = false;
    }


}
