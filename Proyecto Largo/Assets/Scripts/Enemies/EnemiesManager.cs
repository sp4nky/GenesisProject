using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private List<Transform> enemiesTransform = new List<Transform>();
    private void Awake()
    {
        enemiesTransform = GetComponentsInChildren<Transform>().ToList();
    }
    /*
    void Start()
    {
        GameManagement.instance.enemies = this;
        if(GameManagement.instance.toDelete!="")
        {
            removeEnemy(GameManagement.instance.toDelete);
        }
    }
    **/
    public void removeEnemy(string enemyName)
    {
        foreach(Transform trans in enemiesTransform)
        {
            if(trans.name==enemyName)
            {
                trans.gameObject.SetActive(false);
            }
        }

    }
}
