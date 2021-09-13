using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class ItemGrid : MonoBehaviour
{
    public Image _image;
    public Button _button;
    public GameObject itemPrefab;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(ChooseItem);
    }

    private void ChooseItem()
    {
        if (DataList.Players.Contains(itemPrefab.name))
        {
            ChoosePlayer();
        }
        else if (DataList.Pets.Contains(itemPrefab.name))
        {
            ChoosePet();
        }
        else if (DataList.Appearances.Contains(itemPrefab.name))
        {
            ChooseWaiguan();
        }
        else if (DataList.Bombs.Contains(itemPrefab.name))
        {
            ChooseBomb();
        }
    }

    private void ChoosePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject newPlayer = Instantiate(itemPrefab, player.transform.position, Quaternion.identity);
        // newPlayer.transform.localScale *= 2f;
        PlayerController playerController = newPlayer.transform.GetComponent<PlayerController>();

        Transform tou = player.transform.Find("Tou");
        if (tou.childCount > 0)
        {
            WaiguanData waiguanData = tou.GetChild(0).GetComponent<WaiguanData>();
            tou.GetChild(0).SetParent(newPlayer.transform.Find("Tou"));
            waiguanData.ChangeParent(playerController);
        }

        Transform bei = player.transform.Find("Bei");
        if (bei.childCount > 0)
        {
            WaiguanData waiguanData = bei.GetChild(0).GetComponent<WaiguanData>();
            bei.GetChild(0).SetParent(newPlayer.transform.Find("Bei"));
            waiguanData.ChangeParent(playerController);
        }

        Transform jiao = player.transform.Find("Jiao");
        if (jiao.childCount > 0)
        {
            WaiguanData waiguanData = jiao.GetChild(0).GetComponent<WaiguanData>();
            jiao.GetChild(0).SetParent(newPlayer.transform.Find("Jiao"));
            waiguanData.ChangeParent(playerController);
        }

        Destroy(player);
    }

    private void ChooseWaiguan()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject waiguanObj;

        switch (itemPrefab.GetComponent<WaiguanData>().type)
        {
            case 1:
                Transform tou = player.transform.Find("Tou");
                if (tou.childCount > 0)
                {
                    Destroy(tou.GetChild(0).gameObject);
                }

                waiguanObj = Instantiate(itemPrefab, tou.position, quaternion.identity);
                waiguanObj.transform.SetParent(tou);
                break;
            case 2:
                Transform bei = player.transform.Find("Bei");
                if (bei.childCount > 0)
                {
                    Destroy(bei.GetChild(0).gameObject);
                }

                waiguanObj = Instantiate(itemPrefab, bei.position, quaternion.identity);
                waiguanObj.transform.SetParent(bei);
                break;
            case 3:
                Transform jiao = player.transform.Find("Jiao");
                if (jiao.transform.childCount > 0)
                {
                    Destroy(jiao.transform.GetChild(0).gameObject);
                }

                waiguanObj = Instantiate(itemPrefab, jiao.position, quaternion.identity);
                waiguanObj.transform.SetParent(jiao);
                break;
        }
    }

    private void ChoosePet()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Transform oldPet = player.transform.Find("pet");
        if (oldPet)
        {
            Destroy(oldPet.gameObject);
        }
        
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        Vector2 lookAt = playerController.lookAt;
        GameObject pet;
        Vector3 petPos = new Vector3();

        if (lookAt.x >= 0 && lookAt.x >= Math.Abs(lookAt.y))
        {
            petPos = player.GetComponent<Rigidbody2D>().position - Vector2.right * 0.5f;
        }
        else if (lookAt.x < 0 && Math.Abs(lookAt.x) >= Math.Abs(lookAt.y))
        {
            petPos = player.GetComponent<Rigidbody2D>().position - Vector2.left * 0.5f;
        }
        else if (lookAt.y >= 0 && lookAt.y > Math.Abs(lookAt.x))
        {
            petPos = player.GetComponent<Rigidbody2D>().position - Vector2.up * 0.5f;
        }
        else if (lookAt.y < 0 && Math.Abs(lookAt.y) > Math.Abs(lookAt.x))
        {
            petPos = player.GetComponent<Rigidbody2D>().position - Vector2.down * 0.8f;
        }

        pet = Instantiate(itemPrefab, petPos, Quaternion.identity);
        pet.name = "pet";
        pet.GetComponent<Renderer>().sortingOrder = -1;
        pet.transform.SetParent(player.transform);
    }

    private void ChooseBomb()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ChooseBomb(itemPrefab);
    }
}