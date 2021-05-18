using DigitalRuby.RainMaker;
using System.Collections;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public RainScript2D rainScript;
    public float changeTime = 10f;

    void Start()
    {
        GameManagement.instance.rainManager = this;
    }

    public void RainIntensity(float start, float end)
    {
        StartCoroutine(changeRainIntensity(start, end));
    }

    private IEnumerator changeRainIntensity(float start, float end)
    {
        yield return null;
        float t = 0;
        while (t < changeTime)
        {
            rainScript.RainIntensity = Mathf.Lerp(start, end, t / changeTime);
            t += Time.deltaTime;
            yield return null;
        }
        if (end == 0) rainScript.EnableWind = false;
        else
            rainScript.EnableWind = true;
        rainScript.RainIntensity = end;
    }

    public void StopRain()
    {
        rainScript.RainIntensity = 0f;
    }


}
