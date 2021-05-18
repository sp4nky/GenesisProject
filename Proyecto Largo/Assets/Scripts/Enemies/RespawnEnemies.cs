using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    public GameObject prefavEnemy;
    List<GameObject> enemies = new List<GameObject>();
    public int timeToRespawn = 10;
    private float nextRespawn = 0;
    public int maxCountEnemies = 3;
    public RandomizePosition randomizeLocalPosition;
    private bool nightmode = true;

    // Update is called once per frame
    void Update()
    {
        if (nextRespawn <= 0 && enemies.Count < maxCountEnemies)
        {
            GameObject instance = Instantiate(prefavEnemy, transform, false);
            enemies.Add(instance);
            if(randomizeLocalPosition.enable)
            {
                instance.transform.localPosition = new Vector3(UnityEngine.Random.Range(-randomizeLocalPosition.radius, randomizeLocalPosition.radius), UnityEngine.Random.Range(-randomizeLocalPosition.radius, randomizeLocalPosition.radius), 0);
            }
            nextRespawn = timeToRespawn;
        }
        else if(nextRespawn > 0)
            nextRespawn -= Time.deltaTime;
        if(GameManagement.instance.timeManager.actualHour==GameManagement.instance.timeManager.hourStartNight && nightmode)
        {
            nightmode = false;
            maxCountEnemies *= 2;
        }
        if (GameManagement.instance.timeManager.actualHour == GameManagement.instance.timeManager.hourStartDay && !nightmode)
        {
            nightmode = true;
            maxCountEnemies /= 2;
            ClearEnemies();
        }
    }

    public void ClearEnemies()
    {
        foreach (GameObject go in enemies)
        {
            Destroy(go);
        }
        enemies.Clear();
        enemies = new List<GameObject>();
        GameObject instance = Instantiate(prefavEnemy, transform, false);
        enemies.Add(instance);
    }

    [Serializable]
    public class RandomizePosition
    {
        public bool enable;
        [Range(0, 40)]
        public int radius;
    }
}
