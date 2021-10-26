using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class UnityAnimationEvent : UnityEvent<string> { };
[RequireComponent(typeof(Animator))]
public class CharacterSkinController : MonoBehaviour
{
    Animator animator;
    Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead}
    public EyePosition eyeState;
    private bool isJumping = false;
    public UnityAnimationEvent OnAnimationStart;
    public UnityAnimationEvent OnAnimationComplete;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        //-- Sleiman: the following code will notify us when each anim clip ends.
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

            AnimationEvent animationStartEvent = new AnimationEvent();
            animationStartEvent.time = 0;
            animationStartEvent.functionName = "AnimationStartHandler";
            animationStartEvent.stringParameter = clip.name;

            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationCompleteHandler";
            animationEndEvent.stringParameter = clip.name;

            clip.AddEvent(animationStartEvent);
            clip.AddEvent(animationEndEvent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ChangeMaterialSettings(3);
            //ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("jump");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            Debug.Log("Jumping....");
            isJumping = true;           
        }
        if (!isJumping)
        {

        }
    }

    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
        //OnAnimationStart?.Invoke(name);
    }
    /// <summary>
    /// Handles actions to be performed when an animation clip stops playing.
    /// </summary>
    /// <param name="animationName">The name of the animation clip (that is, the state) as defined in the Animation controller. For example, jump, walk, etc.</param>
    public void AnimationCompleteHandler(string animationName)
    {
        Debug.Log($"{name} animation complete.");
        if (animationName == "Jump")
        {
            //-- Sleiman: Jump animation has been ended! Now we can trigger another anim to play, or we can just go back to the 
            // idle (aka normal) state.
            animator.SetTrigger("normal");
        }
        //OnAnimationComplete?.Invoke(name);
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            else
                characterMaterials[i].material.SetTexture("_MainTex",albedoList[index]);
        }
    }


    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }
}
