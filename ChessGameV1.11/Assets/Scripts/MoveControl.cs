using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveControl : MonoBehaviour
{
    GameController gameController;
    BoardService boardService;

    public bool isRokMove = false;
    public bool orderOfMoves = true; // is true: white, is false: black


    public MoveControl(GameController gameController)
    {
        this.gameController = gameController;
        boardService = gameController.boardService;
    }


    public void SelectSoldier(Collider2D collider)
    {
        int squareID = gameController.boardService.DetectSquareID(collider.transform);
        int soldierID = gameController.boardService.boardSquareSoldierID[squareID];
        // soldierID -> 0:kare boş   0'Dan farklı:kare dolu

        if (soldierID != 0)
        {
            Soldier soldier = gameController.soldierService.GetSoldierObjectWithSoldierID(soldierID);
            if (!TurnCheck(soldier, gameController))
            {
                print("Sıra seçilen taşta değil! (MoveControl.cs>SelectSoldier)");
                return;
            }
            
            // Gidebileceği kareler işaretlenecek:
            boardService.SquareColorChange(squareID, ColorEnum.selectColor);
            if (soldier.soldierEnum == SoldierEnum.Pawn)
                PawnMove.MoveDetect(squareID, soldierID, gameController);
            else if (soldier.soldierEnum == SoldierEnum.Knight)
                KnightMove.MoveDetect(squareID, soldierID, gameController);
            else if (soldier.soldierEnum == SoldierEnum.Bishop)
                BishopMove.MoveDetect(squareID, soldierID, gameController);
            else if (soldier.soldierEnum == SoldierEnum.Castle)
                CastleMove.MoveDetect(squareID, soldierID, gameController);
            else if (soldier.soldierEnum == SoldierEnum.Queen)
                QueenMove.MoveDetect(squareID, soldierID, gameController);
            else if (soldier.soldierEnum == SoldierEnum.King)
                KingMove.MoveDetect(squareID, soldierID, gameController);
            else Debug.LogError("Hareket ettirilecek obje yakalanamadı! (MoveControl>SelectSoldier)");
            Debug.Log("Asker Seçildi");
        }
        else
        {
            Debug.Log("Seçilen kare boş");
            gameController.selectSoldierKey = true;
            return;
        }
    }





    public bool MoveSoldier(Collider2D moveCollider, Collider2D soldierCollider, GameController gameController)
    {
        int soldierSquareID = gameController.boardService.DetectSquareID(soldierCollider.transform);
        Soldier soldier = gameController.soldierService.GetSoldierObjectWithSquareID(soldierSquareID);
        if (soldier.soldierEnum == SoldierEnum.Pawn)
            return PawnMove.MovePawn(soldierSquareID, moveCollider, gameController);
        else if (soldier.soldierEnum == SoldierEnum.Knight)
            return KnightMove.MoveKnight(soldierSquareID, moveCollider, gameController);
        else if (soldier.soldierEnum == SoldierEnum.Bishop)
            return BishopMove.MoveBishop(soldierSquareID, moveCollider, gameController);
        else if (soldier.soldierEnum == SoldierEnum.Castle)
            return CastleMove.MoveCastle(soldierSquareID, moveCollider, gameController);
        else if (soldier.soldierEnum == SoldierEnum.Queen)
            return QueenMove.MoveQueen(soldierSquareID, moveCollider, gameController);
        else if (soldier.soldierEnum == SoldierEnum.King)
            return KingMove.MoveKing(soldierSquareID, moveCollider, gameController);
        else Debug.LogError("Hareket ettirilecek obje yakalanamadı! (MoveControl>MoveSoldier)");

        return false;
    }


    private bool TurnCheck(Soldier soldier, GameController gameController)
    {
        if (soldier.isWhite == orderOfMoves)
            return true;
        else
            return false;
    }
}
