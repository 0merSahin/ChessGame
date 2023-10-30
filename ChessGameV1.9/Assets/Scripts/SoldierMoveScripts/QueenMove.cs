using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QueenMove
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;

    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>QueenMove)");
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


    public static bool MoveDetectNotColor(Soldier newSoldier, GameController gameController)
    {
        soldier = newSoldier;
        int squareID = soldier.squareID;

        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>QueenMove)");
        else
        {
            int[] boardSquareSoldierID = gameController.boardService.boardSquareSoldierID;
            int squareIDTemp = squareID;

            for (int i = 0; i < (squareID % 8); i++) // sol kontrol
            {
                if (boardSquareSoldierID[--squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (7 - (squareID % 8)); i++) // sağ kontrol
            {
                if (boardSquareSoldierID[++squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (squareID / 8); i++) // alt kontrol
            {
                squareIDTemp -= 8;
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
            }
            squareIDTemp = squareID;
            for (int i = 0; i < (7 - (squareID / 8)); i++) // üst kontrol
            {
                squareIDTemp += 8;
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
            }

            for (squareIDTemp = squareID - 9; squareIDTemp > -1 && (squareID % 8) != 0; squareIDTemp -= 9) // sol alt çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID - 7; squareIDTemp > -1 && (squareID % 8) != 7; squareIDTemp -= 7) // sağ alt çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID + 7; (squareIDTemp / 8) < 8 && (squareID % 8) != 0; squareIDTemp += 7) // sol üst çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
                    break;
                }
                if (squareIDTemp % 8 == 7 || squareIDTemp % 8 == 0) break;
            }
            for (squareIDTemp = squareID + 9; (squareIDTemp / 8) < 8 && (squareID % 8) != 7; squareIDTemp += 9) // sağ üst çapraz kontrol
            {
                if (boardSquareSoldierID[squareIDTemp] == 0)
                {
                    movableLocationsID.Add(squareIDTemp);
                }
                else
                {
                    KingDetect(squareIDTemp, gameController);
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


    private static void KingDetect(int squareID, GameController gameController)
    {
        if (gameController.boardService.boardSquareSoldierID[squareID] != 0 &&
            soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID).isWhite &&
            gameController.soldierService.GetSoldierObjectWithSquareID(squareID).soldierEnum == SoldierEnum.King)
        {
            gameController.boardService.SquareColorChange(squareID, ColorEnum.threatColor);
            gameController.boardService.SquareColorChange(soldier.squareID, ColorEnum.threatColor);
            movableLocationsID.Add(squareID);
            gameController.kingThreat.kingThreatKey = true;
            gameController.kingThreat.threatedKingSquareID = squareID;
            gameController.kingThreat.threatningSoldierSquareID = soldier.squareID;
        }
    }



    public static bool MoveQueen(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (VariableService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMoveQueen(soldierSquareID, moveSquareID, gameController);
            soldier.hasMoved = true;
            soldier.squareID = moveSquareID;
            gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
            gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
            soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
            soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);

            // Şah tehditi algılama:
            gameController.kingThreat.KingThreatDetect(soldier);

            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (QueenMove.cs>MoveQueen)");
        return false;
    }


    private static bool FightMoveQueen(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (targetSoldier.soldierEnum != SoldierEnum.King)
        {
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        }
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (QueenMove>FightMoveQueen)");
        return false;
    }
}
