using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoHidde : MonoBehaviour
{
    private Text logText;
    public float time = 2;
    
    private void Awake()
    {
        logText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hidde());
    }

    private IEnumerator hidde()
    {
        yield return null;
        float t = 0;
        while(t < time)
        {
            Color c = logText.color;
            c.a = Mathf.Lerp(1, 0, t / time);
            logText.color = c;
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
