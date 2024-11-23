shader_type canvas_item;

uniform vec4 mix_colour : source_color;
uniform float colour_mix_strength;
uniform vec2 blur_radius = vec2(1.0, 1.0);
uniform float blur_strength;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

void fragment() {
    // Calculate the blurred mask
	vec4 textu = texture(TEXTURE, UV);
    vec4 blur_sum = texture(SCREEN_TEXTURE, SCREEN_UV) +
                    texture(SCREEN_TEXTURE, SCREEN_UV + vec2(blur_radius.x, 0.0) * blur_strength) +
                    texture(SCREEN_TEXTURE, SCREEN_UV - vec2(blur_radius.x, 0.0) * blur_strength) +
                    texture(SCREEN_TEXTURE, SCREEN_UV + vec2(0.0, blur_radius.y) * blur_strength) +
                    texture(SCREEN_TEXTURE, SCREEN_UV - vec2(0.0, blur_radius.y) * blur_strength);

    vec4 blurred_colour = blur_sum / 5.0;

    // Use the alpha of the blurred color as the mask for the darkening effect
    vec4 mask = vec4(0.0, 0.0, 0.0, textu.a);

    // Combine the mask with the darkening color
    vec4 darkened_background = mix_colour * mask.a;
	blurred_colour *= mask.a;

    // Mix the blurred color with the darkened background
    COLOR = mix(blurred_colour, darkened_background, colour_mix_strength);
	//COLOR = vec4(1.0, 1.0, 1.0, blurred_colour.r);
	//COLOR = blurred_colour;
	//COLOR *= blurred_colour;
}
