using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;
using System.IO;

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

    internal class AnimationHolder : ResourceHolder<Enum, Animation, int>
    {
        public override void Load(Enum id, string filebase)
        {
            List<Texture> textures = new List<Texture>();

            // Create and load resource
            for(int i = 0; ;i++)
            {
                string filename = filebase.Replace("*", i.ToString());
                if (!File.Exists(filename)) break;
                var texture = new Texture(filename);
                textures.Add(texture);
            }


            if (textures.Count == 0)
            {
                throw new SFML.LoadingFailedException("Animation frames count cannot be zero");
            }

            Sprite[] sprites = new Sprite[textures.Count];
            for(int i = 0; i < textures.Count; i++)
            {
                sprites[i] = new Sprite(textures[i]);
            }

            Animation animation = new Animation(sprites, 1);

            // If loading successful, insert resource to map
            InsertResource(id, animation);
        }

        public override void Load(Enum id, string filebase, int secondParameter)
        {
            List<Texture> textures = new List<Texture>();

            // Create and load resource
            for (int i = 0; ; i++)
            {
                string filename = filebase.Replace("*", i.ToString());
                if (!File.Exists(filename)) break;
                var texture = new Texture(filename);
                textures.Add(texture);
            }

            if(textures.Count == 0)
            {
                throw new SFML.LoadingFailedException("Animation frames count cannot be zero");
            }

            Sprite[] sprites = new Sprite[textures.Count];
            for (int i = 0; i < textures.Count; i++)
            {
                sprites[i] = new Sprite(textures[i]);
            }

            Animation animation = new Animation(sprites, secondParameter);

            // If loading successful, insert resource to map
            InsertResource(id, animation);
        }
    }

    internal class TextureHolder : ResourceHolder<Enum, Texture, IntRect>
    {
        public override void Load(Enum id, string filename)
        {
            // Create and load resource
            var texture = new Texture(filename);

            // If loading successful, insert resource to map
            InsertResource(id, texture);
        }

        public override void Load(Enum id, string filename, IntRect secondParameter)
        {
            // Create and load resource
            var texture = new Texture(filename, secondParameter);

            // If loading successful, insert resource to map
            InsertResource(id, texture);
        }
    }

    internal class SoundHolder : ResourceHolder<Enum, SoundBuffer, int>
    {
        public override void Load(Enum id, string filename)
        {
            // Create and load resource
            var buffer = new SoundBuffer(filename);

            // If loading successful, insert resource to map
            InsertResource(id, buffer);
        }

        public override void Load(Enum id, string filename, int secondParameter)
        {
            // SoundBuffer takes exactly one parameter
            throw new NotImplementedException();
        }
    }

    internal class FontHolder : ResourceHolder<Enum, Font, int>
    {
        public override void Load(Enum id, string filename)
        {
            // Create and load resource
            var font = new Font(filename);

            // If loading successful, insert resource to map
            InsertResource(id, font);
        }

        public override void Load(Enum id, string filename, int secondParameter)
        {
            // Font takes exactly one parameter
            throw new NotImplementedException();
        }
    }
}
