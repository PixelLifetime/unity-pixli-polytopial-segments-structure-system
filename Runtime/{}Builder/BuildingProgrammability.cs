//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "[Building Programmability]", menuName = "Builder/[Building Programmability]")]
//public class BuildingProgrammability : ScriptableObject
//{
//	[SerializeField] private PlacementModule _placementModule;
//	public PlacementModule _PlacementModule => this._placementModule;

//	[SerializeField] private BuildCondition _buildCondition;
//	public BuildCondition _BuildCondition => this._buildCondition;

//	public void Execute(GameObject gameObject, RaycastHit raycastHit, Field field) => this._placementModule.Place(
//		gameObject: gameObject,
//		raycastHit: raycastHit,
//		field: field
//	);

//	public bool BuildPermitted(GameObject gameObject, RaycastHit raycastHit, Field field) => this._buildCondition.IsSatisfied(
//		gameObject: gameObject,
//		raycastHit: raycastHit,
//		fieldMemoization: field._FieldMemoization
//	);

//	public void Exit(GameObject gameObject) => Object.Destroy(obj: gameObject);
//}
