using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    CharacterController _characterController;
    [SerializeField] float _receivedDamage = 20f;
    // [SerializeField] Slider _healthSlider;
    protected override void Start(){
        base.Start();
        _characterController = GetComponent<CharacterController>();
    }
      
    // public override void AddDamage(float damage){
    //     base.AddDamage(damage);
    //     if(isDead){

    //     }
    // }
    void Update(){
       
        if(GameManager.instance.enemyCount == 0){
            //if enemies are defeated set up next level
        }
    }
    
    protected override void Die(){
        base.Die();
        Debug.Log("You died");
        // _characterController.enabled = false;

    }
   
    protected override void UpdateHealth()
    {
        base.UpdateHealth();
        // _healthSlider.value = HealthPercent;
    }

    void OnTriggerEnter(Collider other){
        SkeletonAI skelly = other.GetComponentInParent<SkeletonAI>();
        if(skelly != null){
            // _hitSrc.Stop();
            Debug.Log("I got hit! My health:" + health);
            // _hitSrc.Play();
            AddDamage(_receivedDamage);
        }
    }
}
