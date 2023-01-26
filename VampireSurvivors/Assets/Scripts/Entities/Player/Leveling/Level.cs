using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;

    List<UpgradeData> selectedUpgrades;
    [SerializeField] List<UpgradeData> aquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItems passiveItems;

    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
    }
    int To_Level_Up
    {
        get
        {
            return level * 1000;
        }
    }
    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, To_Level_Up);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvailableOnStart);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, To_Level_Up);
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        if (aquiredUpgrades == null) { aquiredUpgrades = new List<UpgradeData>();  }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponGet:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemGet:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);  
                break;
        }




        aquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if (experience >= To_Level_Up)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (selectedUpgrades == null) {  selectedUpgrades = new List<UpgradeData>(); }  
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));
        audioSource.PlayOneShot(clip);

        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= To_Level_Up;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }
        

        return upgradeList;
    }

    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null) { return; }
        this.upgrades.AddRange(upgradesToAdd);  
    }
}
