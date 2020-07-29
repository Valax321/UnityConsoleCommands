using UnityEngine;

namespace Valax321.ConsoleCommands
{
    /// <summary>
    /// Built-in commands used at runtime.
    /// </summary>
    internal static class RuntimeCommands
    {
        #region Rendering Commands
        
        static ConsoleEnumCallbackVariable<AnisotropicFiltering> s_anisotropicFiltering = 
            new ConsoleEnumCallbackVariable<AnisotropicFiltering>(
                "render_anisotropic_filtering", 
                AnisotropicFiltering.Enable, 
                filtering => QualitySettings.anisotropicFiltering = filtering, 
                "Global anisotropic filtering mode."
                );
        
        static ConsoleCallbackVariable<int> s_antialiasing =
            new ConsoleCallbackVariable<int>(
                "render_antialiasing",
                1,
                antialiasing => QualitySettings.antiAliasing = antialiasing,
                "Anti-aliasing value indicates the number of samples per pixel."
                );
        
        static ConsoleCallbackVariable<float> s_lodBias =
            new ConsoleCallbackVariable<float>(
                "render_lod_bias",
                1.0f,
                lod => QualitySettings.lodBias = lod,
                "Global multiplier for the LOD's switching distance."
                );
        
        static ConsoleCallbackVariable<float> s_shadowDistance =
            new ConsoleCallbackVariable<float>(
                "render_shadow_distance",
                150,
                dist => QualitySettings.shadowDistance = dist,
                "Shadow drawing distance."
                );
        
        static ConsoleEnumCallbackVariable<ShadowmaskMode> s_shadowMaskMode =
            new ConsoleEnumCallbackVariable<ShadowmaskMode>(
                "render_shadowmask_mode",
                ShadowmaskMode.DistanceShadowmask,
                mode => QualitySettings.shadowmaskMode = mode,
                "Set whether static shadow casters should be rendered into realtime shadow maps."
                );
        
        static ConsoleEnumCallbackVariable<ShadowResolution> s_shadowResolution =
            new ConsoleEnumCallbackVariable<ShadowResolution>(
                "render_shadow_resolution",
                ShadowResolution.High,
                res => QualitySettings.shadowResolution = res,
                "The default resolution of the shadow maps."
                );
        
        static ConsoleEnumCallbackVariable<ShadowQuality> s_shadowQuality =
            new ConsoleEnumCallbackVariable<ShadowQuality>(
                "render_shadow_quality",
                ShadowQuality.All,
                quality => QualitySettings.shadows = quality,
                "Realtime Shadows type to be used."
                );
        
        static ConsoleCallbackVariable<int> s_vsyncCount =
            new ConsoleCallbackVariable<int>(
                "render_vsync_count",
                1,
                vsync => QualitySettings.vSyncCount = vsync,
                "The number of VSyncs that should pass between each frame. Use 'Don't Sync' (0) to not wait for VSync. Value must be 0, 1, 2, 3, or 4."
                );
        
        #endregion
    }
}