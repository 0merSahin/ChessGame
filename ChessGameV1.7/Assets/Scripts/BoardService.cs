using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardService : MonoBehaviour
{
    public GameObject[] boardSquare = new GameObject[64];
    public int[] boardSquareSoldierID = new int[64];
    public bool[] reColoredSquare = new bool[64];
    GameController gameController;

    Color selectColor = new Color(0.112f, 0.255f, 0.157f, 1f);
    Color whiteColor = new Color(1f, 1f, 1f, 1f);
    Color blackColor = new Color(0.3764f, 0.3764f, 0.3764f, 1f);
    Color movableColor = new Color(0f, 0.5f, 0f, 1f);
    Color redColor = new Color(1f, 0.2f, 0f, 1f);
    Color rogColor = new Color(0f, 0.5f, 1f, 1f);

    
    public BoardService(GameController gameController)
    {
        for (int i = 0; i < 64; i++) boardSquareSoldierID[i] = 0;
        for (int i = 0; i < 64; i++) reColoredSquare[i] = false;
        this.gameController = gameController;
        FindBoardSquare();
    }


    private void FindBoardSquare()
    {
        GameObject[] tempBoardSquare = new GameObject[64];
        tempBoardSquare = GameObject.FindGameObjectsWithTag("Board");
        int counter = 0;

        for (int i = 0; i < 64; i++)
        {
            for (int j = 0; j < 64; j++)
            {
                if (int.Parse(tempBoardSquare[j].name.Substring(6, 2)) == counter)
                {
                    boardSquare[i] = tempBoardSquare[j];
                    if (counter % 10 == 7) counter += 3;
                    else counter++;
                    break;
                }
            }
        }
    }


    public void BoardAddID(int squareID, int SoldierID)
    {
        boardSquareSoldierID[squareID] = SoldierID;
    }


    public int DetectSquareID(Transform transform)
    {
        for (int i = 0; i < 64; i++)
        {
            if (transform.position.x == boardSquare[i].transform.position.x && transform.position.y == boardSquare[i].transform.position.y)
            {
                return i;
            }
        }
        Debug.LogWarning("Verilen Transform ile eşleşen obje bulunamadı. (BoardService>DetectSquareID)");
        return -1;
    }



    public void SquareColorChange(int squareID, ColorEnum color)
    {
        reColoredSquare[squareID] = true;
        gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = whiteColor;
        if (color == ColorEnum.selectColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = selectColor;
        }
        else if (color == ColorEnum.whiteColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = whiteColor;
        }
        else if (color == ColorEnum.blackColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = blackColor;
        }
        else if (color == ColorEnum.movableColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = movableColor;
        }
        else if (color == ColorEnum.redColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = redColor;
        }
        else if (color == ColorEnum.rogColor)
        {
            gameController.boardService.boardSquare[squareID].GetComponent<SpriteRenderer>().color = rogColor;
        }
        else Debug.LogError("Fonksiyona gönderdiğiniz renk bulunamadı!");
    }


    public void ResetAllSquareColor()
    {
        for (int squareID = 0; squareID < 64; squareID++)
        {
            if (reColoredSquare[squareID])
            {
                int matrisY = squareID / 8;
                int matrisX = squareID % 8;
                if (matrisY % 2 == 0)
                {
                    if (matrisX % 2 == 0) gameController.boardService.SquareColorChange(squareID, ColorEnum.blackColor);
                    else gameController.boardService.SquareColorChange(squareID, ColorEnum.whiteColor);
                }
                else
                {
                    if (matrisX % 2 == 0) gameController.boardService.SquareColorChange(squareID, ColorEnum.whiteColor);
                    else gameController.boardService.SquareColorChange(squareID, ColorEnum.blackColor);
                }
            }
        }
    }


    public Collider2D GetSquareColliderWithSquareID(int squareID)
    {
        return boardSquare[squareID].GetComponent<BoxCollider2D>();
    }

}
