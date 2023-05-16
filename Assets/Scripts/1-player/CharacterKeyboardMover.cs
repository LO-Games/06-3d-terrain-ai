using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover : MonoBehaviour {
    [SerializeField] float speed = 3.5f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 1.5f;

    private CharacterController cc;
    private Animator animator;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction runningAction;

    private void OnEnable() { 
        moveAction.Enable(); 
        jumpAction.Enable();
        runningAction.Enable();
    }

    private void OnDisable() { 
        moveAction.Disable();
        jumpAction.Disable();
        runningAction.Disable();
    }

    void OnValidate() {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Button);
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
        if (jumpAction == null)
            jumpAction = new InputAction(type: InputActionType.Button);
        if (jumpAction.bindings.Count == 0)
            jumpAction.AddBinding("<Keyboard>/space");
        if(runningAction == null)
            runningAction = new InputAction(type: InputActionType.Button);
        if (runningAction.bindings.Count == 0)
            runningAction.AddBinding("<Keyboard>/leftShift");
    }

    void Start() {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    Vector3 velocity = new Vector3(0, 0, 0);

    void Update() {
        if (cc.isGrounded) {
            Vector3 movement = moveAction.ReadValue<Vector2>();
            float currentSpeed = speed;

        if (runningAction.IsPressed() && movement.magnitude > 0) {
            currentSpeed *= 2f; // Double the speed when running
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        } else {
            currentSpeed /= 2f; 
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
        }

          // change player direction based on movement
            // transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.y));


            velocity.x = movement.x * currentSpeed;
            velocity.z = movement.y * currentSpeed;


            // if movement is detected, play walking animation
            if (movement.magnitude > 0 && !runningAction.IsPressed()) {
                animator.SetBool("Walk", true);
            } else {
                animator.SetBool("Walk", false);
            }

        } else {
            animator.SetBool("Jump", false);
            velocity.y -= gravity * Time.deltaTime;
        }

        if (jumpAction.triggered) {
            // Update animation
            animator.SetBool("Jump", true);
            // jump 
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight * Time.deltaTime);
            // Play audio
            // AudioSource.PlayClipAtPoint(jumpAudioClip, transform.position);
        }

        velocity = transform.TransformDirection(velocity);

        cc.Move(velocity * Time.deltaTime);
    }
}