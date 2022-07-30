using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	public class PhysicalSegment : MonoBehaviour
	{
		[SerializeField] private int _cost = 1;
		public int _Cost => this._cost;

		[SerializeField] private PolytopialSegmentsStructure _polytopialSegmentsStructure;
		public PolytopialSegmentsStructure _PolytopialSegmentsStructure => this._polytopialSegmentsStructure;

		private void Start()
		{
			this._polytopialSegmentsStructure = GameObject.FindObjectOfType<GridField>();

			this._polytopialSegmentsStructure._SegmentCostMap.SetCost(position: this.transform.position, cost: this._cost);
		}
	}
}