using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    private NavMeshAgent _navmeshAgent;
    private State lastState;
    private State currentState;
    private bool stateStarted = false;
    private bool isClose = false;
    private bool canRun = false;
    public bool turnOffWalk = false;
    Vector3 playerPos;
    public event System.Action<SkeletonAI> onSkellyKilled;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _navmeshAgent = GetComponent<NavMeshAgent>();
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
        // SwitchAttackAnim(false);
        SwitchRunningAnim(false);
    }
    void StartRunning(){
       _navmeshAgent.isStopped = false;
       _navmeshAgent.speed = 3f;
    //    _animator.SetTrigger("Run");
    //    SwitchAttackAnim(false);
       SwitchRunningAnim(true);
    }
    void StartAttacking(){
        _navmeshAgent.isStopped = true;
        _navmeshAgent.velocity = Vector3.zero;
         
        // SwitchAttackAnim(true);
        SwitchRunningAnim(false);
    }
    void StartToDie(){
        _navmeshAgent.isStopped = true;
        _navmeshAgent.velocity = Vector3.zero;
        GameManager.instance.enemyCount -= 1;
       
        //dying things
    }
    void SwitchAttackAnim(bool choice){//to avoid DRY
        // _animator.SetBool("isAttacking", choice);
    }
    void SwitchRunningAnim(bool choice){
        // _animator.SetBool("isRunning", choice);
    }
    void GetHit(float damage){
        //apply damage to the enemy
        AddDamage(damage);
        // if(!_audioSrc.isPlaying){
        //     _audioSrc.PlayOneShot(hitClip);
        // }
        // _animator.SetTrigger("isHit");
    }
     public override void AddDamage(float damage)
    {
        base.AddDamage(damage);
        if(isDead){
            currentState = State.Dying;
            turnOffWalk = true;
            onSkellyKilled.Invoke(this);
        }
    }
    private void OnTriggerEnter(Collider other){
        Weapon weapon = other.GetComponentInParent<Weapon>();
         if(weapon != null){
            Debug.Log("Enemy got hit for " + weapon.damage);
            GetHit(weapon.damage);
       }
    }
}
