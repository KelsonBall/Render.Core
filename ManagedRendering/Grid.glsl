uniform vec4 GridColor;
uniform float Width;
uniform float Thickness;
uniform vec2 Origin;
uniform vec2 Size;
uniform vec2 Resolution;

out vec4 outColor;

void main()
{    
	vec2 pos = gl_FragCoord.xy - Origin;
    float mod_x = mod(pos.x, 20.0);
    float mod_y = mod(pos.y, 20.0);
    if (mod_x <= 1.0 || mod_y <= 1.0)
    {		
		outColor = GridColor;
    }
    else
    {                
        outColor = vec4(0.2, 0.2, 0.2, 1.0);
    }
}