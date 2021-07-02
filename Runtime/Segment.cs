using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace PixLi
{
	public class Segment : AStarPathfinder.PriorityQueue<Segment>.IItem
	{
		public int Id { get; set; }

		public PolytopialSegmentsStructure PolytopialSegmentsStructure { get; set; }

		public Vector3 LocalPosition { get; set; }
		public Vector3 WorldPosition => this.PolytopialSegmentsStructure.transform.position + this.LocalPosition;

		//? I would rather not do that, I can do every other operation without doing this. Just directly use Id for a key.
		//public override int GetHashCode() => this.Id;

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

		public int Index { get; set; }
	}
}