using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class StartUI : MonoBehaviour
{
    public GameObject bagGrid;
    public ItemGrid itemPrefab;
    public DataList DataList;

    private Button playerBtn;

    private Button WaiguanBtn;

    private Button PetBtn;

    private Button BombBtn;

    private Button StartBtn;
    private Transform loadPanel;
    private Slider _slider;


    private void Awake()
    {
        playerBtn = transform.Find("PlayerBtn").GetComponent<Button>();
        WaiguanBtn = transform.Find("WaiguanBtn").GetComponent<Button>();
        PetBtn = transform.Find("PetBtn").GetComponent<Button>();
        BombBtn = transform.Find("BombBtn").GetComponent<Button>();
        StartBtn = transform.Find("StartBtn").GetComponent<Button>();
        loadPanel = transform.Find("Panel");
        _slider = loadPanel.Find("Slider").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBtn.onClick.AddListener(ShowPlayer);
        WaiguanBtn.onClick.AddListener(ShowWaiguan);
        PetBtn.onClick.AddListener(ShowPet);
        BombBtn.onClick.AddListener(ShowBomb);
        StartBtn.onClick.AddListener(() => { StartCoroutine(StartGame()); });
    }

    private void ShowBomb()
    {
        ClearFrame();
        for (int i = 0; i < DataList.Bombs.Length; i++)
        {
            ItemGrid itemobj = Instantiate(itemPrefab, bagGrid.transform.position, Quaternion.identity);
            itemobj.gameObject.transform.SetParent(bagGrid.transform);

            GameObject prefabs = (GameObject) Resources.Load("Prefab/Bomb/" + DataList.Bombs[i]);
            itemobj.itemPrefab = prefabs;

            Object[] imgs = Resources.LoadAll("Sprits/Bomb/" + DataList.Bombs[i]);
            itemobj._image.sprite = imgs[1] as Sprite;
        }
    }

    private void ShowPet()
    {
        ClearFrame();
        for (int i = 0; i < DataList.Pets.Length; i++)
        {
            ItemGrid itemobj = Instantiate(itemPrefab, bagGrid.transform.position, Quaternion.identity);
            itemobj.gameObject.transform.SetParent(bagGrid.transform);

            GameObject prefabs = (GameObject) Resources.Load("Prefab/Pet/" + DataList.Pets[i]);
            itemobj.itemPrefab = prefabs;

            Object[] imgs = Resources.LoadAll("Sprits/Pet/" + DataList.Pets[i]);
            itemobj._image.sprite = imgs[1] as Sprite;
            ;
        }
    }

    private void ShowWaiguan()
    {
        ClearFrame();
        for (int i = 0; i < DataList.Appearances.Length; i++)
        {
            ItemGrid itemobj = Instantiate(itemPrefab, bagGrid.transform.position, Quaternion.identity);
            itemobj.gameObject.transform.SetParent(bagGrid.transform);

            GameObject prefabs = (GameObject) Resources.Load("Prefab/Appearance/" + DataList.Appearances[i]);
            itemobj.itemPrefab = prefabs;

            Object[] imgs = Resources.LoadAll("Sprits/Appearance/" + DataList.Appearances[i]);
            itemobj._image.sprite = imgs[1] as Sprite;
        }
    }

    private void ShowPlayer()
    {
        ClearFrame();
        for (int i = 0; i < DataList.Players.Length; i++)
        {
            ItemGrid itemobj = Instantiate(itemPrefab, bagGrid.transform.position, Quaternion.identity);
            itemobj.gameObject.transform.SetParent(bagGrid.transform);

            GameObject prefabs = (GameObject) Resources.Load("Prefab/Player/" + DataList.Players[i]);
            itemobj.itemPrefab = prefabs;

            Object[] imgs = Resources.LoadAll("Sprits/Player/" + DataList.Players[i]);
            itemobj._image.sprite = imgs[1] as Sprite;
        }
    }

    private void ClearFrame()
    {
        if (bagGrid.transform.childCount > 0)
        {
            for (int i = 0; i < bagGrid.transform.childCount; i++)
            {
                Destroy(bagGrid.transform.GetChild(i).gameObject);
            }
        }
    }

    private IEnumerator StartGame()
    {
        loadPanel.gameObject.SetActive(true);
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.IntoGameScene();

        AsyncOperation operation = SceneMgr.instance.ToSceneGame("");

        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            _slider.value = operation.progress;

            if (operation.progress >= 0.9f)
            {
                _slider.value = 1;
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}