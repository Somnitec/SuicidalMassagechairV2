

public class UserInterfaceMicroControllerData
{

}

[System.Flags]
public enum UserInputButton
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
    Five = (1 << 8),
    Custom1 = (1 << 10),
    Custom2 = (1 << 11),
    Custom3 = (1 << 12),
    Repeat = (1 << 13),
    Settings = (1 << 14),
    Dutch = (1 << 15),
    English = (1 << 16),
    Horn = (1 << 17),
    Positive = (Yes | ThumbUp),
    AnyButton = ~(Dutch | English), // Not dutch or english
}
