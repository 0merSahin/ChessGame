using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier
{
    public int soldierID;
    public GameObject gameObject;
    public int squareID; // not matris
    public bool hasMoved;
    public bool isWhite;
    public SoldierEnum soldierEnum;

	public Soldier(int soldierID, GameObject gameObject, int squareID, bool isWhite, SoldierEnum soldierEnum)
	{
		this.soldierID = soldierID;
        this.gameObject = gameObject;
        this.squareID = squareID;
        hasMoved = false;
        this.isWhite = isWhite;
        this.soldierEnum = soldierEnum;
    }
}
