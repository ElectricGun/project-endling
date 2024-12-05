shader_type canvas_item;

uniform vec4 first_color : source_color;
uniform vec4 second_color : source_color;
uniform float mix_amount;
uniform float x_amount;
uniform float y_amount;
uniform float position : hint_range(-2, 2) = 0.5;
uniform int stepIntensify : hint_range(1, 100) = 10;

void fragment() {
	float pos = round(smoothstep(0,1,(position + UV.x * x_amount + UV.y * y_amount) / 2.0) * float(stepIntensify));
	vec4 gradient = mix(first_color,second_color,pos/float(stepIntensify));
	//input = vec4(input.rgb * (gradient.r * 2.0), input.a);
	vec4 old = COLOR;
	COLOR = mix(old, gradient, mix_amount);
}