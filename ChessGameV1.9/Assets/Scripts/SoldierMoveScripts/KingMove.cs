using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KingMove
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;

    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>KingMove)");
        else
        {
            int[] boardSquareSoldierID = gameController.boardService.boardSquareSoldierID;

            // Hareket tespiti (8 yönlü):
            if ((squareID / 8 ) != 0) // alt kontrol
            {
                if ((squareID % 8) != 0)
                {
                    if (boardSquareSoldierID[squareID - 9] == 0)
                    {
                        gameController.boardService.SquareColorChange(squareID - 9, ColorEnum.movableColor);
                        movableLocationsID.Add(squareID - 9);
                    }
                    else FightDetect(squareID - 9, gameController);
                }

                if (boardSquareSoldierID[squareID - 8] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 8, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 8);
                }
                else FightDetect(squareID - 8, gameController);

                if ((squareID % 8) != 7)
                {
                    if (boardSquareSoldierID[squareID - 7] == 0)
                    {
                        gameController.boardService.SquareColorChange(squareID - 7, ColorEnum.movableColor);
                        movableLocationsID.Add(squareID - 7);
                    }
                    else FightDetect(squareID - 7, gameController);
                }
            }

            if ((squareID % 8) != 0)
            {
                if (boardSquareSoldierID[squareID - 1] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID - 1, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID - 1);
                }
                else FightDetect(squareID - 1, gameController);
            }

            if ((squareID % 8) != 7)
            {
                if (boardSquareSoldierID[squareID + 1] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 1, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 1);
                }
                else FightDetect(squareID + 1, gameController);
            }

            if ((squareID / 8) != 7) // üst kontrol
            {
                if ((squareID % 8) != 0)
                {
                    if (boardSquareSoldierID[squareID + 7] == 0)
                    {
                        gameController.boardService.SquareColorChange(squareID + 7, ColorEnum.movableColor);
                        movableLocationsID.Add(squareID + 7);
                    }
                    else FightDetect(squareID + 7, gameController);
                }

                if (boardSquareSoldierID[squareID + 8] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + 8, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + 8);
                }
                else FightDetect(squareID + 8, gameController);

                if ((squareID % 8) != 7)
                {
                    if (boardSquareSoldierID[squareID + 9] == 0)
                    {
                        gameController.boardService.SquareColorChange(squareID + 9, ColorEnum.movableColor);
                        movableLocationsID.Add(squareID + 9);
                    }
                    else FightDetect(squareID + 9, gameController);
                }
            }


            // ROG ALGILAMA:
            if (!soldier.hasMoved)
            {
                // sol rog:
                if (boardSquareSoldierID[squareID - 1] == 0)
                {
                    if (boardSquareSoldierID[squareID - 2] == 0)
                    {
                        if (boardSquareSoldierID[squareID - 3] == 0)
                        {
                            if (boardSquareSoldierID[squareID - 4] != 0)
                            {
                                Soldier soldierTemp = gameController.soldierService.GetSoldierObjectWithSquareID(squareID - 4);
                                if (soldierTemp.soldierEnum == SoldierEnum.Castle)
                                {
                                    if (!soldierTemp.hasMoved)
                                    {
                                        gameController.boardService.SquareColorChange(squareID, ColorEnum.selectColor);
                                        gameController.boardService.SquareColorChange(squareID - 2, ColorEnum.movableColor);
                                        gameController.boardService.SquareColorChange(squareID - 4, ColorEnum.rogColor);
                                        movableLocationsID.Add(squareID);
                                        movableLocationsID.Add(squareID - 2);
                                        movableLocationsID.Add(squareID - 4);
                                    }
                                }
                            }
                        }
                    }
                }
                // sağ rog:
                if (boardSquareSoldierID[squareID + 1] == 0)
                {
                    if (boardSquareSoldierID[squareID + 2] == 0)
                    {
                        if (boardSquareSoldierID[squareID + 3] != 0)
                        {
                            Soldier soldierTemp = gameController.soldierService.GetSoldierObjectWithSquareID(squareID + 3);
                            if (soldierTemp.soldierEnum == SoldierEnum.Castle)
                            {
                                if (!soldierTemp.hasMoved)
                                {
                                    gameController.boardService.SquareColorChange(squareID, ColorEnum.selectColor);
                                    gameController.boardService.SquareColorChange(squareID + 2, ColorEnum.movableColor);
                                    gameController.boardService.SquareColorChange(squareID + 3, ColorEnum.rogColor);
                                    movableLocationsID.Add(squareID);
                                    movableLocationsID.Add(squareID + 2);
                                    movableLocationsID.Add(squareID + 3);
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
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

    
    public static bool MoveKing(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (VariableService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMoveKing(soldierSquareID, moveSquareID, gameController);

            else // Boş olsa da rok atılmak isteniyor olabilir.
            {
                if ((moveSquareID + 2) == soldier.squareID || (moveSquareID - 2) == soldier.squareID)
                {
                    Soldier targetSoldier;
                    if (moveSquareID < soldier.squareID)
                        targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldier.squareID - 4);
                    else
                        targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldier.squareID + 3);
                    RokMoveKing(soldier, targetSoldier, gameController);
                }
            }

            if (!gameController.moveControl.isRokMove)
            {
                soldier.hasMoved = true;
                soldier.squareID = moveSquareID;
                gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
                gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
                soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
                soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);
            }
            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (KingMove.cs>MoveKing)");
        return false;
    }
    


    private static bool FightMoveKing(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier kingSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (kingSoldier.isWhite == targetSoldier.isWhite)
            return RokMoveKing(kingSoldier, targetSoldier, gameController);
        
        else if (targetSoldier.soldierEnum != SoldierEnum.King)
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (KingMove>FightMoveKing)");
        return false;
    }



    private static bool RokMoveKing(Soldier kingSoldier, Soldier castleSoldier, GameController gameController)
    {
        if (kingSoldier.soldierEnum == SoldierEnum.King && castleSoldier.soldierEnum == SoldierEnum.Castle)
        {
            kingSoldier.hasMoved = true;
            castleSoldier.hasMoved = true;
            gameController.boardService.boardSquareSoldierID[kingSoldier.squareID] = 0;
            gameController.boardService.boardSquareSoldierID[castleSoldier.squareID] = 0;

            if (kingSoldier.squareID > castleSoldier.squareID) // uzun rok
            {
                kingSoldier.squareID -= 2;
                castleSoldier.squareID += 3;
            }
            else // kısa rok
            {
                kingSoldier.squareID += 2;
                castleSoldier.squareID -= 2;
            }
            gameController.boardService.boardSquareSoldierID[kingSoldier.squareID] = kingSoldier.soldierID;
            gameController.boardService.boardSquareSoldierID[castleSoldier.squareID] = castleSoldier.soldierID;
            kingSoldier.gameObject.transform.position = gameController.boardService.boardSquare[kingSoldier.squareID].transform.position;
            castleSoldier.gameObject.transform.position = gameController.boardService.boardSquare[castleSoldier.squareID].transform.position;
            kingSoldier.gameObject.transform.position = new Vector3(kingSoldier.gameObject.transform.position.x, kingSoldier.gameObject.transform.position.y, -1f);
            castleSoldier.gameObject.transform.position = new Vector3(castleSoldier.gameObject.transform.position.x, castleSoldier.gameObject.transform.position.y, -1f);
            gameController.soldierService.ResetSoldierMoveLists(kingSoldier.squareID);
            gameController.soldierService.ResetSoldierMoveLists(castleSoldier.squareID);

            gameController.moveControl.isRokMove = true;
            return true;
        }
        else Debug.LogError("Algoritmalarda hata var! (KingMove.cs>RokMoveKing)");
        return false;
    }
}
