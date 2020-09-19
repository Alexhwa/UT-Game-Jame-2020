using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WizardController : EnemyController
{
    [Header("Wizard Variables")]
    public GameObject sword;
    public Transform[] spawnPoints;
    public float swordDelay;
    public float swordOffset;

    public override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        attackCooldown = swordOffset * (spawnPoints.Length - 1) + swordDelay;
        base.Attack();
        StartCoroutine(SpawnSwords());
        anim.SetTrigger("Attacking");
    }
    private IEnumerator SpawnSwords()
    {
        List<ProjectileController> swords = new List<ProjectileController>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var swordInst = Instantiate(sword, spawnPoints[i].position, transform.rotation);
            var projCntrl = swordInst.GetComponent<ProjectileController>();
            var swordSpriteRend = swordInst.GetComponentInChildren<SpriteRenderer>();
            swordSpriteRend.DOFade(1, swordOffset).SetEase(Ease.InCubic);
            projCntrl.state = PickUpState.Disabled;

            //Rotation
            Vector3 swordRotation = swordInst.transform.eulerAngles;
            Vector3 targetVect = player.transform.position - swordInst.transform.position;
            
            if(targetVect.x > 0)
            {
                swordRotation.z -= Vector3.Angle(swordInst.transform.up, player.transform.position - swordInst.transform.position);
            }
            else
            {
                swordRotation.z += Vector3.Angle(swordInst.transform.up, player.transform.position - swordInst.transform.position);
            }
            swordInst.transform.DORotate(swordRotation, swordOffset * 2).SetEase(Ease.InCubic);

            swords.Add(projCntrl);
            yield return new WaitForSeconds(swordOffset);
        }
        yield return new WaitForSeconds(swordDelay);
        foreach(ProjectileController e in swords)
        {
            if(e.state != PickUpState.PickedUp)
                e.state = PickUpState.Idle;
        }
    }
}
