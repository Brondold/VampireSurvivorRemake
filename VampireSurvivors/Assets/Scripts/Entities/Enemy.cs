using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 999;
    public int damage = 1;
    public int experience_reward = 400;
    public float moveSpeed = 1f;
    

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp; 
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);  
    }
}




public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestination;
    GameObject targetGameObject;
    Character targetCharacter;
    

    public EnemyStats stats;

    Rigidbody2D rgbd2d;

    


    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        //targetGameObject = targetDestination.gameObject;
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;

    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgbd2d.velocity = direction * stats.moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
      if (collision.gameObject == targetGameObject )
        {
            Attack();
        }
    }

    private void Attack()
    {
         if (targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.hp -= damage; 
        if (stats.hp < 1)
        {
            targetGameObject.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
