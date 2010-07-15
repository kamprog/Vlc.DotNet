using System;
using System.Collections.Generic;

namespace Vlc.DotNet.Core.Interop.Event
{
    internal class VlcEventManager
    {
        private readonly List<VlcEventType> _AttachedEvents;
        private readonly IntPtr p_event_manager;

        /// <summary>
        /// COM pointer to a vlc exception.  We will only use 1 exception pointer, 
        /// so we must always clear it out after use
        /// </summary>
        private libvlc_exception_t p_exception;

        internal VlcEventManager(IntPtr p_event_manager)
        {
            _AttachedEvents = new List<VlcEventType>();

            this.p_event_manager = p_event_manager;
            //Initalize our exception pointer
            p_exception = new libvlc_exception_t();
            p_exception.Initalize();
        }

        internal void AttachEvent(VlcEventType EventType, VlcCallback Callback, IntPtr UserData)
        {
            InteropMethods.libvlc_event_attach(p_event_manager, EventType, Callback, UserData, ref p_exception);
            p_exception.CheckException();
            _AttachedEvents.Add(EventType);
        }

        internal void DetachEvent(VlcEventType EventType, VlcCallback Callback, IntPtr UserData)
        {
            InteropMethods.libvlc_event_detach(p_event_manager, EventType, Callback, UserData, ref p_exception);
            p_exception.CheckException();
            _AttachedEvents.Remove(EventType);
        }

        internal bool IsAttached(VlcEventType EventType)
        {
            return _AttachedEvents.Contains(EventType);
        }
    }
}