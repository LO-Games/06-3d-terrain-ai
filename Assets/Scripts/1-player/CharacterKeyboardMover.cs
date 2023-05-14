using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover : MonoBehaviour {
    [SerializeField] float speed = 3.5f;
    [SerializeField] float gravity = 9.81f;

    private CharacterController cc;
    private Animator animator;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    private void OnEnable() { 
        moveAction.Enable(); 
        jumpAction.Enable();
    }

    private void OnDisable() { 
        moveAction.Disable();
        jumpAction.Disable();
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
    }

    void Start() {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    Vector3 velocity = new Vector3(0, 0, 0);

    void Update() {
        if (cc.isGrounded) {
            // Update animation
            Vector3 movement = moveAction.ReadValue<Vector2>();
            velocity.x = movement.x * speed;
            velocity.z = movement.y * speed;

            // Update animation
        }
        else {
            animator.SetBool("Jump", false);
            velocity.y -= gravity * Time.deltaTime;
        }

        if (jumpAction.triggered) {
            // Update animation
            animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(2 * gravity * 2);

            // Play audio
            // AudioSource.PlayClipAtPoint(jumpAudioClip, transform.position);
        }

        velocity = transform.TransformDirection(velocity);

        cc.Move(velocity * Time.deltaTime);
    }
}