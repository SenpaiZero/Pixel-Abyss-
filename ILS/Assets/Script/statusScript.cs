﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statusScript : MonoBehaviour
{
    [Header("STATUS")]
    private float currentHealth;
    private float MaxHealth;
    private float percentageHp;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI hpText;

    [Space]
    private float currentMana;
    private float maxMana;
    [SerializeField] private Image manaFill;
    [SerializeField] private TextMeshProUGUI manaText;

    [Space]
    private float currentExp;
    private float MaxExp;
    [SerializeField] private Image expFill;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelCount;

    [Space]
    private int coins;
    [SerializeField] private TextMeshProUGUI coinsCount;

    [Header("CHEAT")]
    public GameObject cheatObj;
    private GameObject cheatBtn;
    
    private void Start()
    {
        //health
        MaxHealth = PlayerPrefs.GetFloat("health");


        //mana
        maxMana = PlayerPrefs.GetFloat("mana");

        //exp
        currentExp = PlayerPrefs.GetFloat("EXP");
        MaxExp = PlayerPrefs.GetFloat("ExpToLevelUp");
        

        //Coins
        coins = PlayerPrefs.GetInt("Coins");

    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("isCheat") == 1)
        {
            cheatBtn = GameObject.FindGameObjectWithTag("cheatBtn");
            cheatBtn.SetActive(false);
        }

        //-----GET VARIABLES-----
        //Health
        MaxHealth = PlayerPrefs.GetFloat("health");
        currentHealth = PlayerPrefs.GetFloat("currentHp");

        //Mana
        maxMana = PlayerPrefs.GetFloat("mana");
        currentMana = PlayerPrefs.GetFloat("currentMana");
        
        //Exp
        currentExp = PlayerPrefs.GetFloat("EXP");
        MaxExp = PlayerPrefs.GetFloat("ExpToLevelUp");

        //Coins
        coins = PlayerPrefs.GetInt("Coins");


        //-----Read Variables------
        //Health
        healthFill.fillAmount = currentHealth / MaxHealth;
        hpText.text = "" + currentHealth + " / " + MaxHealth;

        //Mana
        manaFill.fillAmount = currentMana / maxMana;
        manaText.text = "" + currentMana + " / " + maxMana;

        //exp
        expFill.fillAmount = currentExp / MaxExp;
        expText.text = "" + currentExp + " / " + MaxExp.ToString("0");
        levelCount.text = "" + PlayerPrefs.GetInt("Level");

        //Coins
        coinsCount.text = "" + coins;
    }

    //CHEATS
    public void cheatExp()
    {
        PlayerPrefs.SetFloat("EXP", PlayerPrefs.GetFloat("EXP") + 50);
        PlayerPrefs.Save();
         sfxMenu();

        if (PlayerPrefs.GetFloat("EXP") >= PlayerPrefs.GetFloat("ExpToLevelUp"))
        {
            //levelup
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().levelUp();

            Debug.Log("LevelUp");
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("skillPoints", PlayerPrefs.GetInt("skillPoints") + 1);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat("EXP", 0);
            PlayerPrefs.SetFloat("ExpToLevelUp", PlayerPrefs.GetFloat("ExpToLevelUp") + (PlayerPrefs.GetFloat("ExpToLevelUp") / 7));
            PlayerPrefs.Save();
            Debug.Log("Level: " + PlayerPrefs.GetInt("Level"));
        }
    }

    public void completeAllStageCheat()
    {
        PlayerPrefs.SetInt("stage 1", 1);
        PlayerPrefs.SetInt("stage 2", 1);
        PlayerPrefs.SetInt("stage 3", 1);
        PlayerPrefs.SetInt("stage 4", 1);
        PlayerPrefs.SetInt("stage 5", 1);
        PlayerPrefs.Save();
    }

    public void cheatCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);
        PlayerPrefs.Save();
        sfxMenu();
    }

    public void cheatDmg()
    {
        PlayerPrefs.SetFloat("wandDamage", PlayerPrefs.GetFloat("wandDamage") + 5);
        PlayerPrefs.Save();
        sfxMenu();
    }

    public void cheatHP()
    {

        PlayerPrefs.SetFloat("health", PlayerPrefs.GetFloat("health") + 10);
        PlayerPrefs.Save();
        sfxMenu();
    }
    public void cheatMana()
    {
        sfxMenu();
        PlayerPrefs.SetFloat("mana", PlayerPrefs.GetFloat("mana") + 10);
        PlayerPrefs.Save();
    }

    public void resetStat()
    {
        sfxMenu();
        //reset EXP
        PlayerPrefs.SetFloat("EXP", 0);
        PlayerPrefs.SetFloat("ExpToLevelUp", 100);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("skillPoints", 0);

        //reset coins
        PlayerPrefs.SetInt("Coins", 0);

        //reset dmg
        PlayerPrefs.SetFloat("wandDamage", 10);

        //reset mana hp
        PlayerPrefs.SetFloat("health", 50);
        PlayerPrefs.SetFloat("mana", 50);


        PlayerPrefs.SetInt("stage 1", 0);
        PlayerPrefs.SetInt("stage 2", 0);
        PlayerPrefs.SetInt("stage 3", 0);
        PlayerPrefs.SetInt("stage 4", 0);
        PlayerPrefs.SetInt("stage 5", 0);
        PlayerPrefs.Save();
    }

    public void closeCheatObj()
    {
        sfxMenu();
        cheatObj.SetActive(false);
    }

    public void openCheatObj()
    {
        sfxMenu();
        cheatObj.SetActive(true);
    }

    void sfxMenu()
    {

        GameObject.FindGameObjectWithTag("MenuSfx").GetComponent<menuSFX>().menuSound();
    }
}
