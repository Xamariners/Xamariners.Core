// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventHelpers.cs" company="">
//   
// </copyright>
// <summary>
//   Event helper class for event handlers managing.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     Event helper class for event handlers managing.
    /// </summary>
    public static class EventHelpers
    {
        #region Static Fields

        /// <summary>
        ///     The dictionary of event field infos.
        /// </summary>
        private static readonly Dictionary<Type, List<FieldInfo>> eventFieldInfos =
            new Dictionary<Type, List<FieldInfo>>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The raise.
        /// </summary>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <param name="propertyExpression">
        /// The property expression.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Raise<T>(this PropertyChangedEventHandler handler, Expression<Func<T>> propertyExpression)
        {
            if (handler != null)
            {
                var body = propertyExpression.Body as MemberExpression;
                var expression = body.Expression as ConstantExpression;
                handler(expression.Value, new PropertyChangedEventArgs(body.Member.Name));
            }
        }

        /// <summary>
        /// Removes all event handlers.
        /// </summary>
        /// <param name="obj">
        /// Object.
        /// </param>
        public static void RemoveAllEventHandlers(object obj)
        {
            RemoveEventHandler(obj, string.Empty);
        }

        /// <summary>
        /// Removes specific event handler.
        /// </summary>
        /// <param name="obj">
        /// Object.
        /// </param>
        /// <param name="EventName">
        /// Event name.
        /// </param>
        public static void RemoveEventHandler(object obj, string EventName)
        {
            if (obj == null)
            {
                return;
            }

            Type t = obj.GetType();
            List<FieldInfo> event_fields = GetTypeEventFields(t);
            EventHandlerList static_event_handlers = null;

            foreach (FieldInfo fi in event_fields)
            {
                if (EventName != string.Empty
                    && string.Compare(EventName, fi.Name, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    continue;
                }

                if (fi.IsStatic)
                {
                    // STATIC EVENT
                    if (static_event_handlers == null)
                    {
                        static_event_handlers = GetStaticEventHandlerList(t, obj);
                    }

                    object idx = fi.GetValue(obj);
                    Delegate eh = static_event_handlers[idx];
                    if (eh == null)
                    {
                        continue;
                    }

                    Delegate[] dels = eh.GetInvocationList();
                    if (dels == null)
                    {
                        continue;
                    }

                    EventInfo ei = t.GetRuntimeEvent(fi.Name);
                    foreach (Delegate del in dels)
                    {
                        ei.RemoveEventHandler(obj, del);
                    }
                }
                else
                {
                    // INSTANCE EVENT
                    EventInfo ei = t.GetRuntimeEvent(fi.Name);
                    if (ei != null)
                    {
                        object val = fi.GetValue(obj);
                        var mdel = val as Delegate;
                        if (mdel != null)
                        {
                            foreach (Delegate del in mdel.GetInvocationList())
                            {
                                ei.RemoveEventHandler(obj, del);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the event fields.
        /// </summary>
        /// <param name="t">
        /// T.
        /// </param>
        /// <param name="lst">
        /// Lst.
        /// </param>
        private static void BuildEventFields(Type t, List<FieldInfo> lst)
        {
            foreach (EventInfo ei in t.GetRuntimeEvents())
            {
                Type dt = ei.DeclaringType;
                FieldInfo fi = dt.GetRuntimeField(ei.Name);
                if (fi != null)
                {
                    lst.Add(fi);
                }
            }
        }

        /// <summary>
        /// Gets the static event handler list.
        /// </summary>
        /// <returns>
        /// The static event handler list.
        /// </returns>
        /// <param name="t">
        /// T.
        /// </param>
        /// <param name="obj">
        /// Object.
        /// </param>
        private static EventHandlerList GetStaticEventHandlerList(Type t, object obj)
        {
            MethodInfo mi = t.GetRuntimeMethod("get_Events", new Type[] { });
            return (EventHandlerList)mi.Invoke(obj, new object[] { });
        }

        /// <summary>
        /// Gets the type event fields.
        /// </summary>
        /// <returns>
        /// The type event fields.
        /// </returns>
        /// <param name="t">
        /// T.
        /// </param>
        private static List<FieldInfo> GetTypeEventFields(Type t)
        {
            if (eventFieldInfos.ContainsKey(t))
            {
                return eventFieldInfos[t];
            }

            var lst = new List<FieldInfo>();
            BuildEventFields(t, lst);
            eventFieldInfos.Add(t, lst);
            return lst;
        }

        #endregion
    }
}