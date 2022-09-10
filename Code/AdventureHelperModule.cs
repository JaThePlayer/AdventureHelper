using Celeste.Mod.AdventureHelper.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System;

namespace Celeste.Mod.AdventureHelper {
    public class AdventureHelperModule : EverestModule {

        public static AdventureHelperModule Instance;

        public AdventureHelperModule() {
            Instance = this;
        }

        public override Type SettingsType => null;
        public override Type SessionType => typeof(AdventureHelperSession);
        public static AdventureHelperSession Session => (AdventureHelperSession)Instance._Session;

        public static SpriteBank SpriteBank { get; private set; }

        public override void Load() {
            Everest.Events.Level.OnLoadEntity += LevelOnLoadEntity;
            AdventureHelperHooks.Load();
        }

        public override void Unload() {
            Everest.Events.Level.OnLoadEntity -= LevelOnLoadEntity;
            AdventureHelperHooks.Unload();
        }

        public override void LoadContent(bool firstLoad) {
            SpriteBank = new SpriteBank(GFX.Game, "Graphics/KaydenFox/AdventureHelperSprites.xml");
        }

        private bool LevelOnLoadEntity(Level level, LevelData levelData, Vector2 offset, EntityData entityData) {
            if (entityData.Name == "AdventureHelper/CustomCrystalHeart" && !level.Session.HeartGem) {
                level.Add(new CustomCrystalHeart(entityData, offset));
                return true;
            }

            return false;
        }
    }
}
