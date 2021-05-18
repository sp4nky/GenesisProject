using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public StatsUI statsUIPlayer;
    public SizeableBar rPGHealthBar;
    public SizeableBar combatHealthBar;
    public SizeableBar rPGManaBar;
    public SizeableBar combatManaBar;
    public Text playerName;
    //Inventario
    public Text stats;
    public EquipStatsUI equipStats;

    private void Awake()
    {
        GameManagement.instance.stats = this;
    }

    private void Update()
    {
        bool inventory = Input.GetButtonDown("Inventory");
        if(inventory)
        {
            LoadCurrentStats();
        }
    }

    public void LoadCurrentStats()
    {
        CharacterBehaviour playerBehaviour = GameManagement.instance.playerBehaviour;
        stats.text = playerBehaviour.characterData.nivel.ToString() + "\n" +
                        playerBehaviour.characterData.skills[0].damage.ToString() + "\n" +
                        playerBehaviour.getHealth().ToString() + "/" + playerBehaviour.characterData.maxHp.ToString() + "\n" +
                        playerBehaviour.GetMana().ToString() + "/" + playerBehaviour.characterData.maxMana.ToString() + "\n" +
                        playerBehaviour.GetPhysicalDefence().ToString() + "\n" +
                        playerBehaviour.GetMagicalDefence().ToString();

        equipStats.LoadWeaponStats(playerBehaviour.sword);
        if (playerBehaviour.armor) equipStats.LoadArmorStats(playerBehaviour.armor);
        if (playerBehaviour.boots) equipStats.LoadBootsStats(playerBehaviour.boots);
        if (playerBehaviour.helmet) equipStats.LoadHelmetStats(playerBehaviour.helmet);
        if (playerBehaviour.ring) equipStats.LoadShieldStats(playerBehaviour.ring);

    }



    public void UpdatePlayerStatBar(CharacterBehaviour character)
    {
        statsUIPlayer.SetStats(character);
    }
    
    public void SetStatsValues(CharacterBehaviour character)
    {
        CombatStats(character);
        RPGStats(character);
    }

    private void RPGStats(CharacterBehaviour character)
    {
        if (character.characterName == playerName.text)
        {
            rPGHealthBar.SetValue(character.getHealth(), character.characterData.maxHp);
            rPGManaBar.SetValue(character.GetMana(), character.characterData.maxMana);

        }
    }

    private void CombatStats(CharacterBehaviour character)
    {
        if (character.characterName == playerName.text)
        {
            combatHealthBar.SetValue(character.getHealth(), character.characterData.maxHp);
            combatManaBar.SetValue(character.GetMana(), character.characterData.maxMana);

        }
    }
}
