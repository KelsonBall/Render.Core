using OpenTK.Graphics;

namespace Render.Core.Rendering
{
    public class Style
    {
        private static Font defaultFont = null;

        public Font Font { get; set; }
        public int FontSize { get; set; } = 14;
        public Color Fill { get; set; } = Color.Constants.White;
        public Color Stroke { get; set; } = Color.Constants.Black;
        public float StrokeWeight { get; set; } = 1;

        public Style()
        {
            if (defaultFont == null)
                defaultFont = new Font(Font.Options.Arial, 14);
            Font = defaultFont;
        }

        public Style Copy()
        {
            return new Style
            {
                Font = Font,
                Fill = Fill,
                Stroke = Stroke,
                StrokeWeight = StrokeWeight
            };
        }
    }
}
