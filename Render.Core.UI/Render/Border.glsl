uniform vec3 BorderColor;
uniform vec3 BackgroundColor;
uniform vec4 BorderThickness;

uniform vec2 Origin;
uniform vec2 Size;
uniform vec2 Resolution;

out vec4 outColor;

void main()
{    
	vec2 pos = gl_FragCoord.xy - Origin;
	if (pos.x <= BorderThickness.x || pos.x >= Size.x - BorderThickness.x || pos.y <= BorderThickness.y || pos.y >= Size.y - BorderThickness.y)
	{
		outColor = vec4(BorderColor, 1.0);
	}
	else
	{
		outColor = vec4(BackgroundColor, 1.0);
	}
}