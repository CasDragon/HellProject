using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.AreaLogic.Cutscenes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Utility;
using System.Linq;

namespace HellProject
{
    internal class StuffAndThings
    {
        public static void Configure()
        {
            ProgressionConfigurator devilprogression = ProgressionConfigurator.For(ProgressionRefs.DevilProgression);
            Main.log.Log("Patching for Aeon gazes");
            AeonGaze(devilprogression);
            Main.log.Log("Patching for Hells Authority");
            HellsAuthority(devilprogression);
            devilprogression.Configure();
        }
        public static void AeonGaze(ProgressionConfigurator progression)
        {
            progression.RemoveComponents(c => c is RemoveFeatureOnApply);
            LevelEntry[] entries = LevelEntryBuilder.New()
                    .AddEntry(0, FeatureRefs.AeonGazeFeatureEndless.Reference.Get())
                    .AddEntry(0, FeatureRefs.AeonGazeFeature.Reference.Get())
                    .GetEntries();
            progression.AddToLevelEntries(entries);
            progression.AddRemoveFeatureOnApply(FeatureRefs.DragonAzataCompanionFeature.Reference.Get());
            CommandAction aeontodevil = BlueprintTool.Get<CommandAction>("301e51edf1251a9489244d37421d89fb");
            var test = aeontodevil.Action.Actions.ToList();
            test.Remove(a => a is Conditional);
            aeontodevil.Action.Actions = test.ToArray();
        }
        public static void HellsAuthority(ProgressionConfigurator progression)
        {
            progression.RemoveFromLevelEntries(3, FeatureRefs.HellsPowersFeature.Reference.Get());
            progression.AddToLevelEntry(0, FeatureRefs.HellsPowersFeature.Reference.Get());
        }
    }
}
