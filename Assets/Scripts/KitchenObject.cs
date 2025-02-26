using UnityEngine;

public class KitchenObject : MonoBehaviour {
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public KitchenObjectSO getKitchenObjectSO() {
		return kitchenObjectSO;
	}
}
