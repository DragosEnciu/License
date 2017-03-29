using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Implements the observer pattern.
/// </summary>
public class EventManager : MonoBehaviour {
#region State.
    #region Fields.
    public bool LimitQueueProcesing = false;
	public float QueueProcessTime = 0.0f;
    private Queue m_eventQueue = new Queue();
    /// <summary>
    /// Indexes the listeners by the type of event they had subscribed to.
    /// </summary>
    private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
    /// <summary>
    /// Maps the external delegates (specified as parameters) to the internal delegate they had been associated with.
    /// </summary>
    private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
    /// <summary>
    /// A set of delegates that are to be removed from the observers list after they are invoked.
    /// </summary>
    private Dictionary<System.Delegate, System.Delegate> onceLookups = new Dictionary<System.Delegate, System.Delegate>(); // Should be HashSet<T>
    #endregion
    #region Delegates.
    public delegate void EventDelegate<T>(T e) where T : EventManager;
    /// <summary>
    /// Holds non-generic references to the <see cref="EventDelegate{T}"/> delegate to aid in generics usage.
    /// </summary>
    private delegate void EventDelegate(EventManager e);
    #endregion
#endregion
#region Static state.
    #region Fields.
    private static EventManager s_Instance = null;
    // override so we don't have the typecast the object
    public static EventManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = GameObject.FindObjectOfType(typeof(EventManager)) as EventManager;
            }
            return s_Instance;
        }
    }
    #endregion
#endregion
#region Interface.
    #region Private.
    #region Functionality.
    /// <summary>
    ///     <para>If the specified delegate is already contained in the current instance, Null will be returned and it will not be added again.</para>
    ///     <para>
    ///             Otherwise, the delegate will either be added to the existing delegate associated with the generic type T if such delegate exists, 
    ///             or it will be associated with the generic type T.
    ///     </para>
    /// </summary>
    /// <typeparam name="T">The delegate's generic type.</typeparam>
    /// <param name="del">The delegate.</param>
    private EventDelegate AddDelegate<T>(EventDelegate<T> del) 
        where T : EventManager
    {
        EventDelegate tempDel;
        //
        // Early-out if we've already registered this delegate
        if (delegateLookup.ContainsKey(del))
        {
            return null;
        }
        //
		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => del((T)e);
        //
        // Associate the external delegate with the internal delegate that invokes it.
		delegateLookup[del] = internalDelegate;
        //
        // If a delegate already exists for the specified type, add the specified delegate to its execution list.
        // The existing delegate will be obtained in the tempDel reference.
		if (delegates.TryGetValue(typeof(T), out tempDel))
        {
			delegates[typeof(T)] = tempDel += internalDelegate; 
		}
        //
        // Otherwise, add the new type and delegate to the dictionary.
        else
        {
			delegates[typeof(T)] = internalDelegate;
		}
        //
        // Return a reference to the internal delegate.
		return internalDelegate;
	}
    #endregion
    #endregion
    #region Public.
    #region Functionality.
    /// <summary>
    /// Subscribes the specified listener.
    /// </summary>
    /// <typeparam name="T">The delegate's type.</typeparam>
    /// <param name="del">The delegate that will be subscribed.</param>
    public void AddListener<T> (EventDelegate<T> del) 
        where T : EventManager
    {
		AddDelegate<T>(del);
	}
    /// <summary>
    /// Subscribes the specified listener so that it is only executed at the event's next trigger.
    /// </summary>
    /// <typeparam name="T">The delegate's type.</typeparam>
    /// <param name="del">The delegate that will be subscribed.</param>
	public void AddListenerOnce<T> (EventDelegate<T> del) 
        where T : EventManager
    {
		EventDelegate result = AddDelegate<T>(del);

		if (null != result)
        {
            //
            // Mark the delegate for removal after its invokation.
			onceLookups[result] = del;
		}
	}
    /// <summary>
    /// Removes the specified delegate from the listeners list.
    /// </summary>
    /// <typeparam name="T">The delegate's type.</typeparam>
    /// <param name="del">The delegate that is to be removed.</param>
	public void RemoveListener<T> (EventDelegate<T> del) 
        where T : EventManager
    {
		EventDelegate internalDelegate;
        //
        // Attempt to obtain the internal delegate associated with the specified delegate.
		if (delegateLookup.TryGetValue(del, out internalDelegate))
        {
			EventDelegate tempDel;
            //
            // Attempt to obtain the delegate associated with the specified type.
			if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                //
                // Unsubscribe the delegate.
				tempDel -= internalDelegate;
                //
                // If the delegate associated with the specified type had become empty, remove it.
				if (tempDel == null)
                {
					delegates.Remove(typeof(T));
				}
                else
                {
					delegates[typeof(T)] = tempDel;
				}
			}
            //
            // Remove the lookup association.
			delegateLookup.Remove(del);
		}
	}
    /// <summary>
    /// Clears the references to all the delegates.
    /// </summary>
	public void RemoveAll()
    {
		delegates.Clear();
		delegateLookup.Clear();
		onceLookups.Clear();
	}
    /// <summary>
    /// Checks if the specified delegate has any listeners.
    /// </summary>
    /// <typeparam name="T">The delegate's type.</typeparam>
    /// <param name="del">The delegate that is to be checked.</param>
	public bool HasListener<T> (EventDelegate<T> del) 
        where T : EventManager
    {
		return delegateLookup.ContainsKey(del);
	}
    /// <summary>
    /// Triggers the specified event.
    /// </summary>
    /// <param name="e">The event that is to be triggered.</param>
	public void TriggerEvent (EventManager e)
    {
		EventDelegate del;
        //
        // Attempt to obtain the delegate that contains the listeners subscribed to the specified event type.
		if (delegates.TryGetValue(e.GetType(), out del))
        {
			del.Invoke(e);
            //
			// Remove listeners which should only be called once
			foreach (EventDelegate k in delegates[e.GetType()].GetInvocationList())
            {
				if(onceLookups.ContainsKey(k))
                {
					delegates[e.GetType()] -= k;

					if(delegates[e.GetType()] == null)
					{
						delegates.Remove(e.GetType());
					}

					delegateLookup.Remove(onceLookups[k]);
					onceLookups.Remove(k);
				}
			}
		}
        else
        {
			Debug.LogWarning("Event: " + e.GetType() + " has no listeners");
		}
	}

	//Inserts the event into the current queue.
	public bool QueueEvent(EventManager evt)
    {
		if (!delegates.ContainsKey(evt.GetType()))
        {
			Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetType());
			return false;
		}

		m_eventQueue.Enqueue(evt);
		return true;
	}
    #endregion
    #endregion
    #region Unity.
    //Every update cycle the queue is processed, if the queue processing is limited,
    //a maximum processing time per update can be set after which the events will have
    //to be processed next update loop.
    void Update() {
		float timer = 0.0f;
		while (m_eventQueue.Count > 0) {
			if (LimitQueueProcesing) {
				if (timer > QueueProcessTime)
					return;
			}

			EventManager evt = m_eventQueue.Dequeue() as EventManager;
			TriggerEvent(evt);

			if (LimitQueueProcesing)
				timer += Time.deltaTime;
		}
	}
	public void OnApplicationQuit(){
		RemoveAll();
		m_eventQueue.Clear();
		s_Instance = null;
	}
    #endregion
#endregion
}