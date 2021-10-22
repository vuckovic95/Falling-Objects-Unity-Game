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
        int sameCounter = 0;
        int differentCounter = 0;
        int bonus = 0;
        States.ItemType currentType = new States.ItemType();

        for (int i = 0; i < _pickedItemTypes.Count - 1; i++)
        {
            if(i == 0)
            {
                currentType = _pickedItemTypes[i];
            }
            else
            {
                if (currentType == _pickedItemTypes[i])
                {
                    sameCounter++;
                }
                else
                {
                    differentCounter++;
                    //currentType = 
                }
            }
        }

        Actions.IncreaseScoreAction?.Invoke(bonus);
        PopulateHelperSlotList();
    }
}
