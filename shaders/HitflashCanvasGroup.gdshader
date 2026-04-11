shader_type canvas_item;
render_mode unshaded;

uniform sampler2D screen_texture : hint_screen_texture, repeat_disable, filter_nearest;

uniform vec4 hitflash_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform bool hitflash_on = false;

void fragment() {
	vec4 original_color = textureLod(screen_texture, SCREEN_UV, 0.0);
	vec3 final_rgb = hitflash_on ? hitflash_color.rgb : original_color.rgb;
	COLOR = vec4(final_rgb, original_color.a);
}