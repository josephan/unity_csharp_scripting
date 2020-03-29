using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private float _timeRemaining;

    public float TimeRemaining
    {
        get { return _timeRemaining; }
        set { _timeRemaining = value; }
    }

    private int _numCoins;

    public int NumCoins
    {
        get { return _numCoins; }
        set { _numCoins = value; }
    }

    private float _playerHealth;

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    private int maxHealth = 3;

    private float maxTime = 5 * 60;

    private bool isInvulnerable = false;

    void OnEnable()
    {
        DamagePlayerEvent.OnDamagePlayer += DecrementPlayerHealth;
    }

    void OnDisable()
    {
        DamagePlayerEvent.OnDamagePlayer -= DecrementPlayerHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0)
        {
            Restart();
        }
    }

    private void DecrementPlayerHealth(GameObject player)
    {
        if (isInvulnerable)
        {
            return;
        }

        StartCoroutine(InvulnerableDelay());

        PlayerHealth--;

        if (PlayerHealth <= 0)
        {
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;
    }

    private IEnumerator InvulnerableDelay()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1.0f);
        isInvulnerable = false;
    }

    public float GetPlayerHealthPercentage()
    {
        return PlayerHealth / (float)maxHealth;
    }
}
