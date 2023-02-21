# Infinite Curves

Flexalon Update 3.0 adds support for positioning objects before and after the end of a Flexalon Curve Layout.
This scene provides an interesting curve with rendering and animation scripts to test the different options.

[![Infinite Curve Video](https://img.youtube.com/vi/kt3O9AgH8WY/0.jpg)](https://youtu.be/kt3O9AgH8WY?t=29)

## How it works

A Flexalon Curve Layout is used to position the objects. The 'Before Start' and 'After End' properties are configured to repeat the curve infinitely. Try changing these properties to test different options.

The Curve Animator script increases the 'Start At' property of the curve layout over time, causing the
objects to move along the curve.

The Curve Renderer script configures the Line Renderer component attached to the Curve Layout to match the
curve positions, in order to draw the curve at runtime.

Notice that the start and end points of the curve have the same tangent. This makes the 'Repeat' option
for the curve smooth. See what happens if you change one of the tangents.