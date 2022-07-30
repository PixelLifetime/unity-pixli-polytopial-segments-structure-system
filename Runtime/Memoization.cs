using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public class Memoization<TValue> : IMemoization<TValue>
	{
		private readonly PolytopialSegmentsStructure _polytopialSegmentsStructure;

		[SerializeField] private Dictionary<int, TValue> _memory = new Dictionary<int, TValue>();

		public void Memoize(int segmentId, TValue value) => this._memory.Add(key: segmentId, value: value);
		public void Memoize(Segment segment, TValue value) => this._memory.Add(key: segment.Id, value: value);
		public void Memoize(Vector3 position, TValue value) => this._memory.Add(key: this._polytopialSegmentsStructure.GetSegment(position: position).Id, value: value);

		public bool Forget(int segmentId) => this._memory.Remove(key: segmentId);
		public bool Forget(Segment segment) => this.Forget(segmentId: segment.Id);
		public bool Forget(Vector3 position) => this.Forget(segmentId: this._polytopialSegmentsStructure.GetSegment(position: position).Id);

		public TValue GetValue(int segmentId) => this._memory.TryGetValue(key: segmentId, out TValue value) ? value : default;
		public TValue GetValue(Segment segment) => this.GetValue(segmentId: segment.Id);
		public TValue GetValue(Vector3 position) => this.GetValue(segmentId: this._polytopialSegmentsStructure.GetSegment(position: position).Id);

		public Memoization(PolytopialSegmentsStructure polytopialSegmentsStructure)
		{
			this._polytopialSegmentsStructure = polytopialSegmentsStructure;
		}
	}
}