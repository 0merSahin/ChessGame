using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BishopMove
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;

    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>BishopMove)");
        else
        {
            int[] boardSquareSoldierID = gameController.boardService.boardSquareSoldierID;
            int squareIDTemp;

            for (squareIDTemp = squareID - 9; squareIDTemp > -1 && (squareID % 8) != 0; squareIDTemp -= 9) // sol alt çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID - 7; squareIDTemp > -1 && (squareID % 8) != 7; squareIDTemp -= 7) // sağ alt çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID + 7; (squareIDTemp / 8) < 8 && (squareID % 8) != 0; squareIDTemp += 7) // sol üst çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID + 9; (squareIDTemp / 8) < 8 && (squareID % 8) != 7; squareIDTemp += 9) // sağ üst çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            return true;
        }
        return false;
    }


    private static void FightDetect(int squareIDTemp, GameController gameController)
    {
        if (gameController.boardService.boardSquareSoldierID[squareIDTemp] != 0 &&
            soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareIDTemp).isWhite)
        {
            gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.redColor);
            movableLocationsID.Add(squareIDTemp);
        }
    }


    public static bool MoveBishop(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (ListService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMoveBishop(soldierSquareID, moveSquareID, gameController);
            soldier.hasMoved = true;
            soldier.squareID = moveSquareID;
            gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
            gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
            soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
            soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);
            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (BishopMove.cs>MoveBishop)");
        return false;
    }


    private static bool FightMoveBishop(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (targetSoldier.soldierEnum != SoldierEnum.King)
        {
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        }
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (BishopMove>FightMoveBishop)");
        return false;
    }
}
