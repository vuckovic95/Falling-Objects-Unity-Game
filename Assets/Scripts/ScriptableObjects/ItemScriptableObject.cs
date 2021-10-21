using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField]
    private States.ItemType _itemType;
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private int _points;

    private int _speed;

    #region Getters And Setters
    public int SpeedReference
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public int GetPoints
    {
        get { return _points; }
    }

    public States.ItemType Type
    {
        get { return _itemType; }
    }

    public Sprite ItemSprite
    {
        get { return _sprite; }
    }
    #endregion
}
