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

		//? Contains value if the segment is occupied or free.
		public abstract IMemoization<bool> _OccupationMemoization { get; }
		public abstract ISegmentCostMap _SegmentCostMap { get; }

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
		/// Get neighbour segments in a radius.
		/// </summary>
		/// <param name="segment"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public abstract Segment[] GetNeighbours(Segment segment, float radius);

		/// <summary>
		/// Get neighbour segments where distance is calculated in number of steps it takes to reach each segment.
		/// </summary>
		/// <param name="segment"></param>
		/// <param name="maxCost"></param>
		/// <returns></returns>
		public abstract Segment[] GetNeighbours(Segment segment, int maxCost);

		/// <summary>
		/// Get neighbour Segments non alloc.
		/// </summary>
		/// <param name="segment"></param>111111
		/// <param name="neighbourSegmentsBuffer"></param>
		/// <returns></returns>
		public abstract int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer);

		public abstract int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer, float radius);
		public abstract int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer, int maxCost);

#if (SHAPES_URP || SHAPES_HDRP) && DEBUG_PATHFINDER
		//TODO: REMOVE!
		public PathfinderVisualizer PathfinderVisualizer;
#endif
	}
}