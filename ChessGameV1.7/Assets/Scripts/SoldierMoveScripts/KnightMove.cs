using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KnightMove
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;

    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>KnightMove)");
        else
        {
            // 8 farklı yön kontrol edilecek:

            if ((squareID % 8) >= 2 && (squareID / 8) >= 1) // no: 1
            {
                if (gameController.boardService.boardSquareSoldierID[squareID - 10] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 10, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 10);
                }
                else FightDetect(squareID - 10, gameController);
            }
            if ((squareID % 8) >= 1 && (squareID / 8) >= 2) // no: 2
            {
                if (gameController.boardService.boardSquareSoldierID[squareID - 17] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 17, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 17);
                }
                else FightDetect(squareID - 17, gameController);
            }
            if ((squareID % 8) < 7 && (squareID / 8) >= 2) // no: 3
            {
                if (gameController.boardService.boardSquareSoldierID[squareID - 15] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 15, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 15);
                }
                else FightDetect(squareID - 15, gameController);
            }
            if ((squareID % 8) < 6 && (squareID / 8) >= 1) // no: 4
            {
                if (gameController.boardService.boardSquareSoldierID[squareID - 6] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 6, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 6);
                }
                else FightDetect(squareID - 6, gameController);
            }
            if ((squareID % 8) < 6 && (squareID / 8) <= 6) // no: 5
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + 10] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 10, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 10);
                }
                else FightDetect(squareID + 10, gameController);
            }
            if ((squareID % 8) < 7 && (squareID / 8) <= 5) // no: 6
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + 17] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 17, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 17);
                }
                else FightDetect(squareID + 17, gameController);
            }
            if ((squareID % 8) >= 1 && (squareID / 8) <= 5) // no: 7
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + 15] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 15, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 15);
                }
                else FightDetect(squareID + 15, gameController);
            }
            if ((squareID % 8) >= 2 && (squareID / 8) <= 6) // no: 8
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + 6] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 6, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 6);
                }
                else FightDetect(squareID + 6, gameController);
            }
            return true;
        }
        return false;
    }


    public static bool MoveKnight(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (ListService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMoveKnight(soldierSquareID, moveSquareID, gameController);
            soldier.hasMoved = true;
            soldier.squareID = moveSquareID;
            gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
            gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
            soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
            soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);
            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (KnightMove.cs>MoveKnight)");
        return false;
    }



    private static void FightDetect(int squareID, GameController gameController)
    {
        if (gameController.boardService.boardSquareSoldierID[squareID] != 0 &&
            soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID).isWhite)
        {
            gameController.boardService.SquareColorChange(squareID, ColorEnum.redColor);
            movableLocationsID.Add(squareID);
        }
    }


    


    private static bool FightMoveKnight(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (targetSoldier.soldierEnum != SoldierEnum.King)
        {
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        }
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (KnightMove>FightMoveKnight)");
        return false;
    }
}
