using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;

namespace Celeste.Mod.AdventureHelper.Entities {
    public class MultipleNodeTrackSpinner : Entity {
        public float PauseTimer;

        /// <summary>
        /// Percentage along the track it currently travels through.
        /// </summary>
        public float Percent { get; private set; }

        /// <summary>
        /// Node points to travel along before returning to <see cref="Start"/>.
        /// </summary>
        public Vector2[] Path { get; private set; }

        /// <summary>
        /// Index of the current start position.
        /// </summary>
        public int CurrentStart { get; private set; }

        /// <summary>
        /// Time it takes to move from the current start position to the current end position.
        /// </summary>
        public float MoveTime { get; }

        /// <summary>
        /// Time it waits at each node before moving on.
        /// </summary>
        public float PauseTime { get; }

        /// <summary>
        /// Whether the spinner is moving or not.
        /// </summary>
        public bool Moving { get; private set; }

        /// <summary>
        /// Angle at which the spinner is facing.
        /// </summary>
        public float Angle { get; private set; }

        /// <summary>
        /// The text representing the Pause Flag. When the Pause Flag is active, the spinner will stop moving.
        /// </summary>
        public string PauseFlag { get; private set; }

        /// <summary>
        /// The text representing the Pause Flag. When the Pause Flag is active, the spinner will stop moving.
        /// </summary>
        public bool HasPauseFlag { get; private set; }

        /// <summary>
        /// Tracks if the player has died to this entity.
        /// </summary>
        public bool playerDead { get; private set; }

        /// <summary>
        /// If set to true, this will cause the entity to halt its movement during a cutscene.
        /// </summary>
        public bool PauseOnCutscene { get; private set; }

        public MultipleNodeTrackSpinner(EntityData data, Vector2 offset) {
            PauseOnCutscene = data.Bool("pauseOnCutscene");
            PauseFlag = data.Attr("pauseFlag");
            HasPauseFlag = !PauseFlag.Equals("");

            playerDead = false;
            Moving = true;
            Collider = new ColliderList(new Collider[]
            {
                new Circle(6f, 0f, 0f),
            });
            Add(new PlayerCollider(new Action<Player>(OnPlayer)));
            Path = new Vector2[data.Nodes.GetLength(0) + 1];
            Path[0] = data.Position + offset;
            for (int i = 0; i < data.Nodes.GetLength(0); i++) {
                Path[i + 1] = data.Nodes[i] + offset;
            }

            MoveTime = data.Float("moveTime", 0.4f);
            PauseTime = data.Float("pauseTime", 0.2f);
            Angle = (Path[1] - Path[0]).Angle();
            Percent = 0f;
            CurrentStart = 0;
            UpdatePosition();
        }
        public virtual void OnPlayer(Player player) {

            playerDead = player.Die((player.Position - Position).SafeNormalize()) != null;
            if (playerDead) {
                Moving = false;
            }
        }

        public override void Added(Scene scene) {
            base.Added(scene);
            if (HasPauseFlag) {
                SceneAs<Level>().Session.SetFlag(PauseFlag, false);
            }
        }
        public override void Awake(Scene scene) {
            base.Awake(scene);
            OnTrackStart();
        }

        public void UpdatePosition() {
            Vector2 start = Path[CurrentStart];
            Vector2 end = Path[(CurrentStart + 1) % Path.Length];
            Position = Vector2.Lerp(start, end, Ease.SineInOut(Percent));
        }

        public override void Update() {
            base.Update();

            bool cutsceneRunning = false;
            if (PauseOnCutscene) {
                List<CutsceneEntity> cutScene = SceneAs<Level>().Entities.FindAll<CutsceneEntity>();
                foreach (CutsceneEntity element in cutScene) {
                    if (element.Running) {
                        cutsceneRunning = true;
                    }
                }
            }

            bool pauseFlag = false;
            if (HasPauseFlag) {
                pauseFlag = SceneAs<Level>().Session.GetFlag(PauseFlag);
            }

            Moving = !cutsceneRunning && !pauseFlag;

            if (Moving && !playerDead) {
                bool stillPaused = PauseTimer > 0f;
                if (stillPaused) {
                    PauseTimer -= Engine.DeltaTime;
                    bool isUnpaused = PauseTimer <= 0f;
                    if (isUnpaused) {
                        OnTrackStart();
                    }
                } else {
                    Percent = Calc.Approach(Percent, 1f, Engine.DeltaTime / MoveTime);
                    UpdatePosition();
                    bool reachedDestination = Percent >= 1f;
                    if (reachedDestination) {
                        CurrentStart = (CurrentStart + 1) % Path.Length;
                        PauseTimer = PauseTime;
                        Percent = 0f;
                        OnTrackEnd();
                    }
                }
            }
        }

        public virtual void OnTrackStart() {
        }

        public virtual void OnTrackEnd() {
            Angle = (Path[(CurrentStart + 1) % Path.Length] - Path[CurrentStart]).Angle();
        }
    }
}
