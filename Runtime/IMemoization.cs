using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

//TODO: Rename this to just Memoization or SegmentsMemoization.
public interface IMemoization<TValue>
{
	void Memoize(int segmentId, TValue value);
	void Memoize(Segment segment, TValue value);
	void Memoize(Vector3 position, TValue value);

	bool Forget(int segmentId);
	bool Forget(Segment segment);
	bool Forget(Vector3 position);

	TValue GetValue(int segmentId);
	TValue GetValue(Segment segment);
	TValue GetValue(Vector3 position);
}
