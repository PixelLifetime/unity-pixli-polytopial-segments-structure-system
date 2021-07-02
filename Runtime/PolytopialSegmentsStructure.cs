using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public abstract class PolytopialSegmentsStructure : MonoBehaviour
	{
		//TODO: Consider removing bounds, boundless structures allow far more capabilities. [unconnected segments, custom grid editing]
		//? You could also move it to another module. Same way as Volume in NavMesh.
		[SerializeField] protected Bounds bounds;
		public Bounds _Bounds => this.bounds;

		[SerializeField] protected BoundsInt relativeBounds;
		public BoundsInt _RelativeBounds => this.relativeBounds;

		public abstract IFieldMemoization _FieldMemoization { get; }

		//public abstract Vector3 GetNodePosition(Vector3 approxPosition);

		////TODO: Yep, remove this.
		//public abstract FieldGraphicModule _FieldGraphicModule { get; }

		public abstract Segment GetSegment(int id);
		public abstract Segment GetSegment(Vector3 position);

		/// <summary>
		/// Get neighbour segments.
		/// </summary>
		/// <param name="segment"></param>
		/// <returns></returns>
		public abstract Segment[] GetNeighbours(Segment segment);

		/// <summary>
		/// Get neighbour Segments non alloc.
		/// </summary>
		/// <param name="segment"></param>111111
		/// <param name="neighbourSegmentsBuffer"></param>
		/// <returns></returns>
		public abstract int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer);

		//TODO: REMOVE!
		public PathfinderVisualizer PathfinderVisualizer;
	}
}