using Godot;
using System;

namespace FallingDummy.src.commons.animations
{
    public interface IAnimation
    {
        public event EventHandler AnimationEnd;
        public IAnimation Setup(Node node);
        public void Update(double delta);
        public void Stop(bool shouldReset = false);
        public void Start(bool shouldReset = false);
        public IAnimation Reset();
    }
}
