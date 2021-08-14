public abstract class BaseState
{
    protected HumanController controller;
    public HumanController Controller => controller;
    public TacticMap Map => controller.Map;
    public void SetController(HumanController controller)
    {
        this.controller = controller;
    }
    public virtual void OnEnter() {}
    public virtual void OnLeave() {}
}