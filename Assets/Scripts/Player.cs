using System;
using UnityEngine;

public class Player : MonoBehaviour {

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged; 
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    
    private bool isWaling;

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWaling;
    }
    
    private void HandleMovement() {
        // 获取玩家标准化的移动向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWaling = moveDir != Vector3.zero;

        float playerHeight = 2f;
        float playerRadius = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveSpeed * Time.deltaTime);
        
        
        if (!canMove) {
            // 如果不能向目标方向移动
            
            // 尝试在X轴方向移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            
            if (canMove) {
                moveDir = moveDirX;
            }
            else {
                // 如果不能在X轴方向移动
                // 尝试在Z轴方向移动
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove) { 
                    moveDir = moveDirZ;
                }
                else {
                    // 不可移动
                }
            }
        }
        
        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void HandleInteractions() {
        // 获取玩家标准化的移动向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
    
        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
    
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // 有clear counter
                if (clearCounter != selectedCounter) {
                    selectedCounter = clearCounter;
                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
                }
            } else {
                selectedCounter = null;
                OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
            }
        } else {
            selectedCounter = null;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
        }
    }
}
