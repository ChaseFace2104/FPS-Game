using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public int health;
    private int currentHealth;

    public int points = 1;

    public int deathTime;
    private float currentTime = 0;

    private bool active;

    private GameManager gameManager;

    public GameManager GameManager { get { return gameManager; } set { gameManager = value; } }

    private void OnEnable()
    {
        currentHealth = health;
    }

    private void Update()
    {
        if (!active)
        {
            if (currentTime > deathTime)
            {
                currentTime = 0;
                gameObject.transform.GetComponent<MeshRenderer>().enabled = true;
                active = true;
            } else
            {
                currentTime += Time.deltaTime;
                currentHealth = health;
            }
        }
    }

    private void BreakTarget()
    {
        if (gameManager != null)
        {
            gameManager.AddScore(points);
        }
        gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
        active = false;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            BreakTarget();
        }
    }
}
