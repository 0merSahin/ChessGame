using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public static List<int> movableLocationsID = new List<int>();
    public static Soldier soldier;
    private static int addNumber;
    public static GameObject activePanel;



    public static bool MoveDetect(int squareID, int soldierID, GameController gameController)
    {
        soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>PawnMove)");
        else
        {
            if (soldier.isWhite) addNumber = 8;
            else addNumber = -8;

            if (!soldier.hasMoved) // 2 kare gidebilir:
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + addNumber] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + addNumber, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + addNumber);
                    if (gameController.boardService.boardSquareSoldierID[squareID + (addNumber * 2)] == 0)
                    {
                        gameController.boardService.SquareColorChange(squareID + (addNumber * 2), ColorEnum.movableColor);
                        movableLocationsID.Add(squareID + (addNumber * 2));
                    }
                }
            }
            else // Tek kare gidebilir:
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + addNumber] == 0)
                {
                    gameController.boardService.SquareColorChange(squareID + addNumber, ColorEnum.movableColor);
                    movableLocationsID.Add(squareID + addNumber);
                }
            }

            FightDetect(squareID, gameController);
            return true;
        }
        return false;
    }


    public static bool MoveDetectNotColor(Soldier newSoldier, GameController gameController)
    {
        soldier = newSoldier;
        int squareID = soldier.squareID;

        if (soldier == null) Debug.LogError("Soldier nesnesi bulunamadı! (SoldierMoveScripts>PawnMove)");
        else
        {
            if (soldier.isWhite) addNumber = 8;
            else addNumber = -8;

            if (!soldier.hasMoved) // 2 kare gidebilir:
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + addNumber] == 0)
                {
                    movableLocationsID.Add(squareID + addNumber);
                    if (gameController.boardService.boardSquareSoldierID[squareID + (addNumber * 2)] == 0)
                    {
                        movableLocationsID.Add(squareID + (addNumber * 2));
                    }
                }
            }
            else // Tek kare gidebilir:
            {
                if (gameController.boardService.boardSquareSoldierID[squareID + addNumber] == 0)
                {
                    movableLocationsID.Add(squareID + addNumber);
                }
            }

            KingDetect(squareID, gameController);
            return true;
        }
        return false;
    }



    private static void FightDetect(int squareID, GameController gameController)
    {
        if (soldier.isWhite)
        {
            if (squareID + 7 <= 63 && squareID % 8 != 0 && gameController.boardService.boardSquareSoldierID[squareID + 7] != 0 &&
                soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID + 7).isWhite)
            {
                gameController.boardService.SquareColorChange(squareID + 7, ColorEnum.redColor);
                movableLocationsID.Add(squareID + 7);
            }
            if (squareID + 9 <= 63 && squareID % 8 != 7 && gameController.boardService.boardSquareSoldierID[squareID + 9] != 0 &&
                soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID + 9).isWhite)
            {
                gameController.boardService.SquareColorChange(squareID + 9, ColorEnum.redColor);
                movableLocationsID.Add(squareID + 9);
            }
        }
        else
        {
            if (squareID - 7 >= 0 && squareID % 8 != 7 && gameController.boardService.boardSquareSoldierID[squareID - 7] != 0 &&
                soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID - 7).isWhite)
            {
                gameController.boardService.SquareColorChange(squareID - 7, ColorEnum.redColor);
                movableLocationsID.Add(squareID - 7);
            }
            if (squareID - 9 >= 0 && squareID % 8 != 0 && gameController.boardService.boardSquareSoldierID[squareID - 9] != 0 &&
                soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID - 9).isWhite)
            {
                gameController.boardService.SquareColorChange(squareID - 9, ColorEnum.redColor);
                movableLocationsID.Add(squareID - 9);
            }
        }
    }



    private static void KingDetect(int squareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(squareID);
        if (targetSoldier.soldierEnum == SoldierEnum.King)
        {
            if (soldier.isWhite)
            {
                if (squareID + 7 <= 63 && squareID % 8 != 0 && gameController.boardService.boardSquareSoldierID[squareID + 7] != 0 &&
                    soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID + 7).isWhite)
                {
                    gameController.boardService.SquareColorChange(squareID + 7, ColorEnum.threatColor);
                    movableLocationsID.Add(squareID + 7);
                    gameController.kingThreat.kingThreatKey = true;
                    gameController.kingThreat.threatedKingSquareID = squareID + 7;
                    gameController.kingThreat.threatningSoldierSquareID = soldier.squareID;
                }
                if (squareID + 9 <= 63 && squareID % 8 != 7 && gameController.boardService.boardSquareSoldierID[squareID + 9] != 0 &&
                    soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID + 9).isWhite)
                {
                    gameController.boardService.SquareColorChange(squareID + 9, ColorEnum.threatColor);
                    movableLocationsID.Add(squareID + 9);
                    gameController.kingThreat.kingThreatKey = true;
                    gameController.kingThreat.threatedKingSquareID = squareID + 9;
                    gameController.kingThreat.threatningSoldierSquareID = soldier.squareID;
                }
            }
            else
            {
                if (squareID - 7 >= 0 && squareID % 8 != 7 && gameController.boardService.boardSquareSoldierID[squareID - 7] != 0 &&
                    soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID - 7).isWhite)
                {
                    gameController.boardService.SquareColorChange(squareID - 7, ColorEnum.threatColor);
                    movableLocationsID.Add(squareID - 7);
                    gameController.kingThreat.kingThreatKey = true;
                    gameController.kingThreat.threatedKingSquareID = squareID - 7;
                    gameController.kingThreat.threatningSoldierSquareID = soldier.squareID;
                }
                if (squareID - 9 >= 0 && squareID % 8 != 0 && gameController.boardService.boardSquareSoldierID[squareID - 9] != 0 &&
                    soldier.isWhite != gameController.soldierService.GetSoldierObjectWithSquareID(squareID - 9).isWhite)
                {
                    gameController.boardService.SquareColorChange(squareID - 9, ColorEnum.threatColor);
                    movableLocationsID.Add(squareID - 9);
                    gameController.kingThreat.kingThreatKey = true;
                    gameController.kingThreat.threatedKingSquareID = squareID - 9;
                    gameController.kingThreat.threatningSoldierSquareID = soldier.squareID;
                }
            }
        }
    }



    public static bool MovePawn(int soldierSquareID, Collider2D moveCollider, GameController gameController)
    {
        int moveSquareID = gameController.boardService.DetectSquareID(moveCollider.transform);
        if (VariableService.ListIntDetect(moveSquareID, movableLocationsID))
        {
            soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);

            if (gameController.boardService.boardSquareSoldierID[moveSquareID] != 0)
                FightMovePawn(soldierSquareID, moveSquareID, gameController);


            soldier.hasMoved = true;
            soldier.squareID = moveSquareID;
            gameController.boardService.boardSquareSoldierID[soldierSquareID] = 0;
            gameController.boardService.boardSquareSoldierID[moveSquareID] = soldier.soldierID;
            soldier.gameObject.transform.position = gameController.boardService.boardSquare[moveSquareID].transform.position;
            soldier.gameObject.transform.position = new Vector3(soldier.gameObject.transform.position.x, soldier.gameObject.transform.position.y, -1f);

            
            if (soldier.isWhite == true && (soldier.squareID / 8) == 7) // beyaz piyon sınıra ulaştıysa
            {
                GameObject panel = GameObject.Find("SoldierPanelWhite");
                if (panel != null)
                {
                    activePanel = Instantiate(panel, soldier.gameObject.transform.position, Quaternion.identity);
                    if (soldier.squareID % 8 == 0)
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x + 1, activePanel.transform.position.y - 1, -2f);
                    else if (soldier.squareID % 8 == 7)
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x - 1, activePanel.transform.position.y - 1, -2f);
                    else
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x, activePanel.transform.position.y - 1, -2f);
                    // Update methodu Panel algılanmasına geçecek !!!
                    gameController.moveSoldierKey = false;
                    gameController.selectSoldierKey = false;
                    gameController.panelSelectKey = true;
                    gameController.lastPawnTransform = soldier.gameObject.transform;
                    print("GEREKLİ İŞLEMLER YAPILDI");
                }
                else
                {
                    Debug.LogError("Panel objesi bulunamadı! (PawnMove.cs>MovePawn)");
                    return false;
                }
            }
            else if (soldier.isWhite == false && (soldier.squareID / 8) == 0) // siyah piyon sınıra ulaştıysa
            {
                GameObject panel = GameObject.Find("SoldierPanelBlack");
                if (panel != null)
                {
                    activePanel= Instantiate(panel, soldier.gameObject.transform.position, Quaternion.identity);
                    if (soldier.squareID % 8 == 0)
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x + 1, activePanel.transform.position.y + 1, -2f);
                    else if (soldier.squareID % 8 == 7)
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x - 1, activePanel.transform.position.y + 1, -2f);
                    else
                        activePanel.transform.position = new Vector3(activePanel.transform.position.x, activePanel.transform.position.y + 1, -2f);
                    // Update methodu Panel algılanmasına geçecek !!!
                    gameController.moveSoldierKey = false;
                    gameController.selectSoldierKey = false;
                    gameController.panelSelectKey = true;
                    gameController.lastPawnTransform = soldier.gameObject.transform;
                    print("GEREKLİ İŞLEMLER YAPILDI");
                }
                else
                {
                    Debug.LogError("Panel objesi bulunamadı! (PawnMove.cs>MovePawn)");
                    return false;
                }
            }

            // Şah tehditi algılama:
            gameController.kingThreat.KingThreatDetect(soldier);

            return true;
        }
        else Debug.Log("Talep edilen hareket karesi erişilebilir gözükmüyor! (PawnMove.cs>MovePawn)");
        return false;
    }


    public static bool FightMovePawn(int soldierSquareID, int moveSquareID, GameController gameController)
    {
        Soldier targetSoldier = gameController.soldierService.GetSoldierObjectWithSquareID(moveSquareID);
        if (targetSoldier.soldierEnum != SoldierEnum.King)
        {
            gameController.soldierService.DestroySoldier(targetSoldier.soldierID);
        }
        else Debug.LogError("Şah'a saldırmaya çalışıldı! Oyunda hata var (PawnMove>FightMovePawn)");
        return false;
    }
}
