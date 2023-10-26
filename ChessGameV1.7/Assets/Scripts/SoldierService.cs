using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierService : MonoBehaviour
{
    GameController gameController;
    BoardService boardService;
    public List<GameObject> newObjectList;
    public int counter = 1;


    public SoldierService(GameController gameController, BoardService boardService)
    {
        this.gameController = gameController;
        this.boardService = boardService;
        newObjectList = new List<GameObject>();
    }


    public void CreateSoldiers()
    {
        try
        {
            newObjectList.Add(Instantiate(gameController.castleWhite, boardService.boardSquare[0].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.knightWhite, boardService.boardSquare[1].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.bishopWhite, boardService.boardSquare[2].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.queenWhite, boardService.boardSquare[3].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.kingWhite, boardService.boardSquare[4].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.bishopWhite, boardService.boardSquare[5].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.knightWhite, boardService.boardSquare[6].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.castleWhite, boardService.boardSquare[7].transform.position, Quaternion.identity));
            for (int i = 8; i < 16; i++) newObjectList.Add(Instantiate(gameController.pawnWhite, boardService.boardSquare[i].transform.position, Quaternion.identity));
            for (int i = 48; i < 56; i++) newObjectList.Add(Instantiate(gameController.pawnBlack, boardService.boardSquare[i].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.castleBlack, boardService.boardSquare[56].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.knightBlack, boardService.boardSquare[57].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.bishopBlack, boardService.boardSquare[58].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.queenBlack, boardService.boardSquare[59].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.kingBlack, boardService.boardSquare[60].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.bishopBlack, boardService.boardSquare[61].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.knightBlack, boardService.boardSquare[62].transform.position, Quaternion.identity));
            newObjectList.Add(Instantiate(gameController.castleBlack, boardService.boardSquare[63].transform.position, Quaternion.identity));
            
            foreach (var item in newObjectList)
            {
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, -1f);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Taşlar çoğaltılırken bir sorun oluştu: \n" + ex);
        }

        try
        {
            foreach (var item in newObjectList)
            {
                int squareID = boardService.DetectSquareID(item.transform);
                bool isWhite;
                SoldierEnum soldierEnum = 0;
                if (item.name.ToString().Contains("White")) isWhite = true;
                else isWhite = false;
                if (item.name.ToString().Contains("Pawn")) soldierEnum = SoldierEnum.Pawn;
                else if (item.name.ToString().Contains("Knight")) soldierEnum = SoldierEnum.Knight;
                else if (item.name.ToString().Contains("Bishop")) soldierEnum = SoldierEnum.Bishop;
                else if (item.name.ToString().Contains("Castle")) soldierEnum = SoldierEnum.Castle;
                else if (item.name.ToString().Contains("Queen")) soldierEnum = SoldierEnum.Queen;
                else if (item.name.ToString().Contains("King")) soldierEnum = SoldierEnum.King;
                gameController.soldierList.Add(new Soldier(counter, item, squareID, isWhite, soldierEnum));
                boardService.BoardAddID(squareID, counter);
                counter++;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Taşların nesneleri oluşturulurken bir sorun oluştu: \n" + ex);
        }
    }


    public void DestroySoldier(int soldierID)
    {
        try
        {
            GameObject destroyGameObject = null;
            GameObject removeGameObject = null;
            Soldier removeSoldierObject = null;
            foreach (var soldier in gameController.soldierList)
            {
                if (soldier.soldierID == soldierID)
                {
                    boardService.boardSquareSoldierID[soldier.squareID] = 0;
                    foreach (var item in newObjectList)
                    {
                        int squareID = boardService.DetectSquareID(item.transform);
                        if (squareID == soldier.squareID)
                        {
                            removeGameObject = item;
                            destroyGameObject = item;
                        }
                    }
                    removeSoldierObject = soldier;
                    break;
                }
            }
            if (destroyGameObject != null)
            {
                string findString;
                if (removeSoldierObject.isWhite)
                {
                    findString = "Square (" + gameController.defeatedWhiteSoldierCount + ")";
                    gameController.defeatedWhiteSoldierCount++;
                }
                else
                {
                    findString = "Square (" + gameController.defeatedBlackSoldierCount + ")";
                    gameController.defeatedBlackSoldierCount++;
                }
                GameObject soldierPanel = GameObject.Find(findString);
                GameObject newObject = Instantiate(destroyGameObject, soldierPanel.transform.position, Quaternion.identity);
                newObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(destroyGameObject);
                destroyGameObject = null;
            }
            if (removeGameObject != null)
            {
                newObjectList.Remove(removeGameObject);
                removeGameObject = null;
            }
            if (removeSoldierObject != null)
            {
                gameController.soldierList.Remove(removeSoldierObject);
                removeSoldierObject = null;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Asker silinmesinde bir hata oluştu: \n" + ex);
        }
    }

    
    public Soldier GetSoldierObjectWithSoldierID(int soldierID)
    {
        foreach (var item in gameController.soldierList)
        {
            if (item.soldierID == soldierID)
            {
                return item;
            }
        }
        Debug.LogError("Talep ettiğin nesne bulunamadı! (SoldierService.cs");
        return null;
    }


    public Soldier GetSoldierObjectWithSquareID(int squareID)
    {
        foreach (var item in gameController.soldierList)
        {
            if (item.squareID == squareID)
            {
                return item;
            }
        }
        Debug.LogError("Talep ettiğin nesne bulunamadı! (SoldierService.cs");
        return null;
    }


    public void ResetSoldierMoveLists(Collider2D collider)
    {
        int squareID = gameController.boardService.DetectSquareID(collider.transform);
        Soldier soldier = gameController.soldierService.GetSoldierObjectWithSquareID(squareID);

        if (soldier.soldierEnum == SoldierEnum.Pawn)
            ListService.ListDeleteAllDataInt(PawnMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Knight)
            ListService.ListDeleteAllDataInt(KnightMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Bishop)
            ListService.ListDeleteAllDataInt(BishopMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Castle)
            ListService.ListDeleteAllDataInt(CastleMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Queen)
            ListService.ListDeleteAllDataInt(QueenMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.King)
            ListService.ListDeleteAllDataInt(KingMove.movableLocationsID);
        else Debug.LogError("Programda bir hata var! (SoldierService.cs>ResetSoldierLists)");
    }


    public void ResetSoldierMoveLists(int squareID) 
    {
        Soldier soldier = gameController.soldierService.GetSoldierObjectWithSquareID(squareID);

        if (soldier.soldierEnum == SoldierEnum.Pawn)
            ListService.ListDeleteAllDataInt(PawnMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Knight)
            ListService.ListDeleteAllDataInt(KnightMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Bishop)
            ListService.ListDeleteAllDataInt(BishopMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Castle)
            ListService.ListDeleteAllDataInt(CastleMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.Queen)
            ListService.ListDeleteAllDataInt(QueenMove.movableLocationsID);
        else if (soldier.soldierEnum == SoldierEnum.King)
            ListService.ListDeleteAllDataInt(KingMove.movableLocationsID);
        else Debug.LogError("Programda bir hata var! (SoldierService.cs>ResetSoldierLists)");
    }
}
