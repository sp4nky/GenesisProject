using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class NPCSensor : MonoBehaviour
{
    List<NPCInteraction> NPClist= new List<NPCInteraction>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCInteraction NPCInt= collision.GetComponent<NPCInteraction>();
        if(NPCInt && !NPClist.Contains(NPCInt))
        {
            NPClist.Add(NPCInt);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCInteraction NPCInt = collision.GetComponent<NPCInteraction>();
        if (NPCInt && NPClist.Contains(NPCInt))
        {
            NPClist.Remove(NPCInt);
        }
    }

    public List<NPCInteraction> GetNPCList()
    {
        return NPClist;
    }

    public NPCInteraction NearbyNPCInteraction()
    {
        NPCInteraction resNPC= null;
        if (NPClist.Count>0)
        {
            resNPC = NPClist.First();
            float distance = Vector2.Distance(resNPC.transform.position, transform.position);
            foreach (NPCInteraction npc in NPClist)
            {
                if (Vector2.Distance(npc.transform.position, transform.position) < distance)
                {
                    distance = Vector2.Distance(npc.transform.position, transform.position);
                    resNPC = npc;
                }
            }
        }
        return resNPC;
    }

}
