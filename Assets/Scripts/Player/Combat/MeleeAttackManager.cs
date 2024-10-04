using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeAttackManager : MonoBehaviour
{
    // public static MeleeAttackManager instance; void Awake() { instance = this; }

    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private List<GameObject> attackFabs;
    [SerializeField]
    private List<GameObject> attackPos;
    [SerializeField]
    private List<string> animSetBools;
    [SerializeField]
    private List<int> attackAnimTimes;
    [SerializeField]
    private List<int> attackWindowTimes;
    [SerializeField]
    private Animator anim;


    private List<bool> attackAnims;
    private List<bool> attackWindows;
    private List<GameObject> attackHits;

    void Start()
    {
        attackAnims = new List<bool> {false, false, false};
        attackWindows = new List<bool> {false, false, false};
        attackHits = new List<GameObject> {null, null, null};

        PlayerManager.instance.inCombat = false;
    }

    async void Attack(int attackNum) {
        PlayerManager.instance.inCombat = true;
        PlayerManager.instance.isAttacking = true;
        Debug.Log("Performing attack " + attackNum);
        attackHits[attackNum] = Instantiate(attackFabs[attackNum], attackPos[attackNum].transform.position, attackPos[attackNum].transform.rotation);
        attackAnims[attackNum] = true;
        anim.SetBool(animSetBools[attackNum], true);

        await Task.Delay(attackAnimTimes[attackNum]);
        attackWindows[attackNum] = true;
        await Task.Delay(attackWindowTimes[attackNum]);

        Destroy(attackHits[attackNum]);
        attackWindows[attackNum] = false;
        attackAnims[attackNum] = false;
        anim.SetBool(animSetBools[attackNum], false);
        PlayerManager.instance.isAttacking = false;

        await Task.Delay(10000 - attackAnimTimes[attackNum] - attackWindowTimes[attackNum]);
        PlayerManager.instance.inCombat = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !InventoryUI.isActive && !Dialogue.instance.canType) {
            if (!attackAnims.Contains(true)) {
                Attack(0);
            } else
            {
                for (int i = 1; i < attackWindows.Count; i++) {
                    if (attackWindows[i - 1] && !attackAnims[i]) {
                        Attack(i);
                    }
                }
            }
        }
    }
}
