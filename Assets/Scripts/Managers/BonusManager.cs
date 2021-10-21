using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using System;

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
    
    private void Start()
    {
        SubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.ItemPickedAction += AddItemToSlot;
    }

    private void AddItemToSlot(Item item)
    {

    }
}
