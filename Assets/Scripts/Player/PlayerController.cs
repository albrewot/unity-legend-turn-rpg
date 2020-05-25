using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact,
    menu,
    transfer,
    load
}

public class PlayerController : MonoBehaviour {
    //Attributes
    //Serialized
    [SerializeField] public float movementSpeed;
    [SerializeField] public string areaTransitionName;
    [SerializeField] public Vector3 limitOffset;

    //Not-Serialized
    private Vector3 movement;
    private Rigidbody2D playerRb;
    public Animator animator;
    public static PlayerController instance;
    private Vector3 minLimit;
    private Vector3 maxLimit;
    public PlayerState currentState;
    

    //Methods
    public void MovePlayer() {
        MovementInput();

        //clampLimitsAndMove();
        
        UpdateAnimationParams();

    }
    public void AltMovePlayer() {
        if(currentState == PlayerState.walk) {

            MovementInput();

            playerRb.velocity = new Vector2(movement.x, movement.y).normalized * movementSpeed;

            clampLimits();

            UpdateAnimationParams();
        }
        else {
            playerRb.velocity = Vector2.zero;
        }

    }

    public void clampLimits() {
        Vector3 positionLimit = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        positionLimit.x = Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x);
        positionLimit.y = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);
        transform.position = positionLimit;

        //playerRb.MovePosition(positionLimit + movement.normalized * movementSpeed * Time.fixedDeltaTime);
    }

    public void SetBoundaries(Vector3 minBoundary, Vector3 maxBoundary) {
        minLimit = minBoundary + limitOffset;
        maxLimit = maxBoundary - limitOffset;
    }

    public void UpdateAnimationParams() {
        //walk
        if(movement != Vector3.zero) {
            animator.SetFloat("Horizontal", playerRb.velocity.x);
            animator.SetFloat("Vertical", playerRb.velocity.y);
        }
        animator.SetFloat("WalkSpeed", playerRb.velocity.sqrMagnitude);
    }

    public void SetPlayerIdleAnimation() {
        animator.SetFloat("WalkSpeed", 0);
    }

    private void MovementInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void PlayerInstance() {
        if (instance == null) {
            instance = this;
        } else {
            if(instance != this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start() {
        PlayerInstance();
        currentState = PlayerState.walk;
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        AltMovePlayer();
    }
}
