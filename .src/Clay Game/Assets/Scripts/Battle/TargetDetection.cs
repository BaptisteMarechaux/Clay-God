using UnityEngine;
using System.Collections;

public class TargetDetection : MonoBehaviour {
    public bool targetFound;
    public BattleUnit battleUnitTargeted;

	void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Character")
        {
            BattleUnit targetedUnit = col.GetComponent<BattleUnit>();

            if(!targetedUnit.IsEnemy)
            {
                targetFound = true;
            }
        }
    }
}
