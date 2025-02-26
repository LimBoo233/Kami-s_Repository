using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour {

	[SerializeField] private KitchenObjectSO kithKitchenObjectSO;
	[SerializeField] private Transform CounterTopPoint;
	
	public void Interact() {
		Transform kitchenObjectTransform = Instantiate(kithKitchenObjectSO.getPrefab(), CounterTopPoint);
		kitchenObjectTransform.localPosition = Vector3.zero;
		
		Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().getKitchenObjectSO().getName());
	}
}
