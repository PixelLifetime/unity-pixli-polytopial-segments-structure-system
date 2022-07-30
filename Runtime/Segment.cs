using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public class Segment : PriorityQueue<Segment>.IItem, IWorldObject
	{
		public int Id { get; set; }

		public PolytopialSegmentsStructure PolytopialSegmentsStructure { get; set; }

		public Vector3 LocalPosition { get; set; }
		public Vector3 WorldPosition => this.PolytopialSegmentsStructure.transform.position + this.LocalPosition;

		//? I would rather not do that, I can do every other operation without doing this. Just directly use Id for a key.
		//public override int GetHashCode() => this.Id;

		public Segment[] GetNeighbours() => this.PolytopialSegmentsStructure.GetNeighbours(segment: this);
		public Segment[] GetNeighbours(float radius) => this.PolytopialSegmentsStructure.GetNeighbours(segment: this, radius: radius);
		public Segment[] GetNeighbours(int maxCost) => this.PolytopialSegmentsStructure.GetNeighbours(segment: this, maxCost: maxCost);

		public int GetNeighbours(Segment[] neighbourSegmentsBuffer) => this.PolytopialSegmentsStructure.GetNeighbours(segment: this, neighbourSegmentsBuffer: neighbourSegmentsBuffer);
		public int GetNeighbours(Segment[] neighbourSegmentsBuffer, float radius) => this.PolytopialSegmentsStructure.GetNeighbours(segment: this, neighbourSegmentsBuffer: neighbourSegmentsBuffer, radius: radius);
		public int GetNeighbours(Segment[] neighbourSegmentsBuffer, int maxCost) => this.PolytopialSegmentsStructure.GetNeighbours(segment: this, neighbourSegmentsBuffer: neighbourSegmentsBuffer, maxCost: maxCost);

		public void SetCost(int cost) => this.PolytopialSegmentsStructure._SegmentCostMap.SetCost(id: this.Id, cost: cost);
		public int GetCost(int cost) => this.PolytopialSegmentsStructure._SegmentCostMap.GetCost(id: this.Id);

		public Segment(int id, PolytopialSegmentsStructure polytopialSegmentsStructure, Vector3 localPosition)
		{
			this.Id = id;
			this.PolytopialSegmentsStructure = polytopialSegmentsStructure;
			this.LocalPosition = localPosition;
		}

		//TODO: Replace it with debug extension.
		public override string ToString()
		{
			return $"Id: {this.Id}; LocalPosition: {this.LocalPosition}; WorldPosition: {this.WorldPosition};";
		}

		/// <summary>
		/// Dynamic(because it changes frequently) index of this segment in priority queue for easy access and efficient `Contains` method.
		/// Derivative of `IItem`.
		/// </summary>
		public int Index { get; set; }
	}

	public static class SegmentExtensions
	{
		public static void Memoize(this Segment segment)
		{
			//segment.PolytopialSegmentsStructure._SegmentCostMap.LockCostToDefault(id: segment.Id);
			segment.PolytopialSegmentsStructure._OccupationMemoization.Memoize(segmentId: segment.Id, value: true);
		}

		public static bool Forget(this Segment segment)
		{
			//segment.PolytopialSegmentsStructure._SegmentCostMap.UnlockCost(id: segment.Id);
			return segment.PolytopialSegmentsStructure._OccupationMemoization.Forget(segmentId: segment.Id);
		}

		public static bool GetMemoizationValue(this Segment segment) => segment.PolytopialSegmentsStructure._OccupationMemoization.GetValue(segmentId: segment.Id);
	}
}