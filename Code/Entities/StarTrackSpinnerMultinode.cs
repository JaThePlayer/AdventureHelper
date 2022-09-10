using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AdventureHelper.Entities {
    [CustomEntity("AdventureHelper/MultipleNodeTrackSpinner")]
    public class StarTrackSpinnerMultinode : MultipleNodeTrackSpinner {
        public Sprite Sprite;

        private bool hasStarted;
        private bool trail;
        private int colorID;

        public StarTrackSpinnerMultinode(EntityData data, Vector2 offset) : base(data, offset) {
            Add(Sprite = GFX.SpriteBank.Create("moonBlade"));
            colorID = Calc.Random.Choose(0, 1, 2);
            Sprite.Play("idle" + colorID);
            Depth = -50;
            Add(new MirrorReflection());
        }
        public override void OnTrackStart() {
            colorID++;
            colorID %= 3;
            Sprite.Play("spin" + (colorID % 3));
            if (hasStarted) {
                Audio.Play("event:/game/05_mirror_temple/bladespinner_spin", Position);
            }

            hasStarted = true;
            trail = true;

        }
        public override void OnTrackEnd() {
            trail = false;
        }
        public override void Update() {
            bool reachedDestination = PauseTimer > 0f;
            bool wasPaused = !Moving;
            base.Update();
            if (Moving && trail && Scene.OnInterval(0.03f)) {
                SceneAs<Level>().ParticlesBG.Emit(StarTrackSpinner.P_Trail[colorID], 1, Position, Vector2.One * 3f);
            }

            if (wasPaused && Moving && !reachedDestination) {
                if (hasStarted) {
                    colorID++;
                    colorID %= 3;
                    Sprite.Play("spin" + (colorID % 3));
                    Audio.Play("event:/game/05_mirror_temple/bladespinner_spin", Position);
                }
            }
        }
    }
}
