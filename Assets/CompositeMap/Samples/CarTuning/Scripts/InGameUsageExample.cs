using UnityEngine;
using System.Collections;

public class InGameUsageExample : MonoBehaviour {
	public Texture2D Logo;
	public CompositeMap DiffuseCompositeMap;
	public CompositeMap ReflectionCompositeMap;
	public CompositeMap Layout;


	public int SelectedLayoutLayer = -1;
	public int SelectedDiffuseLayer = -1;
	public int SelectedReflectionLayer = -1;


	void Start () {
		DiffuseCompositeMap = Instantiate<CompositeMap> (DiffuseCompositeMap);
		ReflectionCompositeMap = Instantiate<CompositeMap> (ReflectionCompositeMap);
		SelectedLayoutLayer = -1;
		SelectedDiffuseLayer = -1;
	}


	private int NeedSelect = 0;
	void Update () {
		if (NeedSelect!=2)
			return;
		NeedSelect = 0;

		SelectedLayoutLayer = -1;
		SelectedDiffuseLayer = -1;
		SelectedReflectionLayer = -1;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			GameObject go = hit.collider.gameObject;
			if (go.GetComponent<MeshRenderer>().sharedMaterial.mainTexture ==
				DiffuseCompositeMap.OutputTexture){

				Color SelectedPixel = Layout.OutputTexture.GetPixel(
					(int)(hit.textureCoord.x*Layout.OutputTexture.width),
					(int)(hit.textureCoord.y*Layout.OutputTexture.height));
				for (int i=0; i<Layout.Layers.Count; i++){
					if (Layout.Layers[i].Mul==SelectedPixel){
						SelectedLayoutLayer = i;
						for (int j=0; j<DiffuseCompositeMap.Layers.Count; j++){
							if (DiffuseCompositeMap.Layers[j].Mask==Layout.Layers[SelectedLayoutLayer].Mask){
								SelectedDiffuseLayer = j;
								break;
							}
						}
						for (int j=0; j<ReflectionCompositeMap.Layers.Count; j++){
							if (ReflectionCompositeMap.Layers[j].Mask==Layout.Layers[SelectedLayoutLayer].Mask){
								SelectedReflectionLayer = j;
								break;
							}
						}
						break;
					}
				}
			}
		}
	}


	private Rect EditorRect = new Rect(5,5,180,180);
	void OnEditorWindowGUI(int id){
		float topM = 20f;
		float sideM = 8f;
		Rect ContentRect = new Rect (sideM, topM, EditorRect.width-2f*sideM, EditorRect.height-topM);

		GUILayout.BeginArea(ContentRect);

		if (SelectedLayoutLayer == -1) {
			GUILayout.Label("Click on model to select texture region");
			GUILayout.Label("Drag - rotate");
			GUILayout.Label("Scroll - zoom");
		}


		if (SelectedDiffuseLayer > -1) {
			
			Color C = DiffuseCompositeMap.Layers[SelectedDiffuseLayer].Mul;
			Color nC = C;
			
			GUILayout.Label("Diffuse");
			
			GUILayout.BeginHorizontal();
			
			const float ButtonStep = 0.01f;
			if (GUILayout.RepeatButton("-",GUILayout.Height(42))){
				nC.r = Mathf.Max( nC.r-ButtonStep, 0f);
				nC.g = Mathf.Max( nC.g-ButtonStep, 0f);
				nC.b = Mathf.Max( nC.b-ButtonStep, 0f);
				
			}
			
			
			GUILayout.BeginVertical();
			nC.r = GUILayout.HorizontalSlider(nC.r,0f,1f,GUILayout.Height(12));
			nC.g = GUILayout.HorizontalSlider(nC.g,0f,1f,GUILayout.Height(12));
			nC.b = GUILayout.HorizontalSlider(nC.b,0f,1f,GUILayout.Height(12));
			GUILayout.EndVertical();
			
			if (GUILayout.RepeatButton("+",GUILayout.Height(42))){
				nC.r = Mathf.Min( nC.r+ButtonStep, 1f);
				nC.g = Mathf.Min( nC.g+ButtonStep, 1f);
				nC.b = Mathf.Min( nC.b+ButtonStep, 1f);
				
			}
			
			GUILayout.EndHorizontal();

			if (nC != C){
				DiffuseCompositeMap.Layers[SelectedDiffuseLayer].Mul = nC;
				DiffuseCompositeMap.Render();
			}

		}

		if (SelectedReflectionLayer > -1) {
			Color C = ReflectionCompositeMap.Layers [SelectedReflectionLayer].Mul;
			float Metallic = C.r;
			float Smoothness = C.a;
			GUILayout.Label("Metallic");
			Metallic = GUILayout.HorizontalSlider(Metallic,0f,1f,GUILayout.Height(12));
			GUILayout.Label("Smoothness");
			Smoothness = GUILayout.HorizontalSlider(Smoothness,0f,1f,GUILayout.Height(12));
			
			Color nC = new Color(Metallic,Metallic,Metallic,Smoothness);
			if (nC != C){
				ReflectionCompositeMap.Layers[SelectedReflectionLayer].Mul = nC;
				ReflectionCompositeMap.Render();
			}
		}

		GUILayout.EndArea();
		GUI.DragWindow ();
	}


	void OnGUI(){
		EditorRect = GUI.Window(0,EditorRect,OnEditorWindowGUI,"Editor");
		GUI.DrawTexture(new Rect(Screen.width-10-128,Screen.height-30-128,128,128), Logo);


		if (SelectedLayoutLayer > -1) {
			float S = 180f;
			float M = 4f;
			Rect TextureRect = new Rect (Screen.width-S-M, M, S, S);
			GUI.DrawTexture(TextureRect, Layout.Layers [SelectedLayoutLayer].Mask);
		}


		if (!EditorRect.Contains (Input.mousePosition)) {
			if (Event.current.type == EventType.MouseDown)
				NeedSelect = 1;
			if (Event.current.type == EventType.MouseDrag){
				if (Event.current.delta.magnitude>1f)
					NeedSelect = 0;
			}
			if (Event.current.type == EventType.MouseUp)
				NeedSelect++;
		}
	}

	
}
