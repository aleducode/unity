using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

using System;
using System.Linq;




[Serializable]
public class CompositeMapLayer{
	public enum eLayerOperation {Normal, Multiply, Add, Subtract, InvSubtract}
	public enum eMaskChannel {Red,Green,Blue,Alpha,RGBA}




	public bool Enabled = true;
	public Texture2D Mask;
	public eMaskChannel MaskChannel;
	public bool InverseMask;

	public Color Mul = new Color(1f,1f,1f,1f);
	public Texture2D Map;

	public eLayerOperation LayerOperation = eLayerOperation.Normal;
	public Vector4 WriteMask = new Color(1f,1f,1f,1f);

	public CompositeMapLayer(){

	}

	public CompositeMapLayer(Texture2D T){
		Mask = T;
	}

	public Vector2 GetDimensions(){
		Vector2 Dims = Vector2.one;
		if (!Enabled) return Dims;
		if (Mask) Dims = new Vector2(Mask.width,Mask.height);
		if (Map) Dims = new Vector2(Mathf.Max(Dims.x,Map.width),Mathf.Max(Dims.x,Map.height)); 
		return Dims;
	}
}

public class CompositeMap : ScriptableObject {
	[SerializeField]
	public  List<CompositeMapLayer> Layers = new List<CompositeMapLayer>();
	[SerializeField]
	public Texture2D OutputTexture;

	public string OutputPath;
	public bool AutoSaveOnClose = true;



	public Vector2 GetOutputFileDimensions(){
		Vector2 Dims = Vector2.one;
		foreach (CompositeMapLayer l in Layers){
			Vector2 layerDims = l.GetDimensions();
			Dims = new Vector2(Mathf.Max(Dims.x,layerDims.x),Mathf.Max(Dims.x,layerDims.y)); 
		}
		return Dims;
	}

	[NonSerialized]
	public bool Changed = false;

	public void MarkAsChanged(){
		Changed = true;
	}

	private Shader sh;
	private Material mat;

	public void Render(){

		if (!OutputTexture){
			Vector2 dims = GetOutputFileDimensions();
			OutputTexture = new Texture2D((int)dims.x,(int)dims.y,TextureFormat.ARGB32,false);
		}

		if (!sh) sh = Shader.Find("Hidden/CompositeMap");
		if (!mat) mat = new Material(sh);

		RenderTexture[] rts = new RenderTexture[2];
		for (int i=0; i<2; i++){
			rts[i] = RenderTexture.GetTemporary(OutputTexture.width,OutputTexture.height,0,RenderTextureFormat.ARGB32);
			RenderTexture.active = rts[i];
			GL.Clear(false,true,new Color(0f,0f,0f,0f));
		}
		RenderTexture.active = rts[0];


		Rect r = new Rect(0,0,OutputTexture.width,OutputTexture.height);

		bool rtID = false;

		foreach (CompositeMapLayer l in Layers){
			if (l.Enabled){
				mat.SetTexture("_MainTex",rts[(!rtID)?1:0]);
				mat.SetTexture("cmMask",l.Mask);
				mat.SetTexture("cmMap",l.Map);
				mat.SetColor("cmMul",l.Mul);
				mat.SetInt("cmOp",(int)l.LayerOperation);
				mat.SetInt("cmMaskChannel",(int)l.MaskChannel);
				mat.SetInt("cmInverseMask",l.InverseMask?1:0);
				mat.SetVector("cmWriteMask",l.WriteMask);

				RenderTexture.active = rts[(rtID)?1:0];

				mat.SetPass(0);
				GL.PushMatrix();

				GL.LoadOrtho();
				GL.Viewport(new Rect(0, 0, OutputTexture.width,OutputTexture.height));
					GL.Begin(GL.QUADS);
					GL.TexCoord2(0, 0);
					GL.Vertex3(0.0F, 0.0F, 0);
					GL.TexCoord2(0, 1);
					GL.Vertex3(0.0F, 1.0F, 0);
					GL.TexCoord2(1, 1);
					GL.Vertex3(1.0F, 1.0F, 0);
					GL.TexCoord2(1, 0);
					GL.Vertex3(1.0F, 0.0F, 0);
					GL.End();

				GL.PopMatrix();

				rtID = !rtID;
			}
		}

		OutputTexture.ReadPixels(r,0,0);
		OutputTexture.Apply();
		RenderTexture.active = null;

		for (int i=0; i<2; i++){
			RenderTexture.ReleaseTemporary(rts[i]);
		}

	}

}



