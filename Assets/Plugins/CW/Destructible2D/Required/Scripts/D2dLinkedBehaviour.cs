﻿using System;
using UnityEngine;

namespace Destructible2D
{
    /// <summary>This class allows for quick registering and unregistering of class instances that can then be quickly looped through.</summary>
    public abstract class D2dLinkedBehaviour<T> : MonoBehaviour
        where T : D2dLinkedBehaviour<T>
    {
        [NonSerialized]
        public static T FirstInstance;

        [NonSerialized]
        public static int InstanceCount;

        [NonSerialized]
        public T PrevInstance;

        [NonSerialized]
        public T NextInstance;

        protected virtual void OnEnable()
        {
            var t = (T)this;

            if (FirstInstance != null)
            {
                FirstInstance.PrevInstance = t;

                PrevInstance = null;
                NextInstance = FirstInstance;
            }
            else
            {
                PrevInstance = null;
                NextInstance = null;
            }

            FirstInstance = t;
            InstanceCount += 1;
        }

        protected virtual void OnDisable()
        {
            if (FirstInstance == this)
            {
                FirstInstance = NextInstance;

                if (NextInstance != null)
                {
                    NextInstance.PrevInstance = null;
                }
            }
            else
            {
                if (NextInstance != null)
                {
                    NextInstance.PrevInstance = PrevInstance;
                }

                PrevInstance.NextInstance = NextInstance;
            }

            InstanceCount -= 1;
        }
    }
}