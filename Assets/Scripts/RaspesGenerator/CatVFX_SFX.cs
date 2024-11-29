using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVFX_SFX : MonoBehaviour
{
    [SerializeField] AudioSource aSource;
    [SerializeField] AudioClip targetCat;
    [SerializeField] AudioClip notTargetCat;
    [SerializeField] AudioClip dead;
    [SerializeField] AudioClip simpleMow;

    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] ParticleSystem DeadVFX;
    [SerializeField] ParticleSystem DecimasVFX0;
    [SerializeField] ParticleSystem DecimasVFX1;
    [SerializeField] ParticleSystem DecimasVFX2;
    [SerializeField] ParticleSystem DecimasVFX5;

    public void SimpleMow()
    {
        aSource.PlayOneShot(simpleMow);
    }

    public void Hit(bool isTarget)
    {
        hitVFX.Play();
        if(isTarget)
        {
            aSource.PlayOneShot(targetCat);
        }
        else
        {
            aSource.PlayOneShot(notTargetCat);
        }
    }

    public void Dead()
    {
        aSource.PlayOneShot(dead);
    }

    public void ShowDecimas(float value)
    {
        if (value == 0) DecimasVFX0.Play();
        else if (value == 0.1f) DecimasVFX1.Play();
        else if (value == 0.2f) DecimasVFX2.Play();
        else if (value == 0.5f) DecimasVFX5.Play();
    }

}
