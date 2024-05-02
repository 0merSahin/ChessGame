using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VariableService
{
	public static bool ListIntDetect(int item, List<int> ListInt)
	{
		foreach (var listItem in ListInt)
		{
			if (listItem == item) return true;
		}
		return false;
	}

	public static void ListDeleteAllDataInt(List<int> ListInt)
	{
		while (ListInt.Count > 0)
		{
			ListInt.RemoveAt(0);
		}
	}

	public static void ListDeleteAllDataSoldier(List<Soldier> ListSoldier)
	{
		while (ListSoldier.Count > 0)
		{
			ListSoldier.RemoveAt(0);
		}
	}


	public static void DeleteKingThreatVariableData(GameController gameController)
	{
		gameController.kingThreat.kingThreatKey = false;
		gameController.kingThreat.threatedKingSquareID = -1;
		gameController.kingThreat.threatningSoldierSquareID = -1;
	}

	public static void DeleteAllMovableLocationsList()
	{
		ListDeleteAllDataInt(PawnMove.movableLocationsID);
		ListDeleteAllDataInt(KnightMove.movableLocationsID);
		ListDeleteAllDataInt(CastleMove.movableLocationsID);
		ListDeleteAllDataInt(BishopMove.movableLocationsID);
		ListDeleteAllDataInt(QueenMove.movableLocationsID);
		ListDeleteAllDataInt(KingMove.movableLocationsID);
		Debug.Log("Tüm hareket listeleri boşaltıldı.\n(VariableService.cs>DeleteAllMovableLocationsList)");
	}
}
