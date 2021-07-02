using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldMemoization
{
	void Occupy(Vector3 position);
	void Free(Vector3 position);

	bool Occupied(Vector3 position);
}
