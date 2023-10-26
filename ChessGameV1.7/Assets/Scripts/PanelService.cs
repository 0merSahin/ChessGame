using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PanelService : MonoBehaviour
{
    public static void PanelOperation(Collider2D hitCollider, GameController gameController)
    {
        print("Yakalanan objenin adı: " + hitCollider.gameObject.name);

        GameObject newSoldier = null;

        if (hitCollider.gameObject.name == "newSoldierPanel1")
            newSoldier = Instantiate(gameController.queenWhite, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel2")
            newSoldier = Instantiate(gameController.castleWhite, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel3")
            newSoldier = Instantiate(gameController.knightWhite, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel4")
            newSoldier = Instantiate(gameController.bishopWhite, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel5")
            newSoldier = Instantiate(gameController.queenBlack, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel6")
            newSoldier = Instantiate(gameController.castleBlack, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel7")
            newSoldier = Instantiate(gameController.knightBlack, gameController.lastPawnTransform.position, Quaternion.identity);
        else if (hitCollider.gameObject.name == "newSoldierPanel8")
            newSoldier = Instantiate(gameController.bishopBlack, gameController.lastPawnTransform.position, Quaternion.identity);
        else Debug.LogError("Objenin ismiyle uyuşan bir koşul bulunamadı! (PanelService.cs>PanelOperation)");

        int soldierSquareID = gameController.boardService.DetectSquareID(gameController.lastPawnTransform);
        int soldierID = gameController.boardService.boardSquareSoldierID[soldierSquareID];
        PawnMove.FightMovePawn(soldierID, soldierSquareID, gameController);
        Destroy(PawnMove.activePanel);
        PawnMove.activePanel = null;

        int squareID = gameController.boardService.DetectSquareID(newSoldier.transform);
        bool isWhite;
        SoldierEnum soldierEnum = 0;

        if (newSoldier.name.ToString().Contains("White")) isWhite = true;
        else isWhite = false;
        if (newSoldier.name.ToString().Contains("Knight")) soldierEnum = SoldierEnum.Knight;
        else if (newSoldier.name.ToString().Contains("Bishop")) soldierEnum = SoldierEnum.Bishop;
        else if (newSoldier.name.ToString().Contains("Castle")) soldierEnum = SoldierEnum.Castle;
        else if (newSoldier.name.ToString().Contains("Queen")) soldierEnum = SoldierEnum.Queen;
        gameController.soldierList.Add(new Soldier(gameController.soldierService.counter, newSoldier, squareID, isWhite, soldierEnum));
        gameController.boardService.BoardAddID(squareID, gameController.soldierService.counter);
        gameController.soldierService.counter++;
        gameController.soldierService.newObjectList.Add(newSoldier);

        gameController.selectSoldierKey = true;
        gameController.moveSoldierKey = true;
        gameController.panelSelectKey = false;
        gameController.lastPawnTransform = null;
    }
}
