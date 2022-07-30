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

		private IMemoization<bool> _occupationMemoization;
		public override IMemoization<bool> _OccupationMemoization => this._occupationMemoization;

		[SerializeField] private SegmentCostMap _segmentCostMap;
		public override ISegmentCostMap _SegmentCostMap => this._segmentCostMap;

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

		public override Segment[] GetNeighbours(Segment segment, float radius)
		{
			float sqrRadius = radius * radius;

			////TODO: Maybe not create these each function call? HUH?!
			//Queue<Segment> frontier = new Queue<Segment>(capacity: segment.PolytopialSegmentsStructure._RelativeBounds.size.x * segment.PolytopialSegmentsStructure._RelativeBounds.size.y * segment.PolytopialSegmentsStructure._RelativeBounds.size.z);

			//frontier.Enqueue(item: segment);

			////TODO: Maybe not create these each function call? HUH?!
			//Dictionary<int, Segment> visitedSegments = new Dictionary<int, Segment>(capacity: segment.PolytopialSegmentsStructure._RelativeBounds.size.x * segment.PolytopialSegmentsStructure._RelativeBounds.size.y * segment.PolytopialSegmentsStructure._RelativeBounds.size.z);

			//while (frontier.Count > 0)
			//{
			//	Segment current = frontier.Dequeue();

			//	if (!visitedSegments.ContainsKey(current.Id))
			//	{
			//		Segment[] neighbours = current.GetNeighbours();

			//		for (int a = 0; a < neighbours.Length; a++)
			//		{
			//			if ((segment.WorldPosition - neighbours[a].WorldPosition).sqrMagnitude <= sqrRadius && !visitedSegments.ContainsKey(neighbours[a].Id))
			//			{
			//				frontier.Enqueue(neighbours[a]);
			//			}
			//		}

			//		visitedSegments.Add(current.Id, current);
			//	}
			//}

			//Segment[] segments = new Segment[visitedSegments.Values.Count];

			//visitedSegments.Values.CopyTo(segments, 0);

			//return segments;

			//? ↑ Approach above is good for a different type of structure. But NOT REALLY? ↑
			//TODO: Check if you need to do pathfidning for algo below. This is in case a segment that can't be reached is a added. But that is fine, for this type of method I think.
			//? ↓ For a grid there is a more efficient approach. ↓

			//! Algo: Take radius and calculate min/max indices (x,y,z). Now you have a cube of segments. Iterate that cube and get all of the segments that fit in that radius.

			GridSegment gridSegment = this._gridSegmentsCache[segment.Id];

			int minX = Mathf.Clamp((int)((gridSegment.LocalPosition.x - radius) / this._cellSize.x), 0, this.relativeBounds.size.x - 1);
			int maxX = Mathf.Clamp((int)((gridSegment.LocalPosition.x + radius) / this._cellSize.x), 0, this.relativeBounds.size.x - 1);

			int	minY = Mathf.Clamp((int)((gridSegment.LocalPosition.y - radius) / this._cellSize.y), 0, this.relativeBounds.size.y - 1);
			int	maxY = Mathf.Clamp((int)((gridSegment.LocalPosition.y + radius) / this._cellSize.y), 0, this.relativeBounds.size.y - 1);

			int minZ = Mathf.Clamp((int)((gridSegment.LocalPosition.z - radius) / this._cellSize.z), 0, this.relativeBounds.size.z - 1);
			int maxZ = Mathf.Clamp((int)((gridSegment.LocalPosition.z + radius) / this._cellSize.z), 0, this.relativeBounds.size.z - 1);

			int numberOfSegmentsInCubicRadius = (maxX - minX + 1) * (maxY - minY + 1) * (maxZ - minZ + 1);

			List<Segment> segments = new List<Segment>(capacity: numberOfSegmentsInCubicRadius);

			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					for (int z = minZ; z <= maxZ; z++)
					{
						if ((segment.LocalPosition - this._gridSegments[x, y, z].LocalPosition).sqrMagnitude <= sqrRadius)
							segments.Add(item: this._gridSegments[x, y, z]);
					}
				}
			}

			return segments.ToArray();
		}

		//TODO: Add new `GetNeighbours` method that allows to find segments based on radius but `int`. So more like `maxSteps` but steps are a different thing, they are more like how far away unit can reach. But with radius that wouldn't matter.
		//TODO: Plans changed, this method is already the one with `maxSteps`. Make another one but with a new parameter and that method will take into account `Memoization`. So `GetNeighbours(Segment segment, int maxSteps, IMemoization memoization)`.
		//TODO: But even with memoization that is not all the same as with pathfinding. You aren't increasing number of steps taken to reach that segment. Memoization is rather - select segments but only those I can actually walk on.
		//TODO: What you also need is a method with that increase of steps taken to reach some segment which would make it actually `maxSteps` and the method discussed above - we actually need an `int radius` for that.
		//? `maxSteps` renamed to `maxCost`.
		
		public override Segment[] GetNeighbours(Segment segment, int maxCost)
		{
			int capacity = segment.PolytopialSegmentsStructure._RelativeBounds.size.x * segment.PolytopialSegmentsStructure._RelativeBounds.size.y * segment.PolytopialSegmentsStructure._RelativeBounds.size.z;

			//TODO: Maybe not create these each function call? HUH?!
			Queue<Segment> frontier = new Queue<Segment>(capacity: capacity);

			frontier.Enqueue(item: segment);

			//TODO: Maybe not create these each function call? HUH?!
			Dictionary<int, float> cost = new Dictionary<int, float>(capacity: capacity)
			{
				[segment.Id] = 0.0f
			};

			while (frontier.Count > 0)
			{
				Segment current = frontier.Dequeue();

				Segment[] neighbours = current.GetNeighbours();

				for (int a = 0; a < neighbours.Length; a++)
				{
					Segment next = neighbours[a];

					float newCost = cost[current.Id] + this._segmentCostMap.GetCost(next); //+ 1; // + this.Distance(current, neighbours[a]) // + 1 was to replace distance

					if (newCost <= maxCost && (!cost.ContainsKey(next.Id) || newCost < cost[next.Id]))
					{
						cost[next.Id] = newCost;

						frontier.Enqueue(next);
					}
				}
			}

			Segment[] segments = new Segment[cost.Keys.Count - 1];

			int i = 0;
			foreach (int segmentId in cost.Keys)
			{
				if (this._gridSegmentsCache[segmentId] != segment)
				{
					segments[i] = this._gridSegmentsCache[segmentId];

					++i;
				}
			}

			return segments;
		}

		public override int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer)
		{
			throw new System.NotImplementedException();
		}

		public override int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer, float radius)
		{
			throw new System.NotImplementedException();
		}

		public override int GetNeighbours(Segment segment, Segment[] neighbourSegmentsBuffer, int maxCost)
		{
			throw new System.NotImplementedException();
		}

		[SerializeField] private FieldGraphicModule _fieldGraphicModule;
		public FieldGraphicModule _FieldGraphicModule => this._fieldGraphicModule;

		private void Initialize()
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
		}

		private void Awake()
		{
			this.Initialize();

			//TODO: This will not work in Editor. But you need to make it work some other way, because constructing during editor time should also be an option.
			this._occupationMemoization = new Memoization<bool>(polytopialSegmentsStructure: this);
			//this._segmentCostMap = new SegmentCostMap(polytopialSegmentsStructure: this);

			this._segmentCostMap.Initialize(polytopialSegmentsStructure: this);

			//this._fieldGraphicModule.Initialize(this);
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