using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	[RequireComponent(typeof(BoxCollider))]
	public class GridField : PolytopialSegmentsStructure
	{
		private GridSegment[] _gridSegmentsCache;
		private GridSegment[,,] _gridSegments;

		private IFieldMemoization _fieldMemoization;
		public override IFieldMemoization _FieldMemoization => this._fieldMemoization;

		[SerializeField] private Vector3 _cellSize = new Vector3(0.5f, 1.0f, 0.8f);
		public Vector3 _CellSize => this._cellSize;

		public override Segment GetSegment(int id) => this._gridSegmentsCache[id];

		public override Segment GetSegment(Vector3 position)
		{
			//? Clamping edge values because the calculation result can give us either negative value or one too big that goes outside the grid.
			int x = Mathf.Clamp((int)((position.x - this.transform.position.x) / this._cellSize.x), 0, this.relativeBounds.size.x - 1);
			int y = Mathf.Clamp((int)((position.y - this.transform.position.y) / this._cellSize.y), 0, this.relativeBounds.size.y - 1);
			int z = Mathf.Clamp((int)((position.z - this.transform.position.z) / this._cellSize.z), 0, this.relativeBounds.size.z - 1);
			

			return this._gridSegments[x, y, z];
		}

		public override Segment[] GetNeighbours(Segment segment)
		{
			GridSegment gridSegment = this._gridSegmentsCache[segment.Id];

			List<Segment> segments = new List<Segment>(4);

			if (gridSegment.X - 1 >= 0)
				segments.Add(this._gridSegments[gridSegment.X - 1, gridSegment.Y, gridSegment.Z]);

			if (gridSegment.Z - 1 >= 0)
				segments.Add(this._gridSegments[gridSegment.X, gridSegment.Y, gridSegment.Z - 1]);

			if (gridSegment.X + 1 < this.relativeBounds.size.x)
				segments.Add(this._gridSegments[gridSegment.X + 1, gridSegment.Y, gridSegment.Z]);

			if (gridSegment.Z + 1 < this.relativeBounds.size.z)
				segments.Add(this._gridSegments[gridSegment.X, gridSegment.Y, gridSegment.Z + 1]);

			//? For 8?
			//for (int x = -1; x <= 1; x++)
			//{
			//	for (int z = -1; z <= 1; z++)
			//	{
			//		if (x != 0 || z != 0)
			//		{
			//			if (
			//				gridSegment.X + x > 0 && gridSegment.X + x < this.relativeBounds.x
			//			 && gridSegment.Z + z > 0 && gridSegment.Z + z < this.relativeBounds.z
			//			)
			//			{
			//				segments.Add(this._gridSegments[gridSegment.X + x, gridSegment.Y, gridSegment.Z + z]);
			//			}
			//		}
			//	}
			//}

			return segments.ToArray();
		}

		public override int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer)
		{
			throw new System.NotImplementedException();
		}

		[SerializeField] private FieldGraphicModule _fieldGraphicModule;
		public FieldGraphicModule _FieldGraphicModule => this._fieldGraphicModule;

		private void Awake()
		{
			this._gridSegments = new GridSegment[this.relativeBounds.size.x, this.relativeBounds.size.y, this.relativeBounds.size.z];
			this._gridSegmentsCache = new GridSegment[this.relativeBounds.size.x * this.relativeBounds.size.y * this.relativeBounds.size.z];

			int id = 0;
			for (int x = 0; x < this._gridSegments.GetLength(0); x++)
			{
				for (int y = 0; y < this._gridSegments.GetLength(1); y++)
				{
					for (int z = 0; z < this._gridSegments.GetLength(2); z++)
					{
						GridSegment gridSegment = new GridSegment(
							id: id,
							polytopialSegmentsStructure: this,
							localPosition: new Vector3(
								x: x * this._cellSize.x + this._cellSize.x * 0.5f,
								y: y * this._cellSize.y + this._cellSize.y * 0.5f,
								z: z * this._cellSize.z + this._cellSize.z * 0.5f
							)
						)
						{
							X = x,
							Y = y,
							Z = z
						};

						this._gridSegments[x, y, z] = gridSegment;
						this._gridSegmentsCache[id] = gridSegment;

						++id;
					}
				}
			}

			//TODO: This will not work in Editor. But you need to make it work some other way, because constructing during editor time should also be an option.
			this._fieldMemoization = new GridFieldMemoization(this);

			this._fieldGraphicModule.Initialize(this);
		}

#if UNITY_EDITOR
		private BoxCollider _boxCollider;

		private void OnValidate()
		{
			this._boxCollider = this.GetComponent<BoxCollider>();

			this._boxCollider.center = new Vector3(
				x: (this.relativeBounds.size.x * this._cellSize.x) / 2.0f,
				y: (this.relativeBounds.size.y * this._cellSize.y) / 2.0f,
				z: (this.relativeBounds.size.z * this._cellSize.z) / 2.0f
			);

			this._boxCollider.size = new Vector3(
				x: (this.relativeBounds.size.x * this._cellSize.x),
				y: (this.relativeBounds.size.y * this._cellSize.y),
				z: (this.relativeBounds.size.z * this._cellSize.z)
			);
		}

		private void OnDrawGizmos()
		{
			for (int x = 0; x < this.relativeBounds.size.x; x++)
			{
				for (int y = 0; y < this.relativeBounds.size.y; y++)
				{
					for (int z = 0; z < this.relativeBounds.size.z; z++)
					{
						Gizmos.DrawWireCube(
							center: new Vector3(
								x: this.transform.position.x + x * this._cellSize.x + this._cellSize.x * 0.5f,
								y: this.transform.position.y + y * this._cellSize.y + this._cellSize.y * 0.5f,
								z: this.transform.position.z + z * this._cellSize.z + this._cellSize.z * 0.5f
							),
							size: new Vector3(
								x: this._cellSize.x,
								y: this._cellSize.y,
								z: this._cellSize.z
							)
						);
					}
				}
			}
		}
#endif
	}
}