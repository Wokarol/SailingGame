using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Global
{
    // This class does not, and will not adhere to Open-Closed Principle
    // It's the result of increased complexity that comes with non-directly typed references, it's not worth it now

    public class World
    {
        public DynamicObjectReference<Camera> MainCamera = new DynamicObjectReference<Camera>("Main Camera");
    }

    public class DynamicObjectReference<T> where T : UnityEngine.Object
    {
        private string name;
        public T Value { get; private set; }

        public DynamicObjectReference(string name)
        {
            this.name = name;
        }

        public void Set(T obj)
        {
            if (Value != null) {
                Debug.LogWarning($"Overriding <color=cyan>{Value}</color> with <color=cyan>{obj}</color> as \"{name}\"", obj);
            }
            Value = obj;
        }

        public void Remove(T obj)
        {
            if (Value != obj) {
                Debug.LogWarning($"Cannot remove <color=cyan>{obj}</color> as \"{name}\" because current value is <color=cyan>{Value}</color>", obj);
            }
        }

        public static implicit operator T(DynamicObjectReference<T> source)
        {
            return source.Value;
        }
    }
}