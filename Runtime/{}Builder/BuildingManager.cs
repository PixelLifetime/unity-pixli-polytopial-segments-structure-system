//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class BuildingManager : MonoBehaviour
//{
//	//[SerializeField] BuildLogic _buildLogic;  - to be yet implemented

//	private BuildingInfo _activeBuildingInfo;
//	private GameObject _activeGameObject;

//	private RaycastHit _raycastHit;

//	[SerializeField] private Field _defaultField;
//	public Field _DefaultField => this._defaultField;

//	private Field _field;

//	public void ExitBuildingProcess(BuildingInfo buildingInfo)
//	{
//		this._activeBuildingInfo._BuildingProgrammability.Exit(this._activeGameObject);

//		this._activeBuildingInfo = null;
//		this._activeGameObject = null;

//		BLProdGameController._Instance.ChangeGameMode(BLProdGameController.GameMode.Gameplay);
//	}

//	public void EnterBuildingProcess(BuildingInfo buildingInfo)
//	{
//		if (this._activeBuildingInfo != null)
//		{
//			this.ExitBuildingProcess(null);
//		}

//		this._activeBuildingInfo = buildingInfo;

//		this._activeGameObject = this._activeBuildingInfo._BuildingGOProvider.Provide(this._activeBuildingInfo._Graphics);

//		BLProdGameController._Instance.ChangeGameMode(BLProdGameController.GameMode.Build);
//	}

//	[SerializeField] private AudioClip _buildAudioClip;
//	public AudioClip _BuildAudioClip => this._buildAudioClip;

//	public void Build()
//	{
//		if (this._activeBuildingInfo._BuildingProgrammability.BuildPermitted(this._activeGameObject, this._raycastHit, this._field))
//		{
//			this._field._FieldMemoization.Occupy(this._activeGameObject.transform.position);

//			Object.Destroy(this._activeGameObject);

//			this._activeGameObject = this._activeBuildingInfo._BuildingGOProvider.Provide(this._activeBuildingInfo._Building);

//			this._activeBuildingInfo._BuildingProgrammability.Execute(
//				gameObject: this._activeGameObject,
//				raycastHit: this._raycastHit,
//				field: this._field
//			);

//			this._activeBuildingInfo = null;
//			this._activeGameObject = null;

//			AudioPlayer._Instance.Play(this._buildAudioClip, AudioPlayer.Type.SoundEffect);

//			BLProdGameController._Instance.ChangeGameMode(BLProdGameController.GameMode.Gameplay);
//		}
//	}

//	[SerializeField] private RaycastEmitter _raycastEmitter;
//	public RaycastEmitter _RaycastEmitter => this._raycastEmitter;

//	private void Update()
//	{
//		if (this._activeBuildingInfo != null)
//		{
//			Ray ray = Camera.main.ScreenPointToRay(pos: Mouse.current.position.ReadValue());

//			if (this._raycastEmitter.Raycast(ray: ray, raycastHit: out this._raycastHit))
//			{
//				this._field = this._raycastHit.collider.GetComponent<Field>();

//				if (this._field == null)
//					this._field = this._defaultField;

//				this._activeBuildingInfo._BuildingProgrammability.Execute(
//					gameObject: this._activeGameObject,
//					raycastHit: this._raycastHit,
//					field: this._field
//				);

//				if (Mouse.current.leftButton.wasPressedThisFrame)
//				{
//					this.Build();
//				}
//			}
//		}
//	}

//#if UNITY_EDITOR
//	private void OnDrawGizmos()
//	{
//	}
//#endif
//}