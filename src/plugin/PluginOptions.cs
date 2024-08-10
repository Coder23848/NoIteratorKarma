/*
using Menu.Remix.MixedUI;
using UnityEngine;

namespace NoIteratorKarma
{
    public class PluginOptions : OptionInterface
    {
        public static PluginOptions Instance = new();

        public static Configurable<bool> RaiseOneStageInstead = Instance.config.Bind("RaiseOneStageInstead", true, new ConfigurableInfo("Instead of entirely preventing iterators from raising your karma, have them raise it one stage like in the Hunter's campaign."));

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];

            Tabs[0] = new(Instance, "Options");
            CheckBoxOption(RaiseOneStageInstead, 0, "Raise One Stage");
        }

        private void CheckBoxOption(Configurable<bool> setting, float pos, string label)
        {
            Tabs[0].AddItems(new OpCheckBox(setting, new(50, 550 - pos * 30)) { description = setting.info.description }, new OpLabel(new Vector2(90, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
    }
}
*/