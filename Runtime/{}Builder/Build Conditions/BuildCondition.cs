using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildCondition : ScriptableObject
{
	public abstract bool IsSatisfied(GameObject gameObject, RaycastHit raycastHit, IFieldMemoization fieldMemoization);
}
