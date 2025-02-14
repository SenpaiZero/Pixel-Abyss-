﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : MonoBehaviour
{
    private float poisonDamage = 3f;
    private float damageSpeed = 0.3f;

    private float timer = 0;
    private bool isPoison = false;
    private float saveMovementspeed;

    private void Start()
    {
        timer = damageSpeed;
        saveMovementspeed = PlayerPrefs.GetFloat("movementSpeed");
    }

    private void Update()
    {
        timer+=Time.deltaTime;
        if(timer > damageSpeed)
        {
            timer = damageSpeed;
        }


        if(isPoison == true)
        {
            Debug.Log("TickTick");
            if (timer >= damageSpeed)
            {
                Debug.Log("Poison Damage: " + poisonDamage);
                FindObjectOfType<Player>().GetComponent<Player>().playerTakeDamage(poisonDamage);
                timer = 0;
            }
        }
        else
        {
            changeColorExit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString("immuneSlow") != "true")
            {
                changeColorEnter();
                PlayerPrefs.SetFloat("movementSpeed", PlayerPrefs.GetFloat("movementSpeed") / 2);
                PlayerPrefs.Save();
                isPoison = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString("immuneSlow") != "true")
            {
                changeColorExit();
                PlayerPrefs.SetFloat("movementSpeed", saveMovementspeed);
                PlayerPrefs.Save();
                isPoison = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString("immuneSlow") != "true")
            {
                isPoison = true;
                changeColorEnter();
                PlayerPrefs.SetFloat("movementSpeed", PlayerPrefs.GetFloat("movementSpeed") / 2);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString("immuneSlow") != "true")
            {
                isPoison = false;
                changeColorExit();
                PlayerPrefs.SetFloat("movementSpeed", saveMovementspeed);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("movementSpeed", saveMovementspeed);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("movementSpeed", saveMovementspeed);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("movementSpeed", saveMovementspeed);
        PlayerPrefs.Save();
    }

    void changeColorEnter()
    {
        FindObjectOfType<Player>().GetComponent<SpriteRenderer>().color = Color.green;
    }
    void changeColorExit()
    {
        FindObjectOfType<Player>().GetComponent<SpriteRenderer>().color = Color.white;
    }
}
