namespace Planning
{
  public interface IOperation
  {
    void Init(GoapAgent agent);
    void Update(GoapAgent agent);
    void Exit(GoapAgent agent);
    bool IsComplete(GoapAgent agent);
  }
}