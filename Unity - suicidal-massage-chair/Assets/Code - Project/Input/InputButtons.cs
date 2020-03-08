using System;

namespace Input
{
    [Flags]
    public enum AllInputButtons
    {
        Yes = (1 << 0),
        No = (1 << 1),
        Kill = (1 << 2),
        ThumbUp = (1 << 3),
        ThumbDown = (1 << 4),
        One = (1 << 5),
        Two = (1 << 6),
        Three = (1 << 7),
        Four = (1 << 8),
        Five = (1 << 9),
        Custom1 = (1 << 10),
        Custom2 = (1 << 11),
        Custom3 = (1 << 12),
        Repeat = (1 << 13),
        Settings = (1 << 14),
        Dutch = (1 << 15),
        English = (1 << 16),
        Horn = (1 << 17),
        SpecialInputButton = (Kill | One | Two | Three | Four | Five | Repeat | Settings | Dutch | English | Horn),
        NormalButtons = (Yes | No | ThumbDown | ThumbUp | Custom1 | Custom2 | Custom3),
    }

    [Flags]
    public enum SpecialInputButtons
    {
        Kill = (1 << 0),
        One = (1 << 1),
        Two = (1 << 2),
        Three = (1 << 3),
        Four = (1 << 4),
        Five = (1 << 5),
        Repeat = (1 << 6),
        Settings = (1 << 7),
        Dutch = (1 << 8),
        English = (1 << 9),
        Horn = (1 << 10),
    }

    [Flags]
    public enum NormalInputButtons
    {
        Yes = (1 << 0),
        No = (1 << 1),
        ThumbUp = (1 << 2),
        ThumbDown = (1 << 3),
        Custom1 = (1 << 4),
        Custom2 = (1 << 5),
        Custom3 = (1 << 6),
    }
}