using UnityEngine;

namespace PlayerState
{
    public interface State
    {

        // Runs any code that needs to happen only once and when the state is first ran
        void StateEnter();

        // On Update should implement any logic that needs to happen every frame.
        State StateUpdate();

        // On FixedUpdate should implement any logic that needs to happen every physics frame.
        State StateFixedUpdate();
    }
}
