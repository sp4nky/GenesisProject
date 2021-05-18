using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public Text hpText;
    public GameObject hitDamage;
    private float hp = 0;

    // Update is called once per frame
    void Update()
    {
        hpText.text = hp.ToString();
    }

    public void setHp(float hp)
    {
        this.hp = hp;
    }

    public void SetDamageHit(float damage)
    {
        Text txtDmg = hitDamage.GetComponent<Text>();
        txtDmg.text = "-" + damage.ToString();
        Animator anim = hitDamage.GetComponent<Animator>();
        anim.SetTrigger("Hit");
    }

}
