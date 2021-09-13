using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulidManager : MonoBehaviour
{
    public TurretData turret1;

    public TurretData turret2;

    public TurretData turret3;

    public Text moneyText;

    private int money = 2000;

    private TurretData slectedTurretData;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false) //鼠标是否按在UI上
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (hit.collider)
                {
                    MapCube mapCube = hit.collider.gameObject.GetComponent<MapCube>();
                    if (mapCube.turretGo == null && slectedTurretData != null)
                    {
                        if (money >= slectedTurretData.cost)
                        {
                            ChangeMoney(-slectedTurretData.cost);
                            mapCube.BuildTurret(slectedTurretData.turretPrefab);
                        }
                        else
                        {
                            MoneyNotEnough();
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {
                        //LevelUp
                        if (money >= slectedTurretData.costUpgrade)
                        {
                        }
                        else
                        {
                            MoneyNotEnough();
                        }
                    }
                }
            }
        }
    }

    private void MoneyNotEnough()
    {
        Animator animator = moneyText.GetComponent<Animator>();
        animator.SetTrigger("Flicker");
    }

    private void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    public void OnTurret1Slected(bool isOn)
    {
        if (isOn)
        {
            slectedTurretData = turret1;
        }
    }

    public void OnTurret2Slected(bool isOn)
    {
        if (isOn)
        {
            slectedTurretData = turret2;
        }
    }

    public void OnTurret3Slected(bool isOn)
    {
        if (isOn)
        {
            slectedTurretData = turret3;
        }
    }
}