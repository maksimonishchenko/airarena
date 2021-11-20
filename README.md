![Ragdoll Screenshot](/docs/RagdollScreenshot.png)

# Ragdoll Trainer Unity Project

Active ragdoll training with Unity ML-Agents (PyTorch). 

## Ragdoll Agent

Based on [walker example](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Learning-Environment-Examples.md)

The Robot Kyle model from the Unity assets store is used for the ragdoll.

![RobotKyleBlend Image](/docs/RobotKyleBlend.png)

### Features:

* Default Robot Kyle rig replaced with a new rig created in blender. FBX and blend file included.

* Heuristic function inlcuded to drive the joints by user input (for development testing only).

* Added stabilizer to hips and spine. The stabilizer applies torque to help ragdoll balance.

* Added "earlyTraining" bool to toggle reward type for early balance or walking.

* Added "WallsAgent" prefab for training with obstacles (using Ray Perception Sensor 3D).

### Training:

* Currently there are 3 stages: balance, walking, obstacles (walls).
* Set "earlyTraining" bool True/False for balance/walking stages.
* Replace "WalkerAgent" prefab with "WallsAgent" for training with obstacles.

