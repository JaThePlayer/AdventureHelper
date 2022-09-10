using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AdventureHelper.Entities {
    [CustomEntity("AdventureHelper/BladeTrackSpinnerMultinode")]
    public class BladeTrackSpinnerMultinode : MultipleNodeTrackSpinner {
        public Sprite Sprite;
        private bool hasStarted;

        public BladeTrackSpinnerMultinode(EntityData data, Vector2 offset) : base(data, offset) {
            Add(Sprite = GFX.SpriteBank.Create("templeBlade"));
            Sprite.Play("idle");
            Depth = -50;
            Add(new MirrorReflection());
        }
        public override void OnTrackStart() {
            Sprite.Play("spin");
            bool flag = hasStarted;
            if (flag) {
                Audio.Play("event:/game/05_mirror_temple/bladespinner_spin", Position);
            }
            hasStarted = true;
        }

        public override void Update() {
            bool reachedDestination = PauseTimer > 0f;
            bool wasPaused = !Moving;
            base.Update();
            if (wasPaused && Moving && !reachedDestination) {
                if (hasStarted) {
                    Sprite.Play("spin");
                    Audio.Play("event:/game/05_mirror_temple/bladespinner_spin", Position);
                }
            }
        }
    }
}
