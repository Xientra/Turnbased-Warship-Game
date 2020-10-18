using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	List<EnemyUnit> units = new List<EnemyUnit>();

	void Start()
	{
		GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in unitObjects)
		{
			EnemyUnit eu = go.GetComponent<EnemyUnit>();
			if (eu != null)
				units.Add(eu);
		}
	}

	public void TakeTurn(TurnManager responseObject)
	{
		StartCoroutine(ExecuteTurn(responseObject));
	}

	public IEnumerator ExecuteTurn(TurnManager responseObject)
	{
		foreach (EnemyUnit eu in units)
		{
			eu.ResetTurn();
			eu.TakeTurn();
		}

		yield return new WaitForSeconds(1);

		responseObject.EndAITurn();
	}
}
