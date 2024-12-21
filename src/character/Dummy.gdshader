shader_type canvas_item;

uniform float wiggle_strength = 0.05; // Strength of the wiggle
uniform float wiggle_speed = 5.0;     // Speed of the wiggle

void fragment() {
    vec2 uv = UV;
    
    // Add sine-based oscillation for wiggle effect
    uv.x += sin(TIME * wiggle_speed + uv.y * 10.0) * wiggle_strength;
    uv.y += cos(TIME * wiggle_speed + uv.x * 10.0) * wiggle_strength;
    
    // Sample the texture with modified UVs
    COLOR = texture(TEXTURE, uv);
}