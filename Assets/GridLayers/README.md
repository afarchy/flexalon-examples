# Grid Layers

Demonstrates how the new grid layers property can be used to achieve 3D grids, with some physics for cool effects. Requires Flexalon Version 3.0.

[![Grid Layers Video](https://img.youtube.com/vi/fiU362heBr4/0.jpg)](https://youtu.be/fiU362heBr4)

## How it works

A Flexalon Grid Layout is used to position the blocks. The blocks are arranged in column-row-layer order,
with columns configured as +X, rows configued as -Z, and layers configured as +Y.

The GridLayersDemo script drives the action. The demo initially turns physics on, then turns it off and
rebuilds the grid. When physics is turned on, a simple RadialEffector script pushes the blocks outward
from the bottom, creating a sort of crumbling effect.

Flexalon Lerp Animators are used to smoothly rebuild the grid. By setting the interpolation speed to 0,
the lerp animator doesn't interfere with the physics system. When physics is turned on, we can then
set the interpolation speed back to 5 to start the animation.