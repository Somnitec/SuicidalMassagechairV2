namespace Input
{
    public static class InputMapper
    {
        public static NormalInputButtons? NormalInput(AllInputButtons allInput)
        {
            switch (allInput)
            {
                case AllInputButtons.Yes:
                    return NormalInputButtons.Yes;
                case AllInputButtons.No:
                    return NormalInputButtons.No;
                case AllInputButtons.Custom1:
                    return NormalInputButtons.Custom1;
                case AllInputButtons.Custom2:
                    return NormalInputButtons.Custom2;
                case AllInputButtons.Custom3:
                    return NormalInputButtons.Custom3;
                case AllInputButtons.ThumbUp:
                    return NormalInputButtons.ThumbUp;
                case AllInputButtons.ThumbDown:
                    return NormalInputButtons.ThumbDown;
                default:
                case AllInputButtons.Kill:
                case AllInputButtons.One:
                case AllInputButtons.Two:
                case AllInputButtons.Three:
                case AllInputButtons.Four:
                case AllInputButtons.Repeat:
                case AllInputButtons.Settings:
                case AllInputButtons.Dutch:
                case AllInputButtons.English:
                case AllInputButtons.Horn:
                    return null;
            }
        }

        public static SpecialInputButtons? SpecialInput(AllInputButtons allInput)
        {
            switch (allInput)
            {
                case AllInputButtons.Kill:
                    return SpecialInputButtons.Kill;
                case AllInputButtons.One:
                    return SpecialInputButtons.One;
                case AllInputButtons.Two:
                    return SpecialInputButtons.Two;
                case AllInputButtons.Three:
                    return SpecialInputButtons.Three;
                case AllInputButtons.Four:
                    return SpecialInputButtons.Four;
                case AllInputButtons.Five:
                    return SpecialInputButtons.Five;
                case AllInputButtons.Repeat:
                    return SpecialInputButtons.Repeat;
                case AllInputButtons.Settings:
                    return SpecialInputButtons.Settings;
                case AllInputButtons.Dutch:
                    return SpecialInputButtons.Dutch;
                case AllInputButtons.English:
                    return SpecialInputButtons.English;
                case AllInputButtons.Horn: 
                    return SpecialInputButtons.Horn;
                default:
                case AllInputButtons.Yes:
                case AllInputButtons.No:
                case AllInputButtons.Custom1:
                case AllInputButtons.Custom2:
                case AllInputButtons.Custom3:
                case AllInputButtons.ThumbUp:
                case AllInputButtons.ThumbDown:
                    return null;
            }
        }
    }
}