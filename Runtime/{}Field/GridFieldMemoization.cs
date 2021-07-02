using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public class GridFieldMemoization : IFieldMemoization
	{
		private readonly GridField _field;

		//TODO: This is just a proof of concept, there is no need to do O(n) for GridField. I believe each specific grid has to have its own methods of memoization.
		[SerializeField] private List<Vector3> _occupiedPositions = new List<Vector3>();
		//public List<Vector3> _OccupiedPositions => this._occupiedPositions;

		public void Occupy(Vector3 position)
		{
			this._occupiedPositions.Add(this._field.GetSegment(position).WorldPosition);
		}

		public void Free(Vector3 position)
		{
			for (int a = 0; a < this._occupiedPositions.Count; a++)
			{
				if (Vector3.Distance(this._occupiedPositions[a], position) < 0.01f)
				{
					this._occupiedPositions.RemoveAt(a);
				}
			}
		}

		public bool Occupied(Vector3 position)
		{
			for (int a = 0; a < this._occupiedPositions.Count; a++)
			{
				if (Vector3.Distance(this._occupiedPositions[a], position) < 0.01f)
				{
					return true;
				}
			}

			return false;
		}

		public GridFieldMemoization(GridField field)
		{
			this._field = field;
		}
	}
}