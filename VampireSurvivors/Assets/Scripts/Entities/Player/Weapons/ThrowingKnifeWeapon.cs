using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
    public AudioSource audioSource;
    public AudioClip clip;
    [SerializeField] GameObject knifePrefabRight;
    [SerializeField] GameObject knifePrefabLeft;
    [SerializeField] float spread = 0.5f;
    PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


    

    

    public override void Attack()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            audioSource.PlayOneShot(clip);
            if (playerMove.lastHorizontalVector == 1)
            {
                GameObject thrownKnife = Instantiate(knifePrefabRight);
                Vector3 newKnifePosition = transform.position;
                if (weaponStats.numberOfAttacks > 1)
                {
                    newKnifePosition.y -= (spread * weaponStats.numberOfAttacks-1) / 2;
                    newKnifePosition.y += i * spread;
                }
                
                thrownKnife.transform.position = newKnifePosition;
                ThrowingKnifeProjectile throwingKnifeProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
                throwingKnifeProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
                throwingKnifeProjectile.damage = weaponStats.damage;
            }
            else if (playerMove.lastHorizontalVector == -1)
            {
                GameObject thrownKnife = Instantiate(knifePrefabLeft);
                Vector3 newKnifePosition = transform.position;
                if (weaponStats.numberOfAttacks > 1)
                {
                    newKnifePosition.y -= (spread * weaponStats.numberOfAttacks-1) / 2;
                    newKnifePosition.y += i * spread;
                }
                   
                thrownKnife.transform.position = newKnifePosition;
                ThrowingKnifeProjectile throwingKnifeProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
                throwingKnifeProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
                throwingKnifeProjectile.damage = weaponStats.damage;
            }
        }   
     
        
    }
}
