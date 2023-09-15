using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public float duration = .3f;
    public MeshRenderer meshRenderer;

    public Color startColor = Color.black;

    private Color _correctcolor;


    private ColorChange _startColor;
    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctcolor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_correctcolor, duration).SetDelay(.5f);;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            LerpColor();
        }
    }
}
