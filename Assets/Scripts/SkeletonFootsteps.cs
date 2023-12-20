using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonFootsteps : MonoBehaviour
{
    NavMeshAgent _navmeshAgent;
    SkeletonAI _script;
    AudioSource _audioSrc;
    private float walkDelay = 0.13f;
    // Start is called before the first frame updat
    void Start(){
        _navmeshAgent = GetComponentInParent<NavMeshAgent>();
        _audioSrc = GetComponent<AudioSource>();
        _script = GetComponentInParent<SkeletonAI>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_script.turnOffWalk) {
            _audioSrc.Stop();
            _audioSrc.enabled = false;
        }
        if(!_audioSrc.isPlaying && !_script.turnOffWalk) StartCoroutine(RandomWalkSounds());
       
    }
    IEnumerator RandomWalkSounds(){
       
        yield return new WaitForSeconds(walkDelay);
         _audioSrc.volume = Random.Range(0.45f, 0.8f);
         _audioSrc.pitch = Random.Range(0.75f, 1.0f);
        _audioSrc.Play();
    }
}
