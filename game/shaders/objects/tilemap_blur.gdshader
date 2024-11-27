shader_type canvas_item;

//uniform vec4 mix_colour : source_color;
//uniform float colour_mix_strength;
uniform vec2 blur_radius = vec2(1.0, 1.0);
uniform float blur_strength = 0.1;
uniform int samples;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

void fragment() {
    // Calculate the blurred mask
	vec4 textu = texture(TEXTURE, UV);
	vec4 blur_sum = vec4(0.0);
	float total = 0.0;
	for (int i = -samples; i <= samples; i++) {
	    for (int j = -samples; j <= samples; j++) {
            blur_sum += texture(SCREEN_TEXTURE, (SCREEN_UV) + vec2(float(i) * blur_radius.x, float(j) * blur_radius.y) * blur_strength);
			total ++;

	    }
	}

    vec4 blurred_colour = blur_sum / total;

    // Use the alpha of the blurred color as the mask for the darkening effect
    //vec4 mask = vec4(0.0, 0.0, 0.0, textu.a);

    // Combine the mask with the darkening color
    //vec4 darkened_background = mix_colour;// * mask.a;
	//blurred_colour// *= mask.a;

    // Mix the blurred color with the darkened background
	//COLOR = vec4(1.0, 1.0, 1.0, blurred_colour.r);
	COLOR = blurred_colour;
	//COLOR *= blurred_colour;
}
