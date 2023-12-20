using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
public class SkeletonAI : Character
{
    private enum State
    {
        Walking,
        Running,
        Attacking,
        Hit,
        Dying
    }
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float runDistance = 6f;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip deathClip;
    private Animator _animator;
    private NavMeshAgent _navmeshAgent;
    private AudioSource _audioSrc;
    private State lastState;
    private State currentState;
    private bool stateStarted = false;
    private bool isClose = false;
    private bool canRun = false;
    public bool turnOffWalk = false;
    Vector3 playerPos;
    // public event System.Action<SkeletonAI> OnSkellyKilled;
  

    float deleteBodyTime = 3f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _audioSrc = GetComponent<AudioSource>();
        // Debug.Log(OnSkellyKilled);
        StartCoroutine(SkellyStates());
    }
    private IEnumerator SkellyStates(){
        while (true)
        {
            if (lastState != currentState || !stateStarted)
            {
                stateStarted = true;
                switch (currentState)
                {
                    case State.Walking:
                        StartWalking();
                        break;
                    case State.Running:
                        StartRunning();
                        break;
                    case State.Attacking:
                        StartAttacking();
                        break;
                    case State.Dying:
                        StartToDie();
                        break;
                }
                lastState = currentState;
            }
            UpdateSkeleton();
            yield return null;
        }
    }
    void UpdateSkeleton(){
        playerPos = GameManager.instance.Player.transform.position;
        float distToPlayer = Vector3.Distance(transform.position, playerPos);
        // Debug.Log("To player: " + distToPlayer);
        if (distToPlayer <= attackDistance) isClose = true;
        else isClose = false;
        if(distToPlayer >= runDistance) canRun = true;
        else canRun = false;
        switch (currentState)
        {
            case State.Attacking:
                if(!isClose) currentState = State.Walking;
                break;
            case State.Running:
                if(!canRun) currentState = State.Walking;
                NavUpdate();
                break;
            case State.Walking:
                if(isClose) currentState = State.Attacking;
                if(canRun) currentState = State.Running;
                NavUpdate();
                break;
        }
    }
    void NavUpdate(){
        _navmeshAgent.SetDestination(playerPos);
    }
    void StartWalking(){
        _navmeshAgent.isStopped = false;
        _navmeshAgent.speed = 0.8f;
        SwitchRunningAnim(false);
    }
    void StartRunning(){
       _navmeshAgent.isStopped = false;
       _navmeshAgent.speed = 3f;
       SwitchRunningAnim(true);
    }
    void StartAttacking(){
        _navmeshAgent.isStopped = true;
        _navmeshAgent.velocity = Vector3.zero;
        if(!_audioSrc.isPlaying){
            _audioSrc.PlayOneShot(attackClip);
        }
        _animator.SetTrigger("Attack");
        SwitchRunningAnim(false);
    }
    void StartToDie(){
        //on death send gamemanager a message (create in next version)
        _audioSrc.Stop();
        _navmeshAgent.isStopped = true;
        _navmeshAgent.velocity = Vector3.zero;
        turnOffWalk = true;
        _audioSrc.PlayOneShot(deathClip);
        _animator.SetTrigger("Dead");
        // OnSkellyKilled.Invoke(this);
        // GameManager.instance.EnemyKilled(this);
        GameManager.instance.enemyCount -= 1;
        //remove the body of the skeleton after set time
        StartCoroutine(RemoveSkeleton());
    }

    void SwitchRunningAnim(bool choice){
        _animator.SetBool("isRunning", choice);
    }
    void GetHit(float damage){
        //apply damage to the enemy
        AddDamage(damage);
        if(!_audioSrc.isPlaying){
            _audioSrc.PlayOneShot(hitClip);
        }
        _animator.SetTrigger("isHit");
    }
     public override void AddDamage(float damage)
    {
        base.AddDamage(damage);
        if(isDead){
            currentState = State.Dying;
            turnOffWalk = true;
        }
    }
    private void OnTriggerEnter(Collider other){
        Weapon weapon = other.GetComponentInParent<Weapon>();
         if(weapon != null && weapon.isPlayerWeapon){
            Debug.Log("Enemy got hit for " + weapon.damage);
            GetHit(weapon.damage);
       }
    }

    private IEnumerator RemoveSkeleton(){
        yield return new WaitForSeconds(deleteBodyTime);
        Destroy(gameObject);
    }
}
