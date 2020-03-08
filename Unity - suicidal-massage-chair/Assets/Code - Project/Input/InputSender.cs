using Framework;
using UnityEngine;

namespace Input
{
    public static class InputSender
    {
        public static void Send(AllInputButtons allInputButtons)
        {
            Debug.Log($"Sending All Input {allInputButtons.ToString()}");
            Events.Instance.Raise(new AllInput(allInputButtons));
            
            SpecialInputButtons? optionalApecialInputButtons = InputMapper.SpecialInput(allInputButtons);
            NormalInputButtons? optionalNormalInputButtons = InputMapper.NormalInput(allInputButtons);

            if (optionalApecialInputButtons is SpecialInputButtons specialInputButtons)
            {
                Debug.Log($"Sending Special Input {specialInputButtons.ToString()}");
                Events.Instance.Raise(new SpecialInput(specialInputButtons));
            }

            if (optionalNormalInputButtons is NormalInputButtons normalInputButtons)
            {
                Debug.Log($"Sending Normal Input {normalInputButtons.ToString()}");
                Events.Instance.Raise(new NormalInput(normalInputButtons));
            }
        }
    }
}