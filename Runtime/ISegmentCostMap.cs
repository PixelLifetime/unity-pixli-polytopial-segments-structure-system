using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	public interface ISegmentCostMap
	{
		int _DefaultCost { get; }
		PolytopialSegmentsStructure _PolytopialSegmentsStructure { get; }

		void SetCost(int id, int cost);
		void SetCost(Vector3 position, int cost);
		void SetCost(Segment segment, int cost);

		// TODO: Rename `Lock` and `Unlock` to `` and `Restore`.
		void LockCostToDefault(int id);
		void LockCostToDefault(Vector3 position);
		void LockCostToDefault(Segment segment);

		void UnlockCost(int id);
		void UnlockCost(Vector3 position);
		void UnlockCost(Segment segment);

		int GetCost(int id);
		int GetCost(Vector3 position);
		int GetCost(Segment segment);
	}
}