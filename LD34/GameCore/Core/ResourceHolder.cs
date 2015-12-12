using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;

namespace GameCore.Core
{
    internal abstract class ResourceHolder<TIdentifier, TResource, TParameter>
    {
        private Dictionary<TIdentifier, TResource> resourceMap = new Dictionary<TIdentifier, TResource>();

        public abstract void Load(TIdentifier id, string filename);
        public abstract void Load(TIdentifier id, string filename, TParameter secondParameter);

        public TResource Get(TIdentifier id)
        {
            return resourceMap[id];
        }

        protected void InsertResource(TIdentifier id, TResource resource)
        {
            resourceMap.Add(id, resource);
        }
    }

    internal class TextureHolder : ResourceHolder<Textures.ID, Texture, IntRect>
    {
        public override void Load(Textures.ID id, string filename)
        {
            // Create and load resource
            var texture = new Texture(filename);

            // If loading successful, insert resource to map
            InsertResource(id, texture);
        }

        public override void Load(Textures.ID id, string filename, IntRect secondParameter)
        {
            // Create and load resource
            var texture = new Texture(filename, secondParameter);

            // If loading successful, insert resource to map
            InsertResource(id, texture);
        }
    }

    internal class SoundHolder : ResourceHolder<Sounds.ID, SoundBuffer, int>
    {
        public override void Load(Sounds.ID id, string filename)
        {
            // Create and load resource
            var buffer = new SoundBuffer(filename);

            // If loading successful, insert resource to map
            InsertResource(id, buffer);
        }

        public override void Load(Sounds.ID id, string filename, int secondParameter)
        {
            // SoundBuffer takes exactly one parameter
            throw new NotImplementedException();
        }
    }

    internal class FontHolder : ResourceHolder<Fonts.ID, Font, int>
    {
        public override void Load(Fonts.ID id, string filename)
        {
            // Create and load resource
            var font = new Font(filename);

            // If loading successful, insert resource to map
            InsertResource(id, font);
        }

        public override void Load(Fonts.ID id, string filename, int secondParameter)
        {
            // Font takes exactly one parameter
            throw new NotImplementedException();
        }
    }
}
