﻿using MudBlazor;

namespace DeVesen.Bazaar.Client.Shared
{
    public static class MainTheme
    {
        public static MudTheme GeTheme()
            => new()
            {
                // PaletteLight = new PaletteLight
                // {
                //     HoverOpacity = 0.06,

                //     Primary = "#CC0000",
                //     PrimaryDarken = "#A60000",
                //     PrimaryLighten = "#D30000",
                //     PrimaryContrastText = new MudColor("#FFFFFF"),

                //     TextPrimary = new MudColor("#222222"),
                //     TextSecondary = new MudColor("#666666"),
                //     TextDisabled = new MudColor("#B3B3B3"),

                //     ActionDefault = new MudColor("#F5F5F5"),
                //     ActionDisabled = new MudColor("#B3B3B3"),
                //     ActionDisabledBackground = new MudColor("#E5E5E5"),

                //     Dark = "#222222",

                //     Black = "#000000",
                //     White = "#FFFFFF",
                //     OverlayLight = "#ffffff",
                //     OverlayDark = "#222222",

                //     GrayDefault = "#B3B3B3",
                //     GrayDark = "#999999",
                //     GrayDarker = "#666666",
                //     GrayLight = "#D9D9D9",
                //     GrayLighter = "#F5F5F5",

                //     Info = "#0093DD",
                //     InfoDarken = "#007DBC",
                //     InfoLighten = "#04A3F3",
                //     InfoContrastText = "#ffffff",

                //     Success = "#00883D",
                //     SuccessDarken = "#007836",
                //     SuccessLighten = "#01ab4d",
                //     SuccessContrastText = "#ffffff",

                //     Warning = "#EE7F00",
                //     WarningDarken = "#CA6C00",
                //     WarningLighten = "#FF8A03",
                //     WarningContrastText = "#ffffff",

                //     Error = "#D12328",
                //     ErrorDarken = "#D12328",
                //     ErrorLighten = "#D12328",
                //     ErrorContrastText = "#ffffff",

                //     Surface = "#ffffff",
                //     Background = "#ffffff",
                //     BackgroundGray = "#f5f5f5",

                //     DrawerBackground = "#ffffff",
                //     DrawerText = "#cc0000",
                //     DrawerIcon = "#cc0000",

                //     AppbarBackground = "#222222",
                //     AppbarText = "#ffffff",

                //     Divider = "#d9d9d9"
                // },
                LayoutProperties = new LayoutProperties
                {
                    DefaultBorderRadius = "0px",
                    AppbarHeight = "48px"
                },
                Typography = new Typography
                {
                    Default = new Default
                    {
                        FontFamily = new[]
                        {
                        "Cabin",
                        "Helvetica Neue",
                        "Helvetica",
                        "Arial",
                        "sans - serif"
                    }
                    },
                    H4 =
                {
                    FontWeight = 700,
                    LineHeight = 1.167,
                    FontSize = "24px"
                },
                    H5 =
                {
                    FontWeight = 700,
                    FontSize = "20px"
                },
                    H6 =
                {
                    FontWeight = 700,
                    FontSize = "16px"
                },
                    Body1 =
                {
                    FontWeight = 400,
                    FontSize = "16px"
                },
                    Body2 =
                {
                    FontWeight = 400,
                    FontSize = "14px"
                },
                    Subtitle1 =
                {
                    FontWeight = 700,
                    FontSize = "14px"
                },
                    Subtitle2 =
                {
                    FontWeight = 400,
                    FontSize = "14px"
                },
                    Caption =
                {
                    FontWeight = 400,
                    FontSize = "12px"
                },
                    Button =
                {
                    FontWeight = 400,
                    FontSize = "20px",
                    TextTransform = "none"
                }
                }
            };
    }
}
