using System;
using Godot;
using Godot.Collections;

public partial class DemoSceneSelector : Control {
	[Export]
	public Node SceneContainer;
	[Export]
	public OptionButton DemoSceneSelectorButton;

	[Export]
	public Array<PackedScene> DemoScenes;

	public override void _Ready() {
		int selectedSceneIndex = DemoSceneSelectorButton.Selected;
		SwitchToSceneWithIndex(selectedSceneIndex);
		DemoSceneSelectorButton.ItemSelected += SwitchToSceneWithIndex;
	}

	private long _selectedSceneIndex = -1;
	private void SwitchToSceneWithIndex(long sceneIndex) {
		if (sceneIndex > (DemoScenes.Count - 1)) {
			return;
		}
		if (sceneIndex < 0) {
			return;
		}
		if (sceneIndex == _selectedSceneIndex) {
			return;
		}
		_selectedSceneIndex = sceneIndex;

		foreach (Node child in SceneContainer.GetChildren()) {
			child.QueueFree();
		}

		PackedScene sceneToBeLoaded = DemoScenes[(int)_selectedSceneIndex];
		if (sceneToBeLoaded == null) {
			return;
		}

		Node scene = sceneToBeLoaded.InstantiateOrNull<Node>();
		if (scene == null) {
			return;
		}

		SceneContainer.AddChild(scene);
	}
}
