shader_type canvas_item;

uniform vec4 mix_colour : source_color;
uniform float colour_mix_strength;
uniform vec2 blur_radius = vec2(1.0, 1.0);
uniform float blur_strength;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

void fragment() {
    vec4 blur_sum = texture(TEXTURE, UV) +
                    texture(TEXTURE, UV + vec2(blur_radius.x, 0.0) * blur_strength) +
                    texture(TEXTURE, UV - vec2(blur_radius.x, 0.0) * blur_strength) +
                    texture(TEXTURE, UV + vec2(0.0, blur_radius.y) * blur_strength) +
                    texture(TEXTURE, UV - vec2(0.0, blur_radius.y) * blur_strength);

    vec4 blurred_colour = blur_sum / 5.0;

	COLOR = mix(blurred_colour, vec4(mix_colour.r, mix_colour.g, mix_colour.b, min(blurred_colour.a, mix_colour.a)), colour_mix_strength);
	//COLOR *= texture(SCREEN_TEXTURE, SCREEN_UV);
}
