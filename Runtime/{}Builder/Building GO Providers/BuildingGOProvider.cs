using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingGOProvider : ScriptableObject
{
    public abstract GameObject Provide(GameObject gameObject);
}
