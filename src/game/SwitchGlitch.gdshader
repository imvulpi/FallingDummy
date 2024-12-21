shader_type canvas_item;

uniform float glitch_strength : hint_range(0.0, 1.0); // Overall glitch intensity
uniform float glitch_speed = 20.0;                    // Speed of the glitch effect
uniform sampler2D SCREEN_TEXTURE: hint_screen_texture, filter_linear_mipmap;

float random(vec2 uv) {
    return fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453);
}

void fragment() {
    vec2 uv = SCREEN_UV;
    float time = TIME * glitch_speed;

    // 1. Random horizontal UV glitches
    float glitch_line = random(vec2(floor(uv.y * 10.0), floor(time)));
    if (glitch_line > 0.4) {
        uv.x += (glitch_line - 0.5) * glitch_strength * 0.4; // Big jumps
    }

    // 2. Vertical tearing at random intervals
    if (random(vec2(floor(time), 0.0)) > 0.8) {
        uv.y += sin(time * 20.0) * glitch_strength * 0.05;
    }

    // 3. Random color flashes
    vec3 color_shift = vec3(0.0);
    if (random(vec2(floor(time), floor(uv.y * 10.0))) > 0.85) {
        color_shift = vec3(random(vec2(time, uv.y)), random(uv * 3.0), random(uv + time));
    }

    // 4. RGB split (chromatic aberration)
    float r = texture(SCREEN_TEXTURE, uv + vec2(0.01 * glitch_strength, 0)).r;
    float g = texture(SCREEN_TEXTURE, uv).g;
    float b = texture(SCREEN_TEXTURE, uv - vec2(0.01 * glitch_strength, 0)).b;

    vec3 final_color = vec3(r, g, b) + color_shift * glitch_strength * 0.5;

    // Output final color with intensity clamping
    COLOR = vec4(clamp(final_color, 0.0, 1.0), 1.0);
}
