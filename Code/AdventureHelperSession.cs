using System.Collections.Generic;

namespace Celeste.Mod.AdventureHelper {
    internal class AdventureHelperSession : EverestModuleSession {
        public List<DreamBlock> DreamBlocksToCombine = new();
        public List<DreamBlock> DreamBlocksNotToCombine = new();
    }
}
