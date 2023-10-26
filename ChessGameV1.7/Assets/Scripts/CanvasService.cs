using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasService
{
    public Text hamleGostergesi;
    public GameController gameController;


    public CanvasService(GameController gameController)
    {
        this.gameController = gameController;
        hamleGostergesi = GameObject.FindGameObjectWithTag("CanvasText").GetComponent<Text>();
        if (hamleGostergesi == null)
            Debug.LogError("Hamle Göstergesi nesnesi bulunamadı! (CanvasCervice.cs>Constructor)");
        UpdateText();
    }


    public void UpdateText()
    {
        if (gameController.moveControl.orderOfMoves)
            hamleGostergesi.text = "White";
        else
            hamleGostergesi.text = "Black";
    }
}
