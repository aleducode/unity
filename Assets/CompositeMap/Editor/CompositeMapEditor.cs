using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;


[CustomEditor(typeof(CompositeMap))]
public class CompositeMapEditor  : Editor {
	
	private string cmPath;
	
	public Texture2D DeleteIcon;
	public Texture2D UpIcon;
	public Texture2D DownIcon;


	public void OnEnable(){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		LoadOutputTexture();
		cm.Render();
		
		DeleteIcon = Resources.Load<Texture2D>("Delete_CompositeMapButton");
		UpIcon = Resources.Load<Texture2D>("Up_CompositeMapButton");
		DownIcon = Resources.Load<Texture2D>("Down_CompositeMapButton");
		
	}
	
	public void OnDisable(){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		if (!cm) return;
		if (cm.AutoSaveOnClose)
			Save(false);
	}

	[MenuItem("Assets/Create/CompositeMap")]
	public static void CreateAsset(){
		CompositeMap asset = ScriptableObject.CreateInstance<CompositeMap> ();
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == ""){
			path = "Assets";
		}else
		if (Path.GetExtension (path) != ""){
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string Name = "New "+typeof(CompositeMap).ToString();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/" + Name + ".asset");
		AssetDatabase.CreateAsset(asset, assetPathAndName);
		
		AssetDatabase.SaveAssets();
		Selection.activeObject = asset;
		EditorUtility.FocusProjectWindow();
		
	}

	public string GetFullPathOfOutput(){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		string dir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(cm));
		string assetPath = dir+"/"+Path.ChangeExtension(cm.OutputPath,"png");
		return Path.GetFullPath(Application.dataPath+"/../"+assetPath);
	}
	
	public string GetAssetPathOfOutput(){
		string fullPath = GetFullPathOfOutput();
		return fullPath.Substring(Application.dataPath.Length-("Assets").Length);
	}

	public void LoadOutputTexture(){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		cm.OutputTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(GetAssetPathOfOutput(),typeof(Texture2D));
		if (!cm.OutputTexture){
			Vector2 dims = cm.GetOutputFileDimensions();
			cm.OutputTexture = new Texture2D((int)dims.x,(int)dims.y);
		}
	}


	public void CheckTextureSize(){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		Vector2 dims = cm.GetOutputFileDimensions();
		if (cm.OutputTexture)
			if (!AssetDatabase.Contains(cm.OutputTexture))
			if (cm.OutputTexture.width!=dims.x || cm.OutputTexture.height!=dims.y){
				DestroyImmediate(cm.OutputTexture);
				cm.OutputTexture = null;
			}	
	}

	public void Save(bool AllowMapCreate){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		if (!AssetDatabase.Contains(cm.OutputTexture)){
			if (!AllowMapCreate) return;
			DestroyImmediate(cm.OutputTexture);
		}else
			if (!cm.Changed) return;
		
		
		if (string.IsNullOrEmpty(cm.OutputPath)) cm.OutputPath = cm.name;
		
		
		
		cm.OutputTexture = null;
		cm.Render();
		
		byte[] ImageData = cm.OutputTexture.EncodeToPNG();
		string fullPath = GetFullPathOfOutput();
		File.WriteAllBytes(fullPath,ImageData);
		
		cm.Changed = false;
		

		
		string assetPath = GetAssetPathOfOutput();
		AssetDatabase.Refresh();
		
		TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter; 
		textureImporter.isReadable = true;
#if UNITY_5_5_OR_NEWER
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
#else
        textureImporter.textureFormat = TextureImporterFormat.ARGB32;
#endif


        AssetDatabase.ImportAsset(assetPath,ImportAssetOptions.Default);
		
		
		LoadOutputTexture();

		EditorUtility.SetDirty(cm);
		AssetDatabase.SaveAssets();
	}

	public void AddTooltip(string tooltip){
		//Rect rect = GUILayout.
	}


	public Color ColorGUI(Color c){
		EditorGUILayout.BeginVertical();

		GUIContent guicontent = new GUIContent ();
		guicontent.tooltip = "Multiplier."+'\n'+"Api: CompositeMapObject.Layers[i].Mul";
		guicontent.text = "";

		c = EditorGUILayout.ColorField(guicontent,c,GUILayout.MinWidth(48),GUILayout.Height(16));
		Rect r = EditorGUILayout.GetControlRect(GUILayout.MinWidth(48));
		r.height = 12;
		r.y -= 4;
		c.r = GUI.HorizontalSlider(r,c.r,0f,1f);
		r.y += 10;
		c.g = GUI.HorizontalSlider(r,c.g,0f,1f);
		r.y += 10;
		c.b = GUI.HorizontalSlider(r,c.b,0f,1f);
		r.y += 10;
		c.a = GUI.HorizontalSlider(r,c.a,0f,1f);
		EditorGUILayout.EndVertical();
		return c;
	}
	
	private bool ToggleWithTooltip(bool Value, string Tooltip){
		GUIContent C = new GUIContent ();
		C.tooltip = Tooltip;
		C.text = "";
		Rect R = EditorGUILayout.GetControlRect (GUILayout.Width (12));
		bool Result = EditorGUI.Toggle (R, Value);
		EditorGUI.LabelField (R, C);
		return Result;
	}
	
	
	public bool LayerGUI(CompositeMapLayer l, CompositeMap cm){
		EditorGUILayout.BeginHorizontal();
		
		EditorGUILayout.BeginVertical();
		
		l.Enabled = ToggleWithTooltip (l.Enabled, "Enable / Disable Layer");
		
		GUIStyle MiniButtonStyle = new GUIStyle(EditorStyles.miniButton);
		MiniButtonStyle.padding = new RectOffset(0,0,0,0);
		
		if (GUILayout.Button(new GUIContent (DeleteIcon, "Delete Layer"),MiniButtonStyle,GUILayout.Height(14),GUILayout.Width(14))){
			cm.Layers.Remove(l);
			return true;
		}
		
		if (GUILayout.Button(new GUIContent (UpIcon, "Move Layer Up"),MiniButtonStyle,GUILayout.Height(14),GUILayout.Width(14))){
			int i = cm.Layers.IndexOf(l);
			if (i<cm.Layers.Count-1){
				CompositeMapLayer t = l;
				cm.Layers[i] = cm.Layers[i+1];
				cm.Layers[i+1] = t;
				return true;
			}
		}
		
		if (GUILayout.Button(new GUIContent (DownIcon, "Move Layer Down"),MiniButtonStyle,GUILayout.Height(14),GUILayout.Width(14))){
			int i = cm.Layers.IndexOf(l);
			if (i>0){
				CompositeMapLayer t = l;
				cm.Layers[i] = cm.Layers[i-1];
				cm.Layers[i-1] = t;
				return true;
			}
		}
		
		EditorGUILayout.EndVertical();
		
		l.Mask = (Texture2D)EditorGUILayout.ObjectField(l.Mask,typeof(Texture2D),true,GUILayout.Height(64),GUILayout.Width(64));
		
		
		EditorGUILayout.BeginVertical();
		
		string LayerName = "";
		if (l.Mask) LayerName = l.Mask.name;
		else if (l.Map) LayerName = l.Map.name;
		
		EditorGUILayout.LabelField(LayerName,
		                           GUILayout.MinWidth(0),GUILayout.Height(14));
		l.LayerOperation = (CompositeMapLayer.eLayerOperation)EditorGUILayout.EnumPopup(l.LayerOperation,
		                                                                                GUILayout.MinWidth(22),GUILayout.Height(14));
		
		EditorGUILayout.BeginHorizontal();
		l.MaskChannel = (CompositeMapLayer.eMaskChannel)EditorGUILayout.
			IntPopup((int)l.MaskChannel,
			         new string[]{"use Mask.r","use Mask.g","use Mask.b","use Mask.a","use Mask.rgba"},
			new int[]{0,1,2,3,4},
			GUILayout.MinWidth(22),
			GUILayout.Height(14)
			);
		GUI.color = Color.gray;
		l.InverseMask = ToggleWithTooltip (l.InverseMask, "Inverse Mask");
		EditorGUILayout.EndHorizontal();
		
		
		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.red;
		l.WriteMask.x = EditorGUILayout.Toggle(l.WriteMask.x>0,GUILayout.Width(12),GUILayout.MinWidth(0))?1f:0f;
		GUI.color = Color.green;
		l.WriteMask.y = EditorGUILayout.Toggle(l.WriteMask.y>0,GUILayout.Width(12),GUILayout.MinWidth(0))?1f:0f;
		GUI.color = Color.blue;
		l.WriteMask.z = EditorGUILayout.Toggle(l.WriteMask.z>0,GUILayout.Width(12),GUILayout.MinWidth(0))?1f:0f;
		GUI.color = Color.white;
		l.WriteMask.w = EditorGUILayout.Toggle(l.WriteMask.w>0,GUILayout.Width(12),GUILayout.MinWidth(0))?1f:0f;
		
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.EndVertical();
		
		l.Mul = ColorGUI(l.Mul);
		
		l.Map = (Texture2D)EditorGUILayout.ObjectField(l.Map,typeof(Texture2D),true,GUILayout.Height(64),GUILayout.Width(64));
		EditorGUILayout.EndHorizontal();
		return false;
	}
	
	public bool IsDragcmValid(){
		foreach(var O in DragAndDrop.objectReferences){
			if (!(O is Texture2D)) return false;
		}
		return true;
	}
	
	
	
	
	public void ProcessNewLayerDrop(Rect dropArea, CompositeMap cm){
		if (!dropArea.Contains(Event.current.mousePosition)) return;
		
		switch (Event.current.type){
		case EventType.DragUpdated:
			if (IsDragcmValid()) DragAndDrop.visualMode = DragAndDropVisualMode.Link;
			else DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
			Event.current.Use();
			break;     
			
		case EventType.DragPerform:
			DragAndDrop.AcceptDrag();
			
			if (IsDragcmValid()){
				foreach (var O in DragAndDrop.objectReferences){
					cm.Layers.Add(new CompositeMapLayer(O as Texture2D));
				}
				cm.Render();
				cm.MarkAsChanged();
			}
			
			Event.current.Use();
			break;
		}
	}
	
	
	
	public override void OnInspectorGUI (){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		
		EditorGUI.BeginChangeCheck();
		
		
		if (GUILayout.Button("New layer")){
			cm.Layers.Add(new CompositeMapLayer());
		}
		
		
		ProcessNewLayerDrop(GUILayoutUtility.GetLastRect(), cm);
		
		
		for (int i=cm.Layers.Count-1; i>-1; i--){
			if (LayerGUI(cm.Layers[i], cm)){
				cm.Render();
				cm.MarkAsChanged();
				return;
			}
		}
		
		
		EditorGUILayout.Space();
		cm.OutputPath = EditorGUILayout.TextField("Output path:",cm.OutputPath);
		
		
		
		EditorGUILayout.BeginHorizontal();
		cm.AutoSaveOnClose = GUILayout.Toggle(cm.AutoSaveOnClose,"Auto-save on close");
		
		
		if (EditorGUI.EndChangeCheck()){
			CheckTextureSize();
			cm.Render();
			cm.MarkAsChanged();
		}
		
		if (GUILayout.Button("Save")){
			Save(true);
		}
		EditorGUILayout.EndHorizontal();
		
		
	}
	
	
	public override bool HasPreviewGUI(){
		return true;
	}
	
	public override void OnPreviewGUI(Rect R,  GUIStyle background ){
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		if (!cm.OutputTexture) return;
		EditorGUI.DrawPreviewTexture(R,cm.OutputTexture);
	}
	
	public override void OnInteractivePreviewGUI(Rect R,  GUIStyle background ){
		
		CompositeMap cm = (CompositeMap)serializedObject.targetObject;
		if (!cm.OutputTexture) return;
		
		float PreviewAreaAspect = R.height/R.width;
		float TextureAspect = (float)cm.OutputTexture.height/(float)cm.OutputTexture.width;
		
		Rect DrawRect = R;
		
		
		if (PreviewAreaAspect>TextureAspect){
			DrawRect.height /=2f;
			DrawRect.width = DrawRect.height/TextureAspect;
			
			EditorGUI.DrawPreviewTexture(DrawRect,cm.OutputTexture);
			DrawRect.y += DrawRect.height;
			EditorGUI.DrawTextureAlpha(DrawRect,cm.OutputTexture);
		}else{
			DrawRect.width /=2f;
			DrawRect.height = DrawRect.width*TextureAspect;
			
			EditorGUI.DrawPreviewTexture(DrawRect,cm.OutputTexture);
			DrawRect.x += DrawRect.width;
			EditorGUI.DrawTextureAlpha(DrawRect,cm.OutputTexture);
		}
		
	}
	
	public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height){
		Texture2D LogoIcon = Resources.Load<Texture2D>("CompositeMapLogo");
		if (!LogoIcon) return null;
		return Instantiate(LogoIcon) as Texture2D;
	}
	
	
	
}
