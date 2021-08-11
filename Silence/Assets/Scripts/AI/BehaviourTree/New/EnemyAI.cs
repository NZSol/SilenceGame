using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyAI : MonoBehaviour
{
    [SerializeField] float initialHealth;
    float curHealth
    {
        get { return curHealth; }
        set { curHealth = Mathf.Clamp(value, 0, initialHealth); }
    }
    [SerializeField] float lowHealthThreshhold;
    [SerializeField] float healthRestoreRate;

    [SerializeField] float shootRange;
    [SerializeField] float chaseRange;

    [SerializeField] Transform playerTransform;

    private void Start()
    {
        curHealth = initialHealth;
    }

    public float GetCurrentHealth()
    {
        return curHealth;
    }
}
