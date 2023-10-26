using System;
using System.Collections;
using System.Collections.Generic;

public static class ListService
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
}
