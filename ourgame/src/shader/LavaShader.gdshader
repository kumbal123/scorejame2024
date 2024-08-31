shader_type canvas_item;

uniform sampler2D noise: repeat_enable, filter_nearest;
uniform sampler2D noise2: repeat_enable;
uniform sampler2D gradient: repeat_enable;
uniform vec2 movement;
uniform vec2 movement2;

void fragment() {
	COLOR = texture(gradient, texture(noise, UV * 100.0f * TEXTURE_PIXEL_SIZE + TIME * movement * 0.2).rg
					+ texture(noise2, UV * 100.0f * TEXTURE_PIXEL_SIZE + TIME * movement2 * 0.2).rg);
	
}