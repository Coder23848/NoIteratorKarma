using BepInEx;
using UnityEngine;

namespace NoIteratorKarma
{
    [BepInPlugin("com.coder23848.noiteratorkarma", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("lb-fgf-m4r-ik.chatoyant-waterfalls-but-real", BepInDependency.DependencyFlags.SoftDependency)] // since Chasing Wind also raises the player's karma, this mod's hook needs to run before theirs
    public class Plugin : BaseUnityPlugin
    {
#pragma warning disable IDE0051 // Visual Studio is whiny
        private void OnEnable()
#pragma warning restore IDE0051
        {
            // Plugin startup logic
            //On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.SSOracleBehavior.Update += SSOracleBehavior_Update;
        }

        private void SSOracleBehavior_Update(On.SSOracleBehavior.orig_Update orig, SSOracleBehavior self, bool eu)
        {
            DeathPersistentSaveData dpsd = (self.oracle.room.game.session as StoryGameSession).saveState.deathPersistentSaveData;
            int prevKarmaCap = dpsd.karmaCap; // save the player's karma cap

            orig(self, eu);

            if (dpsd.karmaCap > prevKarmaCap)
            {
                dpsd.karmaCap = prevKarmaCap; // undo any raises to the player's karma cap (if it's somehow lowered, there's probably a cool modded storyline going on, and it would be a bad idea to interfere)
            }
            if (dpsd.karma > dpsd.karmaCap)
            {
                dpsd.karma = dpsd.karmaCap; // prevent having more than maximum karma (the player will still get their karma raised to their maximum in cases where they would normally have the cap increased)
            }

            for (int i = 0; i < self.oracle.room.game.cameras.Length; i++)
            {
                if (self.oracle.room.game.cameras[i].hud.karmaMeter != null)
                {
                    self.oracle.room.game.cameras[i].hud.karmaMeter.UpdateGraphic(); // update the karma listed on the HUD (it'll be updated to the wrong value otherwise)
                }
            }
        }

        /*
        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            Debug.Log("No Iterator Karma config setup: " + MachineConnector.SetRegisteredOI(PluginInfo.PLUGIN_GUID, PluginOptions.Instance));
        }
        */
    }
}