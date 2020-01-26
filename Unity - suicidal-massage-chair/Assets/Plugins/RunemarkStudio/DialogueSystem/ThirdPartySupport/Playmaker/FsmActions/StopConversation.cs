#if PLAYMAKER
namespace Runemark.DialogueSystem.Playmaker
{
    using HutongGames.PlayMaker;

    [HelpUrl("http://runemarkstudio.com/dialogue-system-documentation/#playmaker")]
    [ActionCategory("Runemark Dialogue System")]
    [Tooltip("Stops the conversation on the Actor")]
    public class StopConversation : FsmStateAction
    {
        [CheckForComponent(typeof(DialogueBehaviour))]
        [Tooltip("The actor with a DialogueBehaviour on it.")]
        public FsmOwnerDefault Actor;

        public override void Reset()
        {
            Actor = new FsmOwnerDefault();
            Actor.OwnerOption = OwnerDefaultOption.UseOwner;            
        }
               
        
        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(Actor);
            DialogueBehaviour b = go.GetComponent<DialogueBehaviour>();
            b.StopDialogue(b.Conversation);

            Finish();
        }
    }
}
#endif