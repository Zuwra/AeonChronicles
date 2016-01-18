using UnityEngine;
using System.Collections;

public class ResolverConfig : MonoBehaviour {

	void Awake () 
	{
		ManagerResolver.Register<ISelectedManager>(SelectedManager.main);
		ManagerResolver.Register<ICamera>(MainCamera.main);
		ManagerResolver.Register<IEventsManager>(EventsManager.main);
		ManagerResolver.Register<IUIManager>(UIManager.main);
		ManagerResolver.Register<IGUIManager>(GUIManager.main);
		ManagerResolver.Register<IMiniMapController>(MiniMapController.main);
	//	ManagerResolver.Register<IManager>(Manager.main);
		ManagerResolver.Register<IGLManager>(GLManager.main);
		ManagerResolver.Register<IGrid>(Grid.main);
		ManagerResolver.Register<ICursorManager>(CursorManager.main);
		ManagerResolver.Register<ILevelLoader>(LevelLoader.main);
		ManagerResolver.Register<IThreadManager>(ThreadManager.main);
	}
}
