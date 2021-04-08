using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem 
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level = 0;
    private int experience;
    private int experinceToNextLevel;
    private int lightAttack;
    private int heavyAttack;

    public LevelSystem(){
        level = (int)0;
        experience = 0;
        experinceToNextLevel = 100;
        lightAttack = (int)1;
        heavyAttack = (int)3;
    }

    public void AddExperience(int amount) {
        experience += amount;
        if (experience >= experinceToNextLevel) {
            // Enough experince to level up
            level++;
            lightAttack = lightAttack + 1;
            heavyAttack = heavyAttack * 2;
            experience -= experinceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber() {
        return level;
    }

    public int GetLightAttack() {
        return lightAttack;
    }

    public int GetHeavyAttack() {
        return heavyAttack;
    }

    public float GetExperinceNormalized() {
        return (float)experience / experinceToNextLevel;
    }

}