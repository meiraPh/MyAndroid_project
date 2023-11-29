using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem myparticleSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource; 


    private Collider2D[] colliders;

    private void Awake()
    {
        colliders = GetComponents<Collider2D>();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    private void SetCollidersEnabled(bool enabled)
    {
        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = enabled;
            }
        }
    }

    protected virtual void HideItens()
    {
        if(graphicItem != null) graphicItem.SetActive(false);
        SetCollidersEnabled(false);
        //Invoke("HideObject", timeToHide);        
    }

    protected virtual void Collect()
    {   
        HideItens();
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        if(myparticleSystem != null)
        {
            myparticleSystem.transform.SetParent(null);
            myparticleSystem.Play();
        }

        if (audioSource != null) audioSource.Play();
    }
}
