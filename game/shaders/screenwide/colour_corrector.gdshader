shader_type canvas_item;

uniform float blur_strength: hint_range(0.0, 5) = 0.0;
uniform vec4 mix_col: source_color;
uniform float mix_strength;
uniform vec4 add_col: source_color;
uniform vec4 modulate: source_color;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

void fragment(){
	vec4 color = texture(SCREEN_TEXTURE, SCREEN_UV, blur_strength);
	color = mix(color, mix_col, mix_strength);
	color = vec4(color.rgb + (add_col.rgb * add_col.a), color.a);
	COLOR = color.rgba * modulate.rgba;
}