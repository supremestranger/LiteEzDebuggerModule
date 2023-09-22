using System;
using System.Collections.Generic;
using System.Diagnostics;
using Leopotam.EcsLite;
using Debug = UnityEngine.Debug;

namespace LiteEzDebugger {
    public class EzDebuggerModule : IEcsWorldEventListener {
        private readonly string _worldName;
        private readonly Type[] _types;
        private EcsWorld _world;

        public EzDebuggerModule(string worldName = null, params Type[] types) {
#if DEBUG
            if (types == null) {
                throw new Exception("Types array is null!");
            }
#endif
            _types = types;
            _worldName = worldName;
        }

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld(_worldName);
            _world.AddEventListener(this);
        }
        

        public void OnEntityChanged(int entity, short poolId, bool added)
        {
            foreach (var type in _types)
            {
                var pool = _world.GetPoolByType(type);
                if (pool != null) {
                    StackTrace stackTrace = new StackTrace();
                    var frame = stackTrace.GetFrame(3);

                    Debug.Log($"[TRACING] component {type.Name} has been {(added ? "added" : "removed")} in class {frame.GetMethod().ReflectedType} in method {frame.GetMethod().Name}!");
                }
            }
        }

        public void OnEntityCreated(int entity)
        {
            
        }
        

        public void OnEntityDestroyed(int entity) { }
        
        public void OnFilterCreated(EcsFilter filter)
        {
            
        }

        public void OnWorldResized(int capacity) { }
        public void OnWorldDestroyed(EcsWorld world)
        {
            _world.RemoveEventListener(this);
        }

    }
}