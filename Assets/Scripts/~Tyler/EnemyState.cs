public abstract class EnemyState
{
    public abstract string GetClass();
    public abstract void OnStateEnter(EnemyStateManager manager);
    public abstract void OnStateUpdate(EnemyStateManager manager);
    public abstract void OnStateExit(EnemyStateManager manager);
}
