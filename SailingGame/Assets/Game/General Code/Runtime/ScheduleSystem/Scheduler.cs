using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Clocks;

namespace Wokarol
{
    public static class Scheduler
    {
        // Exposed methods
        public static void DelayCall(Action action, float delay)
        {
            // Initialize if needed
            if (delayedCallsQueue == null) {
                delayedCallsQueue = new PriorityQueue<DelayedCall>();
                MonoClock.Instance.OnTickNoDelta += Tick;
            }

            // Enqueues call to priority queue
            delayedCallsQueue.Enqueue(new DelayedCall(action, Time.time + delay));
        }

        // Intenal
        struct DelayedCall : IComparable<DelayedCall>
        {
            public readonly Action Action;
            public readonly float Time;

            public DelayedCall(Action action, float time)
            {
                Action = action ?? throw new ArgumentNullException(nameof(action));
                Time = time;
            }

            public int CompareTo(DelayedCall other)
            {
                if (other.Time == Time)
                    return 0;
                if (other.Time < Time)
                    return 1;
                else
                    return -1;
            }
        }
        static PriorityQueue<DelayedCall> delayedCallsQueue; // Stores all calls with closest one on top
        static void Tick()
        {
            // Loops as long as delayed calls queue is not empty or when top call still waits
            while (delayedCallsQueue.Count() > 0) {
                var call = delayedCallsQueue.Peek();

                // Check if top call is waiting
                if (call.Time < Time.time) {
                    // Calls it if needed
                    call.Action.Invoke();
                    delayedCallsQueue.Dequeue();
                } else {
                    // Breaks while loop if it's still waiting
                    break;
                }
            }
        }
    } 
}
