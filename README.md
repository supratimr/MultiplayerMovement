# MultiplayerMovement

This project is created with Unity 3D with Extra settings.
Currently it is in Unity 2019.3.13f1.
This project is to demonstrate simple player movement and passing player position to move a player representation in another system.
Blue is current player, White is remote player. As current player moves, remote player follows the track.
Few positional update optimization technique are added to reduce data size, frequency of transmit etc. over wire.
Need to add some movement prediction for smooth movement of remote player. To test, open 'SampleScene' and hit 'Play' button.
