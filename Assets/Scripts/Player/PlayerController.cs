using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    //Private
    private bool _canRun;
    private Vector3 _pos;

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;
        var _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            EndGame();
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
}