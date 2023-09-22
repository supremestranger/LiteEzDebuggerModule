using Leopotam.EcsLite;
using LiteEzDebugger;

namespace LiteEzDebugger
{
    public sealed class LiteEzDebuggerInitSystem : IEcsPreInitSystem {
        private EzDebuggerModule _debugger;

        public LiteEzDebuggerInitSystem(EzDebuggerModule debugger) {
            _debugger = debugger;
        }
        
        public void PreInit(IEcsSystems systems) {
            _debugger.Init(systems);
        }
    }
}