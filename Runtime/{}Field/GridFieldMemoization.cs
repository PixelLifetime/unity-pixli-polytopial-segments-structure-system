using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	////TODO: This probably can be general SegmentsMemoization implementation. I guess it's sufficient.
	//public class GridFieldMemoization : IMemoization<bool>
	//{
	//	private readonly GridField _field;

	//	//TODO: This is just a proof of concept, there is no need to do O(n) for GridField. I believe each specific grid has to have its own methods of memoization.
	//	[SerializeField] private List<int> _occupiedSegmentsIds = new List<int>();

	//	[SerializeField] private Dictionary<int, bool> _

	//	public void Memoize(int segmentId, bool value) => this._occupiedSegmentsIds.Add(item: segmentId);
	//	public void Memoize(Segment segment, bool value) => this._occupiedSegmentsIds.Add(item: segment.Id);
	//	public void Memoize(Vector3 position, bool value) => this._occupiedSegmentsIds.Add(this._field.GetSegment(position: position).Id);

	//	public void Forget(int segmentId)
	//	{
	//		for (int a = 0; a < this._occupiedSegmentsIds.Count; a++)
	//		{
	//			if (segmentId == this._occupiedSegmentsIds[a])
	//			{
	//				this._occupiedSegmentsIds.RemoveAt(a);
	//			}
	//		}
	//	}
	//	public void Forget(Segment segment) => this.Forget(segmentId: segment.Id);
	//	public void Forget(Vector3 position) => this.Forget(segmentId: this._field.GetSegment(position: position).Id);

	//	public bool GetValue(int segmentId)
	//	{
	//		for (int a = 0; a < this._occupiedSegmentsIds.Count; a++)
	//		{
	//			if (segmentId == this._occupiedSegmentsIds[a])
	//				return true;
	//		}

	//		return false;
	//	}
	//	public bool GetValue(Segment segment) => this.GetValue(segmentId: segment.Id);
	//	public bool GetValue(Vector3 position) => this.GetValue(segmentId: this._field.GetSegment(position: position).Id);

	//	public GridFieldMemoization(GridField field)
	//	{
	//		this._field = field;
	//	}
	//}
}