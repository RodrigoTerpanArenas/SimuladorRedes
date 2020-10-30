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

## Importing your own models

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
