using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;

namespace Celeste.Mod.AdventureHelper.Entities {
    [CustomEntity("AdventureHelper/DustTrackSpinnerMultinode")]
    public class DustTrackSpinnerMultinode : MultipleNodeTrackSpinner {
        private DustGraphic dusty;
        private Vector2 previousVector;
        private Vector2 nextVector;

        public DustTrackSpinnerMultinode(EntityData data, Vector2 offset) : base(data, offset) {
            Add(dusty = new DustGraphic(true, false, false));
            Vector2 start = Path[CurrentStart];
            Vector2 next = Path[(CurrentStart + 1) % Path.Length];
            dusty.EyeDirection = (dusty.EyeTargetDirection = (next - start).SafeNormalize());
            dusty.OnEstablish = new Action(Establish);
            Depth = -50;
        }
        private void Establish() {
            Vector2 current = Path[CurrentStart];
            Vector2 next = Path[(CurrentStart + 1) % Path.Length];
            Vector2 previous = Path[(CurrentStart - 1 + Path.Length) % Path.Length];
            nextVector = (next - current).SafeNormalize();
            previousVector = (current - previous).SafeNormalize();
            bool flag = Scene.CollideCheck<Solid>(new Rectangle((int) (X + nextVector.X * 4f) - 2, (int) (Y + nextVector.Y * 4f) - 2, 4, 4));
            bool flag2 = !flag;
            if (flag2) {
                nextVector = -nextVector;
                flag = Scene.CollideCheck<Solid>(new Rectangle((int) (X + nextVector.X * 4f) - 2, (int) (Y + nextVector.Y * 4f) - 2, 4, 4));
            }
            bool flag3 = flag;
            if (flag3) {
                float num = (current - next).Length();
                int num2 = 8;
                while ( num2 < num && flag) {
                    flag = flag && Scene.CollideCheck<Solid>(new Rectangle((int) (X + nextVector.X * 4f + previousVector.X *  num2) - 2, (int) (Y + nextVector.Y * 4f + previousVector.Y *  num2) - 2, 4, 4));
                    num2 += 8;
                }
                bool flag4 = flag;
                if (flag4) {
                    List<DustGraphic.Node> list = null;
                    bool flag5 = nextVector.X < 0f;
                    if (flag5) {
                        list = dusty.LeftNodes;
                    } else {
                        bool flag6 = nextVector.X > 0f;
                        if (flag6) {
                            list = dusty.RightNodes;
                        } else {
                            bool flag7 = nextVector.Y < 0f;
                            if (flag7) {
                                list = dusty.TopNodes;
                            } else {
                                bool flag8 = nextVector.Y > 0f;
                                if (flag8) {
                                    list = dusty.BottomNodes;
                                }
                            }
                        }
                    }
                    bool flag9 = list != null;
                    if (flag9) {
                        foreach (DustGraphic.Node node in list) {
                            node.Enabled = false;
                        }
                    }
                    dusty.Position -= nextVector;
                    dusty.EyeDirection = dusty.EyeTargetDirection = Calc.AngleToVector(Calc.AngleLerp(previousVector.Angle(), nextVector.Angle(), 0.3f), 1f);
                }
            }
        }
        public override void Update() {
            base.Update();
            bool flag = Moving && PauseTimer < 0f && Scene.OnInterval(0.02f);
            if (flag) {
                SceneAs<Level>().ParticlesBG.Emit(DustStaticSpinner.P_Move, 1, Position, Vector2.One * 4f);
            }
        }

        public override void OnPlayer(Player player) {
            base.OnPlayer(player);
            dusty.OnHitPlayer();
        }

        public override void OnTrackEnd() {
            base.OnTrackEnd();
            Vector2 current = Path[CurrentStart];
            Vector2 previous = Path[(CurrentStart - 1 + Path.Length) % Path.Length];
            previousVector = (previous - current).SafeNormalize();
            nextVector = Calc.AngleToVector(Angle, 1f);
            dusty.EyeTargetDirection = Calc.AngleToVector(Calc.AngleLerp(previousVector.Angle(), Angle, 1.0f), 1f);
        }
    }
}
