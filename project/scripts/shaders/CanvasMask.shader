shader_type canvas_item;

uniform sampler2D mask_texture;

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	vec4 mask_color = texture(mask_texture, vec2(UV.x, UV.y));
	color.a *= mask_color.a;
	COLOR = color;
}