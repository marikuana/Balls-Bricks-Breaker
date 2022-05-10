public class SuicideBall : BallBase
{
    public override void Impact(Block block)
    {
        block.Damage(Damage);
        Destroy();
    }
}
