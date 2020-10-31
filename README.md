# SimuladorRedes

## Table of contents
* [Introduction](#introduction)
* [Features](#features)
* [Setup](#setup)
* [Functionality](#functionality)
* [Importing your own models](#importing-your-own-models)
* [Status](#status)
* [Contact](#contact)

## Introduction
This proyect serves as a first approach to the creation of a  computational  tool in the Unity engine. Created with the intention of generating visual and descriptive data used as additional training images for  neural  network architectures involved  in  image  recognition  tasks such as the YOLO architecture. 
This  work  is  inspired  by  the  problem  posed  to  acquire enough data to train service robots, with the main goal being to increase the range of objects in the environment with which they can interact.<br/>
This tool provides the users with an environment in which they can easily set up a set of objects to be randomly generated and positioned in a specific work zone. The framework also allows the capture of graphic and descriptive information of the objects in the environment.

## Features
* Easy integration of 3D external 3D models.
* Selection of a variety of objects that can be instatiated in the environment in random positions and rotations.
* System saves information able to be used to train the YOLO architecture.
* On screen controls allow the modification of both light and camera position.
* Automated image capturing.

## Setup
For this release you should have installed the following:
* Unity V-2019.2.12f1 or newer.
* Visual studio community 2017 or newer.

In order to properly save your files, change line 17 from the "DirAct.cs" file as follows:<br/>
from<br/>
`dir1 = PlayerPrefs.GetString("str1", "C:/Users/rodri/Desktop");`<br/>
to<br/>
`dir1 = PlayerPrefs.GetString("str1", "address of your choice");`

## Functionality
### Manual image generation
In order to manually generate models and capture information, the following steps are to be followed.
* Make sure that the `Obj` list in the `ListManager`component has the appropiate prefabs listed.
* On playmode, Select which objects to generate from the Object generation list.
* Optionally select ilumination and camera angle from the corresponding menu.
* Click `Generate`button to generate the objects in the work zone.
* Click the `Capture`button to capture and save the information in the corresponding folders (If non existant, folders will be automatically created on default address).
*  To reset numbering, uncheck the corresponding box in the last menu. To save on the validation folder, uncheck the `Save on training folder` option on the same menu.

### Automatic image generation and other options
Automatic information generation functions as follows:
 * Select the `Generate at random`option in the object list menu.
 * In the fourth menú, select the number of images to be generated in the first input field.
 * Optionally, select the checkmark to shuffle the room every X  iterations and select the number of iterations in the corresponding input field.
 * Select the `Automatic generation` button to start the process. The process will end automatically once the selected number of images is captured.

## Importing your own models
When importing your own models in order to generate custom data, make sure of the following:
* Adjust the scale of your object in accordance to the environment.
* Add a Rigidbody component and a Box Collider component to te model. Make sure to adjust the box collider so that it completely envelops the object.
* Ensure that the center of the box collider coincides with the center of the object.
* If the model comes form a third party source, be sure to deactivate any components that might interfere with collision detection such as additional colliders.

After following these steps, the object can be turned into a prefab (for ease of use, it is recommended that the prefab is catalogued inside the "Prefab" folder of the project).

* Make sure that your prefab is tagged correctly (the tag in this case is taken as the object label in the captured information). This will depend on the dataset you are using.

To use this prefab, it must be placed in one of the lists inside the `ListManager`object. The `Obj` list corresponds to the 14 options in the UI. Allowing to manually coose which objects to generate. `ObjAzr` Corresponds to the list of objects generated when choosing the  "Generate at random choice" and the `Ext` list corresponds to obstacles whose information won't be taken into account.

## Status
The project is currently under developement in order to further increase functionality and ease of use. 

### Roadmap for V2.0:
* Add functions in order to freely control the camera around the environment.
* Allow camera to anchor around specific objects in the environment.
* Add more image capture functions.
* Overhaul object selection and generation functions in order to allow more flexibility in the objects used.
* Add labeling functions for easy use.

## Contact
Project created by Rodrigo Terpán Anreas from the IIMAS institute at the National Autonomous University of México (UNAM). 
Project overseen by Dr. Alfonso Gastelum Strozzi (UNAM). 
For clarification, error reports, pull requests and contributions please contact the following mail:
* rterpan@comunidad.unam.mx
