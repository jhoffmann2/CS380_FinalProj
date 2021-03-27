namespace Planning
{
  public interface IOperation
  {
    void Init();
    void Update();
    void Exit();
    bool IsComplete();
  }
}