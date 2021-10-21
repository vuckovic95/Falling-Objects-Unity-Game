﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Image _image;
    private ItemScriptableObject _itemObject;
    private float _speed;
    private bool _canMove;
    private Transform _transform;

    private void Update()
    {
        if (_canMove)
        {
            _transform.Translate(0, -_speed * Time.deltaTime, 0);
        }
    }

    public void SetProperties()
    {
        _image = GetComponent<Image>();
        _image.sprite = _itemObject.ItemSprite;
        _transform = this.transform;
        _canMove = true;
    }

    #region Getters And Setters 
    public ItemScriptableObject Object
    {
        get { return _itemObject; }
        set { _itemObject = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }
    #endregion
}