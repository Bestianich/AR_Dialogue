using UnityEngine;
using UnityEngine.UI;


    [NodeWidth(300)]
    public class ChangeSpriteNode : ANode
    {
        [Input] public int DefaultInput;
        public string ImageMemoryReference;
        public Sprite Sprite;
        [Output] public int DefaultOutput;

        public override void Execute()
        {
            Image image = _dialogueMemory.Get(ImageMemoryReference) as Image;
            if (image == null)
            {
                Debug.LogError($"Reference {ImageMemoryReference} reference not found!!");
                return;
            }

            image.sprite = Sprite;
        }
    }