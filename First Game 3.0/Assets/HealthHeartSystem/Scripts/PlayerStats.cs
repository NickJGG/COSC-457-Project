/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    public static float health = 3;
    public static float maxHealth = 3;
    public static float maxTotalHealth = 3;

    public static float lastHealth = health;
    public static float lastMaxHealth = maxHealth;
    public static float lastMaxTotalHealth = maxTotalHealth;

    static float currentLevel = 1;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public void Heal(float addingHealth) {
        health += addingHealth;

        ClampHealth();
    }

    public void TakeDamage(float dmg) {
        health -= dmg;

        ClampHealth();

        if (Health <= 0)
            RestartLevel();
    }

    public void AddHealth() {
        if (MaxHealth < MaxTotalHealth) {
            maxHealth += 1;
            health = MaxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth() {
        health = Mathf.Clamp(Health, 0, MaxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Health Pickup")) {
            Heal(1);

            Destroy(other.gameObject);
        } else if (other.CompareTag("Teleporter")) {
            Debug.Log("Current Level: " + currentLevel);

            NextLevel();
        }
    }

    void NextLevel() {
        if (currentLevel < 3) {
            currentLevel++;

            lastHealth = health;
            lastMaxHealth = maxHealth;
            lastMaxTotalHealth = maxTotalHealth;

            SceneManager.LoadScene("Level " + currentLevel);
        } else
            Application.Quit();
    }
    void RestartLevel() {
        health = lastHealth;
        maxHealth = lastMaxHealth;
        maxTotalHealth = lastMaxTotalHealth;

        SceneManager.LoadScene("Level " + currentLevel);
    }
}
