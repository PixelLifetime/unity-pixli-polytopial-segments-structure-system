using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Move to separate file.
[Serializable]
public struct RaycastData
{
	[SerializeField] private float _maxDistance;
	public float _MaxDistance => this._maxDistance;

	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask => this._layerMask;

	[SerializeField] private QueryTriggerInteraction _queryTriggerInteraction;
	public QueryTriggerInteraction _QueryTriggerInteraction => this._queryTriggerInteraction;
}

public class RaycastEmitter : MonoBehaviour
{
	[Serializable]
	private class EditorOnly
	{
		[SerializeField] private bool _debug = true;
		public bool _Debug => this._debug;

		[SerializeField] private Color _debugColor = Color.white;
		public Color _DebugColor => this._debugColor;
	}

	[SerializeField] private RaycastData _raycastData;
	public RaycastData _RaycastData => this._raycastData;

#if UNITY_EDITOR
	[SerializeField] private EditorOnly _editorOnly;
#endif

	public bool Raycast(Ray ray, out RaycastHit raycastHit)
	{
#if UNITY_EDITOR
		if (this._editorOnly._Debug)
			Debug.DrawRay(ray.origin, ray.direction * this._raycastData._MaxDistance, this._editorOnly._DebugColor);
#endif

		return RaycastUtility.Raycast(
			ray: ray,
			raycastHit: out raycastHit,
			raycastData: this._raycastData
		);
	}

	public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit raycastHit)
	{
#if UNITY_EDITOR
		if (this._editorOnly._Debug)
			Debug.DrawRay(origin, direction * this._raycastData._MaxDistance, this._editorOnly._DebugColor);
#endif

		return RaycastUtility.Raycast(
			origin: origin,
			direction: direction,
			raycastHit: out raycastHit,
			raycastData: this._raycastData
		);
	}
}

//TODO: Move to separate file.
public static class RaycastUtility
{
	public static bool Raycast(Ray ray, out RaycastHit raycastHit, RaycastData raycastData) => Physics.Raycast(
		ray: ray,
		hitInfo: out raycastHit,
		maxDistance: raycastData._MaxDistance,
		layerMask: raycastData._LayerMask,
		queryTriggerInteraction: raycastData._QueryTriggerInteraction
	);

	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit raycastHit, RaycastData raycastData) => Physics.Raycast(
		origin: origin,
		direction: direction,
		hitInfo: out raycastHit,
		maxDistance: raycastData._MaxDistance,
		layerMask: raycastData._LayerMask,
		queryTriggerInteraction: raycastData._QueryTriggerInteraction
	);
}