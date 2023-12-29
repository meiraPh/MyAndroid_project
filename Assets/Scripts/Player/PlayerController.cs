using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Company.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //Public 
    [Header("Lerp")]
    public Transform target;

    public float lerpSpeed = 1f;
    
    public Ease ease = Ease.OutBack;

    [Header("Player")]

    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    public bool invencible = true;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("VFX")]
    public ParticleSystem vfxDeath;

    [SerializeField] private BounceHelper _bounceHelper;

    //Private
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPos;
    private float _baseSpeedToAnimation = 12;

    private void Start()
    {
        _startPos = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {
        if(_bounceHelper != null)
            _bounceHelper.Bounce();
    }

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
            if(!invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void EndGame (AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true); 
        animatorManager.Play(animationType);
        if(vfxDeath != null){
            vfxDeath.Play();
        }
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
        transform.DOScale(1, .3f).SetEase(ease);
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
    
    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPos.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPos.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPos.y, .1f);
    }

    public void ChangeCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    
    #endregion
}