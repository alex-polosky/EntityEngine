matrix View;
matrix Projection;

matrix rotationXMatrix;
matrix rotationYMatrix;
matrix rotationZMatrix;
matrix scalingMatrix;
matrix translationLocalMatrix;
matrix translationWorldMatrix;

struct VS_IN
{
	float4 pos : POSITION;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	input.pos = mul(input.pos, rotationXMatrix);
	input.pos = mul(input.pos, rotationYMatrix);
	input.pos = mul(input.pos, rotationZMatrix);

	input.pos = mul(input.pos, scalingMatrix);
	
	input.pos = mul(input.pos, translationLocalMatrix);
	input.pos = mul(input.pos, translationWorldMatrix);

	input.pos = mul(input.pos, View);
	input.pos = mul(input.pos, Projection);

	output.pos = input.pos;
	output.col = (sin(input.pos) + float4(1, 1, 1, 1)) / 2;
	output.col = float4(output.col.r, output.col.g, output.col.b, 1.0f);
	//output.col = float4(1, 0, 0, 0);
	
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