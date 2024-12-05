shader_type canvas_item;

uniform float strength;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture;
void fragment() {
	// Called for every pixel the material is visible on.
	vec4 input_colour = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0);
	vec4 gray_scale = mix(vec4(vec3((input_colour.r + input_colour.g + input_colour.b) / 3.0), input_colour.a), vec4(vec3(0.200 * input_colour.r + 0.300 * input_colour.g + 0.114 * input_colour.b), input_colour.a), 0.5);
	COLOR = mix(input_colour, gray_scale, strength);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
