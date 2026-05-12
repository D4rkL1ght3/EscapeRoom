public interface Interactable
{
    void Interact();

    void CloseUI();

    void Highlight();

    void RemoveHighlight();

    bool isOpen { get; }
}