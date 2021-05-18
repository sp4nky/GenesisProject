using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager : MonoBehaviour
{

    public StatsUI stat;
    public CombatMenu combatMenu;
    private int idSlotSelected = 0;
    private int idSlotMarked = 0;
    public bool isTraining;
    bool right;
    bool left;
    public List<CharacterSlot> slots = new List<CharacterSlot>();

    public void DefaultSlots()
    {
        idSlotMarked = 0;
        idSlotSelected = 0;

        slots = GetComponentsInChildren<CharacterSlot>(true).ToList();
        foreach (CharacterSlot slot in slots)
        {
            slot.gameObject.SetActive(true);
        }

    }

    public CharacterSlot GetSelectedSlot()
    {
        return slots[idSlotSelected];
    }

    public CharacterSlot GetCurrentTurnSlot()
    {
        return slots[idSlotMarked];
    }

    public int GetCharactersCount()
    {
        return slots.Count;
    }

    public int GetExpForTeam()
    {
        int exp = 0;
        foreach (CharacterSlot slot in slots)
        {
            CharacterBehaviour character = slot.getCharacter();
            exp += character.characterData.exp;
        }
        return exp;
    }

    public bool IsLastSelected()
    {
        return idSlotSelected == slots.Count - 1;
    }

    public bool IsFirstSelected()
    {
        return idSlotSelected == 0;
    }

    public void MarkFirstTurn()
    {
        idSlotMarked = 0;
        slots[idSlotMarked].characterMark();
        stat.Show();
        stat.SetStats(slots[idSlotMarked].getCharacter());

        combatMenu.OpenIn(transform.position);
        combatMenu.SetCombatMenu(slots[idSlotMarked].getCharacter());
        
    }

    public void MarkDeselect()
    {
        slots[idSlotMarked].characterMarkDeselected();
    }

    public void NextTurnMark()
    {
        DesmarkAllSlots();
        idSlotMarked++;
        if (idSlotMarked >= slots.Count)
        {
            idSlotMarked = 0;
        }
        stat.Show();
        slots[idSlotMarked].characterMark();
        stat.SetStats(slots[idSlotMarked].getCharacter());
        if (isTraining)
        {
            combatMenu.OpenIn(transform.position);
            combatMenu.SetCombatMenu(slots[idSlotMarked].getCharacter());
        }
        
    }

    public void DesmarkAllSlots()
    {
        foreach (CharacterSlot slot in slots)
        {
            slot.characterMarkDeselected();
        }
        stat.Hidde();
        combatMenu.Close();
    }

    public void DeselectAllSlots()
    {
        foreach (CharacterSlot slot in slots)
        {
            slot.slotDeselected();
        }
    }

    public void SelectFirst()
    {
        idSlotSelected = 0;
        slots[idSlotSelected].slotSelected();
    }

    public void SelectNext()
    {
        DeselectAllSlots();
        idSlotSelected++;
        if (idSlotSelected >= slots.Count)
        {
            idSlotSelected = 0;
        }
        slots[idSlotSelected].slotSelected();
    }

    private void Update()
    {
        right = Input.GetButtonDown("Right");
        left = Input.GetButtonDown("Left");
    }

    public void SelectSolt()
    {
        if (right)
        {
            slots[idSlotSelected].slotDeselected();
            if (idSlotSelected >= slots.Count - 1)
                idSlotSelected = 0;
            else
                idSlotSelected++;

            slots[idSlotSelected].slotSelected();

        }
        if (left)
        {
            slots[idSlotSelected].slotDeselected();
            if (idSlotSelected == 0)
                idSlotSelected = slots.Count - 1;
            else
                idSlotSelected--;
            slots[idSlotSelected].slotSelected();
        }
    }

    public void SetSlotsCharacters(GameObject[] team)
    {

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < team.Length)
            {
                slots[i].setCharacter(team[i]);
            }
        }

        int j = 0;
        while (j < slots.Count)
        {
            if (!slots[j].getCharacter())
            {
                slots[j].gameObject.SetActive(false);
                slots.Remove(slots[j]);
            }
            else
                j++;
        }
    }

    public void ClearEmptySlots()
    {
        int j = 0;
        //alied y enemy estan invertidos
        while (j < slots.Count)
        {
            if (!slots[j].getCharacter())
            {
                slots[j].gameObject.SetActive(false);
                slots.Remove(slots[j]);
            }
            else
                j++;
        }

    }

    public void ClearSlots()
    {
        foreach(CharacterSlot slot in slots)
        {
            slot.RemoveCharacterInstance();
            slot.gameObject.SetActive(false);
        }
    }

    public void RemoveSlot(CharacterSlot slot)
    {
        if (slots.Contains(slot))
        {
            slots.Remove(slot);
            slot.RemoveCharacterInstance();
            slot.gameObject.SetActive(false);
        }
    }
}
