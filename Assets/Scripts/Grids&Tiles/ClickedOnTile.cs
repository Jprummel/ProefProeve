﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour
{
    public delegate void TileClicked(Vector2 tilePosition);
    public static TileClicked s_OnTileClicked;

    TestPlaceCard TestPlacer = TestPlaceCard.s_Instance;
    TileGrid grid = TileGrid.s_Instance;

    public int TilePosX { get; set; }
    public int TilePosY { get; set; }


    void OnMouseDown()
    {
        if(TestPlacer.debugModus)
        {
            DebugPlaceMent();
            return;
        }

        if (CardSelector.s_Instance.SelectedCard == null) return;

        if (s_OnTileClicked != null) s_OnTileClicked(new Vector2(TilePosX, TilePosY));        
    }

    void DebugPlaceMent()
    {
        if(TestPlacer.Rotate)
        {
            grid.RotateCard(TilePosX, TilePosY);
            if (grid.CompleteRoad(TestPlacer.checkPlayer))
            {
                Debug.Log("there is a road for player" + TestPlacer.checkPlayer);
            }
            else
            {
                Debug.Log("there is no road");
            }
        }
        else if (TestPlacer.SwapCards)
        {
            TestPlacer.selectedCards++;
            if (TestPlacer.selectedCards == 1)
            {
                Debug.Log("set one");
                TestPlacer.selectOne.x = TilePosX;
                TestPlacer.selectOne.y = TilePosY;
                TestPlacer.selectedCards++;
            }
            else if (TestPlacer.selectedCards >= 2)
            {
                Debug.Log("set two");
                TestPlacer.selectTwo.x = TilePosX;
                TestPlacer.selectTwo.y = TilePosY;
                if (grid.MoveNode((int)TestPlacer.selectOne.x, (int)TestPlacer.selectOne.y, (int)TestPlacer.selectTwo.x, (int)TestPlacer.selectTwo.y))
                {
                    Debug.Log("swip swap");
                    if (grid.CompleteRoad(TestPlacer.checkPlayer))
                    {
                        Debug.Log("there is a road for player" + TestPlacer.checkPlayer);
                    }
                    else
                    {
                        Debug.Log("there is no road");
                    }
                }
                else
                {
                    Debug.Log("no swap");
                }

                TestPlacer.selectedCards = 0;
            }
        }
        else if (TestPlacer.Destroy)
        {
            if (grid.DestroyNode(TilePosX, TilePosY))
            {
                Debug.Log("Destroy D");
            }
            else
            {
                Debug.Log("YOU HAVE NO POWER HERE");
            }
        }
        else
        {
            if (grid.PlaceNewCard(TilePosX, TilePosY, TestPlacer.Up, TestPlacer.Right, TestPlacer.Down, TestPlacer.Left, TestPlacer.Middle,TestPlacer.ignoreConnection,TestPlacer.ignoreRules,TestPlacer.playAnim,TestPlacer.delayValue))
            {
                Debug.Log("placed");
                if (grid.CompleteRoad(TestPlacer.checkPlayer))
                {
                    Debug.Log("there is a road for player"+ TestPlacer.checkPlayer);
                }
                else
                {
                    Debug.Log("there is no road");
                }
            }
            else
            {
                Debug.Log("cant be placed here");
            }
        }
    }
}
