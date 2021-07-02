using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Building GO Provider Basic]", menuName = "Builder/Building GO Providers/[Building GO Provider Basic]")]
public class BuildingGOProviderBasic : BuildingGOProvider
{
	public override GameObject Provide(GameObject gameObject)
	{
		return Object.Instantiate(original: gameObject);
	}
}
