﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour {

    public TileGrid Grid { get; set; }
    public TestPlaceCard TestPlacer;

    public int m_TilePosX { get; set; }
    public int m_TilePosY { get; set; }

    // Use this for initialization
    void Start () {
        TestPlacer = GameObject.Find("testCard").GetComponent<TestPlaceCard>();
    }

    void OnMouseDown()
    {
        if(Grid.PlaceNewCard(m_TilePosX, m_TilePosY, TestPlacer.Up, TestPlacer.Right, TestPlacer.Down, TestPlacer.Left, TestPlacer.Middle))
        {
            Debug.Log("placed");
            if (Grid.CompleteRoad(1))
            {
                Debug.Log("there is a road");
            }
            else
            {
                Debug.Log("there no road");
            }
        }
        else
        {
            Debug.Log("cant be placed here");
        }

    }
}
