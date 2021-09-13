using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpeekUI : MonoBehaviour
{
    public static SpeekUI instance { get; private set; }
    public Image speek;
    public Text _text;
    public Image image;
    public List<GameObject> petList = new List<GameObject>();
    public GameObject panel;

    private Button nextBtn;
    private List<PetPos> _petPosList = new List<PetPos>();
    private int nowPetIndex;

    class PetPos
    {
        private float x;
        private float y;
        private int petIndex;
        private int oldPetIndex;

        public float X
        {
            get => x;
            set => x = value;
        }

        public float Y
        {
            get => y;
            set => y = value;
        }

        public int PetIndex
        {
            get => petIndex;
            set => petIndex = value;
        }

        public int OldPetIndex
        {
            get => oldPetIndex;
            set => oldPetIndex = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < petList.Count; i++)
        {
            PetPos data = new PetPos();
            data.X = petList[i].transform.position.x;
            data.Y = petList[i].transform.position.y;
            data.PetIndex = i;
            data.OldPetIndex = i;
            _petPosList.Add(data);
        }

        nowPetIndex = 0;
    }

    public IEnumerator Speek(String text)
    {
        speek.gameObject.SetActive(true);
        Vector2 newPos = new Vector2(speek.preferredWidth + 50, speek.preferredHeight + 50);
        Tweener tweener = speek.transform.DOMove(newPos, 1).SetAutoKill(true);
        yield return tweener.WaitForCompletion();

        Tweener tweener2 = _text.DOText(text, 4).SetAutoKill(true);
        yield return tweener2.WaitForCompletion();

        image.gameObject.SetActive(true);
        nextBtn = speek.GetComponent<Button>();

        nextBtn.enabled = true;
        nextBtn.onClick.AddListener(OpenCheckPets);
    }

    private void OpenCheckPets()
    {
        panel.SetActive(true);
        speek.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
        for (int i = 0; i < petList.Count; i++)
        {
            Button btn = petList[i].transform.Find("Pet").gameObject.GetComponent<Button>();

            btn.onClick.AddListener(() =>
            {
                int index = petList.FindIndex(pet =>
                    pet.transform.Find("Pet").gameObject.GetComponent<Button>() == btn);

                int posIndex = _petPosList.FindIndex(pos => pos.OldPetIndex == index);
                
                if (posIndex != 0 && (posIndex == 1 || posIndex == _petPosList.Count - 1))
                {
                    _petPosList[0].PetIndex = index;
                    nowPetIndex = index;
                    petList[index].transform.DOMove(new Vector2(_petPosList[0].X, _petPosList[0].Y), 1f);
                    petList[index].transform.DOScale(new Vector3(1.5f, 1.5f), 1f);
                    for (int j = 0; j < petList.Count; j++)
                    {
                        if (j != index)
                        {
                            int petPosIndex = _petPosList.FindIndex(pos => pos.OldPetIndex == j);
                            

                            if (posIndex + 1 >= _petPosList.Count)
                            {
                                if (petPosIndex + 1 >= _petPosList.Count)
                                {
                                    petPosIndex = 1;
                                }
                                else
                                {
                                    petPosIndex = petPosIndex + 1;
                                }
                            }
                            else
                            {
                                if (petPosIndex - 1 < 0)
                                {
                                    petPosIndex = _petPosList.Count - 1;
                                }
                                else
                                {
                                    petPosIndex = petPosIndex - 1;
                                }
                            }

                            _petPosList[petPosIndex].PetIndex = j;

                            petList[j].transform
                                .DOMove(new Vector2(_petPosList[petPosIndex].X, _petPosList[petPosIndex].Y), 1f);
                            petList[j].transform.DOScale(new Vector3(1f, 1f), 1f);
                        }
                    }

                    for (int j = 0; j < _petPosList.Count; j++)
                    {
                        _petPosList[j].OldPetIndex = _petPosList[j].PetIndex;
                    }
                }
            });
        }
    }

    private void ChoosePet()
    {
    }
}