using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Celeste.Mod.AdventureHelper.Entities {
    public static class ZipMoverSoundController {
        public static Dictionary<string, SoundSource> activeSounds = new();

        public static void PlaySound(string colorCode, SoundType type, Solid block) {
            string name = $"{colorCode}-{type}";
            if (!activeSounds.ContainsKey(name)) {
                Player player = block.Scene.Tracker.GetEntity<Player>();
                if (player != null) {
                    Vector2 position = player.Position;
                    SoundSource source = new() {
                        Position = position
                    };
                    activeSounds.Add(name, source);
                    source.Play("event:/game/01_forsaken_city/zip_mover");
                }
            } else if (type == SoundType.Returning) {
                SoundSource source = activeSounds[name];
                if (!source.Playing) {
                    source.Play("event:/game/01_forsaken_city/zip_mover");
                }
            }
        }

        public static void StopSound(string colorCode, SoundType type) {
            string name = $"{colorCode}-{type}";
            if (activeSounds.ContainsKey(name)) {
                SoundSource source = activeSounds[name];
                activeSounds.Remove(name);
                source.Stop();
            }
        }

        public enum SoundType {
            Returning,
            NonReturning
        }
    }
}
