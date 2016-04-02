matrix View;
matrix Projection;

matrix rotationXMatrix;
matrix rotationYMatrix;
matrix rotationZMatrix;
matrix scalingMatrix;
matrix translationLocalMatrix;
matrix translationWorldMatrix;

texture Texture;

struct VS_IN
{
	float4 pos : POSITION;
	float2 tex : TEXTURE;
	float3 normal : NORMAL;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;

	output.pos = input.pos;
	
	output.pos = mul(input.pos, rotationXMatrix);
	output.pos = mul(input.pos, rotationYMatrix);
	output.pos = mul(input.pos, rotationZMatrix);

	output.pos = mul(input.pos, scalingMatrix);
	
	output.pos = mul(input.pos, translationLocalMatrix);
	output.pos = mul(input.pos, translationWorldMatrix);

	output.pos = mul(input.pos, View);
	output.pos = mul(input.pos, Projection);

	output.col = float4(1, 1, 1, 1);//(sin(input.pos) + float4(1, 1, 1, 1)) / 2;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return input.col;
}

technique10 Render
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}