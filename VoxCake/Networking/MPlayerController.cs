using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using cu = UCamera;

namespace VoxCake.Networking
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class MPlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject volume;
        [SerializeField] private bool canJump;
        [SerializeField] private bool canRun;
        [Header("Models")]
        [SerializeField] private GameObject Head;
        [SerializeField] private GameObject Body;
        [SerializeField] private GameObject LeftArm;
        [SerializeField] private GameObject RightArm;
        [SerializeField] private GameObject LeftLeg;
        [SerializeField] private GameObject RightLeg;

        private CharacterController characterController;
        private AudioSource audioSource;

        private ICharacter character;
        private Vector2 input;
        private Vector3 MoveVector;
        private Vector3 JumpVector;
        private bool isWalking;
        private bool Jumping;
        private float gravityScalar;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
            SetupCameraPosition();

            character = new Sniper(1);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SetupModels();
        }

        private void Update()
        {
            CameraMovement();
            CharacterMovement();
        }

        private void CameraMovement()
        {
            cu.rotationX += Input.GetAxis("Mouse X") * cu.sensitivity;
            cu.rotationY += Input.GetAxis("Mouse Y") * cu.sensitivity;

            cu.rotationX = UCamera.ClampAngle(cu.rotationX, cu.minimumX, cu.maximumX);
            cu.rotationY = UCamera.ClampAngle(cu.rotationY, cu.minimumY, cu.maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(cu.rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(cu.rotationY, -Vector3.right);

            Camera.main.transform.localRotation = cu.originalRotation * yQuaternion;
            transform.localRotation = cu.originalRotation * xQuaternion;
        }

        private void CharacterMovement()
        {
            float speed;
            GetInput(out speed);

            if (!IsGrounded())
            {
                gravityScalar += 0.1f;
            }
            else
            {
                gravityScalar = 0;
                
            }

            JumpVector = transform.up * -character.Gravity * gravityScalar;

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                JumpVector.y += character.JumpHeight * 100;
            }

            MoveVector = transform.forward * input.y + transform.right * input.x;

            characterController.Move((MoveVector * speed + JumpVector) * Time.deltaTime);
        }

        private void SetupCameraPosition()
        {
            Camera.main.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);
            Camera.main.transform.parent = gameObject.transform;
            Camera.main.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

            cu.originalRotation = transform.localRotation;
        }

        private void GetInput(out float speed)
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            //float horizontal = CrossPlatformInputManager.GetAxis("LeftStickX");
            //float vertical = CrossPlatformInputManager.GetAxis("LeftStickY");

            bool wasWalking = isWalking;

            isWalking = !Input.GetKey(KeyCode.LeftShift);
            //isWalking = !CrossPlatformInputManager.GetButton("L1");

            speed = isWalking ? character.WalkSpeed : character.RunSpeed;
            input = new Vector2(horizontal, vertical);

            /*
            if (input.sqrMagnitude > 1) // normalize input if it exceeds 1 in combined length:
            {
                input.Normalize();
            }

            // handle speed change to give an fov kick
            if (m_IsWalking != wasWalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
            */
        }

        private Color32 GetGroundColor()
        {
            Color32 color = new Color32();
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, characterController.radius,
                transform.TransformDirection(Vector3.down), out hit, characterController.height / 3))
            {
                Vector3 position = hit.point;
                position += hit.normal * 0.5f;

                int x = Mathf.RoundToInt(position.x);
                int y = Mathf.RoundToInt(position.y);
                int z = Mathf.RoundToInt(position.z);

                color = UColor.ByteToColor(volume.GetComponent<MVolume>().volume.Voxel[x, y, z]);

                return color;
            }
            return color;
        }

        private bool IsGrounded()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, characterController.radius,
                transform.TransformDirection(Vector3.down), out hit, characterController.height/3))
            {
                //Debug.Log("Grounded");
                return true;
            }
            return false;
        }

        private void SetupModels()
        {
            Head.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.HeadModel, character.Team);
            Body.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.BodyModel, character.Team);
            LeftLeg.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.LeftLegModel, character.Team);
            RightLeg.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.RightLegModel, character.Team);
            LeftArm.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.LeftArmModel, character.Team);
            RightArm.GetComponent<MeshFilter>().mesh = UModelMesh.Get(character.RightArmModel, character.Team);

            Head.AddComponent<MeshRenderer>().material = UModelMesh.material;
            Body.AddComponent<MeshRenderer>().material = UModelMesh.material;
            LeftLeg.AddComponent<MeshRenderer>().material = UModelMesh.material;
            RightLeg.AddComponent<MeshRenderer>().material = UModelMesh.material;
            LeftArm.AddComponent<MeshRenderer>().material = UModelMesh.material;
            RightArm.AddComponent<MeshRenderer>().material = UModelMesh.material;
        }
    }
}