using System.Collections.Generic;

namespace Celeste.Mod.AdventureHelper {
    class AdventureHelperSession : EverestModuleSession {
        public List<DreamBlock> DreamBlocksToCombine = new List<DreamBlock>();
        public List<DreamBlock> DreamBlocksNotToCombine = new List<DreamBlock>();
    }
}
