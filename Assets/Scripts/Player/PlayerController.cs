using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Company.Core.Singleton;
using TMPro;

public class PlayerController : Singleton<PlayerController>
{
    //Public 
    [Header("Lerp")]
    public Transform target;

    public float lerpSpeed = 1f;

    [Header("Player")]

    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;



    public bool invencible = true;

    //Private
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;

    private void Start()
    {
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;
        var _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            if(!invencible) EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    private void EndGame ()
    {
        _canRun = false;
        endScreen.SetActive(true); 
    }

    public void StartToRun()
    {
        _canRun = true;
    }

    #region Power UP's
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s; 
    }
    

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    
    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    
    
    #endregion
}