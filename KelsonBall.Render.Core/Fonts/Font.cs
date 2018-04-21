using OpenTK.Graphics;
using Render.Core.Extensions;
using Render.Core.Textures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TrueTypeSharp;


namespace Render.Core
{
    public class Font
    {
        public enum Options
        {
            Arial,
            AnonymousPro,
            TimesNewRoman,
        }

        private readonly FontGenerator _generator;

        private Dictionary<char, Dictionary<Color4, (rImage texture, int kern, int yshift)>> _cache = new Dictionary<char, Dictionary<Color4, (rImage texture, int kern, int yshift)>>();

        private readonly int _size;

        public int MaxHeight { get; private set; }
        public int MaxOffset { get; private set; }

        public Font(Options font, int size)
        {
            Func<Stream> source = null;
            switch (font)
            {
                case Options.Arial:
                    source = ResourceManager.ResourceGetter("Core.FontFiles.Arial.ttf");
                    break;
                case Options.AnonymousPro:
                    source = resourceGetter("Core.FontFiles.AnonymousPro.ttf");
                    break;
                case Options.TimesNewRoman:
                    source = resourceGetter("Core.FontFiles.TimesNewRoman.ttf");
                    break;
            }


            using (var stream = source())
                _generator = new FontGenerator(new TrueTypeFont(stream));
            _size = size;
            Load();
        }

        public Font(string filename, int size)
            : this (() => File.OpenRead(filename), size)
        {

        }

        public Font(Func<Stream> source, int size)
        {
            using (var stream = source())
                _generator = new FontGenerator(new TrueTypeFont(stream));
            _size = size;
            Load();
        }

        private void Load()
        {
            Color4 color = Color4.Black;

            int heightMax = 0;
            int offsetMax = 0;
            foreach (var letter in AlphabetProvider.Enumerate())
            {
                var current = this[letter, color];
                if (current.texture.Height > heightMax)
                    heightMax = current.texture.Height;
                if (current.yshift > offsetMax)
                    offsetMax = current.yshift;
                current = this[letter.ToString().ToUpper().Single(), color];
                if (current.texture.Height > heightMax)
                    heightMax = current.texture.Height;
                if (current.yshift > offsetMax)
                    offsetMax = current.yshift;
            }
            MaxHeight = heightMax;
            MaxOffset = offsetMax;
        }

        private static readonly Assembly renderAssembly = typeof(Font).Assembly;
        private static readonly string[] ResourceNames = renderAssembly.GetManifestResourceNames().ToArray();
        private Func<Stream> resourceGetter(string resource) => () =>
            renderAssembly.GetManifestResourceStream(ResourceNames.Single(r => r.EndsWith(resource)));

        public (rImage texture, int kern, int yshift) this[char c, Color4 color]
        {
            get
            {
                if (!_cache.ContainsKey(c))
                    _cache[c] = new Dictionary<Color4, (rImage texture, int kern, int yshift)>();
                if (!_cache[c].ContainsKey(color))
                    _cache[c][color] = _generator[c, _size, color];
                return _cache[c][color];
            }
        }
    }
}
