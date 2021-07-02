using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	[Serializable]
	public class FieldGraphicModule
	{
		[SerializeField] private Texture2D _gridTexture2D;
		public Texture2D _GridTexture2D => this._gridTexture2D;

		[SerializeField] private Material _material;
		public Material _Material => this._material;

		private GameObject _gridVisuals;
		private MeshRenderer _gridVisualsMeshRenderer;

		private GridField _field;

		public void Adjust()
		{
			this._gridVisuals.transform.localPosition = new Vector3(
				x: (this._field._RelativeBounds.size.x * this._field._CellSize.x) / 2.0f,
				y: (this._field._RelativeBounds.size.y * this._field._CellSize.y) / 2.0f,
				z: (this._field._RelativeBounds.size.z * this._field._CellSize.z) / 2.0f
			);

			this._gridVisuals.transform.localScale = new Vector3(
				x: (this._field._RelativeBounds.size.x * this._field._CellSize.x),
				z: (this._field._RelativeBounds.size.y * this._field._CellSize.y),
				y: (this._field._RelativeBounds.size.z * this._field._CellSize.z)
			);
		}

		public void Initialize(GridField field)
		{
			this._field = field;

			this._gridVisuals = GameObject.CreatePrimitive(PrimitiveType.Quad);
			this._gridVisuals.name = "{Grid Visuals}";
			this._gridVisuals.transform.SetParent(this._field.transform);
			this._gridVisuals.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

			this._gridVisualsMeshRenderer = this._gridVisuals.GetComponent<MeshRenderer>();

			Material material = new Material(this._material)
			{
				mainTexture = this._gridTexture2D
			};

			material.mainTextureScale = new Vector2(x: this._field._RelativeBounds.size.x, y: this._field._RelativeBounds.size.z);

			this._gridVisualsMeshRenderer.sharedMaterial = material;

			this.Adjust();
		}
	}
}