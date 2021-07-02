using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public class GridSegment : Segment
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public GridSegment(int id, PolytopialSegmentsStructure polytopialSegmentsStructure, Vector3 localPosition) : base(id, polytopialSegmentsStructure, localPosition)
		{
		}
	}
}