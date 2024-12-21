using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingDummy.src.commons.animations
{
    public class GlitchAnimation : IAnimation
    {
        public GlitchAnimation(ColorRect rect, Node attachToNode) { 
            glitchRect = rect;
            AttachToNode = attachToNode;
        }

        public Node AttachToNode { get; set; }
        public ColorRect glitchRect { get; set; }
        public ShaderMaterial glitchMaterial = null;
        public float glitchStrength = 0;

        public bool animationEnabled = false;
        public event EventHandler AnimationEnd;

        public IAnimation Setup(Node node)
        {
            if (glitchMaterial == null)
            {
                Shader shader = ResourceLoader.Load<Shader>("res://src/game/SwitchGlitch.gdshader");

                ShaderMaterial material = new()
                {
                    Shader = shader
                };

                glitchMaterial = material;
            }

            glitchRect.Visible = true;
            glitchRect.Material = glitchMaterial;
            return this;
        }

        public void Update(double delta)
        {
            if (animationEnabled)
            {
                glitchStrength += (float)delta * 3;
                glitchMaterial.SetShaderParameter("glitch_strength", glitchStrength);
                if (glitchStrength >= 1)
                {
                    if (AttachToNode.GetNodeOrNull("animation_timer") == null)
                    {
                        Timer timer = new()
                        {
                            Name = "animation_timer",
                            Autostart = true,
                            WaitTime = 0.7
                        };

                        timer.Timeout += Timer_Timeout;
                        AttachToNode.AddChild(timer);
                    }
                }
            }
        }

        private void Timer_Timeout()
        {
            AnimationEnd.Invoke(this, new EventArgs());
        }

        public void Stop(bool shouldReset = false)
        {
            animationEnabled = false;

            if (AttachToNode != null) 
            {
                Timer timer = AttachToNode.GetNodeOrNull<Timer>("animation_timer");
                timer?.Stop();
                AttachToNode.RemoveChild(glitchRect);
            }

            if (shouldReset) Reset();
        }

        public void Start(bool shouldReset = false)
        {
            animationEnabled = true;

            if (AttachToNode != null)
            {
                Timer timer = AttachToNode.GetNodeOrNull<Timer>("animation_timer");
                timer?.Start();
                AttachToNode.AddChild(glitchRect);
            }

            if (shouldReset) Reset();
        }

        public IAnimation Reset()
        {
            glitchStrength = 0;
            if (AttachToNode != null)
            {
                Timer timer = AttachToNode.GetNodeOrNull<Timer>("animation_timer");
                if (timer != null)
                {
                    AttachToNode.RemoveChild(timer);
                }
                glitchRect.Owner = null;
            }
            return this;
        }
    }
}
