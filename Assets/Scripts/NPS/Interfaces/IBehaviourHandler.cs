namespace NPC.Behaviour
{
    public interface IBehaviourHandler
    {
        void OnEnable();
        void OnDisable();
        void Update();
        void LateUpdate();
        void FixedUpdate();
    }

}
