using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class StackingBar : MonoBehaviour
{
    public GameObject stackPrefab;
    public bool colourChange;
    private List<GameObject> listStacks = new List<GameObject>();

    public void SetValue(float value, float maxValue)
    {
        float percent = 100 * value / maxValue;
        for (int i=0; i < percent/2; i++)
        {
            GameObject stack = Instantiate(stackPrefab,transform);
            stack.transform.localScale = Vector3.one;
            Image img = stack.GetComponent<Image>();
            if (colourChange)
            {
                if (percent < 25)
                {
                    img.color = new Color(178f / 255f, 29f / 255f, 24f / 255f);
                }
                else if (percent < 50)
                {
                    img.color = new Color(248f / 255f, 244f / 255f, 111f / 255f);
                }
                else
                {
                    img.color = new Color(96f / 255f, 164f / 255f, 106f / 255f);

                }
            }
            listStacks.Add(stack);
        }
    }

    public void clearStacksBar()
    {
        foreach (GameObject stack in listStacks)
            Destroy(stack);
        listStacks.Clear();
    }


}
