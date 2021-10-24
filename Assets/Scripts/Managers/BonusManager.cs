using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using System;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    [BoxGroup("Slots")]
    [SerializeField]
    private Transform _slotsHolder;

    private List<GameObject> _slots = new List<GameObject>();
    private List<Transform> _slotsHelperList = new List<Transform>();
    private List<States.ItemType> _pickedItemTypes = new List<States.ItemType>();
    
    private void Start()
    {
        SubscribeToActions();
        PopulateSlots();
        PopulateHelperSlotList();
    }

    private void OnDestroy()
    {
        UnSubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.ItemPickedAction += AddItemToSlot;
        Actions.StartGameAction += PopulateHelperSlotList;
    }

    private void UnSubscribeToActions()
    {
        Actions.ItemPickedAction -= AddItemToSlot;
        Actions.StartGameAction -= PopulateHelperSlotList;
    }

    private void AddItemToSlot(Item item)
    {
       if(_slotsHelperList.Count > 0)
        {
            _slotsHelperList[0].GetChild(0).GetComponent<Image>().sprite = item.Object.ItemSprite;
            _slotsHelperList[0].GetChild(0).GetComponent<Image>().color = new Color(_slotsHelperList[0].GetChild(0).GetComponent<Image>().color.r, _slotsHelperList[0].GetChild(0).GetComponent<Image>().color.g, _slotsHelperList[0].GetChild(0).GetComponent<Image>().color.b, 1);
            _pickedItemTypes.Add(item.Object.Type);
            _slotsHelperList.RemoveAt(0);
        }
        
        if(_slotsHelperList.Count == 0)
        {
            CalculateBonus();
        }
    }

    private void PopulateSlots()
    {
        _slots.Clear();

        foreach (Transform slot in _slotsHolder)
        {
            _slots.Add(slot.gameObject);
        }
    }

    private void PopulateHelperSlotList()
    {
        _slotsHelperList.Clear();
        _pickedItemTypes.Clear();

        foreach (GameObject slot in _slots)
        {
            _slotsHelperList.Add(slot.transform);
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(slot.transform.GetChild(0).GetComponent<Image>().color.r, slot.transform.GetChild(0).GetComponent<Image>().color.g, slot.transform.GetChild(0).GetComponent<Image>().color.b, 0);
        }
    }

    private void CalculateBonus()
    {
        States.ItemType currentType = new States.ItemType();
        Dictionary<States.ItemType, int> dictionary = new Dictionary<States.ItemType, int>();

        //Prvi element dodamo u dictionary i stavimo mu inicijalnu vrednost na 1
        //Za svaki ostali element proveravamo da li je jednak prethodnom.
        //Ako jeste, povecamo mu kolicinu
        //Ako nije, dodamo novi element u dictionary i dodamo inicijalnu kolicinu 1
        //Tako cemo znati koliko razlicitih tipova itema imamo
        for (int i = 0; i < _pickedItemTypes.Count; i++)
        {
            if(i == 0)
            {
                currentType = _pickedItemTypes[i];
                dictionary.Add(_pickedItemTypes[i], 1);
            }
            else
            {
                if (currentType == _pickedItemTypes[i])
                    dictionary[_pickedItemTypes[i]]++;
                else
                {
                    if(dictionary.ContainsKey(_pickedItemTypes[i]))
                        dictionary[_pickedItemTypes[i]]++;

                    else
                        dictionary.Add(_pickedItemTypes[i], 1);
                }
            }
        }

        //Ako imamo samo jedan element u dictionary, znaci da su svi isti
        //Ako 5 elemenata, znaci da su svi razliciti
        //Na kraju sledi provera 2 i 3 ili 3 i 2 gde za svaki od dva elementa u dictionary prolazimo kroz foreach i samo ako mu je value 2 ili 3, povecavamo counter
        //Sto nas dovodi do kraja, ako je counter 2, znaci da oba elementa u dictionary imaju 2 ili 3 vrednost

        //svi su isti
        if(dictionary.Count == 1)
            Actions.BonusPickedAction?.Invoke(40);

        //svi su razliciti
        else if(dictionary.Count == 5)
            Actions.BonusPickedAction?.Invoke(30);

        //2 i 3 ili 3 i 2
        else if(dictionary.Count == 2)
        {
            int counter = 0;
            foreach (var item in dictionary)
            {
                if (item.Value == 2 || item.Value == 3)
                    counter++;
            }
            if(counter == 2)
                Actions.BonusPickedAction?.Invoke(35);
        }

        PopulateHelperSlotList();
    }
}
