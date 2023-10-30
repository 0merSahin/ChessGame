using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingThreat
{
    public GameController gameController;
    public bool kingThreatKey;
    public int threatedKingSquareID;
    public int threatningSoldierSquareID;


    public KingThreat(GameController gameController)
    {
        this.gameController = gameController;
        kingThreatKey = false;
        threatedKingSquareID = -1;
        threatningSoldierSquareID = -1;
    }
    

    public bool KingThreatDetect(Soldier soldier)
    {
        List<int> movableLocationsID = null;
        gameController.soldierService.ResetSoldierMoveLists(soldier);

        if (soldier.soldierEnum == SoldierEnum.Pawn)
        {
            PawnMove.MoveDetectNotColor(soldier, gameController);
            movableLocationsID = PawnMove.movableLocationsID;
        }
        else if (soldier.soldierEnum == SoldierEnum.Knight)
        {
            KnightMove.MoveDetectNotColor(soldier, gameController);
            movableLocationsID = KnightMove.movableLocationsID;
        }
        else if (soldier.soldierEnum == SoldierEnum.Bishop)
        {
            BishopMove.MoveDetectNotColor(soldier, gameController);
            movableLocationsID = BishopMove.movableLocationsID;
        }
        else if (soldier.soldierEnum == SoldierEnum.Castle)
        {
            CastleMove.MoveDetectNotColor(soldier, gameController);
            movableLocationsID = CastleMove.movableLocationsID;
        }
        else if (soldier.soldierEnum == SoldierEnum.Queen)
        {
            QueenMove.MoveDetectNotColor(soldier, gameController);
            movableLocationsID = QueenMove.movableLocationsID;
        }
        else if (soldier.soldierEnum == SoldierEnum.King)
        {
            Debug.Log("Kral için Thread algılama kodu yazılmadı. O farklı bir algoritma! (KingThreat.cs>KingThreatDetect)");
        }
        else Debug.LogError("Verilen nesne ile eşleşen bir Enum bulunamadı! (KingThreat>KingThreatDetect)");

        
        if (kingThreatKey) // King tehditi algılanmış ise
        {
            Debug.Log("Şah tehditi yapıldı.");
            return true;
        }
        return false;
    }
}
