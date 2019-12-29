using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Clocks
{
    public class MonoClock : MonoBehaviour, IClock
    {
        static private MonoClock instance;
        static public MonoClock Instance {
            get {
                if (instance == null) {
                    instance = new GameObject("Mono Clock").AddComponent<MonoClock>();
                    DontDestroyOnLoad(instance);
                }

                return instance;
            }
        }
        public event Action OnTickNoDelta;
        public event Action<float> OnTick;

        private void Update()
        {
            OnTick?.Invoke(Time.deltaTime);
            OnTickNoDelta?.Invoke();
        }
    } 
}
