using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interactable : MonoBehaviour {
    // This allows the control to be grabbed by another wand while it is being held.
    public bool Stealable = true;
    // We restrict the attachedController property so that it can only be set using the TryAttach method.
    public WandController attachedController { get; private set; }

    //Returns true if the controller was attached correctly.
    public bool TryAttach(WandController ctrl) {
        //Only attach if the controller is not holding something already.
        if (ctrl.heldItem != null) return false;
        //Steal object from other controller.
        if (attachedController != null) {
            if (!Stealable) return false;
            DetachController();
        }

        attachedController = ctrl;
        OnBeginInteraction();
        // Perhaps DetachController was called from OnBeginInteraction.
        if (attachedController == null) return false;
        return true;
    }


    public void DetachController() {
        OnEndInteraction();
        if (attachedController.heldItem == this) attachedController.OnItemDetach(this);
        else Debug.LogError("Controller state (HeldItem) was incorrect. Tried to detach " + this + " while holding " + attachedController.heldItem);
        attachedController = null;
    }



    //Called when the user begins interacting with this object. 
    protected virtual void OnBeginInteraction() { }

    //Called  when the user stops interacting with this object. 
    protected virtual void OnEndInteraction() { }

    //No need for OnInteractionStay. You can issue updates for your object in Update() and check if attachedController is null;

    //Called by WandController when an unattached controller overlaps this object's collider.
    public virtual void OnHoverEnter(WandController ctrl) { }

    //Called by WandController when an unattached controller overlaps this object's collider.
    public virtual void OnHoverExit(WandController ctrl) { }

    //Called by WandController on each frame an unattached controller overlaps this object's collider.
    public virtual void OnHoverStay(WandController ctrl) { }


}
