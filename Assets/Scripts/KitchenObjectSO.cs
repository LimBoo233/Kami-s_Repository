using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject {
	[SerializeField] private Transform prefab;
	[SerializeField] private Sprite sprite;
	[SerializeField] private string objectName;

	public Transform getPrefab() {
		return prefab;
	}
	
	public Sprite getSprite() {
		return sprite;
	}
	
	public string getName() {
		return objectName;
	}
}
