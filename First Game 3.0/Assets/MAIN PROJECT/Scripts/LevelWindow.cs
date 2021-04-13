using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    private Text levelText;
    private Text lAttackText;
    private Text hAttackText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;

    private void Awake() {
        levelText = transform.Find("levelText").GetComponent<Text>();
        lAttackText = transform.Find("lAttackPower").GetComponent<Text>();
        hAttackText = transform.Find("hAttackPower").GetComponent<Text>();
        experienceBarImage = transform.Find("ExpBar").GetComponent<Image>();
    }

    private void SetExperienceBarSize(float experienceNormalized){
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber) {
        levelText.text = "Level "+ (int)levelNumber;
    }

    private void SetLightAttack(int lAttack) {
        lAttackText.text = "Light Attack: "+ (int)lAttack;
    }

    private void SetHeavyAttack(int hAttack) {
        hAttackText.text = "Heavy Attack: "+ (int)hAttack;
    }

    public void SetLevelSystem(LevelSystem levelSystem){
        //Set the LevelSystem Object
        this.levelSystem = levelSystem;

        //Update the starting values
        Debug.Log(levelSystem.GetLevelNumber());
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetLightAttack(levelSystem.GetLightAttack());
        SetHeavyAttack(levelSystem.GetHeavyAttack());
        SetExperienceBarSize(levelSystem.GetExperinceNormalized());

        //Subscribe to events
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e) {
        // Level CHanged update text
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetLightAttack(levelSystem.GetLightAttack());
        SetHeavyAttack(levelSystem.GetHeavyAttack());
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e) {
        // Exp Changed update bar
        SetExperienceBarSize(levelSystem.GetExperinceNormalized());
    }
}