using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreCyclopsColors
{
    [Menu("More Cyclops Colors")]
    public class ModOptionssaving : ConfigFile
    {
        [ColorPicker(LabelLanguageId = "EmissionColorPicker",TooltipLanguageId = "EmissionColorPickerHover")]
        public Color colorValue = new Color(1, 1, 1, 1);

        [Toggle(LabelLanguageId = "EnableSpecColoring", TooltipLanguageId = "EnableSpecColoringHover")]
        public bool EnableSpecColoring = false;

        [Slider(LabelLanguageId = "SpecBrightnessPicker", DefaultValue = 4f, Max = 100f, Min = -100f, TooltipLanguageId = "SpecBrightnessPickerHover")]
        public float Brightness = 4f;
    }
}
