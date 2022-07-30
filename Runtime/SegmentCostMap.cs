using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	[System.Serializable]
	public class SegmentCostMap : ISegmentCostMap
	{
		[System.Serializable]
		public struct Data
		{
			public Vector3 Position;
			public int Cost;

			public Data(Vector3 position, int cost)
			{
				this.Position = position;
				this.Cost = cost;
			}
		}

		private Dictionary<int, int> _segmentId_cost = new Dictionary<int, int>();
		private Dictionary<int, int> _segmentId_originalCost = new Dictionary<int, int>();

		[SerializeField] private int _defaultCost = int.MaxValue;
		public int _DefaultCost => this._defaultCost;

		[SerializeField] private PolytopialSegmentsStructure _polytopialSegmentsStructure;
		public PolytopialSegmentsStructure _PolytopialSegmentsStructure => this._polytopialSegmentsStructure;

		public void SetCost(int id, int cost) => this._segmentId_cost[id] = cost;
		public void SetCost(Vector3 position, int cost) => this.SetCost(id: this._polytopialSegmentsStructure.GetSegment(position: position).Id, cost: cost);
		public void SetCost(Segment segment, int cost) => this.SetCost(id: segment.Id, cost: cost);

		public void LockCostToDefault(int id)
		{
			this._segmentId_originalCost[id] = this.GetCost(id: id);

			this.SetCost(id: id, cost: this._defaultCost);
		}
		public void LockCostToDefault(Vector3 position) => this.LockCostToDefault(id: this._polytopialSegmentsStructure.GetSegment(position: position).Id);
		public void LockCostToDefault(Segment segment) => this.LockCostToDefault(id: segment.Id);

		public void UnlockCost(int id) => this.SetCost(id: id, cost: this._segmentId_originalCost[id]);
		public void UnlockCost(Vector3 position) => this.UnlockCost(id: this._polytopialSegmentsStructure.GetSegment(position: position).Id);
		public void UnlockCost(Segment segment) => this.UnlockCost(id: segment.Id);

		public int GetCost(int id) => this._segmentId_cost.TryGetValue(id, out int cost) ? cost : this._defaultCost;
		public int GetCost(Vector3 position) => this.GetCost(id: this._polytopialSegmentsStructure.GetSegment(position: position).Id);
		public int GetCost(Segment segment) => this.GetCost(id: segment.Id);

		public void Initialize(PolytopialSegmentsStructure polytopialSegmentsStructure)
		{
			this._polytopialSegmentsStructure = polytopialSegmentsStructure;

			this._segmentId_cost = new Dictionary<int, int>();
			this._segmentId_originalCost = new Dictionary<int, int>();

			//for (int a = 0; a < data.Length; a++)
			//	this.SetCost(position: data[a].Position, cost: data[a].Cost);
		}

		public SegmentCostMap(PolytopialSegmentsStructure polytopialSegmentsStructure)//, SegmentCostMap.Data[] data)
		{
			this.Initialize(polytopialSegmentsStructure: polytopialSegmentsStructure);
		}

		//#if UNITY_EDITOR
		//		private void Reset()
		//		{
		//		}
		//#endif
	}
}