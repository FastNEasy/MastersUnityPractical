using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    CharacterController characterControl;
    //time between each step
    [SerializeField] float timeBetweenSteps = 0.8f;
    //current time of the env
    private float time = 0f;
    //difference between current time and previous time of the step
    private float timeDif = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //getting the character controller of the rig
        characterControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeDif = Time.time - time; 
        //check if audio needs to be played (is player on the ground, is player walking, is the walking sound playing)
        bool playAudio = characterControl.isGrounded && characterControl.velocity.magnitude > 0 && GetComponent<AudioSource>().isPlaying == false;
        if(playAudio && timeDif > timeBetweenSteps){
            time = Time.time;
            //play audio of random pitch and volume to simulate unequal walking sounds
            GetComponent<AudioSource>().volume = Random.Range(0.8f, 1f);
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
    }
}
