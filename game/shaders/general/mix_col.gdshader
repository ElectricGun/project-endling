shader_type canvas_item;

uniform vec4 add_colour : source_color;
uniform float strength;
uniform float blur_strength: hint_range(0.0, 5) = 0.0;

void fragment() {
	vec4 input_colour = texture(TEXTURE, UV);
	COLOR = mix(input_colour, vec4(add_colour.r, add_colour.g, add_colour.b, min(input_colour.a, add_colour.a)), strength);

}
