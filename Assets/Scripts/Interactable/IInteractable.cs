namespace Interactable
{
    public interface IInteractable
    {
        /// <summary>
        /// Need time to interact with object
        /// </summary>
        float TimedToInteract { get; }
        void Interact();
    }
}