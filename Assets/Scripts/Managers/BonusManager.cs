using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using System;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{

    /*
    5 različitih elemenata - donosi dodatnih 30 poena
    2 ista + 3 ista ili 3 ista + 2 ista - donosi dodatnih 35 poena
    5 istih elemenata - donosi dodatnih 40 poena
    U svim ostalim slučajevima, polja se resetuju na prazna i korisnik ne dobija bonus.
    */

    [BoxGroup("Slots")]
    [SerializeField]
    private List<Transform> _slots = new List<Transform>();

    private List<Transform> _slotsHelperList = new List<Transform>();
    private List<States.ItemType> _pickedItemTypes = new List<States.ItemType>();
    
    private void Start()
    {
        SubscribeToActions();
        PopulateHelperSlotList();

    }

    private void SubscribeToActions()
    {
        Actions.ItemPickedAction += AddItemToSlot;
        Actions.StartGameAction += PopulateHelperSlotList;
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

    private void PopulateHelperSlotList()
    {
        _slotsHelperList.Clear();
        _pickedItemTypes.Clear();

        foreach (Transform slot in _slots)
        {
            _slotsHelperList.Add(slot);
            slot.GetChild(0).GetComponent<Image>().color = new Color(slot.GetChild(0).GetComponent<Image>().color.r, slot.GetChild(0).GetComponent<Image>().color.g, slot.GetChild(0).GetComponent<Image>().color.b, 0);
        }
    }

    private void CalculateBonus()
    {
        int bonus = 0;
        States.ItemType currentType = new States.ItemType();
        Dictionary<States.ItemType, int> dictionary = new Dictionary<States.ItemType, int>();

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
