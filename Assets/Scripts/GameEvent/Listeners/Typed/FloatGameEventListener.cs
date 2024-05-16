using GameEvent.Events.Typed;
using TypedUnityEvent;

namespace GameEvent.Listeners.Typed
{
    /// <summary>
    /// Float-typed <see cref="GameEventListener"/> to use when there is the need for passing through float values.
    /// </summary>
    public class FloatGameEventListener : GameEventListenerBase<float, FloatGameEvent, FloatEvent>
    {
    }
}