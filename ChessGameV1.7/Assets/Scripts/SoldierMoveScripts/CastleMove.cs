using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CastleMove
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;

    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>CastleMove)");
        else
        {
            int[] boardSquareSoldierID = gameController.boardService.boardSquareSoldierID;
            int squareIDTemp = squareID;

            for (int i = 0; i < (squareID % 8); i++) // sol kontrol
            {
                if (boardSquareSoldierID[--squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (7 - (squareID % 8)); i++) // sağ kontrol
            {
                if (boardSquareSoldierID[++squareIDTemp] == 0)
                {
                    gameController.boardService.SquareColorChange(squareIDTemp, ColorEnum.movableColor);
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    FightDetect(squareIDTemp, gameController);
                    break;
                }
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (squareID / 8); i++) // alt kontrol
            {
                squareIDTemp -= 8;
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
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (7 - (squareID / 8)); i++) // üst kontrol
            {
                squareIDTemp += 8;
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


    public static bool MoveCastle(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (ListService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMoveCastle(soldierSquareID, moveSquareID, gameController);
            soldier.hasMoved = true;
            soldier.squareID = moveSquareID;
            gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
            gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
            soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
            soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);
            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (CastleMove.cs>MoveCastle)");
        return false;
    }


    private static bool FightMoveCastle(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (targetSoldier.soldierEnum != SoldierEnum.King)
        {
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        }
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (CastleMove>FightMoveCastle)");
        return false;
    }
}
