using BTAI;
using System.Collections.Generic;
using UnityEngine;

public class TestBT : MonoBehaviour, BTAI.IBTDebugable
{
    Root aiRoot = BT.Root();


    private void OnEnable()
    {
        aiRoot.OpenBranch(
                BT.If(TestVisibleTarget).OpenBranch(
                BT.Call(Aim),
                BT.Call(Shoot)
                 ),
                BT.Sequence().OpenBranch(
                BT.Call(Walk),
                BT.Wait(5.0f),
                BT.Call(Turn),
                BT.Wait(1.0f),
                BT.Call(Turn)
             )
        );
        BT.RunCoroutine(RunCoroutine);
    }
    IEnumerator<BTAI.BTState> RunCoroutine()
    {
        yield return BTAI.BTState.Success;
    }
    private void Turn()
    {
        Debug.Log("执行了 Turn");
    }

    private void Walk()
    {
        Debug.Log("执行了 Walk");
    }

    private void Shoot()
    {
        Debug.Log("执行了 Shoot");
    }

    private void Aim()
    {
        Debug.Log("执行了 Aim");
    }

    private bool TestVisibleTarget()
    {
        var isSuccess = UnityEngine.Random.Range(0, 2) == 1;
        Debug.Log("执行了 TestVisibleTarget    Result:" + isSuccess);

        return isSuccess;
    }

    private void Update()
    {
        aiRoot.Tick();
    }

    public Root GetAIRoot()
    {
        return aiRoot;
    }
}