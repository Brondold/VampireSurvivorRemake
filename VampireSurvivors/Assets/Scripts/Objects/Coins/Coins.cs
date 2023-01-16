using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    [SerializeField] DataContainer data;
    [SerializeField] TMPro.TextMeshProUGUI coinsCountText;

    public void Add(int count)
    {
        audioSource.PlayOneShot(clip);
        data.coins += count;
        coinsCountText.text = "Coins:" + data.coins.ToString();
    }

}
