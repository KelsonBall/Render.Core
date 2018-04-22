namespace Render.Core.Textures
{
    public class Animation
    {
        private Texture[] Frames;
        public int Frame { get; set; }
        public int Length { get; set; }
        public Texture Image => Frames[Frame];

        public Animation(Bitmap sheet, int sheetWidth, int sheetHeight, int sheetPadding)
        {

        }

        public void Step() => Frame = ++Frame % Length;
    }
}
