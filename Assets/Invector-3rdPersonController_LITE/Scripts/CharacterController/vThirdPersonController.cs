using UnityEngine;
using TMPro;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        public TMP_Text coinCounter_TMP;
        private int numOfCoins = 0;
        // public int progressBarValue = 10;
        public GameObject naturalElement;

        public static float damage = 10f;
        public AudioSource audioSource;
        public AudioSource enemyHitSound;

        // Use this for initialization
        void Start()
        {
            //Set Cursor to not be visible
            Cursor.visible = false;
        }
        private void Update()
        {

            coinCounter_TMP.text = numOfCoins.ToString();
            if(gameObject.transform.position.y <= 12)
            {
                Debug.Log("player has fallen off the world");
                // Destroy(gameObject);
                //Application.LoadLevel("Level 1");
                if (Application.loadedLevelName == "Level 1")
                {
                    Application.LoadLevel("Level 1");
                }

                if (Application.loadedLevelName == "Level 2")
                {
                    Application.LoadLevel("Level 2");
                }

                if (Application.loadedLevelName == "Level 3")
                {
                    Application.LoadLevel("Level 3");
                }

                if (Application.loadedLevelName == "Level 4")
                {
                    Application.LoadLevel("Level 4");
                }
            }

            naturalElement.SetActive(false);
           /* if (numOfCoins == 3)
            {
                naturalElement.SetActive(true);
            }*/

            if (Application.loadedLevelName == "Level 1")
            {
                if (numOfCoins == 3)
                {
                    naturalElement.SetActive(true);
                }
            }

            if (Application.loadedLevelName == "Level 2")
            {
                if (numOfCoins == 5)
                {
                    naturalElement.SetActive(true);
                }
            }

            if (Application.loadedLevelName == "Level 3")
            {
                if (numOfCoins == 8)
                {
                    naturalElement.SetActive(true);
                }
            }

            if (Application.loadedLevelName == "Level 4")
            {
                if (numOfCoins == 10)
                {
                    naturalElement.SetActive(true);
                }
            }
        }
        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Coin")
            {
                Debug.Log("Coin gained");
                audioSource.Play();
                Destroy(collision.gameObject);
                numOfCoins += 1;
            }

            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Collided with enemy");
                enemyHitSound.Play();
                GetComponent<PlayerHealth>().ApplyDamage(damage);
               
            }

            if (collision.gameObject.tag == "NaturalElement")
            {
                Debug.Log("Collected Natural Element");

                if (Application.loadedLevelName == "Level 1")
                {                   
                    //Destroy(collision.gameObject);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                }

                if (Application.loadedLevelName == "Level 2")
                {
                   // Destroy(collision.gameObject);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(6);
                }

                if (Application.loadedLevelName == "Level 3")
                {       
                   // Destroy(collision.gameObject);
                   UnityEngine.SceneManagement.SceneManager.LoadScene(7);
                }

                if (Application.loadedLevelName == "Level 4")
                {         
                  //  Destroy(collision.gameObject);
                   UnityEngine.SceneManagement.SceneManager.LoadScene(12);
                }
            }
        }

    }
}