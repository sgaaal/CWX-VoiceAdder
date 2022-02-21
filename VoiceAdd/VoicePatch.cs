using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VoiceAdd
{
    public class VoicePatch : ModulePatch
    {
        private static Type _targetType;

        public VoicePatch()
        {
            _targetType = PatchConstants.EftTypes.Single(IsTargetType);
        }

        private bool IsTargetType(Type type)
        {
            return type.GetMethod("TakePhrasePath") != null;
        }

        protected override MethodBase GetTargetMethod()
        {
            return _targetType.GetMethod("TakePhrasePath");
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            var result = Traverse.Create(_targetType).Field<Dictionary<string, string>>("dictionary_0").Value;

            if(!result.ContainsKey("Monolith_3"))
            {
                result.Add("Monolith_3", "assets/content/audio/phrases/monolith_3_voice.bundle");
            }
        }
    }
}
