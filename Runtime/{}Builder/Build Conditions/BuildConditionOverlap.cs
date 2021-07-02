using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildConditionOverlap", menuName = "Builder/BuildConditions/BuildConditionOverlap")]
public class BuildConditionOverlap : BuildCondition
{
	public override bool IsSatisfied(GameObject gameObject, RaycastHit raycastHit, IFieldMemoization fieldMemoization) => !fieldMemoization.Occupied(position: gameObject.transform.position);
}
