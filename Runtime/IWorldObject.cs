using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	public interface IWorldObject
	{
		Vector3 WorldPosition { get; }
	}
}