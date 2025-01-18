using UnityEngine;

public class BlockLeaf : LeafNode
{
    private int transtionHash;
    private int blockHash;

    public BlockLeaf(string transitionName, string blockName, GameObject agent, Weight weightComponent) : base(agent, weightComponent)
    {
        transtionHash = Animator.StringToHash(transitionName);
        blockHash = Animator.StringToHash(blockName);
    }

    public override NodeStates Evaluate()
    {
        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Parry")
        {
            _animator.Play(blockHash);
        }
        else
        {
            _animator.Play(transtionHash);
        }
        return NodeStates.Success;
    }
}
