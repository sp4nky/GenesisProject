using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public GameObject[] team;

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.team1 = team;
    }


}
