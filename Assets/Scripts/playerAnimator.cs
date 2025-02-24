using UnityEngine;

public class playerAnimator : MonoBehaviour {

    private const string IS_WALIKING = "IsWalking";
    private Animator animator;
    [SerializeField] private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALIKING, player.IsWalking());
    }

    private void Update() {
        animator.SetBool(IS_WALIKING, player.IsWalking());
    }
}
