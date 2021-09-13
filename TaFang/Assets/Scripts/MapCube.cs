using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector] public GameObject turretGo; //保存cube身上的炮台

    public GameObject buildEffect;
    private Renderer _renderer;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(GameObject turret)
    {
        turretGo = Instantiate(turret, transform.position, Quaternion.identity);
        GameObject bulid = Instantiate(buildEffect, transform.position, buildEffect.transform.rotation);
        Destroy(bulid, 1f);
    }

    private void OnMouseEnter()
    {
        if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            _renderer.material.color = Color.cyan;
        }
    }

    private void OnMouseExit()
    {
        _renderer.material.color = Color.white;
    }
}